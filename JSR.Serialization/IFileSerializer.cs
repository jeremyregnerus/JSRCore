namespace JSR.Serialization
{
    /// <summary>
    /// Interface for serializers used for simple loading and saving of files.
    /// </summary>
    /// <typeparam name="T">Type of object to manage.</typeparam>
    public interface IFileSerializer<T>
    {
        /// <summary>
        /// Loads a file using serialization.
        /// </summary>
        /// <param name="filePath">Filepath of the file to load.</param>
        /// <returns>An objdect of type T.</returns>
        T LoadFile(string filePath);

        /// <summary>
        /// Saves a file using serialization.
        /// </summary>
        /// <param name="objectToSave">Object to save using serialization.</param>
        /// <param name="filePath">Filepath of where to save the object.</param>
        void SaveFile(T objectToSave, string filePath);
    }
}
