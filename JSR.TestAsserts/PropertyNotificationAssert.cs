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
            NotifiesPropertiesChanged(PropertyUtilities.GetListOfProperties(objectToTest, true, false, false, true, true, true), objectToTest);
            NotifiesListsChange(objectToTest);
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
            }

            Assert.AreEqual(count, propertiesChanged.Count(p => p == property.Name));
        }

        #endregion

        #region NotifiesListsChange

        /// <summary>
        /// Tests that the lists within an object notify property change.
        /// </summary>
        /// <typeparam name="T">Type with list properties.</typeparam>
        public static void NotifiesListsChange<T>() where T : INotifyPropertyChanged
        {
            NotifiesListsChange(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that the lists within an object notify property change.
        /// </summary>
        /// <typeparam name="T">Type with list properties.</typeparam>
        /// <param name="objectToTest">Object with list properties to test.</param>
        public static void NotifiesListsChange<T>(T objectToTest) where T : INotifyPropertyChanged
        {
            NotifiesListsChange(PropertyUtilities.GetListOfPropertiesWithListTypes(objectToTest, true, true, true), objectToTest);
        }

        /// <summary>
        /// Tests that the lists within an object notify property change.
        /// </summary>
        /// <typeparam name="T">Type with list properties.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesListsChange<T>(List<PropertyInfo> properties) where T : INotifyPropertyChanged
        {
            NotifiesListsChange(properties, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that the list within an object notify property change.
        /// </summary>
        /// <typeparam name="T">Type with list properties.</typeparam>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesListsChange<T>(List<string> propertyNames) where T : INotifyPropertyChanged
        {
            NotifiesListsChange(propertyNames, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that the lists within an object notify property change.
        /// </summary>
        /// <typeparam name="T">Type with list properties.</typeparam>
        /// <param name="propertyNames">List of properties to test.</param>
        /// <param name="objectToTest">Object to with list properties to test.</param>
        public static void NotifiesListsChange<T>(List<string> propertyNames, T objectToTest) where T : INotifyPropertyChanged
        {
            foreach (string propertyName in propertyNames)
            {
                NotifiesListChanged(propertyName, objectToTest);
            }
        }

        /// <summary>
        /// Tests that the lists within an object notify property change.
        /// </summary>
        /// <typeparam name="T">Type with list properties.</typeparam>
        /// <param name="properties">List of list properties to test.</param>
        /// <param name="objectToTest">Object with list properties to test.</param>
        public static void NotifiesListsChange<T>(List<PropertyInfo> properties, T objectToTest) where T : INotifyPropertyChanged
        {
            foreach (PropertyInfo property in properties)
            {
                NotifiesListChanged(property, objectToTest);
            }
        }

        #endregion

        #region NotifiesListChanged

        /// <summary>
        /// Tests that a list notifies when items are randomly added and removed.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="propertyName">Name of property with list to test.</param>
        public static void NotifiesListChanged<T>(string propertyName) where T : INotifyPropertyChanged
        {
            NotifiesListChanged(propertyName, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that a list notifies when items are randomly added and removed.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="property">Property with list to test.</param>
        public static void NotifiesListChanged<T>(PropertyInfo property) where T : INotifyPropertyChanged
        {
            NotifiesListChanged(property, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that a list notifies when items are randomly added and removed.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="propertyName">Name of property with list to test.</param>
        /// <param name="objectToTest">Object with list property to test.</param>
        public static void NotifiesListChanged<T>(string propertyName, T objectToTest) where T : INotifyPropertyChanged
        {
            NotifiesListChanged(objectToTest.GetType().GetProperty(propertyName), objectToTest);
        }

        /// <summary>
        /// Tests that a list notifies when items are randomly added and removed.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="property">Property with list to test.</param>
        /// <param name="objectToTest">Object with list property to test.</param>
        public static void NotifiesListChanged<T>(PropertyInfo property, T objectToTest) where T : INotifyPropertyChanged
        {
            NotifiesListChanged(property.Name, objectToTest, (IList)property.GetValue(objectToTest));
        }

        /// <summary>
        /// Tests that a list notifies when items are randomly added and removed.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="propertyName">Name of property with list to test.</param>
        /// <param name="objectToTest">Object with list property to test.</param>
        /// <param name="listToTest">List to test. This list should derive from <see cref="ObservableCollection{T}"/>.</param>
        public static void NotifiesListChanged<T>(string propertyName, T objectToTest, IList listToTest) where T : INotifyPropertyChanged
        {
            NotifiesListChangedOnItemsAdded(propertyName, objectToTest, listToTest);
            NotifiesListChangedOnItemsRemoved(propertyName, objectToTest, listToTest);
        }

        #endregion

        #region NotifiesListChangedOnItemsAdded

        /// <summary>
        /// Tests that when items are added to a list, the object containing the list notifies change.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="listPropertyName">Name of property with list to test.</param>
        public static void NotifiesListChangedOnItemsAdded<T>(string listPropertyName) where T : INotifyPropertyChanged
        {
            NotifiesListChangedOnItemsAdded(typeof(T).GetType().GetProperty(listPropertyName), Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when items are added to a list, the object containing the list notifies change.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="listProperty">Property with list to test.</param>
        public static void NotifiesListChangedOnItemsAdded<T>(PropertyInfo listProperty) where T : INotifyPropertyChanged
        {
            NotifiesListChangedOnItemsAdded(listProperty, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when items are added to a list, the object containing the list notifies change.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="listPropertyName">Name of property with list to test.</param>
        /// <param name="objectToTest">Object twith list property to test.</param>
        public static void NotifiesListChangedOnItemsAdded<T>(string listPropertyName, T objectToTest) where T : INotifyPropertyChanged
        {
            NotifiesListChangedOnItemsAdded(typeof(T).GetType().GetProperty(listPropertyName), objectToTest);
        }

        /// <summary>
        /// Tests that when items are added to a list, the object containing the list notifies change.
        /// </summary>
        /// <typeparam name="T">Type with list property to test..</typeparam>
        /// <param name="listProperty">Property with list to test.</param>
        /// <param name="objectToTest">Object with list to test.</param>
        public static void NotifiesListChangedOnItemsAdded<T>(PropertyInfo listProperty, T objectToTest) where T : INotifyPropertyChanged
        {
            NotifiesListChangedOnItemsAdded(listProperty.Name, objectToTest, (IList)listProperty.GetValue(objectToTest));
        }

        /// <summary>
        /// Tests that when items are added to a list, the object containing the list notifies change.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="listPropertyName">Name of property with list to test.</param>
        /// <param name="objectToTest">Object with list property to test.</param>
        /// <param name="listToTest">List to test. This list should derive from <see cref="ObservableCollection{T}"/>.</param>
        public static void NotifiesListChangedOnItemsAdded<T>(string listPropertyName, T objectToTest, IList listToTest) where T : INotifyPropertyChanged
        {
            List<string> propertiesChanged = new List<string>();
            objectToTest.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            Assert.AreEqual(ObjectUtilities.AddRandomItemsToList(listToTest), propertiesChanged.Count(x => x == listPropertyName));
        }

        #endregion

        #region NotifiesListChangesOnItemsRemoved

        /// <summary>
        /// Tests that a list notifies change when a random number of items are removed.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="listPropertyName">Name of property with list to test.</param>
        public static void NotifiesListChangedOnItemsRemoved<T>(string listPropertyName) where T : INotifyPropertyChanged
        {
            NotifiesListChangedOnItemsRemoved(listPropertyName, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that a list notifies change when a random number of items are removed.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="listProperty">Property with list to test.</param>
        public static void NotifiesListChangedOnItemsRemoved<T>(PropertyInfo listProperty) where T : INotifyPropertyChanged
        {
            NotifiesListChangedOnItemsRemoved(listProperty, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that a list notifies change when a random number of items are removed.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="listPropertyName">Name of property with list to test.</param>
        /// <param name="objectToTest">Object with list property to test.</param>
        public static void NotifiesListChangedOnItemsRemoved<T>(string listPropertyName, T objectToTest) where T : INotifyPropertyChanged
        {
            NotifiesListChangedOnItemsRemoved(objectToTest.GetType().GetProperty(listPropertyName), objectToTest);
        }

        /// <summary>
        /// Tests that a list notifies change when a random number of items are removed.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="listProperty">Property with list to test.</param>
        /// <param name="objectToTest">Object with list property to test.</param>
        public static void NotifiesListChangedOnItemsRemoved<T>(PropertyInfo listProperty, T objectToTest) where T : INotifyPropertyChanged
        {
            NotifiesListChangedOnItemsRemoved(listProperty.Name, objectToTest, (IList)listProperty.GetValue(objectToTest));
        }

        /// <summary>
        /// Tests that a list notifies change when a random number of items are removed.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="listPropertyName">Name of the property containing the list.</param>
        /// <param name="objectToTest">Object that contains the list.</param>
        /// <param name="listToTest">List to test. This list should derive from <see cref="ObservableCollection{T}"/>.</param>
        public static void NotifiesListChangedOnItemsRemoved<T>(string listPropertyName, T objectToTest, IList listToTest) where T : INotifyPropertyChanged
        {
            if (listToTest.Count <= 0)
            {
                ObjectUtilities.PopulateListWithRandomValues(listToTest);
            }

            List<string> propertiesChanged = new List<string>();
            objectToTest.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            Assert.AreEqual(ObjectUtilities.RemoveRandomItemsFromList(listToTest), propertiesChanged.Count(x => x == listPropertyName));
        }

        #endregion
    }
}
