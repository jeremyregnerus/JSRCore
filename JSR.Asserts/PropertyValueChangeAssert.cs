// <copyright file="PropertyValueChangeAssert.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Reflection;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.Asserts
{
    /// <summary>
    /// Tests property changes within objects.
    /// </summary>
    public static class PropertyValueChangeAssert
    {
        #region PropertiesChangeValues

        /// <summary>
        /// Tests that an object's property values can be changed.
        /// </summary>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="type">Type of object to test.</param>
        public static void PropertiesChangeValues(this Assert assert, Type type)
        {
            assert.PropertiesChangeValues(Activator.CreateInstance(type));
        }

        /// <summary>
        /// Tests that a type's property values can be changed.
        /// </summary>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="type">Type of object to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void PropertiesChangeValues(this Assert assert, Type type, List<string> propertyNames)
        {
            assert.PropertiesChangeValues(Activator.CreateInstance(type), propertyNames);
        }

        /// <summary>
        /// Tests that a type's property values can be changed.
        /// </summary>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="type">Type of object to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void PropertiesChangeValues(this Assert assert, Type type, List<PropertyInfo> properties)
        {
            assert.PropertiesChangeValues(Activator.CreateInstance(type), properties);
        }

        /// <summary>
        /// Tests that an type's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        public static void PropertiesChangeValues<T>(this Assert assert)
        {
            assert.PropertiesChangeValues(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that a type's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void PropertiesChangeValues<T>(this Assert assert, List<string> propertyNames)
        {
            assert.PropertiesChangeValues(Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Tests that a type's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void PropertiesChangeValues<T>(this Assert assert, List<PropertyInfo> properties)
        {
            assert.PropertiesChangeValues(Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Test that an object's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="obj">Object with properties to test properties.</param>
        public static void PropertiesChangeValues<T>(this Assert assert, T obj)
        {
            assert.PropertiesChangeValues(obj, PropertyUtilities.GetReadWriteProperties(obj));
        }

        /// <summary>
        /// Tests that an object's propery values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="obj">Object with properties to test properties.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void PropertiesChangeValues<T>(this Assert assert, T obj, List<string> propertyNames)
        {
            foreach (string propertyName in propertyNames)
            {
                assert.PropertyChangesValue(obj, propertyName);
            }
        }

        /// <summary>
        /// Tests that an object's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void PropertiesChangeValues<T>(this Assert assert, T obj, List<PropertyInfo> properties)
        {
            foreach (PropertyInfo property in properties)
            {
                assert.PropertyChangesValue(obj, property);
            }
        }

        #endregion

        #region PropertyChangesValue

        /// <summary>
        /// Tests that a specific property within an object's value can be changed.
        /// </summary>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="type">Type of object to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void PropertyChangesValue(this Assert assert, Type type, string propertyName)
        {
            assert.PropertyChangesValue(Activator.CreateInstance(type), propertyName);
        }

        /// <summary>
        /// Tests that a specific property with an object can change it's value.
        /// </summary>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="type">Type of object to test.</param>
        /// <param name="property">Property to test.</param>
        public static void PropertyChangesValue(this Assert assert, Type type, PropertyInfo property)
        {
            assert.PropertyChangesValue(Activator.CreateInstance(type), property);
        }

        /// <summary>
        /// Test that a specific property within an object's value can be changed.
        /// </summary>
        /// <typeparam name="T">Type of object with the property to test.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="propertyName">Name of the property to test.</param>
        public static void PropertyChangesValue<T>(this Assert assert, string propertyName)
        {
            assert.PropertyChangesValue(Activator.CreateInstance<T>(), typeof(T).GetProperty(propertyName));
        }

        /// <summary>
        /// Test that a specific property within an object's value can be changed.
        /// </summary>
        /// <typeparam name="T">Type of object with the property to test.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="property">Property to test.</param>
        public static void PropertyChangesValue<T>(this Assert assert, PropertyInfo property)
        {
            assert.PropertyChangesValue(Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Test that a specific property within an object's value can be changed.
        /// </summary>
        /// <typeparam name="T">Type of object with the property to test.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="obj">Object with the property to test.</param>
        /// <param name="propertyName">Name of the property to test.</param>
        public static void PropertyChangesValue<T>(this Assert assert, T obj, string propertyName)
        {
            assert.PropertyChangesValue(obj, typeof(T).GetProperty(propertyName));
        }

        /// <summary>
        /// Tests that a specific property of an object changes values.
        /// </summary>
        /// <typeparam name="T">Type of object to test.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="property">Property to test.</param>
        public static void PropertyChangesValue<T>(this Assert assert, T obj, PropertyInfo property)
        {
            // for a random number of times
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                // create a new value for the property
                dynamic randomValue = RandomUtilities.GetRandom(property.PropertyType);

                // set the property to the new value
                property.SetValue(obj, randomValue);

                // assert that the property value equals the new value
                Assert.AreEqual(randomValue, property.GetValue(obj));
            }

            // if the property type is a class
            if (PropertyUtilities.IsClassProperty(property))
            {
                // check the properties within that class
                assert.PropertiesChangeValues(property.GetValue(obj));
            }
        }

        #endregion
    }
}
