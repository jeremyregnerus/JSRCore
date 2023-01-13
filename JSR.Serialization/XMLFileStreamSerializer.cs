using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace JSR.Serialization
{
    /// <summary>
    /// Deserializes and Deserializes objects using the <see cref="XmlSerializer"/>.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize and deserialize.</typeparam>
    public class XMLFileStreamSerializer<T> : IFileStreamSerializer<T>
    {
        private readonly XmlSerializer serializer = new(typeof(T));

        /// <inheritdoc/>
        public T DeserializeFile(FileStream fileStream)
        {
            using XmlReader reader = XmlReader.Create(fileStream, new XmlReaderSettings());
            var obj = serializer.Deserialize(reader);

            if (obj == null)
            {
                throw new SerializationException($"Failed to deserialize {fileStream.Name}.");
            }

            return (T)obj;
        }

        /// <inheritdoc/>
        public void SerializeFile(T objectToSave, FileStream fileStream)
        {
            using XmlWriter writer = XmlWriter.Create(fileStream, new XmlWriterSettings() { Indent = true, IndentChars = "\t" });
            serializer.Serialize(writer, objectToSave);
        }
    }
}
