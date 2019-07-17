// <copyright file="PropertyChangeAssert.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Reflection;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.TestAsserts
{
    /// <summary>
    /// Tests property changes within objects.
    /// </summary>
    public static class PropertyChangeAssert
    {
        /// <summary>
        /// Tests that an type's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        public static void ChangesValues<T>()
        {
            ChangesValues(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that an object's property values can be changed.
        /// </summary>
        /// <param name="typeToTest">Type of object to test.</param>
        public static void ChangesValues(Type typeToTest)
        {
            ChangesValues((dynamic)Activator.CreateInstance(typeToTest));
        }

        /// <summary>
        /// Test that an object's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="objectToTest">Object with properties to test properties.</param>
        public static void ChangesValues<T>(T objectToTest)
        {
            ChangesValues(PropertyUtilities.GetListOfPropertiesWithPublicGetAndSetMethods<T>(), objectToTest);

            foreach (PropertyInfo property in PropertyUtilities.GetListOfPropertiesWithClassValues(objectToTest))
            {
                ChangesValues((dynamic)property.GetValue(objectToTest));
            }
        }

        /// <summary>
        /// Tests that a type's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="propertyNamesToTest">List of property names to test.</param>
        public static void ChangesValues<T>(List<string> propertyNamesToTest)
        {
            ChangesValues(propertyNamesToTest, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that a type's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="propertiesToTest">List of properties to test.</param>
        public static void ChangesValues<T>(List<PropertyInfo> propertiesToTest)
        {
            ChangesValues(propertiesToTest, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that a type's property values can be changed.
        /// </summary>
        /// <param name="propertyNamesToTest">List of property names to test.</param>
        /// <param name="typeToTest">Type of object to test.</param>
        public static void ChangesValues(List<string> propertyNamesToTest, Type typeToTest)
        {
            dynamic obj = Activator.CreateInstance(typeToTest);
            ChangesValues(propertyNamesToTest, obj);
        }

        /// <summary>
        /// Tests that a type's property values can be changed.
        /// </summary>
        /// <param name="propertiesToTest">List of properties to test.</param>
        /// <param name="typeToTest">Type of object to test.</param>
        public static void ChangesValues(List<PropertyInfo> propertiesToTest, Type typeToTest)
        {
            dynamic obj = Activator.CreateInstance(typeToTest);
            ChangesValues(propertiesToTest, obj);
        }

        /// <summary>
        /// Tests that an object's propery values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="propertyNamesToTest">List of property names to test.</param>
        /// <param name="objectToTest">Object with properties to test properties.</param>
        public static void ChangesValues<T>(List<string> propertyNamesToTest, T objectToTest)
        {
            foreach (string propertyName in propertyNamesToTest)
            {
                ChangesValue(propertyName, objectToTest);
            }
        }

        /// <summary>
        /// Tests that an object's property values can be changed.
        /// </summary>
        /// <typeparam name="T">Type to test properties.</typeparam>
        /// <param name="propertiesToTest">List of properties to test.</param>
        /// <param name="objectToTest">Object with properties to test.</param>
        public static void ChangesValues<T>(List<PropertyInfo> propertiesToTest, T objectToTest)
        {
            foreach (PropertyInfo property in propertiesToTest)
            {
                ChangesValue(property, objectToTest);
            }
        }

        /// <summary>
        /// Test that a specific property within an object's value can be changed.
        /// </summary>
        /// <typeparam name="T">Type of object with the property to test.</typeparam>
        /// <param name="propertyName">Name of the property to test.</param>
        public static void ChangesValue<T>(string propertyName)
        {
            ChangesValue(typeof(T).GetProperty(propertyName), Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Test that a specific property within an object's value can be changed.
        /// </summary>
        /// <typeparam name="T">Type of object with the property to test.</typeparam>
        /// <param name="property">Property to test.</param>
        public static void ChangesValue<T>(PropertyInfo property)
        {
            ChangesValue(property, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that a specific property within an object's value can be changed.
        /// </summary>
        /// <param name="propertyName">Name of property to test.</param>
        /// <param name="typeToTest">Type of object to test.</param>
        public static void ChangesValue(string propertyName, Type typeToTest)
        {
            dynamic obj = Activator.CreateInstance(typeToTest);
            ChangesValue(propertyName, obj);
        }

        /// <summary>
        /// Tests that a specific property with an object can change it's value.
        /// </summary>
        /// <param name="property">Property to test.</param>
        /// <param name="typeToTest">Type of object to test.</param>
        public static void ChangesValue(PropertyInfo property, Type typeToTest)
        {
            dynamic obj = Activator.CreateInstance(typeToTest);
            ChangesValue(property, obj);
        }

        /// <summary>
        /// Test that a specific property within an object's value can be changed.
        /// </summary>
        /// <typeparam name="T">Type of object with the property to test.</typeparam>
        /// <param name="propertyName">Name of the property to test.</param>
        /// <param name="objectToTest">Object with the property to test.</param>
        public static void ChangesValue<T>(string propertyName, T objectToTest)
        {
            ChangesValue(typeof(T).GetProperty(propertyName), objectToTest);
        }

        /// <summary>
        /// Tests that a specific property of an object changes values.
        /// </summary>
        /// <typeparam name="T">Type of object to test.</typeparam>
        /// <param name="property">Property to test.</param>
        /// <param name="objectToTest">Object with property to test.</param>
        public static void ChangesValue<T>(PropertyInfo property, T objectToTest)
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                T randomObject = ObjectUtilities.CreateInstanceWithRandomValues<T>();
                property.SetValue(objectToTest, property.GetValue(randomObject));
                Assert.AreEqual(property.GetValue(randomObject), property.GetValue(objectToTest));
            }
        }
    }
}
