// <copyright file="NotifyPropertyChangedAssert.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Reflection;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.Asserts
{
    /// <summary>
    /// Asserts that object that implement <see cref="INotifyPropertyChanged"/> raise property change notifications.
    /// </summary>
    public static class NotifyPropertyChangedAssert
    {
        #region NotifiesPropertiesChanges

        /// <summary>
        /// Asserts that all properties within a Type raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        public static void NotifiesPropertiesChanged(this Assert assert, Type type)
        {
            NotifiesPropertiesChanged(assert, CheckForINotifyPropertyChanged(type));
        }

        /// <summary>
        /// Asserts that specified properties within an object raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesPropertiesChanged(this Assert assert, Type type, List<string> propertyNames)
        {
            NotifiesPropertiesChanged(assert, CheckForINotifyPropertyChanged(type), propertyNames);
        }

        /// <summary>
        /// Asserts that specified properties within an object raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesPropertiesChanged(this Assert assert, Type type, List<PropertyInfo> properties)
        {
            NotifiesPropertiesChanged(assert, CheckForINotifyPropertyChanged(type), properties);
        }

        /// <summary>
        /// Asserts that all properties within a Type raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        public static void NotifiesPropertiesChanged<T>(this Assert assert) where T : INotifyPropertyChanged
        {
            NotifiesPropertiesChanged(assert, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Asserts that specified properties within a type raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="propertyNames">List of properties to test.</param>
        public static void NotifiesPropertiesChanged<T>(this Assert assert, List<string> propertyNames) where T : INotifyPropertyChanged
        {
            NotifiesPropertiesChanged(assert, Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Asserts that specified properties raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesPropertiesChanged<T>(this Assert assert, List<PropertyInfo> properties) where T : INotifyPropertyChanged
        {
            NotifiesPropertiesChanged(assert, Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Asserts that all properties within an object raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object to test.</param>
        public static void NotifiesPropertiesChanged<T>(this Assert assert, T obj) where T : INotifyPropertyChanged
        {
            NotifiesPropertiesChanged(assert, obj, PropertyUtilities.GetReadWriteProperties(obj));
        }

        /// <summary>
        /// Asserts that specified properties within an object raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyNames">List of properties to test.</param>
        public static void NotifiesPropertiesChanged<T>(this Assert assert, T obj, List<string> propertyNames) where T : INotifyPropertyChanged
        {
            foreach (string propertyName in propertyNames)
            {
                NotifiesPropertyChanged(assert, obj, propertyName);
            }
        }

        /// <summary>
        /// Asserts that specific properties raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesPropertiesChanged<T>(this Assert assert, T obj, List<PropertyInfo> properties) where T : INotifyPropertyChanged
        {
            foreach (PropertyInfo property in properties)
            {
                NotifiesPropertyChanged(assert, obj, property);
            }
        }

        #endregion

        #region NotifiesPropertyChanged

        /// <summary>
        /// Asserts that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesPropertyChanged(this Assert assert, Type type, string propertyName)
        {
            NotifiesPropertyChanged(assert, CheckForINotifyPropertyChanged(type), propertyName);
        }

        /// <summary>
        /// Asserts that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesPropertyChanged(this Assert assert, Type type, PropertyInfo property)
        {
            NotifiesPropertyChanged(assert, CheckForINotifyPropertyChanged(type), property);
        }

        /// <summary>
        /// Asserts that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesPropertyChanged<T>(this Assert assert, string propertyName) where T : INotifyPropertyChanged
        {
            NotifiesPropertyChanged<T>(assert, typeof(T).GetProperty(propertyName)!);
        }

        /// <summary>
        /// Asserts that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesPropertyChanged<T>(this Assert assert, PropertyInfo property) where T : INotifyPropertyChanged
        {
            NotifiesPropertyChanged(assert, Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Asserts that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesPropertyChanged<T>(this Assert assert, T obj, string propertyName) where T : INotifyPropertyChanged
        {
            NotifiesPropertyChanged(assert, obj, typeof(T).GetProperty(propertyName)!);
        }

        /// <summary>
        /// Asserts that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesPropertyChanged<T>(this Assert assert, T obj, PropertyInfo property) where T : INotifyPropertyChanged
        {
            _ = assert;

            // create a list of property change notifications
            List<string> propertiesChanged = new();

            // add an event to add the PropertyChanged property name to the list
            obj.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName ?? string.Empty);

            // for a random number of times
            for (int i = 0; i < new Random().Next(20); i++)
            {
                // populate the property with a random value
                ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);

                // assert that the PropertyChanged raised the property name the same number of times this loop has run
                Assert.AreEqual(i + 1, propertiesChanged.Count(propertyName => propertyName == property.Name));
            }

            // check if the property is a value type
            if (PropertyUtilities.IsValueProperty(property))
            {
                // create a new value for the property
                dynamic newValue = RandomUtilities.GetRandom(property.PropertyType);

                // set the property value
                property.SetValue(obj, newValue);

                // clear the monitored property changes
                propertiesChanged.Clear();

                // change the value 20 times
                for (int j = 0; j < 20; j++)
                {
                    // set the property using the same value generated before
                    property.SetValue(obj, newValue);

                    // assert that the value has stayed the same as the new value
                    Assert.AreEqual(newValue, property.GetValue(obj));

                    // assert that the property change list hasn't raised the property changed because the value hasn't changed
                    Assert.AreEqual(0, propertiesChanged.Count);
                }
            }
        }

        #endregion

        /// <summary>
        /// Checks that a type implmenents <see cref="INotifyPropertyChanged"/> and creates a new instance of that type.
        /// </summary>
        /// <param name="type">Type to validate.</param>
        /// <returns>A new instance of the specified type that implements <see cref="INotifyPropertyChanged"/>.</returns>
        public static INotifyPropertyChanged CheckForINotifyPropertyChanged(Type type)
        {
            Assert.IsTrue(typeof(INotifyPropertyChanged).IsAssignableFrom(type));
            var obj = Activator.CreateInstance(type);

            Assert.IsNotNull(obj);

            return (INotifyPropertyChanged)obj;
        }
    }
}
