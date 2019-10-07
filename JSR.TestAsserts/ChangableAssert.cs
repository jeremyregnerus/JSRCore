// <copyright file="ChangableAssert.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using JSR.BaseClassLibrary;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.TestAsserts
{
    /// <summary>
    /// Tests the implementation of IChangableObject. Primarily focused on raising change events.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:DoNotUseRegions", Justification = "Regions used for signatures.")]
    public static class ChangableAssert
    {
        #region NotifiesIsChangedOnAcceptChanges

        /// <summary>
        /// Tests that an object and it's class and list properties raise the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        public static void NotifiesIsChangedOnAcceptChanges<T>() where T : INotifyOnChanged
        {
            NotifiesIsChangedOnAcceptChanges(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that an object and it's class and list properties raise the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="ChangableBaseClass"/>.</param>
        public static void NotfiesIsChagnedOnAcceptChanges(Type type)
        {
            Assert.IsTrue(typeof(INotifyOnChanged).IsAssignableFrom(type));

            NotifiesIsChangedOnAcceptChanges((INotifyOnChanged)Activator.CreateInstance(type));
        }

        /// <summary>
        /// Tests that an object and it's class and list properties raise the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object to test.</param>
        public static void NotifiesIsChangedOnAcceptChanges<T>(T obj) where T : INotifyOnChanged
        {
            ChangableMonitor<T> monitor = new ChangableMonitor<T>(obj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(obj);

                monitor.ClearNotifications();

                obj.AcceptChanges();

                monitor.AssertNotifications(false);
            }

            ObjectUtilities.PopulateObjectWithRandomValues(obj);

            monitor.ClearNotifications();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.AcceptChanges();
            }

            monitor.AssertNotifications(false);
        }

        #endregion

        #region IsChangedWhenHierarchyChanges

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges(Type type)
        {
            NotifiesIsChangedWhenHierarchyChanges(CreateIChangableObjectInstance(type));
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges(Type type, List<string> propertyNames)
        {
            NotifiesIsChangedWhenHierarchyChanges(CreateIChangableObjectInstance(type), propertyNames);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges(Type type, List<PropertyInfo> properties)
        {
            NotifiesIsChangedWhenHierarchyChanges(CreateIChangableObjectInstance(type), properties);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges(Type type, string propertyName)
        {
            NotifiesIsChangedWhenHierarchyChanges(CreateIChangableObjectInstance(type), propertyName);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges(Type type, PropertyInfo property)
        {
            NotifiesIsChangedWhenHierarchyChanges(CreateIChangableObjectInstance(type), property);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>() where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(List<string> propertyNames) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(List<PropertyInfo> properties) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(string propertyName) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(Activator.CreateInstance<T>(), propertyName);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(PropertyInfo property) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(T obj) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(obj, PropertyUtilities.GetListOfProperties(obj, true, true, false, true, true, true));
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(T obj, List<string> propertyNames) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName))));
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(T obj, List<PropertyInfo> properties) where T : INotifyOnChanged
        {
            foreach (PropertyInfo property in properties)
            {
                NotifiesIsChangedWhenHierarchyChanges(obj, property);
            }

            obj.AcceptChanges();

            ChangableMonitor<T> monitor = new ChangableMonitor<T>(obj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(obj);

                monitor.AssertNotifications(true);
            }

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(obj);
            }

            monitor.AssertNotifications(false);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(T obj, string propertyName) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenHierarchyChanges(obj, obj.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenHierarchyChanges<T>(T obj, PropertyInfo property) where T : INotifyOnChanged
        {
            if (PropertyUtilities.CheckIfPropertyIsReadWrite(property))
            {
                NotifiesIsChangedWhenPropertyChanges(obj, property);
            }

            if (PropertyUtilities.CheckIfPropertyIsClass(property))
            {
                NotifiesIsChangedWhenClassPropertyChanges(obj, property);
            }

            if (PropertyUtilities.CheckIfPropertyIsList(property))
            {
                NotifiesIsChangedWhenListPropertyChanges(obj, property, true, true, true);
            }
        }

        #endregion

        #region NotifiesIsChangedWhenPropertiesChange

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        public static void NotifiesIsChangedWhenPropertiesChange(Type type)
        {
            NotifiesIsChangedWhenPropertiesChange(CreateIChangableObjectInstance(type));
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange(Type type, List<string> propertyNames)
        {
            NotifiesIsChangedWhenPropertiesChange(CreateIChangableObjectInstance(type), propertyNames);
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange(Type type, List<PropertyInfo> properties)
        {
            NotifiesIsChangedWhenPropertiesChange(CreateIChangableObjectInstance(type), properties);
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        public static void NotifiesIsChangedWhenPropertiesChange<T>() where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenPropertiesChange(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(List<string> propertyNames) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenPropertiesChange(Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(List<PropertyInfo> properties) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenPropertiesChange(Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(T obj) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenPropertiesChange(obj, PropertyUtilities.GetListOfReadWriteProperties(obj));
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(T obj, List<string> propertyNames) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenPropertiesChange(obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName))));
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(T obj, List<PropertyInfo> properties) where T : INotifyOnChanged
        {
            foreach (PropertyInfo property in properties)
            {
                NotifiesIsChangedWhenPropertyChanges(obj, property);
            }

            obj.AcceptChanges();

            ChangableMonitor<T> monitor = new ChangableMonitor<T>(obj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (PropertyInfo property in properties.Where(property => PropertyUtilities.CheckIfPropertyIsReadWrite(property)))
                {
                    ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);
                }

                monitor.AssertNotifications(true);
            }

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (PropertyInfo property in properties.Where(property => PropertyUtilities.CheckIfPropertyIsReadWrite(property)))
                {
                    ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);
                }
            }

            monitor.AssertNotifications(false);
        }

        #endregion

        #region NotifiesIsChangedWhenPropertyChanges

        /// <summary>
        /// Tests that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges(Type type, string propertyName)
        {
            NotifiesIsChangedWhenPropertyChanges(CreateIChangableObjectInstance(type), propertyName);
        }

        /// <summary>
        /// Tests that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges(Type type, PropertyInfo property)
        {
            NotifiesIsChangedWhenPropertyChanges(CreateIChangableObjectInstance(type), property);
        }

        /// <summary>
        /// Tests that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(string propertyName) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenPropertyChanges(Activator.CreateInstance<T>(), propertyName);
        }

        /// <summary>
        /// Tests that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(PropertyInfo property) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenPropertyChanges(Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Tests that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(T obj, string propertyName) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenPropertyChanges(obj, obj.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Tests that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/> with property to test.</typeparam>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(T obj, PropertyInfo property) where T : INotifyOnChanged
        {
            if (PropertyUtilities.CheckIfPropertyIsReadWrite(property))
            {
                obj.AcceptChanges();

                ChangableMonitor<T> monitor = new ChangableMonitor<T>(obj);

                for (int i = 0; i < new Random().Next(5, 20); i++)
                {
                    ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);

                    monitor.AssertNotifications(true);
                }

                for (int i = 0; i < new Random().Next(5, 20); i++)
                {
                    ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);
                }

                monitor.AssertNotifications(false);
            }
        }

        #endregion

        #region NotifiesIsChangedWhenClassPropertiesChange

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange(Type type)
        {
            NotifiesIsChangedWhenClassPropertiesChange(CreateIChangableObjectInstance(type));
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        /// <param name="propertyNames">List of property Names to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange(Type type, List<string> propertyNames)
        {
            NotifiesIsChangedWhenClassPropertiesChange(CreateIChangableObjectInstance(type), propertyNames);
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange(Type type, List<PropertyInfo> properties)
        {
            NotifiesIsChangedWhenClassPropertiesChange(CreateIChangableObjectInstance(type), properties);
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        public static void NotifiesIsChangedWhenClassPropertiesChange<T>() where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenClassPropertiesChange(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="propertyNames">List of property Names to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange<T>(List<string> propertyNames) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenClassPropertiesChange(Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange<T>(List<PropertyInfo> properties) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenClassPropertiesChange(Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with child objects to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange<T>(T obj) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenClassPropertiesChange(obj, PropertyUtilities.GetListOfPropertiesWithClassTypes(obj, true, true, false));
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with child objects to test.</param>
        /// <param name="propertyNames">List of property Names to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange<T>(T obj, List<string> propertyNames) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenClassPropertiesChange(obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName))));
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with child objects to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenClassPropertiesChange<T>(T obj, List<PropertyInfo> properties) where T : INotifyOnChanged
        {
            foreach (PropertyInfo property in properties)
            {
                NotifiesIsChangedWhenClassPropertyChanges(obj, property);
            }

            obj.AcceptChanges();

            ChangableMonitor<T> monitor = new ChangableMonitor<T>(obj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (PropertyInfo property in properties)
                {
                    ObjectUtilities.PopulateObjectWithRandomValues(property.GetValue(obj));
                }

                monitor.AssertNotifications(true);
            }

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (PropertyInfo property in properties)
                {
                    ObjectUtilities.PopulateObjectWithRandomValues(property.GetValue(obj));
                }
            }

            monitor.AssertNotifications(false);
        }

        #endregion

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <typeparam name="TList">Type that implements <see cref="IList"/>.</typeparam>
        /// <param name="obj">Object with child objects to test.</param>
        /// <param name="children">Child objects to change.</param>
        public static void NotifiesIsChangedWhenChildClassesChange<TParent, TList>(TParent obj, TList children) where TParent : INotifyOnChanged where TList : IList
        {
            foreach (INotifyOnChanged child in children)
            {
                NotifiesIsChangedWhenChildClassChanges(obj, child);
            }

            obj.AcceptChanges();

            ChangableMonitor<TParent> monitor = new ChangableMonitor<TParent>(obj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (var child in children)
                {
                    ObjectUtilities.PopulateObjectWithRandomValues(child);
                }

                monitor.AssertNotifications(true);
            }

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (var child in children)
                {
                    ObjectUtilities.PopulateObjectWithRandomValues(child);
                }
            }

            monitor.AssertNotifications(false);
        }

        #region NotifiesIsChangedWhenClassPropertyChanges

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenClassPropertyChanges(Type type, string propertyName)
        {
            NotifiesIsChangedWhenClassPropertyChanges(CreateIChangableObjectInstance(type), propertyName);
        }

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenClassPropertyChanges(Type type, PropertyInfo property)
        {
            NotifiesIsChangedWhenClassPropertyChanges(CreateIChangableObjectInstance(type), property);
        }

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenClassPropertyChanges<T>(string propertyName) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenClassPropertyChanges(Activator.CreateInstance<T>(), propertyName);
        }

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenClassPropertyChanges<T>(PropertyInfo property) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenClassPropertyChanges(Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with child to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenClassPropertyChanges<T>(T obj, string propertyName) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenClassPropertyChanges(obj, obj.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with child to test.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenClassPropertyChanges<T>(T obj, PropertyInfo property) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenChildClassChanges(obj, property.GetValue(obj));
        }

        #endregion

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Type of the parent object that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <typeparam name="TChild">Type of the child object.</typeparam>
        /// <param name="parentObj">Object with child to test.</param>
        /// <param name="childObj">Child object to change.</param>
        public static void NotifiesIsChangedWhenChildClassChanges<TParent, TChild>(TParent parentObj, TChild childObj) where TParent : INotifyOnChanged
        {
            ChangableMonitor<TParent> monitor = new ChangableMonitor<TParent>(parentObj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                monitor.Reset();

                ObjectUtilities.PopulateObjectWithRandomValues(childObj);

                monitor.AssertNotifications(true);
            }

            monitor.Reset();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(childObj);
            }

            monitor.AssertNotifications(false);
        }

        #region NotifiesIsChangedWhenListPropertiesChange

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange(Type type, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListPropertiesChange(CreateIChangableObjectInstance(type), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange(Type type, List<string> propertyNames, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListPropertiesChange(CreateIChangableObjectInstance(type), propertyNames, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange(Type type, List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListPropertiesChange(CreateIChangableObjectInstance(type), properties, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange<T>(bool addItems, bool removeItems, bool changeItems) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenListPropertiesChange(Activator.CreateInstance<T>(), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="propertyNames">List of property names to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange<T>(List<string> propertyNames, bool addItems, bool removeItems, bool changeItems) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenListPropertiesChange(Activator.CreateInstance<T>(), propertyNames, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange<T>(List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenListPropertiesChange(Activator.CreateInstance<T>(), properties, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange<T>(T obj, bool addItems, bool removeItems, bool changeItems) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenListPropertiesChange(obj, PropertyUtilities.GetListOfPropertiesWithListTypes(obj, true, true, false), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange<T>(T obj, List<string> propertyNames, bool addItems, bool removeItems, bool changeItems) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenListPropertiesChange(obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName))), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="properties">List of properties to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertiesChange<T>(T obj, List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems) where T : INotifyOnChanged
        {
            foreach (PropertyInfo property in properties)
            {
                NotifiesIsChangedWhenListPropertyChanges(obj, property, addItems, removeItems, changeItems);
            }

            obj.AcceptChanges();

            ChangableMonitor<T> monitor = new ChangableMonitor<T>(obj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (PropertyInfo property in properties.Where(property => typeof(IList).IsAssignableFrom(property.PropertyType)))
                {
                    ObjectUtilities.PopulateListWithRandomValues((IList)property.GetValue(obj), obj.GetType());
                }

                monitor.AssertNotifications(true);
            }

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (PropertyInfo property in properties.Where(property => typeof(IList).IsAssignableFrom(property.PropertyType)))
                {
                    ObjectUtilities.PopulateListWithRandomValues((IList)property.GetValue(obj), obj.GetType());
                }
            }

            monitor.AssertNotifications(false);
        }

        #endregion

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Parent type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <typeparam name="TList">List type that implements <see cref="IChangableCollection{T}"/>.</typeparam>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="lists">List of lists to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListsChange<TParent, TList>(TParent obj, List<TList> lists, bool addItems, bool removeItems, bool changeItems) where TParent : INotifyOnChanged where TList : IList
        {
            foreach (TList list in lists)
            {
                NotifiesIsChangedWhenListChanges(obj, list, addItems, removeItems, changeItems);
            }

            obj.AcceptChanges();

            ChangableMonitor<TParent> monitor = new ChangableMonitor<TParent>(obj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (IList list in lists)
                {
                    ObjectUtilities.PopulateListWithRandomValues(list);
                }

                monitor.AssertNotifications(true);
            }

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (IList list in lists)
                {
                    ObjectUtilities.PopulateListWithRandomValues(list);
                }
            }

            monitor.AssertNotifications(false);
        }

        #region NotifiesIsChangedWhenListPropertyChanges

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertyChanges(Type type, string propertyName, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListPropertyChanges(CreateIChangableObjectInstance(type), propertyName, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="INotifyOnChanged"/>.</param>
        /// <param name="property">Property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertyChanges(Type type, PropertyInfo property, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListPropertyChanges(CreateIChangableObjectInstance(type), property, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertyChanges<T>(string propertyName, bool addItems, bool removeItems, bool changeItems) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenListPropertyChanges<T>(typeof(T).GetProperty(propertyName), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="property">Property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertyChanges<T>(PropertyInfo property, bool addItems, bool removeItems, bool changeItems) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenListPropertyChanges(Activator.CreateInstance<T>(), property, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with list to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertyChanges<T>(T obj, string propertyName, bool addItems, bool removeItems, bool changeItems) where T : INotifyOnChanged
        {
            NotifiesIsChangedWhenListPropertyChanges(obj, obj.GetType().GetProperty(propertyName), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <param name="obj">Object with list to test.</param>
        /// <param name="property">Property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListPropertyChanges<T>(T obj, PropertyInfo property, bool addItems, bool removeItems, bool changeItems) where T : INotifyOnChanged
        {
            if (typeof(IList).IsAssignableFrom(property.PropertyType))
            {
                NotifiesIsChangedWhenListChanges(obj, (IList)property.GetValue(obj), addItems, removeItems, changeItems);
            }
        }

        #endregion

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Parent type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <typeparam name="TList">List type that implements <see cref="IChangableCollection{T}"/>.</typeparam>
        /// <param name="obj">Object with list to test.</param>
        /// <param name="list">List to change.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListChanges<TParent, TList>(TParent obj, TList list, bool addItems, bool removeItems, bool changeItems) where TParent : INotifyOnChanged where TList : IList
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
        /// <typeparam name="TParent">Parent type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <typeparam name="TList">List type that implements <see cref="IChangableCollection{T}"/>.</typeparam>
        /// <param name="obj">Object to test.</param>
        /// <param name="list">List to add items to.</param>
        public static void NotifiesIsChangedWhenListItemsAdded<TParent, TList>(TParent obj, TList list) where TParent : INotifyOnChanged where TList : IList
        {
            obj.AcceptChanges();

            ChangableMonitor<TParent> monitor = new ChangableMonitor<TParent>(obj);

            Type listItemType = list.GetType().GenericTypeArguments[0];
            bool listItemsAreIChangeTracking = typeof(IChangeTracking).IsAssignableFrom(listItemType);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                var item = ObjectUtilities.CreateInstanceWithRandomValues(listItemType);

                if (listItemsAreIChangeTracking)
                {
                    ((IChangeTracking)item).AcceptChanges();
                }

                list.Add(item);

                monitor.AssertNotifications(true);
            }

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                var item = ObjectUtilities.CreateInstanceWithRandomValues(listItemType);

                if (listItemsAreIChangeTracking)
                {
                    ((IChangeTracking)item).AcceptChanges();
                }

                list.Add(item);
            }

            monitor.AssertNotifications(false);
        }

        /// <summary>
        /// Tests that as items are removed from a list, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Parent type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <typeparam name="TList">List type that implements <see cref="IChangableCollection{T}"/>.</typeparam>
        /// <param name="obj">Object to test.</param>
        /// <param name="list">List to remove items from.</param>
        public static void NotifiesIsChangedWhenListItemsRemoved<TParent, TList>(TParent obj, TList list) where TParent : INotifyOnChanged where TList : IList
        {
            if (list.Count == 0)
            {
                ObjectUtilities.PopulateListWithRandomValues(list);
            }

            obj.AcceptChanges();

            ChangableMonitor<TParent> monitor = new ChangableMonitor<TParent>(obj);

            while (list.Count > 0)
            {
                list.RemoveAt(list.Count - 1);

                monitor.AssertNotifications(true);
            }

            ObjectUtilities.PopulateListWithRandomValues(list);
            monitor.Reset();

            while (list.Count > 0)
            {
                list.RemoveAt(list.Count - 1);
            }

            monitor.AssertNotifications(false);

            ObjectUtilities.PopulateListWithRandomValues(list);

            monitor.Reset();

            list.Clear();

            monitor.AssertNotifications(false);
        }

        /// <summary>
        /// Tests that as items in a list change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Parent type that implements <see cref="INotifyOnChanged"/>.</typeparam>
        /// <typeparam name="TList">List type that implements <see cref="IChangableCollection{T}"/>.</typeparam>
        /// <param name="obj">Object to test.</param>
        /// <param name="list">List with items to change.</param>
        public static void NotifiesIsChangedWhenListItemsChange<TParent, TList>(TParent obj, TList list) where TParent : INotifyOnChanged where TList : IList
        {
            if (list.Count == 0)
            {
                ObjectUtilities.PopulateListWithRandomValues(list);
            }

            obj.AcceptChanges();

            ChangableMonitor<TParent> monitor = new ChangableMonitor<TParent>(obj);

            foreach (var item in list)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(item);

                monitor.AssertNotifications(true);
            }

            foreach (var item in list)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(item);
            }

            monitor.AssertNotifications(false);
        }

        /// <summary>
        /// Checks that the type implements <see cref="INotifyOnChanged"/> and creates a new instance of that type.
        /// </summary>
        /// <param name="type">Type to test and create.</param>
        /// <returns>A new instance of the type specified that implements <see cref="IChangeTracking"/>.</returns>
        private static INotifyOnChanged CreateIChangableObjectInstance(Type type)
        {
            Assert.IsTrue(typeof(INotifyOnChanged).IsAssignableFrom(type));

            return (INotifyOnChanged)Activator.CreateInstance(type);
        }
    }
}
