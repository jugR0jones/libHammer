using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace libHammer.Collections
{

    /// <summary>Represents a dictionary of named values.</summary>
    /// <typeparam name="T"></typeparam>
    public class Map<T> : Dictionary<string, T>, IXmlSerializable
    {
        private const string ValueElementName = "value";
        private const string ValueNameAttribute = "name";

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
            if (!reader.ReadToDescendant(ValueElementName))
                return;

            bool useDeserialization = !(typeof(T).IsPrimitive || typeof(T) == typeof(string));
            XmlSerializer serializer = useDeserialization ? new XmlSerializer(typeof(T)) : null;
            TypeConverter converter = useDeserialization ? null : TypeDescriptor.GetConverter(typeof(T));

            if (!useDeserialization && (converter == null || !converter.CanConvertFrom(typeof(string))))
                throw new Exception(string.Format("Type {T} is unsupported by Map structure.", typeof(T).Name));

            do
            {
                bool validReadState = reader.HasAttributes
                                        && reader.MoveToAttribute(ValueNameAttribute)
                                        && reader.HasValue;
                if (!validReadState)
                    throw new Exception("XML does not contain proper definition for the current value.");

                string key = reader.Value;

                reader.Read();
                if (reader.NodeType == System.Xml.XmlNodeType.EndElement)
                    base[key] = default(T);
                else if (useDeserialization)
                    base[key] = (T)serializer.Deserialize(reader);
                else
                    base[key] = (T)converter.ConvertFromString(reader.ReadContentAsString());

            } while (reader.ReadToNextSibling(ValueElementName));
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            bool useSerialization = !(typeof(T).IsPrimitive || typeof(T) == typeof(string));
            XmlSerializer serializer = useSerialization ? new XmlSerializer(typeof(T)) : null;
            TypeConverter converter = useSerialization ? null : TypeDescriptor.GetConverter(typeof(T));

            foreach (var keyValuePair in this)
            {
                writer.WriteStartElement(ValueElementName);
                writer.WriteAttributeString(ValueNameAttribute, keyValuePair.Key);
                if (useSerialization)
                    serializer.Serialize(writer, keyValuePair.Value);
                else
                    writer.WriteString(converter.ConvertToString(keyValuePair.Value));
                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Map&lt;T&gt;"/> class.
        /// </summary>
        public Map()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Map&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public Map(int capacity) : base(capacity) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Map&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public Map(IEqualityComparer<string> comparer) : base(comparer) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Map&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public Map(IDictionary<string, T> dictionary) : base(dictionary) { }
    }

}
