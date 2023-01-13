namespace JSR.Serialization
{
    /// <summary>
    /// Loads and saves objects using the <see cref="XMLFileSerializer{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize and deserialize.</typeparam>
    public class XMLFileSerializer<T> : FileSerializer<T>, IFileSerializer<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XMLFileSerializer{T}"/> class.
        /// </summary>
        public XMLFileSerializer() : base(new XMLFileStreamSerializer<T>())
        {
        }
    }
}
