// <copyright file="BinaryFileStreamSerlializer.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace JSR.FileManagement
{
    /// <summary>
    /// Deserializes and Deserializes objects using the <see cref="BinaryFormatter"/>.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize and deserialize.</typeparam>
    public class BinaryFileStreamSerlializer<T> : IFileStreamSerializer<T>
    {
        private readonly BinaryFormatter formatter = new BinaryFormatter();

        /// <inheritdoc/>
        public T DeserializeFile(FileStream fileStream)
        {
            return (T)formatter.Deserialize(fileStream);
        }

        /// <inheritdoc/>
        public void SerializeFile(T objectToSave, FileStream fileStream)
        {
            formatter.Serialize(fileStream, objectToSave);
        }
    }
}
