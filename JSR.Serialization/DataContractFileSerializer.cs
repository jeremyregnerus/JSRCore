namespace JSR.Serialization
{
    /// <summary>
    /// Loads and saves objects using the <see cref="DataContractFileSerializer{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize.</typeparam>
    public class DataContractFileSerializer<T> : FileSerializer<T>, IFileSerializer<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContractFileSerializer{T}"/> class.
        /// </summary>
        public DataContractFileSerializer() : base(new DataContractFileStreamSerializer<T>())
        {
        }
    }
}
