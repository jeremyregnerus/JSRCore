// <copyright file="NotifyChangeAssert.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Collections;
using System.ComponentModel;
using System.Reflection;
using JSR.BaseClasses;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.Asserts
{
    /// <summary>
    /// Tests the implementation of IChangableObject. Primarily focused on raising change events.
    /// </summary>
    public static class NotifyChangeAssert
    {
        #region NotifiesIsChangedOnAcceptChanges

        /// <summary>
        /// Tests that an object and it's class and list properties raise the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        public static void NotifiesIsChangedOnAcceptChanges(Type type)
        {
            NotifiesIsChangedOnAcceptChanges(CheckForINotifyChanged(type));
        }

        /// <summary>
        /// Tests that an object and it's class and list properties raise the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        public static void NotifiesIsChangedOnAcceptChanges<T>() where T : INotifyChanged
        {
            NotifiesIsChangedOnAcceptChanges(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that an object and it's class and list properties raise the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object to test.</param>
        public static void NotifiesIsChangedOnAcceptChanges<T>(T obj) where T : INotifyChanged
        {
            // track OnChanged events
            int count = 0;
            obj.OnChanged += (s, c) => count++;

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                // fill the object with random values
                ObjectUtilities.PopulateObjectWithRandomValues(obj);

                count = 0;

                // accept the changes on the object
                obj.AcceptChanges();

                // assert that IsChanged was only raised once
                Assert.IsTrue(count == 1);

                // accept changes another random number of times
                for (int j = 0; j < new Random().Next(5, 20); j++)
                {
                    obj.AcceptChanges();
                }

                // re-assert that IsChanged was still only raised once
                Assert.IsTrue(count == 1);
            }
        }

        #endregion

        #region IsChangedWhenHierarchyChanges

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges(Type type)
        {
            NotifiesIsChangedWhenHierarchyChanges(CheckForINotifyChanged(type));
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges(Type type, List<string> propertyNames)
        {
            NotifiesIsChangedWhenHierarchyChanges(CheckForINotifyChanged(type), propertyNames);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges(Type type, List<PropertyInfo> properties)
        {
            NotifiesIsChangedWhenHierarchyChanges(CheckForINotifyChanged(type), properties);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges(Type type, string propertyName)
        {
            NotifiesIsChangedWhenHierarchyChanges(CheckForINotifyChanged(type), propertyName);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges(Type type, PropertyInfo property)
        {
            NotifiesIsChangedWhenHierarchyChanges(CheckForINotifyChanged(type), property);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>() where T : INotifyChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(List<string> propertyNames) where T : INotifyChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(List<PropertyInfo> properties) where T : INotifyChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(string propertyName) where T : INotifyChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(Activator.CreateInstance<T>(), propertyName);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(PropertyInfo property) where T : INotifyChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(T obj) where T : INotifyChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(obj, PropertyUtilities.GetProperties(obj, new GetPropertiesOptions(true) { WriteOnlyProperties = false, InterfaceProperties = false }));
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(T obj, List<string> propertyNames) where T : INotifyChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName)!)));
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(T obj, List<PropertyInfo> properties) where T : INotifyChanged
        {
            // assert change notification on the hiearchy of this object
            foreach (PropertyInfo property in properties)
            {
                NotifiesIsChangedWhenHierarchyChanges(obj, property);
            }

            // Track OnChange events
            int count = 1;
            obj.OnChanged += (s, c) => count++;

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.AcceptChanges();
                count = 0;

                // populate the object with random values
                ObjectUtilities.PopulateObjectWithRandomValues(obj);

                // assert that the change notification was raised only once for changing all of the properties
                Assert.IsTrue(count == 1);
            }

            obj.AcceptChanges();
            count = 0;

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                // populate the object with random values multiple times
                ObjectUtilities.PopulateObjectWithRandomValues(obj);
            }

            // assert that the change notification was raised only once for changing all of the properties multiple times
            Assert.IsTrue(count == 1);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(T obj, string propertyName) where T : INotifyChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(obj, obj.GetType().GetProperty(propertyName) ?? throw new ArgumentNullException(nameof(propertyName)));
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(T obj, PropertyInfo property) where T : INotifyChanged
        {
            if (PropertyUtilities.IsReadWriteProperty(property))
            {
                NotifiesIsChangedWhenPropertyChanges(obj, property);
            }

            if (PropertyUtilities.IsClassProperty(property))
            {
                NotifiesIsChangedWhenClassPropertyChanges(obj, property);
            }

            if (PropertyUtilities.IsListProperty(property))
            {
                NotifiesIsChangedWhenListPropertyChanges(obj, property, true, true, true);
            }
        }

        #endregion

        #region NotifiesIsChangedWhenPropertiesChange

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        public static void NotifiesIsChangedWhenPropertiesChange(Type type)
        {
            NotifiesIsChangedWhenPropertiesChange(CheckForINotifyChanged(type));
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange(Type type, List<string> propertyNames)
        {
            NotifiesIsChangedWhenPropertiesChange(CheckForINotifyChanged(type), propertyNames);
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange(Type type, List<PropertyInfo> properties)
        {
            NotifiesIsChangedWhenPropertiesChange(CheckForINotifyChanged(type), properties);
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        public static void NotifiesIsChangedWhenPropertiesChange<T>() where T : INotifyChanged
        {
            NotifiesIsChangedWhenPropertiesChange(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(List<string> propertyNames) where T : INotifyChanged
        {
            NotifiesIsChangedWhenPropertiesChange(Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(List<PropertyInfo> properties) where T : INotifyChanged
        {
            NotifiesIsChangedWhenPropertiesChange(Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(T obj) where T : INotifyChanged
        {
            NotifiesIsChangedWhenPropertiesChange(obj, PropertyUtilities.GetReadWriteProperties(obj));
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(T obj, List<string> propertyNames) where T : INotifyChanged
        {
            NotifiesIsChangedWhenPropertiesChange(obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName)!)));
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(T obj, List<PropertyInfo> properties) where T : INotifyChanged
        {
            // perform NotifiesIsChangedWhenPropertyChanges for each property provided
            foreach (PropertyInfo property in properties)
            {
                NotifiesIsChangedWhenPropertyChanges(obj, property);
            }

            // Track the OnChanged events
            int count = 0;
            obj.OnChanged += (s, c) => count++;

            // for a random number of time
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.AcceptChanges();
                count = 0;

                // for each property that is readwrite
                foreach (PropertyInfo property in properties.Where(property => PropertyUtilities.IsReadWriteProperty(property)))
                {
                    // populate the property with a new value
                    ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);
                }

                // assert that OnChanged was raised only once regardless of the number of properties changed
                Assert.IsTrue(count == 1);
            }

            obj.AcceptChanges();
            count = 0;

            // for a random number of times populate all the properties of the object
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (PropertyInfo property in properties.Where(property => PropertyUtilities.IsReadWriteProperty(property)))
                {
                    ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);
                }
            }

            // assert that OnChanges was called only once regardless of the number of times all properties were changed
            Assert.IsTrue(count == 1);
        }

        #endregion

        #region NotifiesIsChangedWhenPropertyChanges

        /// <summary>
        /// Tests that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges(Type type, string propertyName)
        {
            NotifiesIsChangedWhenPropertyChanges(CheckForINotifyChanged(type), propertyName);
        }

        /// <summary>
        /// Tests that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges(Type type, PropertyInfo property)
        {
            NotifiesIsChangedWhenPropertyChanges(CheckForINotifyChanged(type), property);
        }

        /// <summary>
        /// Tests that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(string propertyName) where T : INotifyChanged
        {
            NotifiesIsChangedWhenPropertyChanges(Activator.CreateInstance<T>(), propertyName);
        }

        /// <summary>
        /// Tests that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(PropertyInfo property) where T : INotifyChanged
        {
            NotifiesIsChangedWhenPropertyChanges(Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Tests that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(T obj, string propertyName) where T : INotifyChanged
        {
            NotifiesIsChangedWhenPropertyChanges(obj, obj.GetType().GetProperty(propertyName) ?? throw new ArgumentNullException(nameof(propertyName)));
        }

        /// <summary>
        /// Tests that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/> with property to test.</typeparam>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(T obj, PropertyInfo property) where T : INotifyChanged
        {
            // only run this test if the property is ReadWrite
            if (PropertyUtilities.IsReadWriteProperty(property))
            {
                // Track OnChanged events
                int count = 0;
                obj.OnChanged += (s, c) => count++;

                for (int i = 0; i < new Random().Next(5, 20); i++)
                {
                    obj.AcceptChanges();
                    count = 0;

                    // populate the property with a random value
                    ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);

                    // assert that OnChanged was raised once
                    Assert.IsTrue(count == 1);
                }

                obj.AcceptChanges();
                count = 0;

                for (int i = 0; i < new Random().Next(5, 20); i++)
                {
                    // populate the property with different values multiple times
                    ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);
                }

                // assert that OnChanged was raised only once, regardless of how many times the property was changed
                Assert.IsTrue(count == 1);
            }
        }

        #endregion

        #region NotifiesIsChangedWhenClassPropertiesChange

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange(Type type)
        {
            NotifiesIsChangedWhenClassPropertiesChange(CheckForINotifyChanged(type));
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="propertyNames">List of property Names to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange(Type type, List<string> propertyNames)
        {
            NotifiesIsChangedWhenClassPropertiesChange(CheckForINotifyChanged(type), propertyNames);
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange(Type type, List<PropertyInfo> properties)
        {
            NotifiesIsChangedWhenClassPropertiesChange(CheckForINotifyChanged(type), properties);
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        public static void NotifiesIsChangedWhenClassPropertiesChange<T>() where T : INotifyChanged
        {
            NotifiesIsChangedWhenClassPropertiesChange(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="propertyNames">List of property Names to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange<T>(List<string> propertyNames) where T : INotifyChanged
        {
            NotifiesIsChangedWhenClassPropertiesChange(Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange<T>(List<PropertyInfo> properties) where T : INotifyChanged
        {
            NotifiesIsChangedWhenClassPropertiesChange(Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with child objects to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange<T>(T obj) where T : INotifyChanged
        {
            NotifiesIsChangedWhenClassPropertiesChange(obj, PropertyUtilities.GetClassProperties(obj, new GetPropertiesOptions(true) { WriteOnlyProperties = false }));
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with child objects to test.</param>
        /// <param name="propertyNames">List of property Names to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange<T>(T obj, List<string> propertyNames) where T : INotifyChanged
        {
            NotifiesIsChangedWhenClassPropertiesChange(obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName)!)));
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with child objects to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange<T>(T obj, List<PropertyInfo> properties) where T : INotifyChanged
        {
            // for each property in the object, perform this same assertion
            foreach (PropertyInfo property in properties)
            {
                NotifiesIsChangedWhenClassPropertyChanges(obj, property);
            }

            // Track of the OnChanged events
            int count = 0;
            obj.OnChanged += (s, c) => count++;

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.AcceptChanges();
                count = 0;

                // for each class, populate it with random values
                foreach (PropertyInfo property in properties)
                {
                    ObjectUtilities.PopulateObjectWithRandomValues(property.GetValue(obj));
                }

                // assert that OnChanged was raised only once
                Assert.IsTrue(count == 1);
            }

            obj.AcceptChanges();
            count = 0;

            // populate the properties multiple times
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (PropertyInfo property in properties)
                {
                    ObjectUtilities.PopulateObjectWithRandomValues(property.GetValue(obj));
                }
            }

            // assert that OnChanged was only raised once
            Assert.IsTrue(count == 1);
        }

        #endregion

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <typeparam name="TList">Type that implements <see cref="IList"/>.</typeparam>
        /// <param name="obj">Object with child objects to test.</param>
        /// <param name="children">Child objects to change.</param>
        public static void NotifiesIsChangedWhenChildClassesChange<TParent, TList>(TParent obj, TList children) where TParent : INotifyChanged where TList : IList
        {
            // perform this assertion on any child classes
            foreach (INotifyChanged child in children)
            {
                NotifiesIsChangedWhenChildClassChanges(obj, child);
            }

            // Track OnChanged notifications
            int count = 0;
            obj.OnChanged += (s, c) => count++;

            obj.AcceptChanges();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.AcceptChanges();
                count = 0;

                // change the properties of each child object
                foreach (var child in children)
                {
                    ObjectUtilities.PopulateObjectWithRandomValues(child);
                }

                // assert that OnChange was only raised once
                Assert.IsTrue(count == 1);
            }

            obj.AcceptChanges();
            count = 0;

            // change the properties of all child classes multiple times
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (var child in children)
                {
                    ObjectUtilities.PopulateObjectWithRandomValues(child);
                }
            }

            // assert that OnChange was raised only once
            Assert.IsTrue(count == 1);
        }

        #region NotifiesIsChangedWhenClassPropertyChanges

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenClassPropertyChanges(Type type, string propertyName)
        {
            NotifiesIsChangedWhenClassPropertyChanges(CheckForINotifyChanged(type), propertyName);
        }

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenClassPropertyChanges(Type type, PropertyInfo property)
        {
            NotifiesIsChangedWhenClassPropertyChanges(CheckForINotifyChanged(type), property);
        }

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenClassPropertyChanges<T>(string propertyName) where T : INotifyChanged
        {
            NotifiesIsChangedWhenClassPropertyChanges(Activator.CreateInstance<T>(), propertyName);
        }

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenClassPropertyChanges<T>(PropertyInfo property) where T : INotifyChanged
        {
            NotifiesIsChangedWhenClassPropertyChanges(Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with child to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenClassPropertyChanges<T>(T obj, string propertyName) where T : INotifyChanged
        {
            NotifiesIsChangedWhenClassPropertyChanges(obj, obj.GetType().GetProperty(propertyName) ?? throw new ArgumentNullException(nameof(propertyName)));
        }

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with child to test.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenClassPropertyChanges<T>(T obj, PropertyInfo property) where T : INotifyChanged
        {
            NotifiesIsChangedWhenChildClassChanges(obj, property.GetValue(obj));
        }

        #endregion

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Type of the parent object that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <typeparam name="TChild">Type of the child object.</typeparam>
        /// <param name="parentObj">Object with child to test.</param>
        /// <param name="childObj">Child object to change.</param>
        public static void NotifiesIsChangedWhenChildClassChanges<TParent, TChild>(TParent parentObj, TChild childObj) where TParent : INotifyChanged
        {
            // Track OnChange notifications on the parent object
            int count = 0;
            parentObj.OnChanged += (s, c) => count++;

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                parentObj.AcceptChanges();
                count = 0;

                // populate the child object with random values
                ObjectUtilities.PopulateObjectWithRandomValues(childObj);

                // assert that the OnChange notification was only raised once
                Assert.IsTrue(count == 1);
            }

            parentObj.AcceptChanges();
            count = 0;

            // populate the child object with random values multiple times
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(childObj);
            }

            // assert that the OnChange notification was only raised once
            Assert.IsTrue(count == 1);
        }

        #region NotifiesIsChangedWhenListPropertiesChange

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange(Type type, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListPropertiesChange(CheckForINotifyChanged(type), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange(Type type, List<string> propertyNames, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListPropertiesChange(CheckForINotifyChanged(type), propertyNames, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange(Type type, List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListPropertiesChange(CheckForINotifyChanged(type), properties, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange<T>(bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            NotifiesIsChangedWhenListPropertiesChange(Activator.CreateInstance<T>(), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="propertyNames">List of property names to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange<T>(List<string> propertyNames, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            NotifiesIsChangedWhenListPropertiesChange(Activator.CreateInstance<T>(), propertyNames, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange<T>(List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            NotifiesIsChangedWhenListPropertiesChange(Activator.CreateInstance<T>(), properties, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange<T>(T obj, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            NotifiesIsChangedWhenListPropertiesChange(obj, PropertyUtilities.GetListProperties(obj, new GetPropertiesOptions(true) { WriteOnlyProperties = false }), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange<T>(T obj, List<string> propertyNames, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            NotifiesIsChangedWhenListPropertiesChange(obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName)!)), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="properties">List of properties to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange<T>(T obj, List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            // perform this assertion for each property
            foreach (PropertyInfo property in properties)
            {
                NotifiesIsChangedWhenListPropertyChanges(obj, property, addItems, removeItems, changeItems);
            }

            // create counter to monitor OnChanged events
            int count = 0;
            obj.OnChanged += (s, c) => count++;

            obj.AcceptChanges();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.AcceptChanges();
                count = 0;

                foreach (PropertyInfo property in properties.Where(property => typeof(IList).IsAssignableFrom(property.PropertyType)))
                {
                    ObjectUtilities.PopulateListWithRandomValues((IList)property.GetValue(obj)!, obj.GetType());
                }

                // assert that no more than 1 notification was raised
                Assert.IsTrue(count <= 1);
            }

            obj.AcceptChanges();
            count = 0;

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (PropertyInfo property in properties.Where(property => typeof(IList).IsAssignableFrom(property.PropertyType)))
                {
                    ObjectUtilities.PopulateListWithRandomValues((IList)property.GetValue(obj)!, obj.GetType());
                }
            }

            Assert.IsTrue(count <= 1);
        }

        #endregion

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Parent type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <typeparam name="TList">List type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="lists">List of lists to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListsChange<TParent, TList>(TParent obj, List<TList> lists, bool addItems, bool removeItems, bool changeItems) where TParent : INotifyChanged where TList : IList
        {
            foreach (TList list in lists)
            {
                NotifiesIsChangedWhenListChanges(obj, list, addItems, removeItems, changeItems);
            }

            int count = 0;
            obj.OnChanged += (s, c) => count++;

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.AcceptChanges();
                count = 0;

                foreach (IList list in lists)
                {
                    ObjectUtilities.PopulateListWithRandomValues(list);
                }

                Assert.IsTrue(count == 1);
            }

            obj.AcceptChanges();
            count = 0;

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (IList list in lists)
                {
                    ObjectUtilities.PopulateListWithRandomValues(list);
                }
            }

            Assert.IsTrue(count == 1);
        }

        #region NotifiesIsChangedWhenListPropertyChanges

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertyChanges(Type type, string propertyName, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListPropertyChanges(CheckForINotifyChanged(type), propertyName, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="property">Property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertyChanges(Type type, PropertyInfo property, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListPropertyChanges(CheckForINotifyChanged(type), property, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertyChanges<T>(string propertyName, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            NotifiesIsChangedWhenListPropertyChanges<T>(typeof(T).GetProperty(propertyName) ?? throw new ArgumentNullException(nameof(propertyName)), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="property">Property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertyChanges<T>(PropertyInfo property, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            NotifiesIsChangedWhenListPropertyChanges(Activator.CreateInstance<T>(), property, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with list to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertyChanges<T>(T obj, string propertyName, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            NotifiesIsChangedWhenListPropertyChanges(obj, obj.GetType().GetProperty(propertyName) ?? throw new ArgumentNullException(nameof(propertyName)), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with list to test.</param>
        /// <param name="property">Property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertyChanges<T>(T obj, PropertyInfo property, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            if (typeof(IList).IsAssignableFrom(property.PropertyType))
            {
                NotifiesIsChangedWhenListChanges(obj, (IList)property.GetValue(obj)!, addItems, removeItems, changeItems);
            }
        }

        #endregion

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Parent type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <typeparam name="TList">List type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object with list to test.</param>
        /// <param name="list">List to change.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListChanges<TParent, TList>(TParent obj, TList list, bool addItems, bool removeItems, bool changeItems) where TParent : INotifyChanged where TList : IList
        {
            if (addItems)
            {
                NotifiesIsChangedWhenListItemsAdded(obj, list);
            }

            if (removeItems)
            {
                NotifiesIsChangedWhenListItemsRemoved(obj, list);
            }

            if (changeItems)
            {
                NotifiesIsChangedWhenListItemsChange(obj, list);
            }
        }

        /// <summary>
        /// Tests that as items are added to a list, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Parent type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <typeparam name="TList">List type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object to test.</param>
        /// <param name="list">List to add items to.</param>
        public static void NotifiesIsChangedWhenListItemsAdded<TParent, TList>(TParent obj, TList list) where TParent : INotifyChanged where TList : IList
        {
            // get the type of items in the list
            Type listItemType = list.GetType().GenericTypeArguments[0];

            // determine if the items in the list are IChangeTracking
            bool listItemsAreIChangeTracking = typeof(IChangeTracking).IsAssignableFrom(listItemType);

            // track OnChange events
            int count = 0;
            obj.OnChanged += (s, c) => count++;

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.AcceptChanges();
                count = 0;

                dynamic item = ObjectUtilities.CreateInstanceWithRandomValues(listItemType);

                if (listItemsAreIChangeTracking)
                {
                    ((IChangeTracking)item).AcceptChanges();
                }

                list.Add(item);

                Assert.IsTrue(count == 1);
            }

            obj.AcceptChanges();
            count = 0;

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                dynamic item = ObjectUtilities.CreateInstanceWithRandomValues(listItemType);

                if (listItemsAreIChangeTracking)
                {
                    ((IChangeTracking)item).AcceptChanges();
                }

                list.Add(item);
            }

            Assert.IsTrue(count == 1);
        }

        /// <summary>
        /// Tests that as items are removed from a list, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Parent type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <typeparam name="TList">List type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object to test.</param>
        /// <param name="list">List to remove items from.</param>
        public static void NotifiesIsChangedWhenListItemsRemoved<TParent, TList>(TParent obj, TList list) where TParent : INotifyChanged where TList : IList
        {
            if (list.Count == 0)
            {
                ObjectUtilities.PopulateListWithRandomValues(list);
            }

            int count = 0;
            obj.OnChanged += (s, c) => count++;

            while (list.Count > 0)
            {
                obj.AcceptChanges();
                count = 0;

                list.RemoveAt(list.Count - 1);

                Assert.IsTrue(count == 1);
            }

            ObjectUtilities.PopulateListWithRandomValues(list);
            obj.AcceptChanges();
            count = 0;

            while (list.Count > 0)
            {
                list.RemoveAt(list.Count - 1);
            }

            Assert.IsTrue(count == 1);

            ObjectUtilities.PopulateListWithRandomValues(list);

            obj.AcceptChanges();
            count = 0;

            list.Clear();

            Assert.IsTrue(count == 1);
        }

        /// <summary>
        /// Tests that as items in a list change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Parent type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <typeparam name="TList">List type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="obj">Object to test.</param>
        /// <param name="list">List with items to change.</param>
        public static void NotifiesIsChangedWhenListItemsChange<TParent, TList>(TParent obj, TList list) where TParent : INotifyChanged where TList : IList
        {
            // if the list doesn't contain any items
            if (list.Count == 0)
            {
                // populate it with items
                ObjectUtilities.PopulateListWithRandomValues(list);
            }

            // Track OnChange events
            int count = 0;
            obj.OnChanged += (s, c) => count++;

            foreach (var item in list)
            {
                obj.AcceptChanges();
                count = 0;

                ObjectUtilities.PopulateObjectWithRandomValues(item);

                Assert.IsTrue(count == 1);
            }

            obj.AcceptChanges();
            count = 0;

            foreach (var item in list)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(item);
            }

            Assert.IsTrue(count == 1);
        }

        /// <summary>
        /// Checks that the type implements <see cref="INotifyChanged"/> and creates a new instance of that type.
        /// </summary>
        /// <param name="type">Type to test and create.</param>
        /// <returns>A new instance of the type specified that implements <see cref="IChangeTracking"/>.</returns>
        private static INotifyChanged CheckForINotifyChanged(Type type)
        {
            Assert.IsTrue(typeof(INotifyChanged).IsAssignableFrom(type));
            return (INotifyChanged)Activator.CreateInstance(type);
        }
    }
}
