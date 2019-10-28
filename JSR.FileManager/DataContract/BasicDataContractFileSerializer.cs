// <copyright file="BasicDataContractFileSerializer.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

namespace JSR.FileManagement
{
    /// <summary>
    /// Loads and saves objects using a DataContractSerializer.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize.</typeparam>
    public class BasicDataContractFileSerializer<T> : BasicFileSerializer<T>, IFileSerializer<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicDataContractFileSerializer{T}"/> class.
        /// </summary>
        public BasicDataContractFileSerializer() : base(new DataContractFileStreamSerializer<T>())
        {
        }
    }
}
