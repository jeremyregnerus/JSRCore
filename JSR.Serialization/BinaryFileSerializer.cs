// <copyright file="BinaryFileSerializer.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

namespace JSR.Serialization
{
    /// <summary>
    /// Loads and saves objects using the <see cref="BinaryFileSerializer{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize and deserialize.</typeparam>
    public class BinaryFileSerializer<T> : FileSerializer<T>, IFileSerializer<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryFileSerializer{T}"/> class.
        /// </summary>
        public BinaryFileSerializer() : base(new BinaryFileStreamSerlializer<T>())
        {
        }
    }
}
