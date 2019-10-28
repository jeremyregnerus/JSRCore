// <copyright file="DataContractFileManager.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

namespace JSR.FileManagement
{
    /// <summary>
    /// Manages IO aspects of a file using DataContract serialization.
    /// </summary>
    /// <typeparam name="T">Type of object the file contains.</typeparam>
    public class DataContractFileManager<T> : FileManager<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContractFileManager{T}"/> class.
        /// </summary>
        public DataContractFileManager() : base(new DataContractFileStreamSerializer<T>())
        {
        }
    }
}
