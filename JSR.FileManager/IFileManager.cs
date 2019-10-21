// <copyright file="IFileManager.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;

namespace JSR.FileManagement
{
    /// <summary>
    /// Interface for serializers used for mainting control over files.
    /// </summary>
    /// <typeparam name="T">Type of object the file contains.</typeparam>
    public interface IFileManager<T> : IFilePathSerializer<T>, IDisposable
    {
        /// <summary>
        /// Gets the filepath of the managed file / object.
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// Gets a value indicating whether their is a file associated with the object.
        /// </summary>
        bool FileLoaded { get; }

        /// <summary>
        /// Gets a value indicating whether the file is loaded ReadOnly.
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// Saves an object to the current filestream.
        /// </summary>
        /// <param name="objectToSave">Object to save.</param>
        void SaveFile(T objectToSave);

        /// <summary>
        /// Closes the current file.
        /// </summary>
        void CloseFile();
    }
}
