﻿// <copyright file="DataContractFileStreamSerializer.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace JSR.FileManagement
{
    /// <summary>
    /// Deserializes and Deserializes objects using the <see cref="DataContractSerializer"/>.
    /// </summary>
    /// <typeparam name="T">Type of object to serialize and deserialize.</typeparam>
    public class DataContractFileStreamSerializer<T> : IFileStreamSerializer<T>
    {
        private readonly DataContractSerializer serializer = new DataContractSerializer(typeof(T), new DataContractSerializerSettings() { PreserveObjectReferences = true });

        /// <inheritdoc/>
        public T DeserializeFile(FileStream fileStream)
        {
            using (XmlReader reader = XmlReader.Create(fileStream, new XmlReaderSettings()))
            {
                return (T)serializer.ReadObject(reader);
            }
        }

        /// <inheritdoc/>
        public void SerializeFile(T objectToSave, FileStream fileStream)
        {
            using (XmlWriter writer = XmlWriter.Create(fileStream, new XmlWriterSettings() { Indent = true, IndentChars = "\t" }))
            {
                serializer.WriteObject(writer, objectToSave);
            }
        }
    }
}
