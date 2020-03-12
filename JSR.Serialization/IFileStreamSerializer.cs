// <copyright file="IFileStreamSerializer.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.IO;

namespace JSR.Serialization
{
    /// <summary>
    /// Interface for serializing and deserializing objects from a <see cref="FileStream"/>.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize and deserialize.</typeparam>
    public interface IFileStreamSerializer<T>
    {
        /// <summary>
        /// Deserializes an object from a FileStream and returns the object.
        /// </summary>
        /// <param name="fileStream">FileStream to deserialize the object from.</param>
        /// <returns>The object contained within the FileStream.</returns>
        T DeserializeFile(FileStream fileStream);

        /// <summary>
        /// Serializes an object to a FileStream.
        /// </summary>
        /// <param name="objectToSave">Object to serialize.</param>
        /// <param name="fileStream">FileStream to save the object to.</param>
        void SerializeFile(T objectToSave, FileStream fileStream);
    }
}
