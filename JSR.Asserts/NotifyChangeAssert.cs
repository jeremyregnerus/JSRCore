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
    /// Asserts the implementation of IChangableObject. Primarily focused on raising change events.
    /// </summary>
    public static class NotifyChangeAssert
    {
        #region NotifiesIsChangedOnAcceptChanges

        /// <summary>
        /// Asserts that an object and it's class and list properties raise the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        public static void NotifiesIsChangedOnAcceptChanges(this Assert assert, Type type)
        {
            NotifiesIsChangedOnAcceptChanges(assert, CheckForINotifyChanged(type));
        }

        /// <summary>
        /// Asserts that an object and it's class and list properties raise the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        public static void NotifiesIsChangedOnAcceptChanges<T>(this Assert assert) where T : INotifyChanged
        {
            NotifiesIsChangedOnAcceptChanges(assert, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Asserts that an object and it's class and list properties raise the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object to test.</param>
        public static void NotifiesIsChangedOnAcceptChanges<T>(this Assert assert, T obj) where T : INotifyChanged
        {
            _ = assert;

            // track OnChanged events
            int count = 0;
            obj.OnChanged += (s, c) => count++;

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                // fill the object with random values
                ObjectUtilities.PopulateObjectWithRandomValues(obj);

                // reset the count
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
        /// Asserts that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges(this Assert assert, Type type)
        {
            NotifiesIsChangedWhenHierarchyChanges(assert, CheckForINotifyChanged(type));
        }

        /// <summary>
        /// Asserts that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges(this Assert assert, Type type, List<string> propertyNames)
        {
            NotifiesIsChangedWhenHierarchyChanges(assert, CheckForINotifyChanged(type), propertyNames);
        }

        /// <summary>
        /// Asserts that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges(this Assert assert, Type type, List<PropertyInfo> properties)
        {
            NotifiesIsChangedWhenHierarchyChanges(assert, CheckForINotifyChanged(type), properties);
        }

        /// <summary>
        /// Asserts that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges(this Assert assert, Type type, string propertyName)
        {
            NotifiesIsChangedWhenHierarchyChanges(assert, CheckForINotifyChanged(type), propertyName);
        }

        /// <summary>
        /// Asserts that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges(this Assert assert, Type type, PropertyInfo property)
        {
            NotifiesIsChangedWhenHierarchyChanges(assert, CheckForINotifyChanged(type), property);
        }

        /// <summary>
        /// Asserts that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(this Assert assert) where T : INotifyChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(assert, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Asserts that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(this Assert assert, List<string> propertyNames) where T : INotifyChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(assert, Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Asserts that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(this Assert assert, List<PropertyInfo> properties) where T : INotifyChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(assert, Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Asserts that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(this Assert assert, string propertyName) where T : INotifyChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(assert, Activator.CreateInstance<T>(), propertyName);
        }

        /// <summary>
        /// Asserts that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(this Assert assert, PropertyInfo property) where T : INotifyChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(assert, Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Asserts that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with properties to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(this Assert assert, T obj) where T : INotifyChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(assert, obj, PropertyUtilities.GetProperties(obj, new GetPropertiesOptions(true) { WriteOnlyProperties = false, InterfaceProperties = false }));
        }

        /// <summary>
        /// Asserts that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(this Assert assert, T obj, List<string> propertyNames) where T : INotifyChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(assert, obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName)!)));
        }

        /// <summary>
        /// Asserts that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(this Assert assert, T obj, List<PropertyInfo> properties) where T : INotifyChanged
        {
            // assert change notification on the hiearchy of this object
            foreach (PropertyInfo property in properties)
            {
                NotifiesIsChangedWhenHierarchyChanges(assert, obj, property);
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
        /// Asserts that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(this Assert assert, T obj, string propertyName) where T : INotifyChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(assert, obj, obj.GetType().GetProperty(propertyName)!);
        }

        /// <summary>
        /// Asserts that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(this Assert assert, T obj, PropertyInfo property) where T : INotifyChanged
        {
            if (PropertyUtilities.IsReadWriteProperty(property))
            {
                NotifiesIsChangedWhenPropertyChanges(assert, obj, property);
            }

            if (PropertyUtilities.IsClassProperty(property))
            {
                NotifiesIsChangedWhenClassPropertyChanges(assert, obj, property);
            }

            if (PropertyUtilities.IsListProperty(property))
            {
                NotifiesIsChangedWhenListPropertyChanges(assert, obj, property, true, true, true);
            }
        }

        #endregion

        #region NotifiesIsChangedWhenPropertiesChange

        /// <summary>
        /// Asserts that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        public static void NotifiesIsChangedWhenPropertiesChange(this Assert assert, Type type)
        {
            NotifiesIsChangedWhenPropertiesChange(assert, CheckForINotifyChanged(type));
        }

        /// <summary>
        /// Asserts that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange(this Assert assert, Type type, List<string> propertyNames)
        {
            NotifiesIsChangedWhenPropertiesChange(assert, CheckForINotifyChanged(type), propertyNames);
        }

        /// <summary>
        /// Asserts that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange(this Assert assert, Type type, List<PropertyInfo> properties)
        {
            NotifiesIsChangedWhenPropertiesChange(assert, CheckForINotifyChanged(type), properties);
        }

        /// <summary>
        /// Asserts that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(this Assert assert) where T : INotifyChanged
        {
            NotifiesIsChangedWhenPropertiesChange(assert, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Asserts that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(this Assert assert, List<string> propertyNames) where T : INotifyChanged
        {
            NotifiesIsChangedWhenPropertiesChange(assert, Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Asserts that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(this Assert assert, List<PropertyInfo> properties) where T : INotifyChanged
        {
            NotifiesIsChangedWhenPropertiesChange(assert, Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Asserts that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with properties to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(this Assert assert, T obj) where T : INotifyChanged
        {
            NotifiesIsChangedWhenPropertiesChange(assert, obj, PropertyUtilities.GetReadWriteProperties(obj));
        }

        /// <summary>
        /// Asserts that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(this Assert assert, T obj, List<string> propertyNames) where T : INotifyChanged
        {
            NotifiesIsChangedWhenPropertiesChange(assert, obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName)!)));
        }

        /// <summary>
        /// Asserts that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(this Assert assert, T obj, List<PropertyInfo> properties) where T : INotifyChanged
        {
            // perform NotifiesIsChangedWhenPropertyChanges for each property provided
            foreach (PropertyInfo property in properties)
            {
                NotifiesIsChangedWhenPropertyChanges(assert, obj, property);
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
        /// Asserts that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges(this Assert assert, Type type, string propertyName)
        {
            NotifiesIsChangedWhenPropertyChanges(assert, CheckForINotifyChanged(type), propertyName);
        }

        /// <summary>
        /// Asserts that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges(this Assert assert, Type type, PropertyInfo property)
        {
            NotifiesIsChangedWhenPropertyChanges(assert, CheckForINotifyChanged(type), property);
        }

        /// <summary>
        /// Asserts that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(this Assert assert, string propertyName) where T : INotifyChanged
        {
            NotifiesIsChangedWhenPropertyChanges(assert, Activator.CreateInstance<T>(), propertyName);
        }

        /// <summary>
        /// Asserts that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(this Assert assert, PropertyInfo property) where T : INotifyChanged
        {
            NotifiesIsChangedWhenPropertyChanges(assert, Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Asserts that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(this Assert assert, T obj, string propertyName) where T : INotifyChanged
        {
            NotifiesIsChangedWhenPropertyChanges(assert, obj, obj.GetType().GetProperty(propertyName)!);
        }

        /// <summary>
        /// Asserts that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/> with property to test.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(this Assert assert, T obj, PropertyInfo property) where T : INotifyChanged
        {
            _ = assert;

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
        /// Asserts that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange(this Assert assert, Type type)
        {
            NotifiesIsChangedWhenClassPropertiesChange(assert, CheckForINotifyChanged(type));
        }

        /// <summary>
        /// Asserts that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="propertyNames">List of property Names to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange(this Assert assert, Type type, List<string> propertyNames)
        {
            NotifiesIsChangedWhenClassPropertiesChange(assert, CheckForINotifyChanged(type), propertyNames);
        }

        /// <summary>
        /// Asserts that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange(this Assert assert, Type type, List<PropertyInfo> properties)
        {
            NotifiesIsChangedWhenClassPropertiesChange(assert, CheckForINotifyChanged(type), properties);
        }

        /// <summary>
        /// Asserts that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange<T>(this Assert assert) where T : INotifyChanged
        {
            NotifiesIsChangedWhenClassPropertiesChange(assert, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Asserts that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="propertyNames">List of property Names to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange<T>(this Assert assert, List<string> propertyNames) where T : INotifyChanged
        {
            NotifiesIsChangedWhenClassPropertiesChange(assert, Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Asserts that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange<T>(this Assert assert, List<PropertyInfo> properties) where T : INotifyChanged
        {
            NotifiesIsChangedWhenClassPropertiesChange(assert, Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Asserts that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with child objects to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange<T>(this Assert assert, T obj) where T : INotifyChanged
        {
            NotifiesIsChangedWhenClassPropertiesChange(assert, obj, PropertyUtilities.GetClassProperties(obj, new GetPropertiesOptions(true) { WriteOnlyProperties = false }));
        }

        /// <summary>
        /// Asserts that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with child objects to test.</param>
        /// <param name="propertyNames">List of property Names to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange<T>(this Assert assert, T obj, List<string> propertyNames) where T : INotifyChanged
        {
            NotifiesIsChangedWhenClassPropertiesChange(assert, obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName)!)));
        }

        /// <summary>
        /// Asserts that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with child objects to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange<T>(this Assert assert, T obj, List<PropertyInfo> properties) where T : INotifyChanged
        {
            // for each property in the object, perform this same assertion
            foreach (PropertyInfo property in properties)
            {
                NotifiesIsChangedWhenClassPropertyChanges(assert, obj, property);
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
        /// Asserts that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <typeparam name="TList">Type that implements <see cref="IList"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with child objects to test.</param>
        /// <param name="children">Child objects to change.</param>
        public static void NotifiesIsChangedWhenChildClassesChange<TParent, TList>(this Assert assert, TParent obj, TList children) where TParent : INotifyChanged where TList : IList
        {
            // perform this assertion on any child classes
            foreach (INotifyChanged child in children)
            {
                NotifiesIsChangedWhenChildClassChanges(assert, obj, child);
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
        /// Asserts that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenClassPropertyChanges(this Assert assert, Type type, string propertyName)
        {
            NotifiesIsChangedWhenClassPropertyChanges(assert, CheckForINotifyChanged(type), propertyName);
        }

        /// <summary>
        /// Asserts that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenClassPropertyChanges(this Assert assert, Type type, PropertyInfo property)
        {
            NotifiesIsChangedWhenClassPropertyChanges(assert, CheckForINotifyChanged(type), property);
        }

        /// <summary>
        /// Asserts that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenClassPropertyChanges<T>(this Assert assert, string propertyName) where T : INotifyChanged
        {
            NotifiesIsChangedWhenClassPropertyChanges(assert, Activator.CreateInstance<T>(), propertyName);
        }

        /// <summary>
        /// Asserts that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenClassPropertyChanges<T>(this Assert assert, PropertyInfo property) where T : INotifyChanged
        {
            NotifiesIsChangedWhenClassPropertyChanges(assert, Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Asserts that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with child to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenClassPropertyChanges<T>(this Assert assert, T obj, string propertyName) where T : INotifyChanged
        {
            NotifiesIsChangedWhenClassPropertyChanges(assert, obj, obj.GetType().GetProperty(propertyName)!);
        }

        /// <summary>
        /// Asserts that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with child to test.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenClassPropertyChanges<T>(this Assert assert, T obj, PropertyInfo property) where T : INotifyChanged
        {
            NotifiesIsChangedWhenChildClassChanges(assert, obj, property.GetValue(obj));
        }

        #endregion

        /// <summary>
        /// Asserts that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Type of the parent object that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <typeparam name="TChild">Type of the child object.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="parentObj">Object with child to test.</param>
        /// <param name="childObj">Child object to change.</param>
        public static void NotifiesIsChangedWhenChildClassChanges<TParent, TChild>(this Assert assert, TParent parentObj, TChild childObj) where TParent : INotifyChanged
        {
            _ = assert;

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
        /// Asserts that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange(this Assert assert, Type type, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListPropertiesChange(assert, CheckForINotifyChanged(type), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange(this Assert assert, Type type, List<string> propertyNames, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListPropertiesChange(assert, CheckForINotifyChanged(type), propertyNames, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange(this Assert assert, Type type, List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListPropertiesChange(assert, CheckForINotifyChanged(type), properties, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange<T>(this Assert assert, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            NotifiesIsChangedWhenListPropertiesChange(assert, Activator.CreateInstance<T>(), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange<T>(this Assert assert, List<string> propertyNames, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            NotifiesIsChangedWhenListPropertiesChange(assert, Activator.CreateInstance<T>(), propertyNames, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="properties">List of properties to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange<T>(this Assert assert, List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            NotifiesIsChangedWhenListPropertiesChange(assert, Activator.CreateInstance<T>(), properties, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange<T>(this Assert assert, T obj, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            NotifiesIsChangedWhenListPropertiesChange(assert, obj, PropertyUtilities.GetListProperties(obj, new GetPropertiesOptions(true) { WriteOnlyProperties = false }), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange<T>(this Assert assert, T obj, List<string> propertyNames, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            NotifiesIsChangedWhenListPropertiesChange(assert, obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName)!)), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="properties">List of properties to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange<T>(this Assert assert, T obj, List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            // perform this assertion for each property
            foreach (PropertyInfo property in properties)
            {
                NotifiesIsChangedWhenListPropertyChanges(assert, obj, property, addItems, removeItems, changeItems);
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
        /// Asserts that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Parent type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <typeparam name="TList">List type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="lists">List of lists to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListsChange<TParent, TList>(this Assert assert, TParent obj, List<TList> lists, bool addItems, bool removeItems, bool changeItems) where TParent : INotifyChanged where TList : IList
        {
            foreach (TList list in lists)
            {
                NotifiesIsChangedWhenListChanges(assert, obj, list, addItems, removeItems, changeItems);
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
        /// Asserts that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertyChanges(this Assert assert, Type type, string propertyName, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListPropertyChanges(assert, CheckForINotifyChanged(type), propertyName, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="INotifyChanged"/>.</param>
        /// <param name="property">Property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertyChanges(this Assert assert, Type type, PropertyInfo property, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListPropertyChanges(assert, CheckForINotifyChanged(type), property, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="propertyName">Name of property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertyChanges<T>(this Assert assert, string propertyName, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            NotifiesIsChangedWhenListPropertyChanges<T>(assert, typeof(T).GetProperty(propertyName)!, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="property">Property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertyChanges<T>(this Assert assert, PropertyInfo property, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            NotifiesIsChangedWhenListPropertyChanges(assert, Activator.CreateInstance<T>(), property, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with list to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertyChanges<T>(this Assert assert, T obj, string propertyName, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            NotifiesIsChangedWhenListPropertyChanges(assert, obj, obj.GetType().GetProperty(propertyName)!, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with list to test.</param>
        /// <param name="property">Property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertyChanges<T>(this Assert assert, T obj, PropertyInfo property, bool addItems, bool removeItems, bool changeItems) where T : INotifyChanged
        {
            if (typeof(IList).IsAssignableFrom(property.PropertyType))
            {
                NotifiesIsChangedWhenListChanges(assert, obj, (IList)property.GetValue(obj)!, addItems, removeItems, changeItems);
            }
        }

        #endregion

        /// <summary>
        /// Asserts that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Parent type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <typeparam name="TList">List type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with list to test.</param>
        /// <param name="list">List to change.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListChanges<TParent, TList>(this Assert assert, TParent obj, TList list, bool addItems, bool removeItems, bool changeItems) where TParent : INotifyChanged where TList : IList
        {
            if (addItems)
            {
                NotifiesIsChangedWhenListItemsAdded(assert, obj, list);
            }

            if (removeItems)
            {
                NotifiesIsChangedWhenListItemsRemoved(assert, obj, list);
            }

            if (changeItems)
            {
                NotifiesIsChangedWhenListItemsChange(assert, obj, list);
            }
        }

        /// <summary>
        /// Asserts that as items are added to a list, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Parent type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <typeparam name="TList">List type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object to test.</param>
        /// <param name="list">List to add items to.</param>
        public static void NotifiesIsChangedWhenListItemsAdded<TParent, TList>(this Assert assert, TParent obj, TList list) where TParent : INotifyChanged where TList : IList
        {
            _ = assert;

            Type listItemType = list.GetType().GenericTypeArguments[0];

            int count = 0;
            obj.OnChanged += (s, c) => count++;

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.AcceptChanges();
                count = 0;

                dynamic item = ObjectUtilities.CreateInstanceWithRandomValues(listItemType);

                if (item is IChangeTracking tracking)
                {
                    tracking.AcceptChanges();
                }

                list.Add(item);

                Assert.IsTrue(count == 1);
            }

            obj.AcceptChanges();
            count = 0;

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                dynamic item = ObjectUtilities.CreateInstanceWithRandomValues(listItemType);

                if (item is IChangeTracking tracking)
                {
                    tracking.AcceptChanges();
                }

                list.Add(item);
            }

            Assert.IsTrue(count == 1);
        }

        /// <summary>
        /// Asserts that as items are removed from a list, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Parent type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <typeparam name="TList">List type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object to test.</param>
        /// <param name="list">List to remove items from.</param>
        public static void NotifiesIsChangedWhenListItemsRemoved<TParent, TList>(this Assert assert, TParent obj, TList list) where TParent : INotifyChanged where TList : IList
        {
            _ = assert;

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
        /// Asserts that as items in a list change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Parent type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <typeparam name="TList">List type that implements <see cref="INotifyChanged"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object to test.</param>
        /// <param name="list">List with items to change.</param>
        public static void NotifiesIsChangedWhenListItemsChange<TParent, TList>(this Assert assert, TParent obj, TList list) where TParent : INotifyChanged where TList : IList
        {
            _ = assert;

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
            var obj = Activator.CreateInstance(type);
            Assert.IsNotNull(obj);

            return (INotifyChanged)obj;
        }
    }
}
