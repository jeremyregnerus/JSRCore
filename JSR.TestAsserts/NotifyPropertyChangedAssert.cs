// <copyright file="NotifyPropertyChangedAssert.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.TestAsserts
{
    /// <summary>
    /// Tests objects that object that implement <see cref="INotifyPropertyChanged"/> raise property change notifications.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:DoNotUseRegions", Justification = "Regions used for signatures.")]
    public static class NotifyPropertyChangedAssert
    {
        #region NotifiesPropertiesChanges

        /// <summary>
        /// Tests that all properties within a Type raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        public static void NotifiesPropertiesChanged(Type type)
        {
            NotifiesPropertiesChanged(CreateINotifyPropertyChangeInstance(type));
        }

        /// <summary>
        /// Tests that specified properties within an object raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesPropertiesChanged(Type type, List<string> propertyNames)
        {
            NotifiesPropertiesChanged(CreateINotifyPropertyChangeInstance(type), propertyNames);
        }

        /// <summary>
        /// Tests that specified properties within an object raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesPropertiesChanged(Type type, List<PropertyInfo> properties)
        {
            NotifiesPropertiesChanged(CreateINotifyPropertyChangeInstance(type), properties);
        }

        /// <summary>
        /// Tests that all properties within a Type raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        public static void NotifiesPropertiesChanged<T>() where T : INotifyPropertyChanged
        {
            NotifiesPropertiesChanged(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that specified properties within a type raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="propertyNames">List of properties to test.</param>
        public static void NotifiesPropertiesChanged<T>(List<string> propertyNames) where T : INotifyPropertyChanged
        {
            NotifiesPropertiesChanged(Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Tests that specified properties raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesPropertiesChanged<T>(List<PropertyInfo> properties) where T : INotifyPropertyChanged
        {
            NotifiesPropertiesChanged(Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Tests that all properties within an object raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="obj">Object to test.</param>
        public static void NotifiesPropertiesChanged<T>(T obj) where T : INotifyPropertyChanged
        {
            NotifiesPropertiesChanged(obj, PropertyUtilities.GetListOfReadWriteProperties(obj));
        }

        /// <summary>
        /// Tests that specified properties within an object raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyNames">List of properties to test.</param>
        public static void NotifiesPropertiesChanged<T>(T obj, List<string> propertyNames) where T : INotifyPropertyChanged
        {
            foreach (string property in propertyNames)
            {
                NotifiesPropertyChanged(obj, property);
            }
        }

        /// <summary>
        /// Tests that specific properties raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesPropertiesChanged<T>(T obj, List<PropertyInfo> properties) where T : INotifyPropertyChanged
        {
            foreach (PropertyInfo property in properties)
            {
                NotifiesPropertyChanged(obj, property);
            }
        }

        #endregion

        #region NotifiesPropertyChanged

        /// <summary>
        /// Tests that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesPropertyChanged(Type type, string propertyName)
        {
            NotifiesPropertyChanged(CreateINotifyPropertyChangeInstance(type), propertyName);
        }

        /// <summary>
        /// Tests that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesPropertyChanged(Type type, PropertyInfo property)
        {
            NotifiesPropertyChanged(CreateINotifyPropertyChangeInstance(type), property);
        }

        /// <summary>
        /// Tests that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesPropertyChanged<T>(string propertyName) where T : INotifyPropertyChanged
        {
            NotifiesPropertyChanged<T>(typeof(T).GetProperty(propertyName));
        }

        /// <summary>
        /// Tests that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="property">Property to test.</param>
        public static void NotifiesPropertyChanged<T>(PropertyInfo property) where T : INotifyPropertyChanged
        {
            NotifiesPropertyChanged(Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Tests that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="obj">Object to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesPropertyChanged<T>(T obj, string propertyName) where T : INotifyPropertyChanged
        {
            NotifiesPropertyChanged(obj, typeof(T).GetProperty(propertyName));
        }

        /// <summary>
        /// Tests that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesPropertyChanged<T>(T obj, PropertyInfo property) where T : INotifyPropertyChanged
        {
            List<string> propertiesChanged = new List<string>();
            obj.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            int count = new Random().Next(5, 20);

            for (int i = 0; i < count; i++)
            {
                ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);
            }

            Assert.AreEqual(count, propertiesChanged.Count(propertyName => propertyName == property.Name));

            if (PropertyUtilities.CheckIfPropertyIsValue(property))
            {
                for (int i = 0; i < new Random().Next(5, 20); i++)
                {
                    ObjectUtilities.PopulateObjectWithRandomValues(obj);
                    T copiedObject = ObjectUtilities.GetSerializedCopyOfObject(obj);

                    propertiesChanged.Clear();

                    property.SetValue(obj, property.GetValue(copiedObject));

                    Assert.AreEqual(property.GetValue(obj), property.GetValue(copiedObject));
                    Assert.AreEqual(0, propertiesChanged.Count);
                }
            }
        }

        #endregion

        /// <summary>
        /// Tests that the type implements <see cref="INotifyPropertyChanged"/> and creates a new instance of the type.
        /// </summary>
        /// <param name="type">Type to test and create.</param>
        /// <returns>A new instance of the type.</returns>
        private static INotifyPropertyChanged CreateINotifyPropertyChangeInstance(Type type)
        {
            Assert.IsTrue(typeof(INotifyPropertyChanged).IsAssignableFrom(type));

            return (INotifyPropertyChanged)Activator.CreateInstance(type);
        }
    }
}
