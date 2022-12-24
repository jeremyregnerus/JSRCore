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
    /// Tests objects that object that implement <see cref="INotifyPropertyChanged"/> raise property change notifications.
    /// </summary>
    public static class NotifyPropertyChangedAssert
    {
        #region NotifiesPropertiesChanges

        /// <summary>
        /// Tests that all properties within a Type raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        public static void NotifiesPropertiesChanged(Type type)
        {
            NotifiesPropertiesChanged(CheckForINotifyPropertyChanged(type));
        }

        /// <summary>
        /// Tests that specified properties within an object raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesPropertiesChanged(Type type, List<string> propertyNames)
        {
            NotifiesPropertiesChanged(CheckForINotifyPropertyChanged(type), propertyNames);
        }

        /// <summary>
        /// Tests that specified properties within an object raise the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesPropertiesChanged(Type type, List<PropertyInfo> properties)
        {
            NotifiesPropertiesChanged(CheckForINotifyPropertyChanged(type), properties);
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
            NotifiesPropertiesChanged(obj, PropertyUtilities.GetReadWriteProperties(obj));
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
            NotifiesPropertyChanged(CheckForINotifyPropertyChanged(type), propertyName);
        }

        /// <summary>
        /// Tests that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyPropertyChanged"/>.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesPropertyChanged(Type type, PropertyInfo property)
        {
            NotifiesPropertyChanged(CheckForINotifyPropertyChanged(type), property);
        }

        /// <summary>
        /// Tests that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesPropertyChanged<T>(string propertyName) where T : INotifyPropertyChanged
        {
            NotifiesPropertyChanged<T>(typeof(T).GetProperty(propertyName)!);
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
            NotifiesPropertyChanged(obj, typeof(T).GetProperty(propertyName) ?? throw new ArgumentNullException(nameof(propertyName)));
        }

        /// <summary>
        /// Tests that a property raises the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyPropertyChanged"/>.</typeparam>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesPropertyChanged<T>(T obj, PropertyInfo property) where T : INotifyPropertyChanged
        {
            // create a list of property change notifications
            List<string> propertiesChanged = new();

            // add an event to add the PropertyChanged property name to the list
            obj.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            // for a random number of times
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                // populate the property with a random value
                ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);

                // assert that the PropertyChanged raised the property name the same number of times this loop has run
                Assert.AreEqual(i + 1, propertiesChanged.Count(propertyName => propertyName == property.Name));
            }

            // check if the property is a value type
            if (PropertyUtilities.IsValueProperty(property))
            {
                // for a random number of times
                for (int i = 0; i < new Random().Next(5, 20); i++)
                {
                    // create a new value for the property
                    dynamic newValue = RandomUtilities.GetRandom(property.PropertyType);

                    // set the property value
                    property.SetValue(obj, newValue);

                    // clear the monitored property changes
                    propertiesChanged.Clear();

                    // for another random number of times
                    for (int j = 0; j < new Random().Next(5, 20); j++)
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
            return (INotifyPropertyChanged)Activator.CreateInstance(type);
        }
    }
}
