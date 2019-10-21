// <copyright file="FileManager.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JSR.BaseClassLibrary;

namespace JSR.FileManagement
{
    /// <summary>
    /// Manages basic saving and loading of files persistently.
    /// </summary>
    /// <typeparam name="T">Type of object the file contains.</typeparam>
    public class FileManager<T> : BaseClass, IFileManager<T>, IDisposable
    {
        private readonly IFileStreamSerializer<T> serializer;

        private FileStream fileStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileManager{T}"/> class.
        /// </summary>
        /// <param name="fileStreamSerializer"><see cref="IFileStreamSerializer{T}"/> to use for serialization and deserialization.</param>
        public FileManager(IFileStreamSerializer<T> fileStreamSerializer)
        {
            serializer = fileStreamSerializer;
        }

        /// <inheritdoc/>
        public string FilePath { get => FileLoaded ? fileStream?.Name : string.Empty; }

        /// <inheritdoc/>
        public bool FileLoaded { get => fileStream != null; }

        /// <inheritdoc/>
        public bool IsReadOnly { get => FileLoaded ? !fileStream.CanWrite : true; }

        private FileStream FileStream
        {
            set
            {
                fileStream = value;

                NotifyPropertiesChanged(new string[] { nameof(FilePath), nameof(FileLoaded), nameof(IsReadOnly) });
            }
        }

        /// <inheritdoc/>
        public T LoadFile(string filePath)
        {
            CloseFile();

            FileInfo fileInfo = new FileInfo(filePath);

            if (fileInfo.IsReadOnly)
            {
                FileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            }
            else
            {
                FileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            }

            return serializer.DeserializeFile(fileStream);
        }

        /// <inheritdoc/>
        public void SaveFile(T objectToSave)
        {
            if (IsReadOnly)
            {
                throw new InvalidOperationException($"The file {FilePath} is ReadOnly.");
            }

            serializer.SerializeFile(objectToSave, fileStream);
        }

        /// <inheritdoc/>
        public void SaveFile(T objectToSave, string filePath)
        {
            if (File.Exists(filePath))
            {
                if (filePath == FilePath)
                {
                    SaveFile(objectToSave);
                }
                else
                {
                    FileInfo fileInfo = new FileInfo(filePath);

                    if (fileInfo.IsReadOnly)
                    {
                        throw new InvalidOperationException($"The file {FilePath} is ReadOnly");
                    }
                }
            }

            CloseFile();

            FileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);

            SaveFile(objectToSave);
        }

        /// <inheritdoc/>
        public void CloseFile()
        {
            if (fileStream != null)
            {
                using (fileStream)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }

                FileStream = null;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">True if the object is already disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                CloseFile();
            }
        }
    }
}
