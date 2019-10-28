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
    public class FileManager<T> : BaseClass, IDisposable
    {
        private readonly IFileStreamSerializer<T> serializer;

        private FileStream fileStream;

        private T managedObject;

        public void NewFile()
        {
            if (IsLoaded)
            {
                switch (ui.ReturnValue)
                {
                    case YesNoCancel.Yes:
                        NewFile(true);
                        break;
                    case YesNoCancel.No:
                        NewFile(false);
                        break;
                    case YesNoCancel.Cancel:
                        return;
                }
            }
            else
            {
                NewFile(false);
            }
        }

        /// <summary>
        /// Creates a new <see cref="ManagedObject"/> without a <see cref="FileStream"/>.
        /// </summary>
        /// <param name="save">Specify if a currently opened object should be saved.</param>
        public void NewFile(bool save)
        {
            CloseFile(save);
            ManagedObject = Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Loads the <see cref="ManagedObject"/> from a filepath.
        /// </summary>
        /// <param name="filePath">Filepath to load the object from.</param>
        public void LoadFile(string filePath)
        {
            CloseFile();


        }

        /// <summary>
        /// Closes the current file at <see cref="FilePath"/> and sets the <see cref="ManagedObject"/> to its default state.
        /// </summary>
        /// <param name="save">Specify if the current file should be saved.</param>
        public void CloseFile(bool save)
        {
            if (save)
            {
                SaveFile();
            }

            CloseFileStream();
            ManagedObject = default;
        }

        /// <summary>
        /// Saves the current <see cref="ManagedObject"/> to the <see cref="FilePath"/>.
        /// </summary>
        public void SaveFile()
        {
            if (ManagedObject == null)
            {
                return;
            }

            if (!IsLoaded)
            {
                throw new IOException($"There is no file to save to.");
            }

            if (!IsReadWrite)
            {
                throw new IOException($"The current file is ReadOnly");
            }

            serializer.SerializeFile(ManagedObject, FileStream);
        }


    }
}
