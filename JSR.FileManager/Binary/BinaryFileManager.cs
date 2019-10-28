// <copyright file="BinaryFileManager.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

namespace JSR.FileManagement
{
    /// <summary>
    /// Manages IO aspects of a file using Binary serialization.
    /// </summary>
    /// <typeparam name="T">Type of object the file contains.</typeparam>
    public class BinaryFileManager<T> : FileManager<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryFileManager{T}"/> class.
        /// </summary>
        public BinaryFileManager() : base(new BinaryFileStreamSerlializer<T>())
        {
        }
    }
}
