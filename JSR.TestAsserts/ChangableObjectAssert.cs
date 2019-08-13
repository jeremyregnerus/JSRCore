// <copyright file="ChangableObjectAssert.cs" company="Jeremy Regnerus">
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
    public static class ChangableObjectAssert
    {
        #region NotifiesIsChangedOnAcceptChanges

        /// <summary>
        /// Tests that an object and it's class and list properties raise the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        public static void NotifiesIsChangedOnAcceptChanges<T>() where T : IChangableObject
        {
            NotifiesIsChangedOnAcceptChanges(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that an object and it's class and list properties raise the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="ChangableObject"/>.</param>
        public static void NotfiesIsChagnedOnAcceptChanges(Type type)
        {
            Assert.IsTrue(typeof(IChangableObject).IsAssignableFrom(type));

            NotifiesIsChangedOnAcceptChanges((IChangableObject)Activator.CreateInstance(type));
        }

        /// <summary>
        /// Tests that an object and it's class and list properties raise the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object to test.</param>
        public static void NotifiesIsChangedOnAcceptChanges<T>(T obj) where T : IChangableObject
        {
            ChangableObjectAssertTracker<T> tracker = new ChangableObjectAssertTracker<T>(obj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(obj);

                tracker.ClearNotifications();

                obj.AcceptChanges();

                tracker.AssertWasChanged();
            }

            tracker.Reset();

            ObjectUtilities.PopulateObjectWithRandomValues(obj);

            tracker.ClearNotifications();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.AcceptChanges();
            }

            tracker.AssertWasChanged();
        }

        #endregion

        #region NotifiesIsChangedWhenChanged

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        public static void NotifiesIsChangedWhenChanged(Type type)
        {
            NotifiesIsChangedWhenChanged(CreateIChangableObjectInstance(type));
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenChanged(Type type, List<string> propertyNames)
        {
            NotifiesIsChangedWhenChanged(CreateIChangableObjectInstance(type), propertyNames);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenChanged(Type type, List<PropertyInfo> properties)
        {
            NotifiesIsChangedWhenChanged(CreateIChangableObjectInstance(type), properties);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenChanged(Type type, string propertyName)
        {
            NotifiesIsChangedWhenChanged(CreateIChangableObjectInstance(type), propertyName);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenChanged(Type type, PropertyInfo property)
        {
            NotifiesIsChangedWhenChanged(CreateIChangableObjectInstance(type), property);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        public static void NotifiesIsChangedWhenChanged<T>() where T : IChangableObject
        {
            NotifiesIsChangedWhenChanged(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenChanged<T>(List<string> propertyNames) where T : IChangableObject
        {
            NotifiesIsChangedWhenChanged(Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenChanged<T>(List<PropertyInfo> properties) where T : IChangableObject
        {
            NotifiesIsChangedWhenChanged(Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenChanged<T>(string propertyName) where T : IChangableObject
        {
            NotifiesIsChangedWhenChanged(Activator.CreateInstance<T>(), propertyName);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenChanged<T>(PropertyInfo property) where T : IChangableObject
        {
            NotifiesIsChangedWhenChanged(Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        public static void NotifiesIsChangedWhenChanged<T>(T obj) where T : IChangableObject
        {
            NotifiesIsChangedWhenChanged(obj, PropertyUtilities.GetListOfProperties(obj, true, true, true, true, true, true));
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenChanged<T>(T obj, List<string> propertyNames) where T : IChangableObject
        {
            NotifiesIsChangedWhenChanged(obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName))));
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenChanged<T>(T obj, List<PropertyInfo> properties) where T : IChangableObject
        {
            foreach (PropertyInfo property in properties)
            {
                NotifiesIsChangedWhenChanged(obj, property);
            }

            ChangableObjectAssertTracker<T> tracker = new ChangableObjectAssertTracker<T>(obj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                tracker.Reset();

                ObjectUtilities.PopulateObjectWithRandomValues(obj);

                tracker.AssertWasChanged();
            }

            tracker.Reset();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(obj);
            }

            tracker.AssertWasChanged();
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenChanged<T>(T obj, string propertyName) where T : IChangableObject
        {
            NotifiesIsChangedWhenChanged(obj, obj.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Tests that when properties, classes and lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenChanged<T>(T obj, PropertyInfo property) where T : IChangableObject
        {
            if (PropertyUtilities.CheckIfPropertyIsReadWrite(property))
            {
                NotifiesIsChangedWhenPropertyChanges(obj, property);
            }

            if (PropertyUtilities.CheckIfPropertyIsClass(property))
            {
                NotifiesIsChangedWhenChildChanges(obj, property);
            }

            if (PropertyUtilities.CheckIfPropertyIsList(property))
            {
                NotifiesIsChangedWhenListChanges(obj, property, true, true, true);
            }
        }

        #endregion

        #region NotifiesIsChangedWhenPropertiesChange

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        public static void NotifiesIsChangedWhenPropertiesChange(Type type)
        {
            NotifiesIsChangedWhenPropertiesChange(CreateIChangableObjectInstance(type));
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange(Type type, List<string> propertyNames)
        {
            NotifiesIsChangedWhenPropertiesChange(CreateIChangableObjectInstance(type), propertyNames);
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange(Type type, List<PropertyInfo> properties)
        {
            NotifiesIsChangedWhenPropertiesChange(CreateIChangableObjectInstance(type), properties);
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        public static void NotifiesIsChangedWhenPropertiesChange<T>() where T : IChangableObject
        {
            NotifiesIsChangedWhenPropertiesChange(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(List<string> propertyNames) where T : IChangableObject
        {
            NotifiesIsChangedWhenPropertiesChange(Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(List<PropertyInfo> properties) where T : IChangableObject
        {
            NotifiesIsChangedWhenPropertiesChange(Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(T obj) where T : IChangableObject
        {
            NotifiesIsChangedWhenPropertiesChange(obj, PropertyUtilities.GetListOfReadWriteProperties(obj));
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(T obj, List<string> propertyNames) where T : IChangableObject
        {
            NotifiesIsChangedWhenPropertiesChange(obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName))));
        }

        /// <summary>
        /// Tests that when properties change, the object raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(T obj, List<PropertyInfo> properties) where T : IChangableObject
        {
            foreach (PropertyInfo property in properties)
            {
                NotifiesIsChangedWhenPropertyChanges(obj, property);
            }

            ChangableObjectAssertTracker<T> tracker = new ChangableObjectAssertTracker<T>(obj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                tracker.Reset();

                foreach (PropertyInfo property in properties)
                {
                    ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);
                }

                tracker.AssertWasChanged();
            }

            tracker.Reset();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (PropertyInfo property in properties)
                {
                    ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);
                }
            }

            tracker.AssertWasChanged();
        }

        #endregion

        #region NotifiesIsChangedWhenPropertyChanges

        /// <summary>
        /// Tests that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges(Type type, string propertyName)
        {
            NotifiesIsChangedWhenPropertyChanges(CreateIChangableObjectInstance(type), propertyName);
        }

        /// <summary>
        /// Tests that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges(Type type, PropertyInfo property)
        {
            NotifiesIsChangedWhenPropertyChanges(CreateIChangableObjectInstance(type), property);
        }

        /// <summary>
        /// Tests that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(string propertyName) where T : IChangableObject
        {
            NotifiesIsChangedWhenPropertyChanges(Activator.CreateInstance<T>(), propertyName);
        }

        /// <summary>
        /// Tests that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(PropertyInfo property) where T : IChangableObject
        {
            NotifiesIsChangedWhenPropertyChanges(Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Tests that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(T obj, string propertyName) where T : IChangableObject
        {
            NotifiesIsChangedWhenPropertyChanges(obj, obj.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Tests that a property raises the object's <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/> with property to test.</typeparam>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(T obj, PropertyInfo property) where T : IChangableObject
        {
            obj.AcceptChanges();

            List<string> propertiesChanged = new List<string>();
            obj.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<bool> changeStates = new List<bool>();
            obj.OnChanged += (sender, wasChanged) => changeStates.Add(wasChanged);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);
            }

            Assert.AreEqual(1, propertiesChanged.Count(propertyName => propertyName == nameof(IChangableObject.IsChanged)));
            Assert.AreEqual(1, changeStates.Count(wasChanged => wasChanged == true));

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.AcceptChanges();
                propertiesChanged.Clear();
                changeStates.Clear();

                ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);

                Assert.AreEqual(1, propertiesChanged.Count(propertyName => propertyName == nameof(IChangableObject.IsChanged)));
                Assert.AreEqual(1, changeStates.Count(wasChanged => wasChanged == true));
            }
        }

        #endregion

        #region NotifiesIsChangedWhenChildrenChange

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        public static void NotifiesIsChangedWhenChildrenChange(Type type)
        {
            NotifiesIsChangedWhenChildrenChange(CreateIChangableObjectInstance(type));
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        /// <param name="propertyNames">List of property Names to test.</param>
        public static void NotifiesIsChangedWhenChildrenChange(Type type, List<string> propertyNames)
        {
            NotifiesIsChangedWhenChildrenChange(CreateIChangableObjectInstance(type), propertyNames);
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenChildrenChange(Type type, List<PropertyInfo> properties)
        {
            NotifiesIsChangedWhenChildrenChange(CreateIChangableObjectInstance(type), properties);
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        public static void NotifiesIsChangedWhenChildrenChange<T>() where T : IChangableObject
        {
            NotifiesIsChangedWhenChildrenChange(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="propertyNames">List of property Names to test.</param>
        public static void NotifiesIsChangedWhenChildrenChange<T>(List<string> propertyNames) where T : IChangableObject
        {
            NotifiesIsChangedWhenChildrenChange(Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenChildrenChange<T>(List<PropertyInfo> properties) where T : IChangableObject
        {
            NotifiesIsChangedWhenChildrenChange(Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with child objects to test.</param>
        public static void NotifiesIsChangedWhenChildrenChange<T>(T obj) where T : IChangableObject
        {
            NotifiesIsChangedWhenChildrenChange(obj, PropertyUtilities.GetListOfPropertiesWithClassTypes(obj, true, true, true));
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with child objects to test.</param>
        /// <param name="propertyNames">List of property Names to test.</param>
        public static void NotifiesIsChangedWhenChildrenChange<T>(T obj, List<string> propertyNames) where T : IChangableObject
        {
            NotifiesIsChangedWhenChildrenChange(obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName))));
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with child objects to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void NotifiesIsChangedWhenChildrenChange<T>(T obj, List<PropertyInfo> properties) where T : IChangableObject
        {
            foreach (PropertyInfo property in properties)
            {
                NotifiesIsChangedWhenChildChanges(obj, property);
            }

            ChangableObjectAssertTracker<T> tracker = new ChangableObjectAssertTracker<T>(obj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                tracker.Reset();

                foreach (PropertyInfo property in properties)
                {
                    ObjectUtilities.PopulateObjectWithRandomValues(property.GetValue(obj));
                }

                tracker.AssertWasChanged();
            }

            tracker.Reset();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (PropertyInfo property in properties)
                {
                    ObjectUtilities.PopulateObjectWithRandomValues(property.GetValue(obj));
                }
            }

            tracker.AssertWasChanged();
        }

        /// <summary>
        /// Tests that when child classes change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with child objects to test.</param>
        /// <param name="children">Child objects to change.</param>
        public static void NotifiesIsChangedWhenChildrenChange<T>(T obj, List<IChangableObject> children) where T : IChangableObject
        {
            foreach (IChangableObject child in children)
            {
                NotifiesIsChangedWhenChildChanges(obj, child);
            }

            ChangableObjectAssertTracker<T> tracker = new ChangableObjectAssertTracker<T>(obj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                tracker.Reset();

                foreach (var child in children)
                {
                    ObjectUtilities.PopulateObjectWithRandomValues(child);
                }

                tracker.AssertWasChanged();
            }

            tracker.Reset();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (var child in children)
                {
                    ObjectUtilities.PopulateObjectWithRandomValues(child);
                }
            }

            tracker.AssertWasChanged();
        }

        #endregion

        #region NotifiesIsChangedWhenChildChanges

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenChildChanges(Type type, string propertyName)
        {
            NotifiesIsChangedWhenChildChanges(CreateIChangableObjectInstance(type), propertyName);
        }

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenChildChanges(Type type, PropertyInfo property)
        {
            NotifiesIsChangedWhenChildChanges(CreateIChangableObjectInstance(type), property);
        }

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenChildChanges<T>(string propertyName) where T : IChangableObject
        {
            NotifiesIsChangedWhenChildChanges(Activator.CreateInstance<T>(), propertyName);
        }

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenChildChanges<T>(PropertyInfo property) where T : IChangableObject
        {
            NotifiesIsChangedWhenChildChanges(Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with child to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenChildChanges<T>(T obj, string propertyName) where T : IChangableObject
        {
            NotifiesIsChangedWhenChildChanges(obj, obj.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with child to test.</param>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenChildChanges<T>(T obj, PropertyInfo property) where T : IChangableObject
        {
            Assert.IsTrue(typeof(IChangableObject).IsAssignableFrom(property.PropertyType));

            NotifiesIsChangedWhenChildChanges(obj, (IChangableObject)property.GetValue(obj));
        }

        /// <summary>
        /// Tests that when a child class changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="IChangableObject"/> and contains the TChild type.</typeparam>
        /// <typeparam name="TChild">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="parentObj">Object with child to test.</param>
        /// <param name="childObj">Child object to change.</param>
        public static void NotifiesIsChangedWhenChildChanges<TParent, TChild>(TParent parentObj, TChild childObj) where TParent : IChangableObject where TChild : IChangableObject
        {
            ChangableObjectAssertTracker<TParent> tracker = new ChangableObjectAssertTracker<TParent>(parentObj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(childObj);
            }

            tracker.AssertWasChanged();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                tracker.Reset();

                ObjectUtilities.PopulateObjectWithRandomValues(childObj);

                tracker.AssertWasChanged();
            }
        }

        #endregion

        #region NotifiesIsChangedWhenListsChange

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListsChange(Type type, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListsChange(CreateIChangableObjectInstance(type), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListsChange(Type type, List<string> propertyNames, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListsChange(CreateIChangableObjectInstance(type), propertyNames, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListsChange(Type type, List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListsChange(CreateIChangableObjectInstance(type), properties, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListsChange<T>(bool addItems, bool removeItems, bool changeItems) where T : IChangableObject
        {
            NotifiesIsChangedWhenListsChange(Activator.CreateInstance<T>(), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="propertyNames">List of property names to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListsChange<T>(List<string> propertyNames, bool addItems, bool removeItems, bool changeItems) where T : IChangableObject
        {
            NotifiesIsChangedWhenListsChange(Activator.CreateInstance<T>(), propertyNames, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListsChange<T>(List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems) where T : IChangableObject
        {
            NotifiesIsChangedWhenListsChange(Activator.CreateInstance<T>(), properties, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListsChange<T>(T obj, bool addItems, bool removeItems, bool changeItems) where T : IChangableObject
        {
            NotifiesIsChangedWhenListsChange(obj, PropertyUtilities.GetListOfPropertiesWithListTypes(obj, true, true, true), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListsChange<T>(T obj, List<string> propertyNames, bool addItems, bool removeItems, bool changeItems) where T : IChangableObject
        {
            NotifiesIsChangedWhenListsChange(obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName))), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="properties">List of properties to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListsChange<T>(T obj, List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems) where T : IChangableObject
        {
            foreach (PropertyInfo property in properties)
            {
                NotifiesIsChangedWhenListChanges(obj, property, addItems, removeItems, changeItems);
            }

            ChangableObjectAssertTracker<T> tracker = new ChangableObjectAssertTracker<T>(obj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                tracker.Reset();

                foreach (PropertyInfo property in properties.Where(property => PropertyUtilities.CheckIfPropertyIsList(property)))
                {
                    ObjectUtilities.PopulateListWithRandomValues((IList)property.GetValue(obj), obj.GetType());
                }

                tracker.AssertWasChanged();
            }

            tracker.Reset();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (PropertyInfo property in properties.Where(property => PropertyUtilities.CheckIfPropertyIsList(property)))
                {
                    ObjectUtilities.PopulateListWithRandomValues((IList)property.GetValue(obj), obj.GetType());
                }
            }

            tracker.AssertWasChanged();
        }

        /// <summary>
        /// Tests that when lists change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="lists">List of lists to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListsChange<T>(T obj, List<IList> lists, bool addItems, bool removeItems, bool changeItems) where T : IChangableObject
        {
            foreach (IList list in lists)
            {
                NotifiesIsChangedWhenListChanges(obj, list, addItems, removeItems, changeItems);
            }

            ChangableObjectAssertTracker<T> tracker = new ChangableObjectAssertTracker<T>(obj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                tracker.Reset();

                foreach (IList list in lists)
                {
                    ObjectUtilities.PopulateListWithRandomValues(list);
                }

                tracker.AssertWasChanged();
            }

            tracker.Reset();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                foreach (IList list in lists)
                {
                    ObjectUtilities.PopulateListWithRandomValues(list);
                }
            }

            tracker.AssertWasChanged();
        }

        #endregion

        #region NotifiesIsChangedWhenListChanges

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListChanges(Type type, string propertyName, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListChanges(CreateIChangableObjectInstance(type), propertyName, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangableObject"/>.</param>
        /// <param name="property">Property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListChanges(Type type, PropertyInfo property, bool addItems, bool removeItems, bool changeItems)
        {
            NotifiesIsChangedWhenListChanges(CreateIChangableObjectInstance(type), property, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListChanges<T>(string propertyName, bool addItems, bool removeItems, bool changeItems) where T : IChangableObject
        {
            NotifiesIsChangedWhenListChanges<T>(typeof(T).GetProperty(propertyName), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="property">Property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListChanges<T>(PropertyInfo property, bool addItems, bool removeItems, bool changeItems) where T : IChangableObject
        {
            NotifiesIsChangedWhenListChanges(Activator.CreateInstance<T>(), property, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with list to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListChanges<T>(T obj, string propertyName, bool addItems, bool removeItems, bool changeItems) where T : IChangableObject
        {
            NotifiesIsChangedWhenListChanges(obj, obj.GetType().GetProperty(propertyName), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with list to test.</param>
        /// <param name="property">Property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListChanges<T>(T obj, PropertyInfo property, bool addItems, bool removeItems, bool changeItems) where T : IChangableObject
        {
            Assert.IsTrue(typeof(IList).IsAssignableFrom(property.PropertyType));

            NotifiesIsChangedWhenListChanges(obj, (IList)property.GetValue(obj), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list changes, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object with list to test.</param>
        /// <param name="list">List to change.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void NotifiesIsChangedWhenListChanges<T>(T obj, IList list, bool addItems, bool removeItems, bool changeItems) where T : IChangableObject
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

        #endregion

        /// <summary>
        /// Tests that as items are added to a list, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object to test.</param>
        /// <param name="list">List to add items to.</param>
        public static void NotifiesIsChangedWhenListItemsAdded<T>(T obj, IList list) where T : IChangableObject
        {
            ChangableObjectAssertTracker<T> tracker = new ChangableObjectAssertTracker<T>(obj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                var item = ObjectUtilities.CreateInstanceWithRandomValues(list.GetType().GenericTypeArguments[0]);

                Assert.IsTrue(item is IChangableObject);
                ((IChangableObject)item).AcceptChanges();

                list.Add(item);
            }

            tracker.AssertWasChanged();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                tracker.Reset();

                var item = ObjectUtilities.CreateInstanceWithRandomValues(list.GetType().GenericTypeArguments[0]);

                Assert.IsTrue(item is IChangableObject);
                ((IChangableObject)item).AcceptChanges();

                list.Add(item);

                tracker.AssertWasChanged();
            }
        }

        /// <summary>
        /// Tests that as items are removed from a list, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object to test.</param>
        /// <param name="list">List to remove items from.</param>
        public static void NotifiesIsChangedWhenListItemsRemoved<T>(T obj, IList list) where T : IChangableObject
        {
            if (list.Count == 0)
            {
                ObjectUtilities.PopulateListWithRandomValues(list);
            }

            ChangableObjectAssertTracker<T> tracker = new ChangableObjectAssertTracker<T>(obj);

            while (list.Count > 0)
            {
                list.RemoveAt(list.Count - 1);
            }

            tracker.AssertWasChanged();

            ObjectUtilities.PopulateListWithRandomValues(list);

            while (list.Count > 0)
            {
                tracker.Reset();

                list.RemoveAt(list.Count - 1);

                tracker.AssertWasChanged();
            }

            ObjectUtilities.PopulateListWithRandomValues(list);

            tracker.Reset();

            list.Clear();

            tracker.AssertWasChanged();
        }

        /// <summary>
        /// Tests that as items in a list change, the parent raises the <see cref="OnChangedEventHandler"/> once.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="obj">Object to test.</param>
        /// <param name="list">List with items to change.</param>
        public static void NotifiesIsChangedWhenListItemsChange<T>(T obj, IList list) where T : IChangableObject
        {
            if (list.Count == 0)
            {
                ObjectUtilities.PopulateListWithRandomValues(list);
            }

            ChangableObjectAssertTracker<T> tracker = new ChangableObjectAssertTracker<T>(obj);

            foreach (IChangableObject item in list)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(item);
            }

            tracker.AssertWasChanged();

            foreach (IChangableObject item in list)
            {
                tracker.Reset();

                ObjectUtilities.PopulateObjectWithRandomValues(item);

                tracker.AssertWasChanged();
            }
        }

        /// <summary>
        /// Checks that the type implements <see cref="IChangableObject"/> and creates a new instance of that type.
        /// </summary>
        /// <param name="type">Type to test and create.</param>
        /// <returns>A new instance of the type specified that implements <see cref="IChangeTracking"/>.</returns>
        private static IChangableObject CreateIChangableObjectInstance(Type type)
        {
            Assert.IsTrue(typeof(IChangableObject).IsAssignableFrom(type));

            return (IChangableObject)Activator.CreateInstance(type);
        }
    }
}
