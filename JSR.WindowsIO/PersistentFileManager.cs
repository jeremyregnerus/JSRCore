// <copyright file="PersistentFileManager.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JSR.BaseClassLibrary;
using JSR.FileManagement;

namespace JSR.WindowsIO
{
    /// <summary>
    /// Maintains control over file resource through opening, saving and closing.
    /// </summary>
    /// <typeparam name="T">Type of file object to manage.</typeparam>
    public class PersistentFileManager<T> : BaseClass, IDisposable where T : IChangeTracking
    {
        private readonly string fileType;
        private readonly string extension;

        private readonly IFileStreamSerializer<T> serializer;

        private T managedObject;
        private FileStream fileStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistentFileManager{T}"/> class.
        /// </summary>
        /// <param name="serializer">Serializer to serialize and deserialize the file.</param>
        /// <param name="fileType">Type of file being managed.</param>
        /// <param name="extension">The file extension of the file being managed.</param>
        public PersistentFileManager(IFileStreamSerializer<T> serializer, string fileType, string extension)
        {
            this.serializer = serializer;
            this.fileType = fileType;
            this.extension = extension;
        }

        /// <summary>
        /// Gets the object managed by this file manager.
        /// </summary>
        public T ManagedObject
        {
            get => managedObject;

            private set => SetValue(value, ref managedObject);
        }

        /// <summary>
        /// Gets the path where the active file is saved.
        /// </summary>
        public string FilePath { get => IsLoaded ? fileStream.Name : string.Empty; }

        /// <summary>
        /// Gets a value indicating whether there is a file loaded or not.
        /// </summary>
        public bool IsLoaded { get => fileStream != null; }

        /// <summary>
        /// Gets a value indicating whether the active file can be written to.
        /// </summary>
        public bool CanWrite { get => IsLoaded ? fileStream.CanWrite : false; }

        /// <summary>
        /// Gets a value indicating whether the active file can be read.
        /// </summary>
        public bool CanRead { get => IsLoaded ? fileStream.CanRead : false; }

        /// <inheritdoc/>
        public override bool IsChanged
        {
            get => ManagedObject != null ? ManagedObject.IsChanged : false;
        }

        private string Filter { get => $"{fileType} (*{extension})|*{extension}"; }

        /// <summary>
        /// Creates a new object to be managed.
        /// </summary>
        public void CreateNew()
        {
            if (IsChanged)
            {
                DialogResult result = MessageBox.Show($"Save the currently open file?\n{Path.GetFileName(FilePath)}", "Save Open File", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    Save();
                }
            }
        }

        public void Open()
        {

        }

        /// <summary>
        /// Saves the <see cref="ManagedObject"/>'s file.
        /// </summary>
        /// <returns>True if the file was succesfully saved.</returns>
        public bool Save()
        {
            if (IsLoaded)
            {
                if (CheckToSaveAndContinue())
                {

                }

                if (!CanWrite)
                {
                    if (MessageBox.Show($"{FilePath} is ReadOnly. Would you like to save the file in a new location?", $"File is Read Only", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        return SaveAs();
                    }

                    return false;
                }
                else
                {
                    ManagedObject.AcceptChanges();
                    serializer.SerializeFile(ManagedObject, fileStream);
                    return true;
                }
            }
            else
            {
                return SaveAs();
            }
        }

        /// <summary>
        /// Save the <see cref="ManagedObject"/> to a new file location.
        /// </summary>
        /// <returns>True if the file was saved; false otherwise.</returns>
        public bool SaveAs()
        {
            using (SaveFileDialog dialog = new SaveFileDialog() { Filter = Filter, Title = $"Save {fileType} File", DefaultExt = extension, RestoreDirectory = true })
            {
                if (dialog.ShowDialog() == DialogResult.OK && CheckIfFileIsWritable(dialog.FileName))
                {
                    fileStream = File.Create(dialog.FileName);
                    return Save();
                }

                return false;
            }
        }

        /// <summary>
        /// Closes the currently open file.
        /// </summary>
        /// <returns>True if the file was closed; otherwise false.</returns>
        public bool Close()
        {
            if (!CheckToSaveAndContinue())
            {
                return false;
            }

            if (ManagedObject != default)
            {
                ManagedObject = default;
            }

            if (IsLoaded)
            {
                SetFileStream(null);
            }

            return true;
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
                ManagedObject = default;
                SetFileStream(null);
            }
        }

        /// <summary>
        /// Checks if a file can be written to a specific location.
        /// </summary>
        /// <param name="filePath">The filepath to check against. If the string is null or empty, the current filepath will be evaluated.</param>
        /// <returns>True if the file can be written to; false if the file is readonly.</returns>
        private bool CheckIfFileIsWritable(string filePath)
        {
            if (string.IsNullOrEmpty(FilePath) || filePath == FilePath)
            {
                return CanWrite;
            }
            else
            {
                return File.Exists(filePath) ? !new FileInfo(FilePath).IsReadOnly : true;
            }
        }

        /// <summary>
        /// Sets the current filestream to a new value.
        /// </summary>
        /// <param name="newFileStream">FileStream to use. This value can be null to set the FileStream to nothing.</param>
        private void SetFileStream(FileStream newFileStream)
        {
            if (fileStream != null)
            {
                using (fileStream)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }

            fileStream = newFileStream;

            NotifyPropertiesChanged(new string[] { nameof(FilePath), nameof(IsLoaded), nameof(CanWrite), nameof(CanRead) });
        }
    }
}
