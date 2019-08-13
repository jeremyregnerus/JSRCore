// <copyright file="PropertyChangeAssert.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.TestAsserts
{
    /// <summary>
    /// Tests property changes within objects.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:DoNotUseRegions", Justification = "Regions used for signatures.")]
    public static class PropertyChangeAssert
    {
        #region ChangesValues

        /// <summary>
        /// Tests that an object's property values can be changed.
        /// </summary>
        /// <param name="type">Type of object to test.</param>
        public static void ChangesValues(Type type)
        {
            ChangesValues(Activator.CreateInstance(type));
        }

        /// <summary>
        /// Tests that a type's property values can be changed.
        /// </summary>
        /// <param name="type">Type of object to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void ChangesValues(Type type, List<string> propertyNames)
        {
            ChangesValues(Activator.CreateInstance(type), propertyNames);
        }

        /// <summary>
        /// Tests that a type's property values can be changed.
        /// </summary>
        /// <param name="type">Type of object to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void ChangesValues(Type type, List<PropertyInfo> properties)
        {
            ChangesValues(Activator.CreateInstance(type), properties);
        }

        /// <summary>
        /// Tests that an type's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        public static void ChangesValues<T>()
        {
            ChangesValues(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that a type's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void ChangesValues<T>(List<string> propertyNames)
        {
            ChangesValues(Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Tests that a type's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="properties">List of properties to test.</param>
        public static void ChangesValues<T>(List<PropertyInfo> properties)
        {
            ChangesValues(Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Test that an object's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="obj">Object with properties to test properties.</param>
        public static void ChangesValues<T>(T obj)
        {
            ChangesValues(obj, PropertyUtilities.GetListOfReadWriteProperties(obj));
        }

        /// <summary>
        /// Tests that an object's propery values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="obj">Object with properties to test properties.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void ChangesValues<T>(T obj, List<string> propertyNames)
        {
            foreach (string propertyName in propertyNames)
            {
                ChangesValue(obj, propertyName);
            }
        }

        /// <summary>
        /// Tests that an object's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void ChangesValues<T>(T obj, List<PropertyInfo> properties)
        {
            foreach (PropertyInfo property in properties)
            {
                ChangesValue(obj, property);
            }
        }

        #endregion

        #region ChangesValue

        /// <summary>
        /// Tests that a specific property within an object's value can be changed.
        /// </summary>
        /// <param name="type">Type of object to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void ChangesValue(Type type, string propertyName)
        {
            ChangesValue(Activator.CreateInstance(type), propertyName);
        }

        /// <summary>
        /// Tests that a specific property with an object can change it's value.
        /// </summary>
        /// <param name="type">Type of object to test.</param>
        /// <param name="property">Property to test.</param>
        public static void ChangesValue(Type type, PropertyInfo property)
        {
            ChangesValue(Activator.CreateInstance(type), property);
        }

        /// <summary>
        /// Test that a specific property within an object's value can be changed.
        /// </summary>
        /// <typeparam name="T">Type of object with the property to test.</typeparam>
        /// <param name="propertyName">Name of the property to test.</param>
        public static void ChangesValue<T>(string propertyName)
        {
            ChangesValue(Activator.CreateInstance<T>(), typeof(T).GetProperty(propertyName));
        }

        /// <summary>
        /// Test that a specific property within an object's value can be changed.
        /// </summary>
        /// <typeparam name="T">Type of object with the property to test.</typeparam>
        /// <param name="property">Property to test.</param>
        public static void ChangesValue<T>(PropertyInfo property)
        {
            ChangesValue(Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Test that a specific property within an object's value can be changed.
        /// </summary>
        /// <typeparam name="T">Type of object with the property to test.</typeparam>
        /// <param name="obj">Object with the property to test.</param>
        /// <param name="propertyName">Name of the property to test.</param>
        public static void ChangesValue<T>(T obj, string propertyName)
        {
            ChangesValue(obj, obj.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Tests that a specific property of an object changes values.
        /// </summary>
        /// <typeparam name="T">Type of object to test.</typeparam>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="property">Property to test.</param>
        public static void ChangesValue<T>(T obj, PropertyInfo property)
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                var randomObject = ObjectUtilities.CreateInstanceWithRandomValues(obj.GetType());
                property.SetValue(obj, property.GetValue(randomObject));
                Assert.AreEqual(property.GetValue(randomObject), property.GetValue(obj));
            }

            if (PropertyUtilities.CheckIfPropertyIsClass(property))
            {
                ChangesValues(property.GetValue(obj));
            }
        }

        #endregion
    }
}
