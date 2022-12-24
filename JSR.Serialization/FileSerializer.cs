// <copyright file="FileSerializer.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

namespace JSR.Serialization
{
    /// <summary>
    /// Provides basic serialization and deserialization of files.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize and deserialize.</typeparam>
    public class FileSerializer<T> : IFileSerializer<T>
    {
        private readonly IFileStreamSerializer<T> serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSerializer{T}"/> class.
        /// </summary>
        /// <param name="fileStreamSerializer">FileStreamSerializer to use for serialization and deserialization.</param>
        public FileSerializer(IFileStreamSerializer<T> fileStreamSerializer)
        {
            serializer = fileStreamSerializer;
        }

        /// <inheritdoc/>
        public T LoadFile(string filePath)
        {
            using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read);
            return serializer.DeserializeFile(fileStream);
        }

        /// <inheritdoc/>
        public void SaveFile(T objectToSave, string filePath)
        {
            using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            serializer.SerializeFile(objectToSave, fileStream);
        }
    }
}
