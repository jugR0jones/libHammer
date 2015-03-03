using libHammer.Design_Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace libHammer.Xml
{

    /// <summary>
    /// 
    /// </summary>
    public class ExternalConfigurationBase : IXmlSerializable, ISupportLazyInitialization
    {
        private Dictionary<Type, XmlSerializer> _serializers;

        [XmlIgnore]
        public string Location { get; protected set; }

        public virtual void Save()
        {
            using (var writer = new XmlTextWriter(Location, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement(this.GetType().Name);
                this.WriteXml(writer);
                writer.WriteEndDocument();
            }
        }

        public virtual void Initialize()
        {
            try
            {
                if (File.Exists(Location))
                    using (XmlReader reader = XmlReader.Create(Location))
                    {
                        reader.ReadToFollowing(this.GetType().Name);
                        this.ReadXml(reader);
                        reader.ReadEndElement();
                    }
            }
            catch (Exception ex)
            {
                throw new InvalidConfigurationException("Exception occred while loading the configuration from disk", ex);
            }
        }

        public ExternalConfigurationBase()
        {
            Assembly currentAssembly = Assembly.GetCallingAssembly();
            Assembly mainAssembly = Assembly.GetEntryAssembly() ?? currentAssembly;

            Location = string.Format("{0}\\{1}.config",
                                            Path.GetDirectoryName(currentAssembly.Location),
                                            Path.GetFileNameWithoutExtension(mainAssembly.Location));
        }

        public ExternalConfigurationBase(string configPath)
        {
            Location = configPath;
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            Type type = this.GetType();
            PropertyInfo targetProperty;

            for (int i = reader.AttributeCount - 1; i >= 0; i--)
            {
                reader.MoveToAttribute(i);
                if (null == (targetProperty = GetProperty(type, reader.Name)))
                    continue;

                TypeConverter converter = TypeDescriptor.GetConverter(targetProperty.PropertyType);
                if (converter.CanConvertFrom(typeof(string)))
                    targetProperty.SetValue(this, converter.ConvertFrom(reader.Value), null);
            }

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Whitespace
                        || reader.NodeType == XmlNodeType.ProcessingInstruction
                        || reader.NodeType == XmlNodeType.Comment)
                {
                    continue;
                }
                else if (reader.NodeType != XmlNodeType.EndElement && null != (targetProperty = GetProperty(type, reader.Name)))
                {
                    reader.Read();

                    if (targetProperty.PropertyType.IsPrimitive || typeof(string) == targetProperty.PropertyType)
                    {
                        TypeConverter converter = TypeDescriptor.GetConverter(targetProperty.PropertyType);
                        targetProperty.SetValue(this, converter.ConvertFrom(reader.ReadContentAsString()), null);
                    }
                    else
                    {
                        object value = GetSerializer(targetProperty.PropertyType).Deserialize(reader);
                        targetProperty.SetValue(this, value, null);
                    }
                    reader.ReadEndElement();
                }
            }
        }

        private static PropertyInfo GetProperty(Type type, string propertyName)
        {
            PropertyInfo retVal = type.GetProperty(propertyName);
            if (retVal == null)
            {
                retVal = Array.Find(type.GetProperties(),
                                    property =>
                                    {
                                        object[] attributes = property.GetCustomAttributes(
                                                                    typeof(XmlAttributeAttribute),
                                                                    true);
                                        return null != Array.Find(attributes, match =>
                                        {
                                            var attrib = (XmlAttributeAttribute)match;
                                            return propertyName.Equals(attrib.AttributeName);
                                        });
                                    });
            }
            return retVal;
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            Type type = this.GetType();

            PropertyInfo[] properties = type.GetProperties();
            List<PropertyInfo> attributedProperties = new List<PropertyInfo>(properties.Length);
            List<PropertyInfo> normalProperties = new List<PropertyInfo>(properties.Length);

            foreach (var property in properties)
            {
                if (!property.CanWrite
                        || null == property.GetValue(this, null)
                        || 0 < property.GetCustomAttributes(typeof(XmlIgnoreAttribute), true).Length)
                {
                    continue;
                }
                else if (0 < property.GetCustomAttributes(typeof(XmlAttributeAttribute), true).Length)
                {
                    attributedProperties.Add(property);
                }
                else
                    normalProperties.Add(property);
            }

            foreach (var property in attributedProperties)
            {
                object[] attributes = property.GetCustomAttributes(typeof(XmlAttributeAttribute), true);
                XmlAttributeAttribute attribute = attributes[0] as XmlAttributeAttribute;
                writer.WriteAttributeString(attribute.AttributeName, property.GetValue(this, null).ToString());
            }

            foreach (var property in normalProperties)
            {
                object propertyValue = property.GetValue(this, null);
                if (property.PropertyType.IsPrimitive || typeof(string) == property.PropertyType)
                {
                    writer.WriteElementString(property.Name, property.GetValue(this, null).ToString());
                }
                else
                {
                    writer.WriteStartElement(property.Name);
                    GetSerializer(property.PropertyType).Serialize(writer, propertyValue);
                    writer.WriteEndElement();
                }
            }
        }

        private XmlSerializer GetSerializer(Type type)
        {
            if (_serializers == null)
            {
                _serializers = new Dictionary<Type, XmlSerializer>();
                _serializers.Add(type, new XmlSerializer(type));
            }
            else if (!_serializers.ContainsKey(type))
                _serializers.Add(type, new XmlSerializer(type));

            return _serializers[type];
        }
    }

}
