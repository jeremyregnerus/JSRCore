// <copyright file="BinaryFilePathSerializer.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

namespace JSR.FileManagement
{
    /// <summary>
    /// Loads and saves objects using binary serialization.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize and deserialize.</typeparam>
    public class BinaryFilePathSerializer<T> : FileSerializer<T>, IFilePathSerializer<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryFilePathSerializer{T}"/> class.
        /// </summary>
        public BinaryFilePathSerializer() : base(new BinaryFileStreamSerlializer<T>())
        {
        }
    }
}
