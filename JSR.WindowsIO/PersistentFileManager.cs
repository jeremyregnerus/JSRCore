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
    public class PersistentFileManager<T> : BaseClass, IDisposable where T : IChangeTracking
    {
        private readonly string fileType;
        private readonly string extension;

        private IFileStreamSerializer<T> serializer;

        private T managedObject;
        private FileStream fileStream;

        public PersistentFileManager(IFileStreamSerializer<T> serializer, string fileType, string extension)
        {
            this.serializer = serializer;
            this.fileType = fileType;
            this.extension = extension;
        }

        public T ManagedObject
        {
            get => managedObject;

            set => SetValue(value, ref managedObject);
        }

        public string FilePath { get => IsLoaded ? fileStream.Name : string.Empty; }

        public bool IsLoaded { get => fileStream != null; }

        public bool CanWrite { get => IsLoaded ? fileStream.CanWrite : false; }

        public bool CanRead { get => IsLoaded ? fileStream.CanRead : false; }

        public override bool IsChanged
        {
            get => ManagedObject.IsChanged;
        }

        private string Filter { get => $"{fileType} (*{extension})|*{extension}"; }

        public void NewFile()
        {
            if (IsLoaded)
            {
                DialogResult result = MessageBox.Show($"Save the currently open file?\n{Path.GetFileName(FilePath)}", "Save Open File", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    SaveFile();
                }
            }

            Close
        }

        /// <summary>
        /// Saves the <see cref="ManagedObject"/>'s file.
        /// </summary>
        /// <returns>True if the file was succesfully saved.</returns>
        public bool SaveFile()
        {
            if (IsLoaded)
            {
                if (!CanWrite)
                {
                    if (MessageBox.Show($"{FilePath} is ReadOnly. Would you like to save the file in a new location?", $"File is Read Only", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        return SaveFileAs();
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
                return SaveFileAs();
            }
        }

        /// <summary>
        /// Save the <see cref="ManagedObject"/> to a new file location.
        /// </summary>
        /// <returns>True if the file was saved; false otherwise.</returns>
        public bool SaveFileAs()
        {
            using (SaveFileDialog dialog = new SaveFileDialog() { Filter = Filter, Title = $"Save {fileType} File", DefaultExt = extension, RestoreDirectory = true })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    fileStream = File.Create(dialog.FileName);
                    return SaveFile();
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CloseFile()
        {
            if (IsLoaded)
            {

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
                ManagedObject = default;
                SetFileStream(null);
            }
        }

        private bool CheckToSaveAndContinue()
        {
            if (IsLoaded && IsChanged)
            {
                DialogResult result = MessageBox.Show($"The current file has changed since it's last save.\n{Path.GetFileName(FilePath)}\nWould you like to save before you continue?", "Save modified file", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    SaveFile();
                }
            }
        }

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
