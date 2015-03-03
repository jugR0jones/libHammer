using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace libHammer.Xml
{

    /// <summary>
    /// Represents a section of the Application Configuration file. Provides easy maintenance via XML Serialization.
    /// </summary>
    public class ConfigurationSectionBase : System.Configuration.ConfigurationSection, IXmlSerializable
    {
        private Dictionary<Type, XmlSerializer> _serializers;

        /// <summary>
        /// Reads XML from the configuration file.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> that reads from the configuration file.</param>
        /// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
        /// <exception cref="T:System.Configuration.ConfigurationErrorsException">
        /// The element to read is locked.
        /// - or -
        /// An attribute of the current node is not recognized.
        /// - or -
        /// The lock status of the current node cannot be determined.
        /// </exception>
        protected override void DeserializeElement(System.Xml.XmlReader reader, bool serializeCollectionKey)
        {
            ReadXml(reader);
        }

        /// <summary>
        /// Writes the contents of this configuration element to the configuration file when implemented in a derived class.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> that writes to the configuration file.</param>
        /// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
        /// <returns>
        /// true if any data was actually serialized; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.Configuration.ConfigurationErrorsException">
        /// The current attribute is locked at a higher configuration level.
        /// </exception>
        protected override bool SerializeElement(System.Xml.XmlWriter writer, bool serializeCollectionKey)
        {
            WriteXml(writer);
            return true;
        }

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema"/> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> method.
        /// </returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> stream from which the object is deserialized.</param>
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

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
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
