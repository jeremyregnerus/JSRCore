// <copyright file="PropertyChangeAssert.cs" company="Jeremy Regnerus">
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
    /// Tests objects that support property change and property change notification.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:DoNotUseRegions", Justification = "Regions used for signatures.")]
    public static class PropertyChangeAssert
    {
        #region Changes Values

        /// <summary>
        /// Tests that an object's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type of object to test.</typeparam>
        public static void ChangesValues<T>()
        {
            ChangesValues(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Test that an object's property values can be changed.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> to test.</typeparam>
        /// <param name="objectToTest"><see cref="object"/> to test.</param>
        public static void ChangesValues<T>(T objectToTest)
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                T copiedObject = ObjectUtilities.CreateInstanceWithRandomValues<T>();

                ObjectUtilities.CopyValuesFromObjectToObject(copiedObject, objectToTest);

                EquivalencyAssert.AreEquivalent(objectToTest, copiedObject);
            }
        }

        /// <summary>
        /// Test that an object's property values can be changed.
        /// </summary>
        /// <param name="typeToTest">Type of object to test.</param>
        public static void ChangesValues(Type typeToTest)
        {
            ChangesValues(Activator.CreateInstance(typeToTest));
        }

        /// <summary>
        /// Test that a specific property within an object's value can be changed.
        /// </summary>
        /// <typeparam name="T">Type of object with the property to test.</typeparam>
        /// <param name="propertyName">Name of the property to test.</param>
        public static void ChangesValue<T>(string propertyName)
        {
            ChangesValue(typeof(T).GetProperty(propertyName), Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Test that a specific property within an object's value can be changed.
        /// </summary>
        /// <typeparam name="T">Type of object with the property to test.</typeparam>
        /// <param name="propertyName">Name of the property to test.</param>
        /// <param name="objectToTest">Object with the property to test.</param>
        public static void ChangesValue<T>(string propertyName, T objectToTest)
        {
            ChangesValue(typeof(T).GetProperty(propertyName), objectToTest);
        }

        public static void ChangesValue(string propertyName, Type typeToTest)
        {
            ChangesValue(typeToTest.GetProperty(propertyName), typeToTest);
        }

        /// <summary>
        /// Test that a specific property within an object's value can be changed.
        /// </summary>
        /// <typeparam name="T">Type of object with the property to test.</typeparam>
        /// <param name="property">Property to test.</param>
        public static void ChangesValue<T>(PropertyInfo property)
        {
            ChangesValue(property, Activator.CreateInstance<T>());
        }

        public static void ChangesValue(PropertyInfo property, Type typeToTest)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            var objectToTest = ObjectUtilities.CreateInstanceWithRandomValues(typeToTest);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                var randomObject = ObjectUtilities.CreateInstanceWithRandomValues(typeToTest);

                property.SetValue(objectToTest, property.GetValue(randomObject));

                Assert.AreEqual(property.GetValue(randomObject), property.GetValue(objectToTest));
            }
        }

        /// <summary>
        /// Tests that a specific property of an object changes values.
        /// </summary>
        /// <typeparam name="T">Type of object to test.</typeparam>
        /// <param name="property">Property to test.</param>
        /// <param name="objectToTest">Object with property to test.</param>
        public static void ChangesValue<T>(PropertyInfo property, T objectToTest)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                T randomObject = ObjectUtilities.CreateInstanceWithRandomValues<T>();

                property.SetValue(objectToTest, property.GetValue(randomObject));

                Assert.AreEqual(property.GetValue(randomObject), property.GetValue(objectToTest));
            }
        }

        #endregion

        /// <summary>
        /// Tests that all properties on a Type notify property changes.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        public static void NotifiesEachPropertyChanges<T>() where T : INotifyPropertyChanged
        {
            NotifiesEachPropertyChanges(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that all properties on an object notify property changes.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="objectToTest"><see cref="object"/> to test.</param>
        public static void NotifiesEachPropertyChanges<T>(T objectToTest) where T : INotifyPropertyChanged
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                List<string> propertiesChanged = new List<string>();
                objectToTest.PropertyChanged += (x, y) => propertiesChanged.Add(y.PropertyName);

                ObjectUtilities.PopulateObjectWithRandomValues(objectToTest);

                foreach (string propertyName in PropertyUtilities.GetListOfPropertyNamesWithPublicSetMethod(objectToTest.GetType()))
                {
                    CollectionAssert.Contains(propertiesChanged, propertyName);
                }
            }
        }

        /// <summary>
        /// Tests that specified properties raise the PropertyChanged event for a Type.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="propertyNames">List of properties to test for the PropertyChanged event.</param>
        public static void NotifiesPropertiesChanged<T>(List<string> propertyNames) where T : INotifyPropertyChanged
        {
            NotifiesPropertiesChanged(propertyNames, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that specified properties raise the PropertyChanged event for an object.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="propertyNames">List of properties to test for the PropertyChanged event.</param>
        /// <param name="objectToTest">Object to test.</param>
        public static void NotifiesPropertiesChanged<T>(List<string> propertyNames, T objectToTest) where T : INotifyPropertyChanged
        {
            foreach (string property in propertyNames)
            {
                NotifiesPropertyChanged(property, objectToTest);
            }
        }

        /// <summary>
        /// Tests that a property raises the PropertyChanged event for a Type.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="propertyName">Name of property to test for the PropertyChanged event.</param>
        public static void NotifiesPropertyChanged<T>(string propertyName) where T : INotifyPropertyChanged
        {
            NotifiesPropertyChanged(propertyName, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that a property raises the PropertyChanged event for an object.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="propertyName">Name of property to test for the PropertyChanged event.</param>
        /// <param name="objectToTest">Object to test.</param>
        public static void NotifiesPropertyChanged<T>(string propertyName, T objectToTest) where T : INotifyPropertyChanged
        {
            List<string> propertyChanges = new List<string>();
            objectToTest.PropertyChanged += (x, y) => propertyChanges.Add(y.PropertyName);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulatePropertyWithRandomValue(objectToTest, propertyName);
                CollectionAssert.Contains(propertyChanges, propertyName);
                propertyChanges.Clear();
            }
        }

        public static void NotifiesListChangedOnItemsAdded<T>(string listPropertyName) where T : INotifyPropertyChanged
        {
            NotifiesListChangedOnItemsAdded(typeof(T).GetType().GetProperty(listPropertyName), Activator.CreateInstance<T>());
        }

        public static void NotifiesListChangedOnItemsAdded<T>(PropertyInfo listProperty) where T : INotifyPropertyChanged
        {
            NotifiesListChangedOnItemsAdded(listProperty, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when items are added to a list, the object containing the list notifies change.
        /// </summary>
        /// <typeparam name="T">Type of object that contains the list property.</typeparam>
        /// <param name="listPropertyName">Name of the property that contains the list.</param>
        /// <param name="objectToTest">Object that contains the list.</param>
        public static void NotifiesListChangedOnItemsAdded<T>(string listPropertyName, T objectToTest) where T : INotifyPropertyChanged
        {
            NotifiesListChangedOnItemsAdded(typeof(T).GetType().GetProperty(listPropertyName), objectToTest);
        }

        /// <summary>
        /// Tests that when items are added to a list, the object containing the list notifies change.
        /// </summary>
        /// <typeparam name="T">Type of object that contains the list property.</typeparam>
        /// <param name="listProperty">Property that contains the list.</param>
        /// <param name="objectToTest">Object that contains the list.</param>
        public static void NotifiesListChangedOnItemsAdded<T>(PropertyInfo listProperty, T objectToTest) where T : INotifyPropertyChanged
        {
            if (!typeof(IList).IsAssignableFrom(listProperty.PropertyType) || !typeof(INotifyPropertyChanged).IsAssignableFrom(listProperty.PropertyType))
            {
                throw new ArgumentException($"The property {listProperty.Name} does not implement either or both IList or INotifyPropertyChanged", listProperty.Name);
            }

            var propertyValue = listProperty.GetValue(objectToTest);
            Type listType = propertyValue.GetType().GenericTypeArguments[0];

            List<string> propertiesChanged = new List<string>();
            objectToTest.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            int count = new Random().Next(5, 20);

            for (int i = 0; i < count; i++)
            {
                ((IList)propertyValue).Add(Activator.CreateInstance(listType));

                Assert.AreEqual(i, propertiesChanged.Count(x => x == listProperty.Name));
            }
        }

        /// <summary>
        /// Tests that when items are added to a list, the object containing the list notifies change.
        /// </summary>
        /// <typeparam name="T">Type of object that contains the list property.</typeparam>
        /// <typeparam name="TlistItem">Type of object contained within the list.</typeparam>
        /// <param name="listProperty">Property that contains the list.</param>
        /// <param name="objectToTest">Object that contains the list.</param>
        /// <param name="list">List to test.</param>
        public static void NotifiesListChangedOnItemsAdded<T, TlistItem>(PropertyInfo listProperty, T objectToTest, ObservableCollection<TlistItem> list) where T : INotifyPropertyChanged
        {
            NotifiesListChangedOnItemsAdded(listProperty.Name, objectToTest, list);
        }

        /// <summary>
        /// Tests that when items are added to a list, the object containing the list notifies change.
        /// </summary>
        /// <typeparam name="T">Type of object that contains the list property.</typeparam>
        /// <typeparam name="TlistItem">Type of object contained within the list.</typeparam>
        /// <param name="listPropertyName">Name of the property that contains the list.</param>
        /// <param name="objectToTest">Object that contains the list.</param>
        /// <param name="list">The list to add items to.</param>
        public static void NotifiesListChangedOnItemsAdded<T, TlistItem>(string listPropertyName, T objectToTest, ObservableCollection<TlistItem> list) where T : INotifyPropertyChanged
        {
            List<string> propertiesChanged = new List<string>();
            objectToTest.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            int count = new Random().Next(5, 20);

            for (int i = 0; i < count; i++)
            {
                list.Add(Activator.CreateInstance<TlistItem>());

                CollectionAssert.Contains(propertiesChanged, listPropertyName);
            }

            Assert.AreEqual(count, propertiesChanged.Count(x => x == listPropertyName));
        }
    }
}
