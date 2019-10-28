// <copyright file="BasicBinaryFileSerializer.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

namespace JSR.FileManagement
{
    /// <summary>
    /// Loads and saves objects using binary serialization.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize and deserialize.</typeparam>
    public class BasicBinaryFileSerializer<T> : BasicFileSerializer<T>, IFileSerializer<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicBinaryFileSerializer{T}"/> class.
        /// </summary>
        public BasicBinaryFileSerializer() : base(new BinaryFileStreamSerlializer<T>())
        {
        }
    }
}
