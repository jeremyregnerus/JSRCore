// <copyright file="BasicXMLFileSerializer.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

namespace JSR.FileManagement
{
    /// <summary>
    /// Loads and saves objects using XML serialization.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize and deserialize.</typeparam>
    public class BasicXMLFileSerializer<T> : BasicFileSerializer<T>, IFileSerializer<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicXMLFileSerializer{T}"/> class.
        /// </summary>
        public BasicXMLFileSerializer() : base(new XMLFileStreamSerializer<T>())
        {
        }
    }
}
