// <copyright file="PropertyNotificationAssert.cs" company="Jeremy Regnerus">
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
using System.Text;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.TestAsserts
{
    /// <summary>
    /// Tests objects that object that implement <see cref="INotifyPropertyChanged"/> raise property change notifications.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:DoNotUseRegions", Justification = "Regions used for signatures.")]
    public static class PropertyNotificationAssert
    {
        #region NotifiesPropertiesChanges

        /// <summary>
        /// Tests that all properties within a Type raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        public static void NotifiesPropertiesChanged<T>() where T : INotifyPropertyChanged
        {
            NotifiesPropertiesChanged(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that all properties within an object raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="objectToTest">Object to test.</param>
        public static void NotifiesPropertiesChanged<T>(T objectToTest) where T : INotifyPropertyChanged
        {
            NotifiesPropertiesChanged(PropertyUtilities.GetListOfPropertiesWithPublicGetAndSetMethods(objectToTest), objectToTest);

            // TODO: - 1 Implement checking for items added and removed from a list.
            ////NotifiesListChangedOnItemsAdded(PropertyUtilities.GetListOfPropertiesWithListValues(objectToTest), objectToTest);
        }

        /// <summary>
        /// Tests that specified properties within a type raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="propertyNames">List of properties to test.</param>
        public static void NotifiesPropertiesChanged<T>(List<string> propertyNames) where T : INotifyPropertyChanged
        {
            NotifiesPropertiesChanged(propertyNames, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that specified properties within an object raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="propertyNames">List of properties to test.</param>
        /// <param name="objectToTest">Object with properties to test.</param>
        public static void NotifiesPropertiesChanged<T>(List<string> propertyNames, T objectToTest) where T : INotifyPropertyChanged
        {
            foreach (string property in propertyNames)
            {
                NotifiesPropertyChanged(property, objectToTest);
            }
        }

        /// <summary>
        /// Tests that specified properties raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesPropertiesChanged<T>(List<PropertyInfo> properties) where T : INotifyPropertyChanged
        {
            NotifiesPropertiesChanged(properties, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that specific properties raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        /// <param name="objectToTest">Object with properties to test.</param>
        public static void NotifiesPropertiesChanged<T>(List<PropertyInfo> properties, T objectToTest) where T : INotifyPropertyChanged
        {
            foreach (PropertyInfo property in properties)
            {
                NotifiesPropertyChanged(property, objectToTest);
            }
        }

        #endregion

        #region NotifiesPropertyChanged

        /// <summary>
        /// Tests that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesPropertyChanged<T>(string propertyName) where T : INotifyPropertyChanged
        {
            NotifiesPropertyChanged(typeof(T).GetProperty(propertyName), Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="property">Property to test.</param>
        public static void NotifiesPropertyChanged<T>(PropertyInfo property) where T : INotifyPropertyChanged
        {
            NotifiesPropertyChanged(property, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        /// <param name="objectToTest">Object to test.</param>
        public static void NotifiesPropertyChanged<T>(string propertyName, T objectToTest) where T : INotifyPropertyChanged
        {
            NotifiesPropertyChanged(typeof(T).GetProperty(propertyName), objectToTest);
        }

        /// <summary>
        /// Tests that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="property">Property to test.</param>
        /// <param name="objectToTest">Object with property to test.</param>
        public static void NotifiesPropertyChanged<T>(PropertyInfo property, T objectToTest) where T : INotifyPropertyChanged
        {
            List<string> propertiesChanged = new List<string>();
            objectToTest.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            int count = new Random().Next(5, 20);

            for (int i = 0; i < count; i++)
            {
                ObjectUtilities.PopulatePropertyWithRandomValue(objectToTest, property);

                CollectionAssert.Contains(propertiesChanged, property.Name);
                propertiesChanged.Clear();
            }
        }

        #endregion

        #region NotifiesListChangedOnItemsAdded

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
            dynamic list = listProperty.GetValue(objectToTest);

            NotifiesListChangedOnItemsAdded(listProperty, objectToTest, list);



            if (!typeof(IList).IsAssignableFrom(listProperty.PropertyType) || !typeof(INotifyPropertyChanged).IsAssignableFrom(listProperty.PropertyType))
            {
                throw new ArgumentException($"The property {listProperty.Name} does not implement either or both IList or INotifyPropertyChanged", listProperty.Name);
            }






            Type listType = list.GetType().GenericTypeArguments[0];

            List<string> propertiesChanged = new List<string>();
            objectToTest.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            int count = new Random().Next(5, 20);

            for (int i = 0; i < count; i++)
            {
                ((IList)list).Add(Activator.CreateInstance(listType));

                Assert.AreEqual(i, propertiesChanged.Count(x => x == listProperty.Name));
            }
        }

        /// <summary>
        /// Tests that lists raise the <see cref="PropertyChangedEventHandler"/> when items are added.
        /// </summary>
        /// <typeparam name="T">Type that contains the list property.</typeparam>
        /// <typeparam name="TlistItem">Type of object contained within the list.</typeparam>
        /// <param name="listProperty">Property that contains the list.</param>
        /// <param name="objectToTest">Object that contains the list.</param>
        /// <param name="list">List to test.</param>
        public static void NotifiesListChangedOnItemsAdded<T, TlistItem>(PropertyInfo listProperty, T objectToTest, ObservableCollection<TlistItem> list) where T : INotifyPropertyChanged
        {
            NotifiesListChangedOnItemsAdded(listProperty.Name, objectToTest, list);
        }

        /// <summary>
        /// Tests that lists raise the <see cref="PropertyChangedEventHandler"/> when items are added.
        /// </summary>
        /// <typeparam name="T">Type that contains the list property.</typeparam>
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

        #endregion
    }
}
