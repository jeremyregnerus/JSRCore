// <copyright file="PropertyValueChangeAssert.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Reflection;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.Asserts
{
    /// <summary>
    /// Asserts property changes within objects.
    /// </summary>
    public static class PropertyValueChangeAssert
    {
        #region PropertiesChangeValues

        /// <summary>
        /// Asserts that an object's property values can be changed.
        /// </summary>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="type">Type of object to test.</param>
        public static void PropertiesChangeValues(this Assert assert, Type type)
        {
            PropertiesChangeValues(assert, Activator.CreateInstance(type));
        }

        /// <summary>
        /// Asserts that a type's property values can be changed.
        /// </summary>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="type">Type of object to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void PropertiesChangeValues(this Assert assert, Type type, List<string> propertyNames)
        {
            PropertiesChangeValues(assert, Activator.CreateInstance(type), propertyNames);
        }

        /// <summary>
        /// Asserts that a type's property values can be changed.
        /// </summary>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="type">Type of object to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void PropertiesChangeValues(this Assert assert, Type type, List<PropertyInfo> properties)
        {
            PropertiesChangeValues(assert, Activator.CreateInstance(type), properties);
        }

        /// <summary>
        /// Asserts that an type's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        public static void PropertiesChangeValues<T>(this Assert assert)
        {
            PropertiesChangeValues(assert, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Asserts that a type's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void PropertiesChangeValues<T>(this Assert assert, List<string> propertyNames)
        {
            PropertiesChangeValues(assert, Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Asserts that a type's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void PropertiesChangeValues<T>(this Assert assert, List<PropertyInfo> properties)
        {
            PropertiesChangeValues(assert, Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Asserts that an object's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="obj">Object with properties to test properties.</param>
        public static void PropertiesChangeValues<T>(this Assert assert, T obj)
        {
            PropertiesChangeValues(assert, obj, PropertyUtilities.GetReadWriteProperties(obj));
        }

        /// <summary>
        /// Asserts that an object's propery values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="obj">Object with properties to test properties.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void PropertiesChangeValues<T>(this Assert assert, T obj, List<string> propertyNames)
        {
            foreach (string propertyName in propertyNames)
            {
                PropertyChangesValue(assert, obj, propertyName);
            }
        }

        /// <summary>
        /// Asserts that an object's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void PropertiesChangeValues<T>(this Assert assert, T obj, List<PropertyInfo> properties)
        {
            foreach (PropertyInfo property in properties)
            {
                PropertyChangesValue(assert, obj, property);
            }
        }

        #endregion

        #region PropertyChangesValue

        /// <summary>
        /// Asserts that a specific property within an object's value can be changed.
        /// </summary>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="type">Type of object to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void PropertyChangesValue(this Assert assert, Type type, string propertyName)
        {
            PropertyChangesValue(assert, Activator.CreateInstance(type), propertyName);
        }

        /// <summary>
        /// Asserts that a specific property with an object can change it's value.
        /// </summary>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="type">Type of object to test.</param>
        /// <param name="property">Property to test.</param>
        public static void PropertyChangesValue(this Assert assert, Type type, PropertyInfo property)
        {
            PropertyChangesValue(assert, Activator.CreateInstance(type), property);
        }

        /// <summary>
        /// Asserts that a specific property within an object's value can be changed.
        /// </summary>
        /// <typeparam name="T">Type of object with the property to test.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="propertyName">Name of the property to test.</param>
        public static void PropertyChangesValue<T>(this Assert assert, string propertyName)
        {
            PropertyChangesValue(assert, Activator.CreateInstance<T>(), typeof(T).GetProperty(propertyName)!);
        }

        /// <summary>
        /// Asserts that a specific property within an object's value can be changed.
        /// </summary>
        /// <typeparam name="T">Type of object with the property to test.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="property">Property to test.</param>
        public static void PropertyChangesValue<T>(this Assert assert, PropertyInfo property)
        {
            PropertyChangesValue(assert, Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Asserts that a specific property within an object's value can be changed.
        /// </summary>
        /// <typeparam name="T">Type of object with the property to test.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="obj">Object with the property to test.</param>
        /// <param name="propertyName">Name of the property to test.</param>
        public static void PropertyChangesValue<T>(this Assert assert, T obj, string propertyName)
        {
            PropertyChangesValue(assert, obj, typeof(T).GetProperty(propertyName)!);
        }

        /// <summary>
        /// Asserts that a specific property of an object changes values.
        /// </summary>
        /// <typeparam name="T">Type of object to test.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="property">Property to test.</param>
        public static void PropertyChangesValue<T>(this Assert assert, T obj, PropertyInfo property)
        {
            // create a new value for the property
            dynamic randomValue = RandomUtilities.GetRandom(property.PropertyType);

            // set the property to the new value
            property.SetValue(obj, randomValue);

            // assert that the property value equals the new value
            Assert.AreEqual(randomValue, property.GetValue(obj));

            // if the property type is a class
            if (PropertyUtilities.IsClassProperty(property))
            {
                // check the properties within that class
                PropertiesChangeValues(assert, property.GetValue(obj));
            }
        }

        #endregion
    }
}
