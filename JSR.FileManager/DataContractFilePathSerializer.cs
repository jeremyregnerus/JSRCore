// <copyright file="DataContractFilePathSerializer.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

namespace JSR.FileManagement
{
    /// <summary>
    /// Loads and saves objects using a DataContractSerializer.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize.</typeparam>
    public class DataContractFilePathSerializer<T> : FileSerializer<T>, IFilePathSerializer<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContractFilePathSerializer{T}"/> class.
        /// </summary>
        public DataContractFilePathSerializer() : base(new DataContractFileStreamSerializer<T>())
        {
        }
    }
}
