// <copyright file="ChangeTrackingAssert.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using JSR.BaseClassLibrary;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.TestAsserts
{
    /// <summary>
    /// Tests and checks objects that implement IChangeTracking.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:DoNotUseRegions", Justification = "Regions used for signatures.")]
    public static class ChangeTrackingAssert
    {
        #region AcceptsChanges

        /// <summary>
        /// Tests that an object and it's class and list properties can accept changes.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        public static void AcceptsChanges(Type type)
        {
            AcceptsChanges(CreateIChangeTrackingInstance(type));
        }

        /// <summary>
        /// Tests that an object and it's class and list properties can accept changes.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        public static void AcceptsChanges<T>() where T : IChangeTracking
        {
            AcceptsChanges(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that an object and it's class and list properties can accept changes.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object to test.</param>
        public static void AcceptsChanges<T>(T obj) where T : IChangeTracking
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(obj);

                obj.AcceptChanges();
                Assert.IsFalse(obj.IsChanged);

                foreach (PropertyInfo property in PropertyUtilities.GetListOfPropertiesWithClassTypes(obj, true, true, false).Where(property => typeof(IChangeTracking).IsAssignableFrom(property.PropertyType)))
                {
                    Assert.IsFalse(((IChangeTracking)property.GetValue(obj)).IsChanged);
                }

                foreach (PropertyInfo property in PropertyUtilities.GetListOfPropertiesWithListTypes(obj, true, true, false).Where(property => typeof(IChangeTracking).IsAssignableFrom(property.PropertyType)))
                {
                    Assert.IsFalse(((IChangeTracking)property.GetValue(obj)).IsChanged);
                }
            }
        }

        #endregion

        #region IsChangedWhenChanged

        /// <summary>
        /// /// Tests that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        public static void IsChangedWhenChanged(Type type)
        {
            IsChangedWhenChanged(CreateIChangeTrackingInstance(type));
        }

        /// <summary>
        /// /// Tests that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void IsChangedWhenChanged(Type type, List<string> propertyNames)
        {
            IsChangedWhenChanged(CreateIChangeTrackingInstance(type), propertyNames);
        }

        /// <summary>
        /// /// Tests that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void IsChangedWhenChanged(Type type, List<PropertyInfo> properties)
        {
            IsChangedWhenChanged(CreateIChangeTrackingInstance(type), properties);
        }

        /// <summary>
        /// /// Tests that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void IsChangedWhenChanged(Type type, string propertyName)
        {
            IsChangedWhenChanged(CreateIChangeTrackingInstance(type), propertyName);
        }

        /// <summary>
        /// /// Tests that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="property">Property to test.</param>
        public static void IsChangedWhenChanged(Type type, PropertyInfo property)
        {
            IsChangedWhenChanged(CreateIChangeTrackingInstance(type), property);
        }

        /// <summary>
        /// /// Tests that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        public static void IsChangedWhenChanged<T>() where T : IChangeTracking
        {
            IsChangedWhenChanged(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// /// Tests that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void IsChangedWhenChanged<T>(List<string> propertyNames) where T : IChangeTracking
        {
            IsChangedWhenChanged(Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// /// Tests that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        public static void IsChangedWhenChanged<T>(List<PropertyInfo> properties) where T : IChangeTracking
        {
            IsChangedWhenChanged(Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// /// Tests that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="propertyName">Property name to test.</param>
        public static void IsChangedWhenChanged<T>(string propertyName) where T : IChangeTracking
        {
            IsChangedWhenChanged(Activator.CreateInstance<T>(), propertyName);
        }

        /// <summary>
        /// /// Tests that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="property">Property to test.</param>
        public static void IsChangedWhenChanged<T>(PropertyInfo property) where T : IChangeTracking
        {
            IsChangedWhenChanged(Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// /// Tests that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        public static void IsChangedWhenChanged<T>(T obj) where T : IChangeTracking
        {
            IsChangedWhenChanged(obj, new List<PropertyInfo>(obj.GetType().GetProperties()));
        }

        /// <summary>
        /// /// Tests that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void IsChangedWhenChanged<T>(T obj, List<string> propertyNames) where T : IChangeTracking
        {
            foreach (string propertyName in propertyNames)
            {
                IsChangedWhenChanged(obj, propertyName);
            }
        }

        /// <summary>
        /// /// Tests that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void IsChangedWhenChanged<T>(T obj, List<PropertyInfo> properties) where T : IChangeTracking
        {
            foreach (PropertyInfo property in properties)
            {
                IsChangedWhenChanged(obj, property);
            }
        }

        /// <summary>
        /// /// Tests that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void IsChangedWhenChanged<T>(T obj, string propertyName) where T : IChangeTracking
        {
            IsChangedWhenChanged(obj, obj.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Tests that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="property">Property to test.</param>
        public static void IsChangedWhenChanged<T>(T obj, PropertyInfo property) where T : IChangeTracking
        {
            if (PropertyUtilities.CheckIfPropertyIsReadWrite(property))
            {
                IsChangedWhenPropertyChanges(obj, property);
            }

            if (PropertyUtilities.CheckIfPropertyIsClass(property))
            {
                IsChangedWhenClassPropertyChanges(obj, property);
            }

            if (PropertyUtilities.CheckIfPropertyIsList(property))
            {
                IsChangedWhenListPropertyChanges(obj, property, true, true, true);
            }
        }

        #endregion

        #region IsChangedWhenPropertiesChange

        /// <summary>
        /// Tests that each property changes the IsChanged state of an object when their values change.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenPropertiesChange(Type type)
        {
            IsChangedWhenPropertiesChange(CreateIChangeTrackingInstance(type));
        }

        /// <summary>
        /// Tests that each property changes the IsChanged state of an object when their values change.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void IsChangedWhenPropertiesChange(Type type, List<string> propertyNames)
        {
            IsChangedWhenPropertiesChange(CreateIChangeTrackingInstance(type), propertyNames);
        }

        /// <summary>
        /// Tests that each property changes the IsChanged state of an object when their values change.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void IsChangedWhenPropertiesChange(Type type, List<PropertyInfo> properties)
        {
            IsChangedWhenPropertiesChange(CreateIChangeTrackingInstance(type), properties);
        }

        /// <summary>
        /// Tests that each property changes the IsChanged state of an object when their values changed.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> to test.</typeparam>
        public static void IsChangedWhenPropertiesChange<T>() where T : IChangeTracking
        {
            IsChangedWhenPropertiesChange(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that each property changes the IsChanged state of an object when their values change.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> to test.</typeparam>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void IsChangedWhenPropertiesChange<T>(List<string> propertyNames) where T : IChangeTracking
        {
            IsChangedWhenPropertiesChange(Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Tests that each property changes the IsChanged state of an object when their values change.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> to test.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        public static void IsChangedWhenPropertiesChange<T>(List<PropertyInfo> properties) where T : IChangeTracking
        {
            IsChangedWhenPropertiesChange(Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Tests that each property changes the IsChanged state of an object when their values change.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> to test.</typeparam>
        /// <param name="obj">Object that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenPropertiesChange<T>(T obj) where T : IChangeTracking
        {
            IsChangedWhenPropertiesChange(obj, PropertyUtilities.GetListOfReadWriteProperties(obj));
        }

        /// <summary>
        /// Tests that each property changes the IsChanged state of an object when their values change.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> to test.</typeparam>
        /// <param name="obj">Object that implements <see cref="IChangeTracking"/> to test.</param>
        /// <param name="propertyNames">List of property names to change.</param>
        public static void IsChangedWhenPropertiesChange<T>(T obj, List<string> propertyNames) where T : IChangeTracking
        {
            IsChangedWhenPropertiesChange(obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName))));
        }

        /// <summary>
        /// Tests that each property changes the IsChanged state of an object when their values change.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> to test.</typeparam>
        /// <param name="obj">Object that implements <see cref="IChangeTracking"/> to test.</param>
        /// <param name="properties">List of properties to change.</param>
        public static void IsChangedWhenPropertiesChange<T>(T obj, List<PropertyInfo> properties) where T : IChangeTracking
        {
            foreach (PropertyInfo property in properties)
            {
                IsChangedWhenPropertyChanges(obj, property);
            }

            obj.AcceptChanges();

            foreach (PropertyInfo property in properties.Where(property => PropertyUtilities.CheckIfPropertyIsReadWrite(property)))
            {
                ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);
            }

            Assert.IsTrue(obj.IsChanged);
        }

        #endregion

        #region IsChangedWhenPropertyChanges

        /// <summary>
        /// Tests that a property changes the IsChanged state of an object when its value changes.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> to test.</param>
        /// <param name="propertyName">Name of property to change.</param>
        public static void IsChangedWhenPropertyChanges(Type type, string propertyName)
        {
            IsChangedWhenPropertyChanges(CreateIChangeTrackingInstance(type), propertyName);
        }

        /// <summary>
        /// Tests that a property changes the IsChanged state of an object when its value changes.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> to test.</param>
        /// <param name="property">Property to change.</param>
        public static void IsChangedWhenPropertyChanges(Type type, PropertyInfo property)
        {
            IsChangedWhenPropertyChanges(CreateIChangeTrackingInstance(type), property);
        }

        /// <summary>
        /// Tests that a property changes the IsChanged state of an object when its value changes.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="propertyName">Name of property to change.</param>
        public static void IsChangeWhenPropertyChanges<T>(string propertyName) where T : IChangeTracking
        {
            IsChangedWhenPropertyChanges(Activator.CreateInstance<T>(), propertyName);
        }

        /// <summary>
        /// Tests that a property changes the IsChanged state of an object when its value changes.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="property">Property to change.</param>
        public static void IsChangedWhenPropertyChanges<T>(PropertyInfo property) where T : IChangeTracking
        {
            IsChangedWhenPropertyChanges(Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Tests that an object's IsChanged property is true when a specific property changes.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object that implements <see cref="IChangeTracking"/> to test.</param>
        /// <param name="propertyName">Name of property to change.</param>
        public static void IsChangedWhenPropertyChanges<T>(T obj, string propertyName) where T : IChangeTracking
        {
            IsChangedWhenPropertyChanges(obj, obj.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Tests that a property changes the IsChanged state of an object when its value changes.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object that implements <see cref="IChangeTracking"/> to test.</param>
        /// <param name="property">Property to change.</param>
        public static void IsChangedWhenPropertyChanges<T>(T obj, PropertyInfo property) where T : IChangeTracking
        {
            if (!OutputUtilities.EvaluateIsReadWriteProperty(property))
            {
                return;
            }

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.AcceptChanges();

                ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);

                Assert.IsTrue(obj.IsChanged);
            }

            obj.AcceptChanges();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);
            }

            Assert.IsTrue(obj.IsChanged);
        }

        #endregion

        #region IsChangedWhenChildClassesChange

        /// <summary>
        /// Tests that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> with class properties to test.</param>
        public static void IsChangedWhenClassPropertiesChange(Type type)
        {
            IsChangedWhenClassPropertiesChange(CreateIChangeTrackingInstance(type));
        }

        /// <summary>
        /// Tests that when the child class properties change, the parent's Ischanged property is changed to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> with class properties.</param>
        /// <param name="propertyNames">List of property names that implement <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertiesChange(Type type, List<string> propertyNames)
        {
            IsChangedWhenClassPropertiesChange(CreateIChangeTrackingInstance(type), propertyNames);
        }

        /// <summary>
        /// Tests that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> with class properties.</param>
        /// <param name="properties">List of properties that implement <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertiesChange(Type type, List<PropertyInfo> properties)
        {
            IsChangedWhenClassPropertiesChange(CreateIChangeTrackingInstance(type), properties);
        }

        /// <summary>
        /// Tests that when the child class properties change, the parent's IsChanged state is changed to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> with class properties.</typeparam>
        public static void IsChangedWhenClassPropertiesChange<T>() where T : IChangeTracking
        {
            IsChangedWhenClassPropertiesChange(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> with class properties to test.</typeparam>
        /// <param name="propertyNames">List of property names that implement <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertiesChange<T>(List<string> propertyNames) where T : IChangeTracking
        {
            IsChangedWhenClassPropertiesChange(Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Tests that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> with class properties to test.</typeparam>
        /// <param name="properties">List of properties that implement <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertiesChange<T>(List<PropertyInfo> properties) where T : IChangeTracking
        {
            IsChangedWhenClassPropertiesChange(Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Tests that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> with class properties.</typeparam>
        /// <param name="obj">Object to test.</param>
        public static void IsChangedWhenClassPropertiesChange<T>(T obj) where T : IChangeTracking
        {
            IsChangedWhenClassPropertiesChange(obj, PropertyUtilities.GetListOfPropertiesWithClassTypes(obj, true, true, false));
        }

        /// <summary>
        /// Tests that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyNames">List of class property names to test. Each class should implement <see cref="IChangeTracking"/>.</param>
        public static void IsChangedWhenClassPropertiesChange<T>(T obj, List<string> propertyNames) where T : IChangeTracking
        {
            IsChangedWhenClassPropertiesChange(obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName))));
        }

        /// <summary>
        /// Tests that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">List of class properties to test. Each class should implement <see cref="IChangeTracking"/>.</param>
        public static void IsChangedWhenClassPropertiesChange<T>(T obj, List<PropertyInfo> properties) where T : IChangeTracking
        {
            foreach (PropertyInfo property in properties)
            {
                IsChangedWhenClassPropertyChanges(obj, property);
            }

            obj.AcceptChanges();

            foreach (PropertyInfo property in properties)
            {
                ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);

                if (typeof(IChangeTracking).IsAssignableFrom(property.PropertyType))
                {
                    Assert.IsTrue(((IChangeTracking)property.GetValue(obj)).IsChanged);
                }
            }

            Assert.IsTrue(obj.IsChanged);
        }

        #endregion

        /// <summary>
        /// Tests that when the child classes change, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="IChangeTracking"/> with child classes.</typeparam>
        /// <typeparam name="TChild">Type that implements <see cref="IChangeTracking"/> as a child class of the parent.</typeparam>
        /// <param name="parent">Parent object that contains child classes contained within the list.</param>
        /// <param name="children">Class objects contained within the properties of the object being tested.</param>
        public static void IsChangedWhenChildClassesChange<TParent, TChild>(TParent parent, List<(TChild child, string propertyName)> children) where TParent : IChangeTracking
        {
            foreach ((TChild child, string propertyName) in children)
            {
                IsChangedWhenChildClassChanges(parent, child, propertyName);
            }

            parent.AcceptChanges();

            foreach ((TChild child, string propertyName) in children)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(child);

                if (typeof(IChangeTracking).IsAssignableFrom(child.GetType()))
                {
                    Assert.IsTrue(((IChangeTracking)child).IsChanged);
                }
            }

            Assert.IsTrue(parent.IsChanged);
        }

        #region IsChangedWhenChildClassChanges

        /// <summary>
        /// Tests that when a property with a class value changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="propertyName">Name of property that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertyChanges(Type type, string propertyName)
        {
            IsChangedWhenClassPropertyChanges(CreateIChangeTrackingInstance(type), propertyName);
        }

        /// <summary>
        /// Tests that when a property with a class value changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="property">Property that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertyChanges(Type type, PropertyInfo property)
        {
            IsChangedWhenClassPropertyChanges(CreateIChangeTrackingInstance(type), property);
        }

        /// <summary>
        /// Tests that when a property with a class value changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="propertyName">Name of property that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertyChanges<T>(string propertyName) where T : IChangeTracking
        {
            IsChangedWhenClassPropertyChanges<T>(typeof(T).GetProperty(propertyName));
        }

        /// <summary>
        /// Tests that when a property with a class value changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="property">Property that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertyChanges<T>(PropertyInfo property) where T : IChangeTracking
        {
            IsChangedWhenClassPropertyChanges(Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Tests that when a property with a class value changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="propertyName">Name of property that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertyChanges<T>(T obj, string propertyName) where T : IChangeTracking
        {
            IsChangedWhenClassPropertyChanges(obj, obj.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Tests that when a property with a class value changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="property">Property that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertyChanges<T>(T obj, PropertyInfo property) where T : IChangeTracking
        {
            IsChangedWhenChildClassChanges(obj, property.GetValue(obj), property.Name);
        }

        #endregion

        /// <summary>
        /// Tests that when a child class changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="IChangeTracking"/> and contains the TChild type.</typeparam>
        /// <typeparam name="TChild">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="parentObj">Parent object to test.</param>
        /// <param name="childObj">Child object to change.</param>
        /// <param name="childPropertyName">Name of the property that contains the child.</param>
        public static void IsChangedWhenChildClassChanges<TParent, TChild>(TParent parentObj, TChild childObj, string childPropertyName) where TParent : IChangeTracking
        {
            bool childIsIChangeTracking = OutputUtilities.ExpectedImplementation(ImplementationTypeEnum.ClassValue, childPropertyName, childObj.GetType(), typeof(IChangeTracking));

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                parentObj.AcceptChanges();

                ObjectUtilities.PopulateObjectWithRandomValues(childObj);

                Assert.IsTrue(parentObj.IsChanged);

                if (childIsIChangeTracking)
                {
                    Assert.IsTrue(((IChangeTracking)childObj).IsChanged);
                }
            }

            parentObj.AcceptChanges();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(childObj);
            }

            Assert.IsTrue(parentObj.IsChanged);

            if (childIsIChangeTracking)
            {
                Assert.IsTrue(((IChangeTracking)childObj).IsChanged);
            }
        }

        #region IsChangedWhenListsChange

        /// <summary>
        /// Tests that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListPropertiesChange(Type type, bool addItems, bool removeItems, bool changeItems)
        {
            IsChangedWhenListPropertiesChange(CreateIChangeTrackingInstance(type), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="propertyNames">List of property names that implement <see cref="IList"/> and <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListPropertiesChange(Type type, List<string> propertyNames, bool addItems, bool removeItems, bool changeItems)
        {
            IsChangedWhenListPropertiesChange(CreateIChangeTrackingInstance(type), propertyNames, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="properties">List of properties that implement <see cref="IList"/> and <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListPropertiesChange(Type type, List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems)
        {
            IsChangedWhenListPropertiesChange(CreateIChangeTrackingInstance(type), properties, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListPropertiesChange<T>(bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListPropertiesChange(Activator.CreateInstance<T>(), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="propertyNames">List of property names that implement <see cref="IList"/> and <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListPropertiesChange<T>(List<string> propertyNames, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListPropertiesChange(Activator.CreateInstance<T>(), propertyNames, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="properties">List of properties that implement <see cref="IList"/> and <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListPropertiesChange<T>(List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListPropertiesChange(Activator.CreateInstance<T>(), properties, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with list properties to test.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListPropertiesChange<T>(T obj, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListPropertiesChange(obj, PropertyUtilities.GetListOfPropertiesWithListTypes(obj, true, true, false), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with list properties to test.</param>
        /// <param name="propertyNames">List of property names that implement <see cref="IList"/> and <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListPropertiesChange<T>(T obj, List<string> propertyNames, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListPropertiesChange(obj, new List<PropertyInfo>(propertyNames.Select(propertyName => obj.GetType().GetProperty(propertyName))), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="properties">List of properties that implement <see cref="IList"/> and <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListPropertiesChange<T>(T obj, List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            foreach (PropertyInfo property in properties)
            {
                IsChangedWhenListPropertyChanges(obj, property, addItems, removeItems, changeItems);
            }

            obj.AcceptChanges();

            foreach (PropertyInfo property in properties)
            {
                if (typeof(IList).IsAssignableFrom(property.PropertyType))
                {
                    ObjectUtilities.PopulateListWithRandomValues((IList)property.GetValue(obj));

                    if (typeof(IChangeTracking).IsAssignableFrom(property.PropertyType))
                    {
                        Assert.IsTrue(((IChangeTracking)property.GetValue(obj)).IsChanged);
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Tests that when lists change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="TParent">Type implements <see cref="IChangeTracking"/>.</typeparam>
        /// <typeparam name="TList">Type that implements <see cref="IChangeTrackingCollection{T}"/>.</typeparam>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="listPropertyNames">List of <see cref="IList"/> and property names.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListsChange<TParent, TList>(TParent obj, List<(IList list, string listPropertyName)> listPropertyNames, bool addItems, bool removeItems, bool changeItems) where TParent : IChangeTracking where TList : IList
        {
            foreach ((IList list, string propertyName) in listPropertyNames)
            {
                IsChangedWhenListChanges(obj, list, propertyName, addItems, removeItems, changeItems);
            }

            obj.AcceptChanges();

            foreach ((IList list, string propertyName) in listPropertyNames)
            {
                ObjectUtilities.PopulateListWithRandomValues(list);

                if (typeof(IChangeTracking).IsAssignableFrom(list.GetType()))
                {
                    Assert.IsTrue(((IChangeTracking)list).IsChanged);
                }
            }

            Assert.IsTrue(obj.IsChanged);
        }

        #region IsChangedWhenListChanges

        /// <summary>
        /// Tests that when a list property changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="propertyName">Name of list property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListPropertyChanges(Type type, string propertyName, bool addItems, bool removeItems, bool changeItems)
        {
            IsChangedWhenListPropertyChanges(CreateIChangeTrackingInstance(type), propertyName, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list property changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="property">List property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListPropertyChanges(Type type, PropertyInfo property, bool addItems, bool removeItems, bool changeItems)
        {
            IsChangedWhenListPropertyChanges(CreateIChangeTrackingInstance(type), property, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list property changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="propertyName">Name of list property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListPropertyChanges<T>(string propertyName, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListPropertyChanges<T>(typeof(T).GetProperty(propertyName), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list property changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="property">List property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListPropertyChanges<T>(PropertyInfo property, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListPropertyChanges(Activator.CreateInstance<T>(), property, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list property changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with list property.</param>
        /// <param name="propertyName">Name of list property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListPropertyChanges<T>(T obj, string propertyName, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListPropertyChanges(obj, obj.GetType().GetProperty(propertyName), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list property changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with list property.</param>
        /// <param name="property">List property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListPropertyChanges<TParent>(TParent obj, PropertyInfo property, bool addItems, bool removeItems, bool changeItems) where TParent : IChangeTracking
        {
            if (OutputUtilities.ExpectedImplementation(ImplementationTypeEnum.PropertyValue, property.Name, property.PropertyType, typeof(IList)))
            {
                IsChangedWhenListChanges(obj, (IList)property.GetValue(obj), property.Name, addItems, removeItems, changeItems);
            }
        }

        #endregion

        /// <summary>
        /// Tests that when a list changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <typeparam name="TList">Type that implements <see cref="IList"/>.</typeparam>
        /// <param name="obj">Object with list to test.</param>
        /// <param name="list">List to test.</param>
        /// <param name="listPropertyName">Name of property that contains the list.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListChanges<TParent, TList>(TParent obj, TList list, string listPropertyName, bool addItems, bool removeItems, bool changeItems) where TParent : IChangeTracking where TList : IList
        {
            if (addItems)
            {
                IsChangedWhenListItemsAdded(obj, list, listPropertyName);
            }

            if (removeItems)
            {
                IsChangedWhenListItemsRemoved(obj, list, listPropertyName);
            }

            if (changeItems)
            {
                IsChangedWhenListItemsChange(obj, list, listPropertyName);
            }
        }

        /// <summary>
        /// Tests that when items are added to a list, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <typeparam name="TList">Type that implements <see cref="IList"/>.</typeparam>
        /// <param name="obj">Object with list to add items to.</param>
        /// <param name="list">List to add items to.</param>
        /// <param name="listPropertyName">Name of property that contains the list.</param>
        public static void IsChangedWhenListItemsAdded<TParent, TList>(TParent obj, TList list, string listPropertyName) where TParent : IChangeTracking where TList : IList
        {
            bool listIsIChangeTracking = OutputUtilities.ExpectedImplementation(ImplementationTypeEnum.PropertyValue, listPropertyName, list.GetType(), typeof(IChangeTracking));

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.AcceptChanges();

                ObjectUtilities.AddRandomItemsToList(list);

                Assert.IsTrue(obj.IsChanged);

                if (listIsIChangeTracking)
                {
                    Assert.IsTrue(((IChangeTracking)list).IsChanged);
                }
            }

            obj.AcceptChanges();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.AddRandomItemsToList(list);
            }

            Assert.IsTrue(obj.IsChanged);

            if (listIsIChangeTracking)
            {
                Assert.IsTrue(((IChangeTracking)list).IsChanged);
            }
        }

        /// <summary>
        /// Tests that when items are removed from a list, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <typeparam name="TList">Type that implements <see cref="IList"/>.</typeparam>
        /// <param name="obj">Object with list to remove items from.</param>
        /// <param name="list">List to remove items from.</param>
        /// <param name="listPropertyName">Name of property that contains the list.</param>
        public static void IsChangedWhenListItemsRemoved<TParent, TList>(TParent obj, TList list, string listPropertyName) where TParent : IChangeTracking where TList : IList
        {
            bool listIsIChangeTracking = OutputUtilities.ExpectedImplementation(ImplementationTypeEnum.PropertyValue, listPropertyName, list.GetType(), typeof(IChangeTracking));

            if (list.Count == 0)
            {
                ObjectUtilities.PopulateListWithRandomValues(list);
            }

            while (list.Count > 0)
            {
                obj.AcceptChanges();

                list.RemoveAt(list.Count - 1);

                Assert.IsTrue(obj.IsChanged);

                if (listIsIChangeTracking)
                {
                    Assert.IsTrue(((IChangeTracking)list).IsChanged);
                }
            }

            ObjectUtilities.PopulateListWithRandomValues(list);
            obj.AcceptChanges();

            while (list.Count > 0)
            {
                list.RemoveAt(list.Count - 1);
            }

            Assert.IsTrue(obj.IsChanged);

            if (listIsIChangeTracking)
            {
                Assert.IsTrue(((IChangeTracking)list).IsChanged);
            }

            ObjectUtilities.PopulateListWithRandomValues(list);
            obj.AcceptChanges();

            list.Clear();

            Assert.IsTrue(obj.IsChanged);

            if (listIsIChangeTracking)
            {
                Assert.IsTrue(((IChangeTracking)list).IsChanged);
            }
        }

        /// <summary>
        /// Test that when items in a list change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <typeparam name="TList">Type that implements <see cref="IList"/>.</typeparam>
        /// <param name="obj">Object with list of items to change.</param>
        /// <param name="list">List with items to change.</param>
        /// <param name="listPropertyName">Name of property that contains the list.</param>
        public static void IsChangedWhenListItemsChange<TParent, TList>(TParent obj, TList list, string listPropertyName) where TParent : IChangeTracking where TList : IList
        {
            bool listIsIChangeTracking = OutputUtilities.ExpectedImplementation(ImplementationTypeEnum.PropertyValue, listPropertyName, list.GetType(), typeof(IChangeTracking));
            bool listItemsAreIsChangeTracking = OutputUtilities.ExpectedImplementation(ImplementationTypeEnum.ListItemValue, listPropertyName, list.GetType().GenericTypeArguments[0], typeof(IChangeTracking));

            if (list.Count == 0)
            {
                ObjectUtilities.PopulateListWithRandomValues(list);
            }

            obj.AcceptChanges();

            foreach (var item in list)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(item);

                Assert.IsTrue(obj.IsChanged);

                if (listIsIChangeTracking)
                {
                    Assert.IsTrue(((IChangeTracking)list).IsChanged);
                }

                if (listItemsAreIsChangeTracking)
                {
                    Assert.IsTrue(((IChangeTracking)item).IsChanged);
                }

                obj.AcceptChanges();
            }

            foreach (var item in list)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(item);
            }

            Assert.IsTrue(obj.IsChanged);

            if (listIsIChangeTracking)
            {
                Assert.IsTrue(((IChangeTracking)list).IsChanged);
            }

            foreach (IChangeTracking item in list)
            {
                Assert.IsTrue(item.IsChanged);
            }
        }

        /// <summary>
        /// Checks that the type implements <see cref="IChangeTracking"/> and creates a new instance of that type.
        /// </summary>
        /// <param name="type">Type to test and create.</param>
        /// <returns>A new instance of the type specified that implements <see cref="IChangeTracking"/>.</returns>
        private static IChangeTracking CreateIChangeTrackingInstance(Type type)
        {
            Assert.IsTrue(typeof(IChangeTracking).IsAssignableFrom(type));

            return (IChangeTracking)Activator.CreateInstance(type);
        }
    }
}
