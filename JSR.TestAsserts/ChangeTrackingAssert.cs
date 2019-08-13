// <copyright file="ChangeTrackingAssert.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
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

                foreach (PropertyInfo property in PropertyUtilities.GetListOfPropertiesWithClassTypes(obj, true, true, true).Where(property => typeof(IChangeTracking).IsAssignableFrom(property.PropertyType)))
                {
                    Assert.IsFalse(((IChangeTracking)property.GetValue(obj)).IsChanged);
                }

                foreach (PropertyInfo property in PropertyUtilities.GetListOfPropertiesWithListTypes(obj, true, true, true).Where(property => typeof(IChangeTracking).IsAssignableFrom(property.PropertyType)))
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
            IsChangedWhenChanged(obj, PropertyUtilities.GetListOfProperties(obj, true, true, true, true, true, true));
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
                IsChangedWhenChildClassChanges(obj, property);
            }

            if (PropertyUtilities.CheckIfPropertyIsList(property))
            {
                IsChangedWhenListChanges(obj, property, true, true, true);
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
            foreach (string propertyName in propertyNames)
            {
                IsChangedWhenPropertyChanges(obj, propertyName);
            }
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
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.AcceptChanges();

                ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);

                Assert.IsTrue(obj.IsChanged);
            }
        }

        #endregion

        #region IsChangedWhenChildClassesChange

        /// <summary>
        /// Tests that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> with class properties to test.</param>
        public static void IsChangedWhenChildClassesChange(Type type)
        {
            IsChangedWhenChildClassesChange(CreateIChangeTrackingInstance(type));
        }

        /// <summary>
        /// Tests that when the child class properties change, the parent's Ischanged property is changed to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> with class properties.</param>
        /// <param name="propertyNames">List of property names that implement <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenChildClassesChange(Type type, List<string> propertyNames)
        {
            IsChangedWhenChildClassesChange(CreateIChangeTrackingInstance(type), propertyNames);
        }

        /// <summary>
        /// Tests that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> with class properties.</param>
        /// <param name="properties">List of properties that implement <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenChildClassesChange(Type type, List<PropertyInfo> properties)
        {
            IsChangedWhenChildClassesChange(CreateIChangeTrackingInstance(type), properties);
        }

        /// <summary>
        /// Tests that when the child class properties change, the parent's IsChanged state is changed to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> with class properties.</typeparam>
        public static void IsChangedWhenChildClassesChange<T>() where T : IChangeTracking
        {
            IsChangedWhenChildClassesChange(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> with class properties to test.</typeparam>
        /// <param name="propertyNames">List of property names that implement <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenChildClassesChange<T>(List<string> propertyNames) where T : IChangeTracking
        {
            IsChangedWhenChildClassesChange(Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Tests that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> with class properties to test.</typeparam>
        /// <param name="properties">List of properties that implement <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenChildClassesChange<T>(List<PropertyInfo> properties) where T : IChangeTracking
        {
            IsChangedWhenChildClassesChange(Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Tests that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> with class properties.</typeparam>
        /// <param name="obj">Object to test.</param>
        public static void IsChangedWhenChildClassesChange<T>(T obj) where T : IChangeTracking
        {
            IsChangedWhenChildClassesChange(obj, PropertyUtilities.GetListOfPropertiesWithClassTypes(obj, true, true, true));
        }

        /// <summary>
        /// Tests that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyNames">List of class property names to test. Each class should implement <see cref="IChangeTracking"/>.</param>
        public static void IsChangedWhenChildClassesChange<T>(T obj, List<string> propertyNames) where T : IChangeTracking
        {
            foreach (string propertyName in propertyNames)
            {
                IsChangedWhenChildClassChanges(obj, propertyName);
            }
        }

        /// <summary>
        /// Tests that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">List of class properties to test. Each class should implement <see cref="IChangeTracking"/>.</param>
        public static void IsChangedWhenChildClassesChange<T>(T obj, List<PropertyInfo> properties) where T : IChangeTracking
        {
            foreach (PropertyInfo property in properties)
            {
                IsChangedWhenChildClassChanges(obj, property);
            }
        }

        /// <summary>
        /// Tests that when the child classes change, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="IChangeTracking"/> with child classes.</typeparam>
        /// <param name="parent">Parent object that contains child classes contained within the list.</param>
        /// <param name="children">Class objects contained within the properties of the object being tested.</param>
        public static void IsChangedWhenChildClassesChange<TParent>(TParent parent, List<IChangeTracking> children) where TParent : IChangeTracking
        {
            foreach (IChangeTracking child in children)
            {
                IsChangedWhenChildClassChanges(parent, child);
            }
        }

        #endregion

        #region IsChangedWhenChildClassChanges

        /// <summary>
        /// Tests that when a property with a class value changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="propertyName">Name of property that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenChildClassChanges(Type type, string propertyName)
        {
            IsChangedWhenChildClassChanges(CreateIChangeTrackingInstance(type), propertyName);
        }

        /// <summary>
        /// Tests that when a property with a class value changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="property">Property that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenChildClassChanges(Type type, PropertyInfo property)
        {
            IsChangedWhenChildClassChanges(CreateIChangeTrackingInstance(type), property);
        }

        /// <summary>
        /// Tests that when a property with a class value changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="propertyName">Name of property that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenChildClassChanges<T>(string propertyName) where T : IChangeTracking
        {
            IsChangedWhenChildClassChanges<T>(typeof(T).GetProperty(propertyName));
        }

        /// <summary>
        /// Tests that when a property with a class value changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="property">Property that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenChildClassChanges<T>(PropertyInfo property) where T : IChangeTracking
        {
            IsChangedWhenChildClassChanges(Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Tests that when a property with a class value changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="propertyName">Name of property that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenChildClassChanges<T>(T obj, string propertyName) where T : IChangeTracking
        {
            IsChangedWhenChildClassChanges(obj, obj.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Tests that when a property with a class value changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="property">Property that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenChildClassChanges<T>(T obj, PropertyInfo property) where T : IChangeTracking
        {
            Assert.IsTrue(typeof(IChangeTracking).IsAssignableFrom(property.PropertyType));

            IsChangedWhenChildClassChanges(obj, (IChangeTracking)property.GetValue(obj));
        }

        /// <summary>
        /// Tests that when a child class changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="IChangeTracking"/> and contains the TChild type.</typeparam>
        /// <typeparam name="TChild">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="parentObj">Parent object to test.</param>
        /// <param name="childObj">Child object to change.</param>
        public static void IsChangedWhenChildClassChanges<TParent, TChild>(TParent parentObj, TChild childObj) where TParent : IChangeTracking where TChild : IChangeTracking
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                parentObj.AcceptChanges();

                ObjectUtilities.PopulateObjectWithRandomValues(childObj);

                Assert.IsTrue(childObj.IsChanged);
                Assert.IsTrue(parentObj.IsChanged);
            }
        }

        #endregion

        #region IsChangedWhenListsChange

        /// <summary>
        /// Tests that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListsChange(Type type, bool addItems, bool removeItems, bool changeItems)
        {
            IsChangedWhenListsChange(CreateIChangeTrackingInstance(type), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="propertyNames">List of property names that implement <see cref="IList"/> and <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListsChange(Type type, List<string> propertyNames, bool addItems, bool removeItems, bool changeItems)
        {
            IsChangedWhenListsChange(CreateIChangeTrackingInstance(type), propertyNames, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="properties">List of properties that implement <see cref="IList"/> and <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListsChange(Type type, List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems)
        {
            IsChangedWhenListsChange(CreateIChangeTrackingInstance(type), properties, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListsChange<T>(bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListsChange(Activator.CreateInstance<T>(), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="propertyNames">List of property names that implement <see cref="IList"/> and <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListsChange<T>(List<string> propertyNames, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListsChange(Activator.CreateInstance<T>(), propertyNames, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="properties">List of properties that implement <see cref="IList"/> and <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListsChange<T>(List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListsChange(Activator.CreateInstance<T>(), properties, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with list properties to test.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListsChange<T>(T obj, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListsChange(obj, PropertyUtilities.GetListOfPropertiesWithListTypes(obj, true, true, true), addItems, removeItems, changeItems);
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
        public static void IsChangedWhenListsChange<T>(T obj, List<string> propertyNames, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            foreach (string propertyName in propertyNames)
            {
                IsChangedWhenListChanges(obj, propertyName, addItems, removeItems, changeItems);
            }
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
        public static void IsChangedWhenListsChange<T>(T obj, List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            foreach (PropertyInfo property in properties)
            {
                IsChangedWhenListChanges(obj, property, addItems, removeItems, changeItems);
            }
        }

        /// <summary>
        /// Tests that when lists change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="lists">List of lists that implement <see cref="IChangeTracking"/> to test.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListsChange<T>(T obj, List<IList> lists, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            foreach (IList list in lists)
            {
                IsChangedWhenListChanges(obj, list, addItems, removeItems, changeItems);
            }
        }

        #endregion

        #region IsChangedWhenListChanges

        /// <summary>
        /// Tests that when a list property changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="propertyName">Name of list property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListChanges(Type type, string propertyName, bool addItems, bool removeItems, bool changeItems)
        {
            IsChangedWhenListChanges(CreateIChangeTrackingInstance(type), propertyName, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list property changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="property">List property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListChanges(Type type, PropertyInfo property, bool addItems, bool removeItems, bool changeItems)
        {
            IsChangedWhenListChanges(CreateIChangeTrackingInstance(type), property, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list property changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="propertyName">Name of list property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListChanges<T>(string propertyName, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListChanges<T>(typeof(T).GetProperty(propertyName), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list property changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="property">List property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListChanges<T>(PropertyInfo property, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListChanges(Activator.CreateInstance<T>(), property, addItems, removeItems, changeItems);
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
        public static void IsChangedWhenListChanges<T>(T obj, string propertyName, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListChanges(obj, obj.GetType().GetProperty(propertyName), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list property changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with list property.</param>
        /// <param name="property">List property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListChanges<T>(T obj, PropertyInfo property, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            Assert.IsTrue(typeof(IList).IsAssignableFrom(property.PropertyType));

            IsChangedWhenListChanges(obj, (IList)property.GetValue(obj), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Tests that when a list changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with list to test.</param>
        /// <param name="list">List to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListChanges<T>(T obj, IList list, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            if (addItems)
            {
                IsChangedWhenListItemsAdded(obj, list);
            }

            if (removeItems)
            {
                IsChangedWhenListItemsRemoved(obj, list);
            }

            if (changeItems)
            {
                IsChangedWhenListItemsChange(obj, list);
            }
        }

        #endregion

        /// <summary>
        /// Tests that when items are added to a list, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with list.</param>
        /// <param name="list">List to add items to.</param>
        public static void IsChangedWhenListItemsAdded<T>(T obj, IList list) where T : IChangeTracking
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.AcceptChanges();

                ObjectUtilities.AddRandomItemsToList(list);

                Assert.IsTrue(obj.IsChanged);
            }
        }

        /// <summary>
        /// Tests that when items are removed from a list, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with list to test.</param>
        /// <param name="list">List to remove items from.</param>
        public static void IsChangedWhenListItemsRemoved<T>(T obj, IList list) where T : IChangeTracking
        {
            if (list.Count == 0)
            {
                ObjectUtilities.PopulateListWithRandomValues(list);
            }

            while (list.Count > 0)
            {
                obj.AcceptChanges();

                list.RemoveAt(list.Count - 1);

                Assert.IsTrue(obj.IsChanged);
            }
        }

        /// <summary>
        /// Test that when items in a list change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object with list to test.</param>
        /// <param name="list">List with items to change.</param>
        public static void IsChangedWhenListItemsChange<T>(T obj, IList list) where T : IChangeTracking
        {
            if (list.Count == 0)
            {
                ObjectUtilities.PopulateListWithRandomValues(list);
            }

            foreach (IChangeTracking item in list)
            {
                obj.AcceptChanges();

                ObjectUtilities.PopulateObjectWithRandomValues(item);

                Assert.IsTrue(item.IsChanged);
                Assert.IsTrue(obj.IsChanged);
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
