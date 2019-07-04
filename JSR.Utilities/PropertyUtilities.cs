// <copyright file="PropertyUtilities.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace JSR.Utilities
{
    /// <summary>
    /// Provides Utilties for Class <see cref="PropertyInfo"/> properties.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:DoNotUseRegions", Justification = "Used for Signature separation.")]
    public static class PropertyUtilities
    {
        #region CheckIfPropertyHasPublicGetAndSetMethod

        /// <summary>
        /// Checks if a property has a public Get and Set method for a Type.
        /// </summary>
        /// <typeparam name="T">Type of object that contains the named property.</typeparam>
        /// <param name="propertyName">Name of the property to check.</param>
        /// <returns>True if the property is both public Get and Set.</returns>
        public static bool CheckIfPropertyHasPublicGetAndSetMethod<T>(string propertyName)
        {
            // TODO: 2: Determine if this check is required.
            if (typeof(T) == typeof(object))
            {
                throw new Exception($"The generic type {typeof(T)} should not be type {typeof(object)}.");
            }

            return CheckIfPropertyHasPublicGetAndSetMethod(typeof(T).GetRuntimeProperty(propertyName));
        }

        /// <summary>
        /// Checks if a property has a public Get and Set method for a Type.
        /// </summary>
        /// <typeparam name="T">Type of object that contains the named property.</typeparam>
        /// <param name="propertyName">Name of the property to check.</param>
        /// <param name="objectWithProperty">An object that contains the named property.</param>
        /// <returns>True if the property is both public Get and Set.</returns>
        public static bool CheckIfPropertyHasPublicGetAndSetMethod<T>(string propertyName, T objectWithProperty)
        {
            return CheckIfPropertyHasPublicGetAndSetMethod(objectWithProperty.GetType().GetRuntimeProperty(propertyName));
        }

        /// <summary>
        /// Checks if a property has a public Get and Set method for a Type.
        /// </summary>
        /// <param name="propertyName">Name of the property to check.</param>
        /// <param name="type">Type of object that contains the named property.</param>
        /// <returns>True if the property is both public Get and Set.</returns>
        public static bool CheckIfPropertyHasPublicGetAndSetMethod(string propertyName, Type type)
        {
            return CheckIfPropertyHasPublicGetAndSetMethod(type.GetRuntimeProperty(propertyName));
        }

        /// <summary>
        /// Checks if a property has a public Get and Set method for a Type.
        /// </summary>
        /// <param name="property">Property to check.</param>
        /// <returns>True if the property is both public Get and Set.</returns>
        public static bool CheckIfPropertyHasPublicGetAndSetMethod(PropertyInfo property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return property.GetMethod != null && property.GetMethod.IsPublic && property.SetMethod != null && property.SetMethod.IsPublic;
        }

        #endregion

        #region GetListOfPropertiesWithPublicGetAndSetMethods

        /// <summary>
        /// Get a list of <see cref="PropertyInfo"/> for each property that has both public Get and Set accessors.
        /// </summary>
        /// <typeparam name="T">Type of object to get the properties from.</typeparam>
        /// <returns>List of <see cref="PropertyInfo"/> with public Get and Set Accessors.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithPublicGetAndSetMethods<T>()
        {
            return GetListOfPropertiesWithPublicGetAndSetMethods(typeof(T));
        }

        /// <summary>
        /// Get a list of <see cref="PropertyInfo"/> for each property that has both public Get and Set accessors.
        /// </summary>
        /// <typeparam name="T">Type of object to get the properties from.</typeparam>
        /// <param name="objectWithProperties">An Object that contains properties to evaluate.</param>
        /// <returns>List of <see cref="PropertyInfo"/> with public Get and Set Accessors.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithPublicGetAndSetMethods<T>(T objectWithProperties)
        {
            return GetListOfPropertiesWithPublicGetAndSetMethods(objectWithProperties.GetType());
        }

        /// <summary>
        /// Get a list of <see cref="PropertyInfo"/> for each property that has both public Get and Set accessors.
        /// </summary>
        /// <param name="type">Type of object to get the properties from.</param>
        /// <returns>List of <see cref="PropertyInfo"/> with public Get and Set Accessors.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithPublicGetAndSetMethods(Type type)
        {
            return new List<PropertyInfo>(type.GetRuntimeProperties().Where(property => property.GetMethod != null && property.GetMethod.IsPublic && property.SetMethod != null && property.SetMethod.IsPublic));
        }

        /// <summary>
        /// Gets a list of Property Names that have public Get and Set methods.
        /// </summary>
        /// <typeparam name="T">Type of object to evaluate.</typeparam>
        /// <returns>A list of <see cref="string"/> for each property that has a public get and set method.</returns>
        public static List<string> GetListOfPropertyNamesWithPublicGetAndSetMethods<T>()
        {
            return GetListOfPropertyNamesWithPublicGetAndSetMethods(typeof(T));
        }

        /// <summary>
        /// Gets a list of Property Names that have public Get and Set methods.
        /// </summary>
        /// <typeparam name="T">Type of object to evaluate.</typeparam>
        /// <param name="objectWithProperties">Object to evaluate.</param>
        /// <returns>A list of <see cref="string"/> for each property that has a public get and set method.</returns>
        public static List<string> GetListOfPropertyNamesWithPublicGetAndSetMethods<T>(T objectWithProperties)
        {
            return GetListOfPropertyNamesWithPublicGetAndSetMethods(objectWithProperties.GetType());
        }

        /// <summary>
        /// Gets a list of Property Names that have public Get and Set methods.
        /// </summary>
        /// <param name="type">Type of object to evaluate.</param>
        /// <returns>A list of <see cref="string"/> for each property that has a public get and set method.</returns>
        public static List<string> GetListOfPropertyNamesWithPublicGetAndSetMethods(Type type)
        {
            return new List<string>(GetListOfPropertiesWithPublicGetAndSetMethods(type).Select(property => property.Name));
        }

        #endregion

        #region CheckIfPropertyHasPublicGetMethod

        /// <summary>
        /// Checks if a property has a public get method for a Type.
        /// </summary>
        /// <typeparam name="T">Type with the named property.</typeparam>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property has a public get method, otherwise false.</returns>
        public static bool CheckIfPropertyHasPublicGetMethod<T>(string propertyName)
        {
            // TODO: 2: Determine if this check is required.
            if (typeof(T) == typeof(object))
            {
                throw new Exception($"The generic type {typeof(T)} should not be type {typeof(object)}.");
            }

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
        /// Checks if a property has a public get method for an object.
        /// </summary>
        /// <param name="propertyName">Name of the property to evaluate.</param>
        /// <param name="type">Type with named property.</param>
        /// <returns>True if the property has a public get method, otherwise false.</returns>
        public static bool CheckIfPropertyHasPublicGetMethod(string propertyName, Type type)
        {
            return CheckIfPropertyHasPublicGetMethod(type.GetRuntimeProperty(propertyName));
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

        #endregion

        #region GetListOfPropertiesWithPublicGetMethod

        /// <summary>
        /// Get a list of <see cref="PropertyInfo"/> that have public Get methods.
        /// </summary>
        /// <typeparam name="T">Type that contains properties.</typeparam>
        /// <returns>A list of <see cref="PropertyInfo"/> that have public Get methods.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithPublicGetMethod<T>()
        {
            return GetListOfPropertiesWithPublicGetMethod(typeof(T));
        }

        /// <summary>
        /// Get a list of <see cref="PropertyInfo"/> that have public Get methods.
        /// </summary>
        /// <typeparam name="T">Type that contains properties.</typeparam>
        /// <param name="objectWithProperties">Object that contains properties.</param>
        /// <returns>A list of <see cref="PropertyInfo"/> that have public Get methods.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithPublicGetMethod<T>(T objectWithProperties)
        {
            return GetListOfPropertiesWithPublicGetMethod(objectWithProperties.GetType());
        }

        /// <summary>
        /// Get a list of <see cref="PropertyInfo"/> that have public Get methods.
        /// </summary>
        /// <param name="type">Type that has properties.</param>
        /// <returns>A list of <see cref="PropertyInfo"/> that have public Get methods.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithPublicGetMethod(Type type)
        {
            // TODO: 2: Determine if this check is required.
            if (type == typeof(object))
            {
                throw new Exception($"The {nameof(type)} object should not be type {typeof(object)}.");
            }

            return new List<PropertyInfo>(type.GetRuntimeProperties().Where(property => property.GetMethod != null && property.GetMethod.IsPublic));
        }

        /// <summary>
        /// Get a list of property names that have public Get methods.
        /// </summary>
        /// <typeparam name="T">Type that contains properties.</typeparam>
        /// <returns>A list of property names that have public get methods.</returns>
        public static List<string> GetListOfPropertyNamesWithPublicGetMethod<T>()
        {
            return GetListOfPropertyNamesWithPublicGetMethod(typeof(T));
        }

        /// <summary>
        /// Get a list of property names that have public Get methods.
        /// </summary>
        /// <typeparam name="T">Type that contains properties.</typeparam>
        /// <param name="objectWithProperties">Object that contains properties.</param>
        /// <returns>A list of property names that have public get methods.</returns>
        public static List<string> GetListOfPropertyNamesWithPublicGetMethod<T>(T objectWithProperties)
        {
            return GetListOfPropertyNamesWithPublicGetMethod(objectWithProperties.GetType());
        }

        /// <summary>
        /// Get a list of property names that have public Get methods.
        /// </summary>
        /// <param name="type">Type that contains properties.</param>
        /// <returns>A list of property names that have public get methods.</returns>
        public static List<string> GetListOfPropertyNamesWithPublicGetMethod(Type type)
        {
            return new List<string>(GetListOfPropertiesWithPublicGetMethod(type).Select(property => property.Name));
        }

        #endregion

        #region GetListOfReadOnlyProperties

        /// <summary>
        /// Gets a list of properties that are read-only.
        /// </summary>
        /// <typeparam name="T">Type of object with proeprties to evaluate.</typeparam>
        /// <returns>A list of properties that are read-only.</returns>
        public static List<PropertyInfo> GetListOfReadOnlyProperties<T>()
        {
            return GetListOfReadOnlyProperties(typeof(T));
        }

        /// <summary>
        /// Gets a list of properties that are read-only.
        /// </summary>
        /// <typeparam name="T">Type of object with proeprties to evaluate.</typeparam>
        /// <param name="objectWithProperties">Object with properties to evaluate.</param>
        /// <returns>A list of properties that are read-only.</returns>
        public static List<PropertyInfo> GetListOfReadOnlyProperties<T>(T objectWithProperties)
        {
            return GetListOfReadOnlyProperties(objectWithProperties.GetType());
        }

        /// <summary>
        /// Gets a list of properties that are read-only.
        /// </summary>
        /// <param name="type">Type of object with proeprties to evaluate.</param>
        /// <returns>A list of properties that are read-only.</returns>
        public static List<PropertyInfo> GetListOfReadOnlyProperties(Type type)
        {
            return new List<PropertyInfo>(type.GetRuntimeProperties().Where(p => p.GetMethod != null && p.GetMethod.IsPublic && (p.SetMethod == null || !p.SetMethod.IsPublic)));
        }

        #endregion

        #region CheckIfPropertyHasPublicSetMethod

        /// <summary>
        /// Checks if the property has a public set method.
        /// </summary>
        /// <typeparam name="T">Type that contains the named property.</typeparam>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property has a public set method, otherwise false.</returns>
        public static bool CheckIfPropertyHasPublicSetMethod<T>(string propertyName)
        {
            // TODO: 2: Determine if this check is required.
            if (typeof(T) == typeof(object))
            {
                throw new Exception($"The generic type {typeof(T)} should not be type {typeof(object)}.");
            }

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
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <param name="type">Type with named property to check.</param>
        /// <returns>True if the property has a public set method, otherwise false.</returns>
        public static bool CheckIfPropertyHasPublicSetMethod(string propertyName, Type type)
        {
            return CheckIfPropertyHasPublicSetMethod(type.GetRuntimeProperty(propertyName));
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

        #endregion

        #region GetListOfPropertiesWithPublicSetMethod

        /// <summary>
        /// Get a list of <see cref="PropertyInfo"/> that have public Set methods.
        /// </summary>
        /// <typeparam name="T">Type that contains properties.</typeparam>
        /// <returns>A list of <see cref="PropertyInfo"/> that have public Set methods.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithPublicSetMethod<T>()
        {
            return GetListOfPropertiesWithPublicSetMethod(typeof(T));
        }

        /// <summary>
        /// Get a list of <see cref="PropertyInfo"/> that have public Set methods.
        /// </summary>
        /// <typeparam name="T">Type that contains properties.</typeparam>
        /// <param name="objectWithProperties">Object that contains properties.</param>
        /// <returns>A list of <see cref="PropertyInfo"/> that have public Set methods.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithPublicSetMethod<T>(T objectWithProperties)
        {
            return GetListOfPropertiesWithPublicSetMethod(objectWithProperties.GetType());
        }

        /// <summary>
        /// Get a list of <see cref="PropertyInfo"/> that have public Set methods.
        /// </summary>
        /// <param name="type">Type that contains properties.</param>
        /// <returns>A list of <see cref="PropertyInfo"/> that have public Set methods.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithPublicSetMethod(Type type)
        {
            // TODO: 2: Determine if this check is required.
            if (type == typeof(object))
            {
                throw new Exception($"The {nameof(type)} object should not be type {typeof(object)}.");
            }

            return new List<PropertyInfo>(type.GetRuntimeProperties().Where(property => property.SetMethod != null && property.SetMethod.IsPublic));
        }

        /// <summary>
        /// Get a list of property names that have public set methods.
        /// </summary>
        /// <typeparam name="T">Type that contains properties.</typeparam>
        /// <returns>A list of property names that have public set methods.</returns>
        public static List<string> GetListOfPropertyNamesWithPublicSetMethod<T>()
        {
            return GetListOfPropertyNamesWithPublicSetMethod(typeof(T));
        }

        /// <summary>
        /// Get a list of property names that have public set methods.
        /// </summary>
        /// <typeparam name="T">Type that contains properties.</typeparam>
        /// <param name="objectWithProperties">Object that contains properties.</param>
        /// <returns>A list of property names that have public set methods.</returns>
        public static List<string> GetListOfPropertyNamesWithPublicSetMethod<T>(T objectWithProperties)
        {
            return GetListOfPropertyNamesWithPublicSetMethod(objectWithProperties.GetType());
        }

        /// <summary>
        /// Get a list of property names that have public set methods.
        /// </summary>
        /// <param name="type">Type that contains properties.</param>
        /// <returns>A list of property names that have public set methods.</returns>
        public static List<string> GetListOfPropertyNamesWithPublicSetMethod(Type type)
        {
            return new List<string>(GetListOfPropertiesWithPublicSetMethod(type).Select(property => property.Name));
        }

        #endregion

        #region GetListofWriteOnlyProperties

        /// <summary>
        /// Gets a list of properties that are write-only.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to evaluate.</typeparam>
        /// <returns>A list of properties that are write-only.</returns>
        public static List<PropertyInfo> GetListOfWriteOnlyProperties<T>()
        {
            return GetListOfPropertiesWithClassValues(typeof(T));
        }

        /// <summary>
        /// Gets a list of properties that are write-only.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to evaluate.</typeparam>
        /// <param name="objectWithProperties">Object with properties to evalute.</param>
        /// <returns>A list of properties that are write-only.</returns>
        public static List<PropertyInfo> GetListOfWriteOnlyProperties<T>(T objectWithProperties)
        {
            return GetListOfPropertiesWithClassValues(objectWithProperties.GetType());
        }

        /// <summary>
        /// Gets a list of properties that are write-only.
        /// </summary>
        /// <param name="type">Type of object with properties to evaluate.</param>
        /// <returns>A list of properties that are write-only.</returns>
        public static List<PropertyInfo> GetListOfWriteOnlyProperties(Type type)
        {
            return new List<PropertyInfo>(type.GetRuntimeProperties().Where(p => p.SetMethod != null && p.SetMethod.IsPublic && (p.GetMethod == null || !p.SetMethod.IsPublic)));
        }

        #endregion

        #region GetListOfPropertiesWithClassValues

        /// <summary>
        /// Gets a list of Properties where their value type is a class.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to evaluate.</typeparam>
        /// <returns>A list of properties with class value types.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithClassValues<T>()
        {
            return GetListOfPropertiesWithClassValues(typeof(T));
        }

        /// <summary>
        /// Gets a list of Properties where their value type is a class.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to evaluate.</typeparam>
        /// <param name="objectWithProperties">Object with properties to evaluate.</param>
        /// <returns>A list of properties with class value types.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithClassValues<T>(T objectWithProperties)
        {
            return GetListOfPropertiesWithClassValues(objectWithProperties.GetType());
        }

        /// <summary>
        /// Gets a list of Properties where their value type is a class.
        /// </summary>
        /// <param name="type">Type of object with properties to evaluate.</param>
        /// <returns>A list of properties with class value types.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithClassValues(Type type)
        {
            return new List<PropertyInfo>(type.GetRuntimeProperties().Where(p => p.PropertyType.IsClass));
        }

        /// <summary>
        /// Gets a list of property names where their value type is a class.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to evaluate.</typeparam>
        /// <returns>A list of property names that have class value types.</returns>
        public static List<string> GetListOfPropertyNamesWithClassValues<T>()
        {
            return new List<string>(GetListOfPropertiesWithClassValues(typeof(T)).Select(p => p.Name));
        }

        /// <summary>
        /// Gets a list of property names where their value type is a class.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to evaluate.</typeparam>
        /// <param name="objectWithProperties">Object with properties to evaluate.</param>
        /// <returns>A list of property names that have class value types.</returns>
        public static List<string> GetListOfPropertyNamesWithClassValues<T>(T objectWithProperties)
        {
            return new List<string>(GetListOfPropertiesWithClassValues(objectWithProperties.GetType()).Select(p => p.Name));
        }

        /// <summary>
        /// Gets a list of property names where their value type is a class.
        /// </summary>
        /// <param name="type">Type of object with properties to evaluate.</param>
        /// <returns>A list of property names that have class value types.</returns>
        public static List<string> GetListOfPropertyNamesWithClassValues(Type type)
        {
            return new List<string>(GetListOfPropertiesWithClassValues(type).Select(p => p.Name));
        }

        #endregion

        #region GetListOfPropertiesWithListValues

        /// <summary>
        /// Gets a list of properties that have lists as their value type.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to evaluate.</typeparam>
        /// <returns>A list of properties with lists as their value type.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithListValues<T>()
        {
            return GetListOfPropertiesWithListValues(typeof(T));
        }

        /// <summary>
        /// Gets a list of properties that have lists as their value type.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to evaluate.</typeparam>
        /// <param name="objectWithProperties">Object with properties to evaluate.</param>
        /// <returns>A list of properties with lists as their value type.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithListValues<T>(T objectWithProperties)
        {
            return GetListOfPropertiesWithListValues(objectWithProperties.GetType());
        }

        /// <summary>
        /// Gets a list of properties that have lists as their value type.
        /// </summary>
        /// <param name="type">Type of object with properties to evaluate.</param>
        /// <returns>A list of properties with lists as their value type.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithListValues(Type type)
        {
            return new List<PropertyInfo>(type.GetRuntimeProperties().Where(p => typeof(IList).IsAssignableFrom(p.PropertyType)));
        }

        /// <summary>
        /// Gets a list of property names that have lists as their value type.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to evaluate.</typeparam>
        /// <returns>A list of property names with lists as their value type.</returns>
        public static List<string> GetListOfPropertyValuesWithListValues<T>()
        {
            return new List<string>(GetListOfPropertiesWithListValues(typeof(T)).Select(p => p.Name));
        }

        /// <summary>
        /// Gets a list of property names that have lists as their value type.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to evaluate.</typeparam>
        /// <param name="objectWithProperties">An object with properties to evaluate.</param>
        /// <returns>A list of property names with lists as their value type.</returns>
        public static List<string> GetListOfPropertyValuesWithListValues<T>(T objectWithProperties)
        {
            return new List<string>(GetListOfPropertiesWithListValues(objectWithProperties.GetType()).Select(p => p.Name));
        }

        /// <summary>
        /// Gets a list of property names that have lists as their value type.
        /// </summary>
        /// <param name="type">Type of object with properties to evaluate.</param>
        /// <returns>A list of property names with lists as their value type.</returns>
        public static List<string> GetListOfPropertyValuesWithListValues(Type type)
        {
            return new List<string>(GetListOfPropertiesWithListValues(type).Select(p => p.Name));
        }

        #endregion
    }
}
