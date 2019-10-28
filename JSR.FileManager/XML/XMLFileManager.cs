// <copyright file="XMLFileManager.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Xml.Serialization;

namespace JSR.FileManagement
{
    /// <summary>
    /// Manages IO aspects of a file using Xml Serialization.
    /// </summary>
    /// <typeparam name="T">Type of object the file contains.</typeparam>
    public class XMLFileManager<T> : FileManager<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XMLFileManager{T}"/> class.
        /// </summary>
        public XMLFileManager() : base(new XMLFileStreamSerializer<T>())
        {
        }
    }
}
