// <copyright file="FileManagerSimpleUI.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JSR.BaseClassLibrary;
using JSR.FileManagement;

namespace JSR.WindowsIO
{
    /// <summary>
    /// Provides simple file open and save dialogs for Windows applications.
    /// </summary>
    /// <typeparam name="T">Type of file to provide dialogs for.</typeparam>
    public class FileManagerSimpleUI<T> : BaseClass
    {
        private readonly IFileSerializer<T> serializer;

        private string filePath;

        /// <summary>
        /// Gets the filepath of the last opened or saved file.
        /// </summary>
        public string FilePath
        {
            get => filePath;
            private set => SetValue(value, ref filePath);
        }

        /// <summary>
        /// Gets the type of file managed by this FileManager.
        /// </summary>
        public string FileType { get; }

        /// <summary>
        /// Gets the Extension of the filetype managed by this FileManager.
        /// </summary>
        public string Extension { get; }

        private string Filter
        {
            get => $"{FileType}(*{Extension})|*{Extension}";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileManagerSimpleUI{T}"/> class.
        /// </summary>
        /// <param name="filePathSerializer"></param>
        /// <param name="fileType"></param>
        /// <param name="extension"></param>
        public FileManagerSimpleUI(IFileSerializer<T> filePathSerializer, string fileType, string extension)
        {
            serializer = filePathSerializer;

            FileType = fileType;
            Extension = extension;
        }

        public T Open()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return serializer.LoadFile(openFileDialog.FileName);
            }
            else
            {
                return default;
            }
        }

        public void Save()
        {

        }

        public void SaveAs()
        {

        }
    }
}
