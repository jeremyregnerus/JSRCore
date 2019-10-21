// <copyright file="XMLFilePathSerializer.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

namespace JSR.FileManagement
{
    /// <summary>
    /// Loads and saves objects using XML serialization.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize and deserialize.</typeparam>
    public class XMLFilePathSerializer<T> : FileSerializer<T>, IFilePathSerializer<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XMLFilePathSerializer{T}"/> class.
        /// </summary>
        public XMLFilePathSerializer() : base(new XMLFileStreamSerializer<T>())
        {
        }
    }
}
