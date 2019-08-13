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
        #region NotifiesChanges

        /// <summary>
        /// Tests change notification for properties and lists.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        public static void NotifiesChanges(Type type)
        {
            NotifiesChanges(CreateINotifyPropertyChangeInstance(type));
        }

        /// <summary>
        /// Tests change notification for properties and lists.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        /// <param name="propertyNames">Names of properties to test.</param>
        public static void NotifiesChanges(Type type, List<string> propertyNames)
        {
            NotifiesChanges(CreateINotifyPropertyChangeInstance(type), propertyNames);
        }

        /// <summary>
        /// Tests change notification for properties and lists.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        /// <param name="properties">Properties to test.</param>
        public static void NotifiesChanges(Type type, List<PropertyInfo> properties)
        {
            NotifiesChanges(CreateINotifyPropertyChangeInstance(type), properties);
        }

        /// <summary>
        /// Tests change notification for properties and lists.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesChanges(Type type, string propertyName)
        {
            NotifiesChanges(CreateINotifyPropertyChangeInstance(type), propertyName);
        }

        /// <summary>
        /// Tests change notification for properties and lists.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesChanges(Type type, PropertyInfo property)
        {
            NotifiesChanges(CreateINotifyPropertyChangeInstance(type), property);
        }

        /// <summary>
        /// Tests change notification for properties and lists.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        public static void NotifiesChanges<T>() where T : INotifyPropertyChanged
        {
            NotifiesChanges(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests change notification for properties and lists.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="propertyNames">Names of propertiest to test.</param>
        public static void NotifiesChanges<T>(List<string> propertyNames) where T : INotifyPropertyChanged
        {
            NotifiesChanges(Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Tests change notification for properties and lists.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="properties">Properties to test.</param>
        public static void NotfiesChanges<T>(List<PropertyInfo> properties) where T : INotifyPropertyChanged
        {
            NotifiesChanges(Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Tests change notification for properties and lists.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotfiesChanges<T>(string propertyName) where T : INotifyPropertyChanged
        {
            NotifiesChanges<T>(typeof(T).GetProperty(propertyName));
        }

        /// <summary>
        /// Tests change notification for properties and lists.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="property">Property to test.</param>
        public static void NotifiesChanges<T>(PropertyInfo property) where T : INotifyPropertyChanged
        {
            NotifiesChanges(Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Tests change notification for properties and lists.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        public static void NotifiesChanges<T>(T obj) where T : INotifyPropertyChanged
        {
            NotifiesChanges(obj, PropertyUtilities.GetListOfProperties(obj, true, true, true, true, true, true));
        }

        /// <summary>
        /// Tests change notification for properties and lists.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyNames">Names of properties to test.</param>
        public static void NotifiesChanges<T>(T obj, List<string> propertyNames) where T : INotifyPropertyChanged
        {
            foreach (string propertyName in propertyNames)
            {
                NotifiesChanges(obj, propertyName);
            }
        }

        /// <summary>
        /// Tests change notification for properties and lists.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyName">Name of properties to test.</param>
        public static void NotifiesChanges<T>(T obj, string propertyName) where T : INotifyPropertyChanged
        {
            NotifiesChanges(obj, obj.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Tests change notification for properties and lists.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">Properties to test.</param>
        public static void NotifiesChanges<T>(T obj, List<PropertyInfo> properties) where T : INotifyPropertyChanged
        {
            foreach (PropertyInfo property in properties)
            {
                NotifiesChanges(obj, property);
            }
        }

        /// <summary>
        /// Tests change notification for properties and lists.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesChanges<T>(T obj, PropertyInfo property) where T : INotifyPropertyChanged
        {
            if (PropertyUtilities.CheckIfPropertyIsReadWrite(property))
            {
                NotifiesPropertyChanged(obj, property);
            }

            if (typeof(IList).IsAssignableFrom(property.PropertyType))
            {
                NotifiesListChanged(obj, property, true, true);
            }
        }

        #endregion

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
        public static void NotifeisPropertiesChanged(Type type, List<PropertyInfo> properties)
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
            PropertyNotificationAssertTracker<T> tracker = new PropertyNotificationAssertTracker<T>(obj);

            int count = new Random().Next(5, 20);

            for (int i = 0; i < count; i++)
            {
                ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);
            }

            tracker.AssertPropertyCount(property.Name, count);
        }

        #endregion

        #region NotifiesListsChange

        /// <summary>
        /// Tests that the lists within an object notify property change.
        /// </summary>
        /// <param name="type">Type with list properties.</param>
        /// <param name="addItems">Test when items added to lists.</param>
        /// <param name="removeItems">Test when items removed from lists.</param>
        public static void NotifiesListsChange(Type type, bool addItems, bool removeItems)
        {
            NotifiesListsChange(CreateINotifyPropertyChangeInstance(type), addItems, removeItems);
        }

        /// <summary>
        /// Tests that the lists within an object notify property change.
        /// </summary>
        /// <param name="type">Type with list properties.</param>
        /// <param name="propertyNames">List of list property names to test.</param>
        /// <param name="addItems">Test when items added to lists.</param>
        /// <param name="removeItems">Test when items removed from lists.</param>
        public static void NotifiesListsChange(Type type, List<string> propertyNames, bool addItems, bool removeItems)
        {
            NotifiesListsChange(CreateINotifyPropertyChangeInstance(type), propertyNames, addItems, removeItems);
        }

        /// <summary>
        /// Tests that the lists within an object notify property change.
        /// </summary>
        /// <param name="type">Type with list properties.</param>
        /// <param name="properties">List of list properties to test.</param>
        /// <param name="addItems">Test when items added to lists.</param>
        /// <param name="removeItems">Test when items removed from lists.</param>
        public static void NotifiesListsChange(Type type, List<PropertyInfo> properties, bool addItems, bool removeItems)
        {
            NotifiesListsChange(CreateINotifyPropertyChangeInstance(type), properties, addItems, removeItems);
        }

        /// <summary>
        /// Tests that the lists within an object notify property change.
        /// </summary>
        /// <typeparam name="T">Type with list properties.</typeparam>
        /// <param name="addItems">Test when items added to lists.</param>
        /// <param name="removeItems">Test when items removed from lists.</param>
        public static void NotifiesListsChange<T>(bool addItems, bool removeItems) where T : INotifyPropertyChanged
        {
            NotifiesListsChange(Activator.CreateInstance<T>(), addItems, removeItems);
        }

        /// <summary>
        /// Tests that the list within an object notify property change.
        /// </summary>
        /// <typeparam name="T">Type with list properties.</typeparam>
        /// <param name="propertyNames">List of property names to test.</param>
        /// <param name="addItems">Test when items added to lists.</param>
        /// <param name="removeItems">Test when items removed from lists.</param>
        public static void NotifiesListsChange<T>(List<string> propertyNames, bool addItems, bool removeItems) where T : INotifyPropertyChanged
        {
            NotifiesListsChange(Activator.CreateInstance<T>(), propertyNames, addItems, removeItems);
        }

        /// <summary>
        /// Tests that the lists within an object notify property change.
        /// </summary>
        /// <typeparam name="T">Type with list properties.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        /// <param name="addItems">Test when items added to lists.</param>
        /// <param name="removeItems">Test when items removed from lists.</param>
        public static void NotifiesListsChange<T>(List<PropertyInfo> properties, bool addItems, bool removeItems) where T : INotifyPropertyChanged
        {
            NotifiesListsChange(Activator.CreateInstance<T>(), properties, addItems, removeItems);
        }

        /// <summary>
        /// Tests that the lists within an object notify property change.
        /// </summary>
        /// <typeparam name="T">Type with list properties.</typeparam>
        /// <param name="obj">Object with list properties to test.</param>
        /// <param name="addItems">Test when items added to lists.</param>
        /// <param name="removeItems">Test when items removed from lists.</param>
        public static void NotifiesListsChange<T>(T obj, bool addItems, bool removeItems) where T : INotifyPropertyChanged
        {
            NotifiesListsChange(obj, PropertyUtilities.GetListOfPropertiesWithListTypes(obj, true, true, true), addItems, removeItems);
        }

        /// <summary>
        /// Tests that the lists within an object notify property change.
        /// </summary>
        /// <typeparam name="T">Type with list properties.</typeparam>
        /// <param name="obj">Object to with list properties to test.</param>
        /// <param name="propertyNames">List of properties to test.</param>
        /// <param name="addItems">Test when items added to lists.</param>
        /// <param name="removeItems">Test when items removed from lists.</param>
        public static void NotifiesListsChange<T>(T obj, List<string> propertyNames, bool addItems, bool removeItems) where T : INotifyPropertyChanged
        {
            foreach (string propertyName in propertyNames)
            {
                NotifiesListChanged(obj, propertyName, addItems, removeItems);
            }
        }

        /// <summary>
        /// Tests that the lists within an object notify property change.
        /// </summary>
        /// <typeparam name="T">Type with list properties.</typeparam>
        /// <param name="obj">Object with list properties to test.</param>
        /// <param name="properties">List of list properties to test.</param>
        /// <param name="addItems">Test when items added to lists.</param>
        /// <param name="removeItems">Test when items removed from lists.</param>
        public static void NotifiesListsChange<T>(T obj, List<PropertyInfo> properties, bool addItems, bool removeItems) where T : INotifyPropertyChanged
        {
            foreach (PropertyInfo property in properties)
            {
                NotifiesListChanged(obj, property, addItems, removeItems);
            }
        }

        /// <summary>
        /// Tests that the lists within an object notify property change.
        /// </summary>
        /// <typeparam name="T">Type with lists.</typeparam>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="listsAndPropertyNames">Pair of list and their associated property name.</param>
        /// <param name="addItems">Test when items added to lists.</param>
        /// <param name="removeItems">Test when items removed from lists.</param>
        public static void NotifiesListsChange<T>(T obj, List<(string propertyName, IList list)> listsAndPropertyNames, bool addItems, bool removeItems) where T : INotifyPropertyChanged
        {
            foreach (var (propertyName, list) in listsAndPropertyNames)
            {
                NotifiesListChanged(obj, propertyName, list, addItems, removeItems);
            }
        }

        #endregion

        #region NotifiesListChanged

        /// <summary>
        /// Tests that a list notifies when items are randomly added and removed.
        /// </summary>
        /// <param name="type">Type with list property to test.</param>
        /// <param name="propertyName">Name of property with list to test.</param>
        /// <param name="addItems">Test when items added to list.</param>
        /// <param name="removeItems">Test when items removed from list.</param>
        public static void NotifiesListChanged(Type type, string propertyName, bool addItems, bool removeItems)
        {
            NotifiesListChanged(CreateINotifyPropertyChangeInstance(type), propertyName, addItems, removeItems);
        }

        /// <summary>
        /// Tests that a list notifies when items are randomly added and removed.
        /// </summary>
        /// <param name="type">Type with list property to test.</param>
        /// <param name="property">Property with list to test.</param>
        /// <param name="addItems">Test when items added to list.</param>
        /// <param name="removeItems">Test when items removed from list.</param>
        public static void NotifiesListChanged(Type type, PropertyInfo property, bool addItems, bool removeItems)
        {
            NotifiesListChanged(CreateINotifyPropertyChangeInstance(type), property, addItems, removeItems);
        }

        /// <summary>
        /// Tests that a list notifies when items are randomly added and removed.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="propertyName">Name of property with list to test.</param>
        /// <param name="addItems">Test when items added to list.</param>
        /// <param name="removeItems">Test when items removed from list.</param>
        public static void NotifiesListChanged<T>(string propertyName, bool addItems, bool removeItems) where T : INotifyPropertyChanged
        {
            NotifiesListChanged(Activator.CreateInstance<T>(), propertyName, addItems, removeItems);
        }

        /// <summary>
        /// Tests that a list notifies when items are randomly added and removed.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="property">Property with list to test.</param>
        /// <param name="addItems">Test when items added to list.</param>
        /// <param name="removeItems">Test when items removed from list.</param>
        public static void NotifiesListChanged<T>(PropertyInfo property, bool addItems, bool removeItems) where T : INotifyPropertyChanged
        {
            NotifiesListChanged(Activator.CreateInstance<T>(), property, addItems, removeItems);
        }

        /// <summary>
        /// Tests that a list notifies when items are randomly added and removed.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="obj">Object with list property to test.</param>
        /// <param name="propertyName">Name of property with list to test.</param>
        /// <param name="addItems">Test when items added to list.</param>
        /// <param name="removeItems">Test when items removed from list.</param>
        public static void NotifiesListChanged<T>(T obj, string propertyName, bool addItems, bool removeItems) where T : INotifyPropertyChanged
        {
            NotifiesListChanged(obj, obj.GetType().GetProperty(propertyName), addItems, removeItems);
        }

        /// <summary>
        /// Tests that a list notifies when items are randomly added and removed.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="obj">Object with list property to test.</param>
        /// <param name="property">Property with list to test.</param>
        /// <param name="addItems">Test when items added to list.</param>
        /// <param name="removeItems">Test when items removed from list.</param>
        public static void NotifiesListChanged<T>(T obj, PropertyInfo property, bool addItems, bool removeItems) where T : INotifyPropertyChanged
        {
            Assert.IsTrue(typeof(IList).IsAssignableFrom(property.PropertyType));

            NotifiesListChanged(obj, property.Name, (IList)property.GetValue(obj), addItems, removeItems);
        }

        /// <summary>
        /// Tests that a list notifies when items are randomly added and removed.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="obj">Object with list property to test.</param>
        /// <param name="propertyName">Name of property with list to test.</param>
        /// <param name="list">List to test. This list should derive from <see cref="ObservableCollection{T}"/>.</param>
        /// <param name="addItems">Test when items added to list.</param>
        /// <param name="removeItems">Test when items removed from list.</param>
        public static void NotifiesListChanged<T>(T obj, string propertyName, IList list, bool addItems, bool removeItems) where T : INotifyPropertyChanged
        {
            if (addItems)
            {
                NotifiesListChangedOnItemsAdded(obj, propertyName, list);
            }

            if (removeItems)
            {
                NotifiesListChangedOnItemsRemoved(obj, propertyName, list);
            }
        }

        #endregion

        /// <summary>
        /// Tests that when items are added to a list, the object containing the list notifies change.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="obj">Object with list property to test.</param>
        /// <param name="propertyName">Name of property with list to test.</param>
        /// <param name="list">List to test. This list should derive from <see cref="ObservableCollection{T}"/>.</param>
        public static void NotifiesListChangedOnItemsAdded<T>(T obj, string propertyName, IList list) where T : INotifyPropertyChanged
        {
            PropertyNotificationAssertTracker<T> tracker = new PropertyNotificationAssertTracker<T>(obj);

            int originalListCount = list.Count;

            ObjectUtilities.AddRandomItemsToList(list);

            tracker.AssertPropertyCount(propertyName, list.Count - originalListCount);
        }

        /// <summary>
        /// Tests that a list notifies change when a random number of items are removed.
        /// </summary>
        /// <typeparam name="T">Type with list property to test.</typeparam>
        /// <param name="obj">Object that contains the list.</param>
        /// <param name="propertyName">Name of the property containing the list.</param>
        /// <param name="list">List to test. This list should derive from <see cref="ObservableCollection{T}"/>.</param>
        public static void NotifiesListChangedOnItemsRemoved<T>(T obj, string propertyName, IList list) where T : INotifyPropertyChanged
        {
            if (list.Count <= 0)
            {
                ObjectUtilities.PopulateListWithRandomValues(list);
            }

            PropertyNotificationAssertTracker<T> tracker = new PropertyNotificationAssertTracker<T>(obj);

            int originalListCount = list.Count;

            ObjectUtilities.RemoveRandomItemsFromList(list);

            tracker.AssertPropertyCount(propertyName, originalListCount - list.Count);
        }

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
