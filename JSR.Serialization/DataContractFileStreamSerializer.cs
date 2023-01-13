using System.Runtime.Serialization;
using System.Xml;

namespace JSR.Serialization
{
    /// <summary>
    /// Deserializes and Deserializes objects using the <see cref="DataContractSerializer"/>.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize and deserialize.</typeparam>
    public class DataContractFileStreamSerializer<T> : IFileStreamSerializer<T>
    {
        private readonly DataContractSerializer serializer = new(typeof(T), new DataContractSerializerSettings() { PreserveObjectReferences = true });

        /// <inheritdoc/>
        public T DeserializeFile(FileStream fileStream)
        {
            using XmlReader reader = XmlReader.Create(fileStream, new XmlReaderSettings());
            var obj = serializer.ReadObject(reader);

            if (obj == null)
            {
                throw new SerializationException($"Failed to deserialize {fileStream.Name}");
            }

            return (T)obj;
        }

        /// <inheritdoc/>
        public void SerializeFile(T objectToSave, FileStream fileStream)
        {
            using XmlWriter writer = XmlWriter.Create(fileStream, new XmlWriterSettings() { Indent = true, IndentChars = "\t" });
            serializer.WriteObject(writer, objectToSave);
        }
    }
}
