// <copyright file="PropertyUtilities.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JSR.Utilities
{
    /// <summary>
    /// Provides Utilties for Class <see cref="PropertyInfo"/> properties.
    /// </summary>
    public static class PropertyUtilities
    {
        /// <summary>
        /// Checks if a property has a public get method for a Type.
        /// </summary>
        /// <typeparam name="T">Type with the named property.</typeparam>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property has a public get method, otherwise false.</returns>
        public static bool CheckIfPropertyHasPublicGetMethod<T>(string propertyName)
        {
            return CheckIfPropertyHasPublicGetMethod(typeof(T).GetRuntimeProperty(propertyName));
        }

        /// <summary>
        /// Checks if a property has a public get method for an object.
        /// </summary>
        /// <typeparam name="T">Type of object with named property.</typeparam>
        /// <param name="propertyName">Name of the property to evaluate.</param>
        /// <param name="objectWithProperty">Object with named property.</param>
        /// <returns>True if the property has a public get method, otherwise false.</returns>
        public static bool CheckIfPropertyHasPublicGetMethod<T>(string propertyName, T objectWithProperty)
        {
            return CheckIfPropertyHasPublicGetMethod(objectWithProperty.GetType().GetRuntimeProperty(propertyName));
        }

        /// <summary>
        /// Checks if a property has a public get method.
        /// </summary>
        /// <param name="property"><see cref="PropertyInfo"/> to check for public Get method.</param>
        /// <returns>True if the property has a public get method, otherwise false.</returns>
        public static bool CheckIfPropertyHasPublicGetMethod(PropertyInfo property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property), $"The argument {nameof(property)} cannot be null. Ensure the property exists.");
            }

            return property.GetMethod != null && property.GetMethod.IsPublic;
        }

        /// <summary>
        /// Checks if the property has a public set method.
        /// </summary>
        /// <typeparam name="T">Type that contains the named property.</typeparam>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property has a public set method, otherwise false.</returns>
        public static bool CheckIfPropertyHasPublicSetMethod<T>(string propertyName)
        {
            return CheckIfPropertyHasPublicSetMethod(typeof(T).GetRuntimeProperty(propertyName));
        }

        /// <summary>
        /// Checks if the property has a public set method.
        /// </summary>
        /// <typeparam name="T">Type that contains the named property.</typeparam>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <param name="objectWithProperty">Object that contains the named property.</param>
        /// <returns>True if the property has a public set method, otherwise false.</returns>
        public static bool CheckIfPropertyHasPublicSetMethod<T>(string propertyName, T objectWithProperty)
        {
            return CheckIfPropertyHasPublicSetMethod(objectWithProperty.GetType().GetRuntimeProperty(propertyName));
        }

        /// <summary>
        /// Checks if the property has a public set method.
        /// </summary>
        /// <param name="property"><see cref="PropertyInfo"/> to check for public Set method.</param>
        /// <returns>True if the property has a public set method, otherwise false.</returns>
        public static bool CheckIfPropertyHasPublicSetMethod(PropertyInfo property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property), $"The argument {nameof(property)} cannot be null. Ensure the property exists.");
            }

            return property.SetMethod != null && property.SetMethod.IsPublic;
        }

        /// <summary>
        /// Get a list of <see cref="PropertyInfo"/> that have public Get methods.
        /// </summary>
        /// <typeparam name="T">Type that contains properties.</typeparam>
        /// <returns>A list of <see cref="PropertyInfo"/> that have public Get methods.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithPublicGetMethod<T>()
        {
            return new List<PropertyInfo>(typeof(T).GetRuntimeProperties().Where(p => p.GetMethod.IsPublic));
        }

        /// <summary>
        /// Get a list of <see cref="PropertyInfo"/> that have public Get methods.
        /// </summary>
        /// <typeparam name="T">Type that contains properties.</typeparam>
        /// <param name="objectWithProperties">Object that contains properties.</param>
        /// <returns>A list of <see cref="PropertyInfo"/> that have public Get methods.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithPublicGetMethod<T>(T objectWithProperties)
        {
            return new List<PropertyInfo>(objectWithProperties.GetType().GetRuntimeProperties().Where(p => p.GetMethod.IsPublic));
        }

        /// <summary>
        /// Get a list of <see cref="PropertyInfo"/> that have public Get methods.
        /// </summary>
        /// <param name="type">Type that has properties.</param>
        /// <returns>A list of <see cref="PropertyInfo"/> that have public Get methods.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithPublicGetMethod(Type type)
        {
            return new List<PropertyInfo>(type.GetRuntimeProperties().Where(p => p.GetMethod.IsPublic));
        }

        /// <summary>
        /// Get a list of <see cref="PropertyInfo"/> that have public Set methods.
        /// </summary>
        /// <typeparam name="T">Type that contains properties.</typeparam>
        /// <returns>A list of <see cref="PropertyInfo"/> that have public Set methods.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithPublicSetMethod<T>()
        {
            return new List<PropertyInfo>(typeof(T).GetRuntimeProperties().Where(x => x.SetMethod.IsPublic));
        }

        /// <summary>
        /// Get a list of <see cref="PropertyInfo"/> that have public Set methods.
        /// </summary>
        /// <typeparam name="T">Type that contains properties.</typeparam>
        /// <param name="objectWithProperties">Object that contains properties.</param>
        /// <returns>A list of <see cref="PropertyInfo"/> that have public Set methods.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithPublicSetMethod<T>(T objectWithProperties)
        {
            return new List<PropertyInfo>(objectWithProperties.GetType().GetRuntimeProperties().Where(x => x.SetMethod.IsPublic));
        }

        /// <summary>
        /// Get a list of <see cref="PropertyInfo"/> that have public Set methods.
        /// </summary>
        /// <param name="type">Type that contains properties.</param>
        /// <returns>A list of <see cref="PropertyInfo"/> that have public Set methods.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithPublicSetMethod(Type type)
        {
            return new List<PropertyInfo>(type.GetRuntimeProperties().Where(x => x.SetMethod.IsPublic));
        }

        /// <summary>
        /// Get a list of property names that have public Get methods.
        /// </summary>
        /// <typeparam name="T">Type that contains properties.</typeparam>
        /// <returns>A list of property names that have public get methods.</returns>
        public static List<string> GetListOfPropertyNamesWithPublicGetMethod<T>()
        {
            return new List<string>(typeof(T).GetRuntimeProperties().Where(x => x.GetMethod.IsPublic).Select(x => x.Name));
        }

        /// <summary>
        /// Get a list of property names that have public Get methods.
        /// </summary>
        /// <typeparam name="T">Type that contains properties.</typeparam>
        /// <param name="objectWithProperties">Object that contains properties.</param>
        /// <returns>A list of property names that have public get methods.</returns>
        public static List<string> GetListOfPropertyNamesWithPublicGetMethod<T>(T objectWithProperties)
        {
            return new List<string>(objectWithProperties.GetType().GetRuntimeProperties().Where(x => x.GetMethod.IsPublic).Select(x => x.Name));
        }

        /// <summary>
        /// Get a list of property names that have public Get methods.
        /// </summary>
        /// <param name="type">Type that contains properties.</param>
        /// <returns>A list of property names that have public get methods.</returns>
        public static List<string> GetListOfPropertyNamesWithPublicGetMethod(Type type)
        {
            return new List<string>(type.GetRuntimeProperties().Where(x => x.GetMethod.IsPublic).Select(x => x.Name));
        }

        /// <summary>
        /// Get a list of property names that have public set methods.
        /// </summary>
        /// <typeparam name="T">Type that contains properties.</typeparam>
        /// <returns>A list of property names that have public set methods.</returns>
        public static List<string> GetListOfPropertyNamesWithPublicSetMethod<T>()
        {
            return new List<string>(typeof(T).GetRuntimeProperties().Where(x => x.SetMethod.IsPublic).Select(x => x.Name));
        }

        /// <summary>
        /// Get a list of property names that have public set methods.
        /// </summary>
        /// <typeparam name="T">Type that contains properties.</typeparam>
        /// <param name="objectWithProperties">Object that contains properties.</param>
        /// <returns>A list of property names that have public set methods.</returns>
        public static List<string> GetListOfPropertyNamesWithPublicSetMethod<T>(T objectWithProperties)
        {
            return new List<string>(objectWithProperties.GetType().GetRuntimeProperties().Where(x => x.SetMethod.IsPublic).Select(x => x.Name));
        }

        /// <summary>
        /// Get a list of property names that have public set methods.
        /// </summary>
        /// <param name="type">Type that contains properties.</param>
        /// <returns>A list of property names that have public set methods.</returns>
        public static List<string> GetListOfPropertyNamesWithPublicSetMethod(Type type)
        {
            return new List<string>(type.GetRuntimeProperties().Where(x => x.SetMethod.IsPublic).Select(x => x.Name));
        }
    }
}
