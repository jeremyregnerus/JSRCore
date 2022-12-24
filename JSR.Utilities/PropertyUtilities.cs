// <copyright file="PropertyUtilities.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections;
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
        #region IS

        #region IsReadWriteProperty

        /// <summary>
        /// Evaluates if a property is readwrite.
        /// </summary>
        /// <typeparam name="T">Type with property to evaluate.</typeparam>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property is readwrite.</returns>
        public static bool IsReadWriteProperty<T>(string propertyName)
        {
            return IsReadWriteProperty(typeof(T).GetProperty(propertyName));
        }

        /// <summary>
        /// evaluate if a property is readwrite.
        /// </summary>
        /// <typeparam name="T">Type with property to evaluate.</typeparam>
        /// <param name="obj">Object with property to evaluate.</param>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property is readwrite.</returns>
        public static bool IsReadWriteProperty<T>(T obj, string propertyName)
        {
            return IsReadWriteProperty(obj.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Evaluates if a property is readwrite.
        /// </summary>
        /// <param name="type">Type with property to evaluate.</param>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property is readwrite.</returns>
        public static bool IsReadWriteProperty(Type type, string propertyName)
        {
            return IsReadWriteProperty(type.GetProperty(propertyName));
        }

        /// <summary>
        /// Evaluates if a property is readwrite.
        /// </summary>
        /// <param name="property">Property to evaluate.</param>
        /// <returns>True if the property is readwrite.</returns>
        public static bool IsReadWriteProperty(PropertyInfo property)
        {
            return property.GetMethod != null && property.GetMethod.IsPublic && property.SetMethod != null && property.SetMethod.IsPublic;
        }

        #endregion

        #region IsReadOnlyProperty

        /// <summary>
        /// Evaluates if a property is readonly.
        /// </summary>
        /// <typeparam name="T">Type with property to evaluate.</typeparam>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property is readonly.</returns>
        public static bool IsReadOnlyProperty<T>(string propertyName)
        {
            return IsReadOnlyProperty(typeof(T).GetProperty(propertyName));
        }

        /// <summary>
        /// Evaluates if a property is readonly.
        /// </summary>
        /// <typeparam name="T">Type with property to evaluate.</typeparam>
        /// <param name="obj">Object with property to evaluate.</param>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property is readonly.</returns>
        public static bool IsReadOnlyProperty<T>(T obj, string propertyName)
        {
            return IsReadOnlyProperty(obj.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Evaluates if a property is readonly.
        /// </summary>
        /// <param name="type">Type with property to evaluate.</param>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property is readonly.</returns>
        public static bool IsReadOnlyProperty(Type type, string propertyName)
        {
            return IsReadOnlyProperty(type.GetProperty(propertyName));
        }

        /// <summary>
        /// Evaluates if a property is readonly.
        /// </summary>
        /// <param name="property">Property to evaluate.</param>
        /// <returns>True if the property is readonly.</returns>
        public static bool IsReadOnlyProperty(PropertyInfo property)
        {
            return property.GetMethod != null && property.GetMethod.IsPublic && (property.SetMethod == null || !property.SetMethod.IsPublic);
        }

        #endregion

        #region IsWriteOnlyProperty

        /// <summary>
        /// Evaluates if a property is writeonly.
        /// </summary>
        /// <typeparam name="T">Type with property to evaluate.</typeparam>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property is writeonly.</returns>
        public static bool IsWriteOnlyProperty<T>(string propertyName)
        {
            return IsWriteOnlyProperty(typeof(T).GetProperty(propertyName));
        }

        /// <summary>
        /// Evaluates if a property is writeonly.
        /// </summary>
        /// <typeparam name="T">Type with property to evaluate.</typeparam>
        /// <param name="obj">Object with property to evaluate.</param>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property is writeonly.</returns>
        public static bool IsWriteOnlyProperty<T>(T obj, string propertyName)
        {
            return IsWriteOnlyProperty(obj.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Evaluates if a property is writeonly.
        /// </summary>
        /// <param name="type">Type with property to evaluate.</param>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property is writeonly.</returns>
        public static bool IsWriteOnlyProperty(Type type, string propertyName)
        {
            return IsWriteOnlyProperty(type.GetProperty(propertyName));
        }

        /// <summary>
        /// Evaluates if a property is writeonly.
        /// </summary>
        /// <param name="property">Property to evaluate.</param>
        /// <returns>True if the property is writeonly.</returns>
        public static bool IsWriteOnlyProperty(PropertyInfo property)
        {
            return property.SetMethod != null && property.SetMethod.IsPublic && (property.GetMethod == null || !property.GetMethod.IsPublic);
        }

        #endregion

        #region IsClassProperty

        /// <summary>
        /// Evaluates if a property contains a class type.
        /// </summary>
        /// <typeparam name="T">Type with property to evaluate.</typeparam>
        /// <param name="propertyName">Property name to evaluate.</param>
        /// <returns>True if the property is a class.</returns>
        public static bool IsClassProperty<T>(string propertyName)
        {
            return IsClassProperty(typeof(T).GetProperty(propertyName));
        }

        /// <summary>
        /// Evaluates if a property contains a class type.
        /// </summary>
        /// <typeparam name="T">Type with property to evaluate.</typeparam>
        /// <param name="obj">Object with property to evaluate.</param>
        /// <param name="propertyName">Property name to evaluate.</param>
        /// <returns>True if the property is a class.</returns>
        public static bool IsClassProperty<T>(T obj, string propertyName)
        {
            return IsClassProperty(obj.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Evaluates if a property contains a class type.
        /// </summary>
        /// <param name="type">Type with property to evaluate.</param>
        /// <param name="propertyName">Property name to evaluate.</param>
        /// <returns>True if the property is a class.</returns>
        public static bool IsClassProperty(Type type, string propertyName)
        {
            return IsClassProperty(type.GetProperty(propertyName));
        }

        /// <summary>
        /// Evaluates if a property contains class type.
        /// </summary>
        /// <param name="property">Property to evaluate.</param>
        /// <returns>True if the property is a class.</returns>
        public static bool IsClassProperty(PropertyInfo property)
        {
            return property.PropertyType.IsClass && !typeof(IList).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string);
        }

        #endregion

        #region IsInterfaceProperty

        /// <summary>
        /// Evaluates if a property contains an interface type.
        /// </summary>
        /// <typeparam name="T">Type with property to evaluate.</typeparam>
        /// <param name="propertyName">Property name to evaluate.</param>
        /// <returns>True if the property is an interface.</returns>
        public static bool IsInterfaceProperty<T>(string propertyName)
        {
            return IsInterfaceProperty(typeof(T).GetProperty(propertyName));
        }

        /// <summary>
        /// Evaluates if a property contains an interface type.
        /// </summary>
        /// <typeparam name="T">Type with property to evaluate.</typeparam>
        /// <param name="obj">Object with property to evaluate.</param>
        /// <param name="propertyName">Property name to evaluate.</param>
        /// <returns>True if the property is an interface.</returns>
        public static bool IsInterfaceProperty<T>(T obj, string propertyName)
        {
            return IsInterfaceProperty(obj.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Evaluates if a property contains an interface type.
        /// </summary>
        /// <param name="type">Type with property to evaluate.</param>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property is an interface type.</returns>
        public static bool IsInterfaceProperty(Type type, string propertyName)
        {
            return IsInterfaceProperty(type.GetProperty(propertyName));
        }

        /// <summary>
        /// Evaluates if a property contains an interface type.
        /// </summary>
        /// <param name="property">Property to evaluate.</param>
        /// <returns>True if the property is an interface type.</returns>
        public static bool IsInterfaceProperty(PropertyInfo property)
        {
            return property.PropertyType.IsInterface;
        }

        #endregion

        #region IsListProperty

        /// <summary>
        /// Evaluates if the property is a list.
        /// </summary>
        /// <typeparam name="T">Type with property to evaluate.</typeparam>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property is a list.</returns>
        public static bool IsListProperty<T>(string propertyName)
        {
            return IsListProperty(typeof(T).GetProperty(propertyName));
        }

        /// <summary>
        /// Evaluates if the property is a list.
        /// </summary>
        /// <typeparam name="T">Type with property to evaluate.</typeparam>
        /// <param name="obj">Object with property to evaluate.</param>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property is a list.</returns>
        public static bool IsListProperty<T>(T obj, string propertyName)
        {
            return IsListProperty(obj.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Evaluates if a property contains a list type.
        /// </summary>
        /// <param name="type">Type with property to evaluate.</param>
        /// <param name="propertyName">Property Name to evaluate.</param>
        /// <returns>True if the property is a list.</returns>
        public static bool IsListProperty(Type type, string propertyName)
        {
            return IsListProperty(type.GetProperty(propertyName));
        }

        /// <summary>
        /// Evaluates if a property contains a list type.
        /// </summary>
        /// <param name="property">Property to evaluate.</param>
        /// <returns>True if the property is a list.</returns>
        public static bool IsListProperty(PropertyInfo property)
        {
            return typeof(IList).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string);
        }

        #endregion

        #region IsValueProperty

        /// <summary>
        /// Evaluates if a property contains a value type.
        /// </summary>
        /// <typeparam name="T">Type with property to evaluate.</typeparam>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property is a value type.</returns>
        public static bool IsValueProperty<T>(string propertyName)
        {
            return IsValueProperty(typeof(T), propertyName);
        }

        /// <summary>
        /// Evaluates if a property contains a value type.
        /// </summary>
        /// <typeparam name="T">Type with property to evaluate.</typeparam>
        /// <param name="obj">Object with property to evaluate.</param>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property is a value type.</returns>
        public static bool IsValueProperty<T>(T obj, string propertyName)
        {
            return IsValueProperty(obj.GetType(), propertyName);
        }

        /// <summary>
        /// Evaluates if a property contains a value type.
        /// </summary>
        /// <param name="type">Type with property to evaluate.</param>
        /// <param name="propertyName">Name of property to evaluate.</param>
        /// <returns>True if the property is a value type.</returns>
        public static bool IsValueProperty(Type type, string propertyName)
        {
            return IsValueProperty(type.GetProperty(propertyName));
        }

        /// <summary>
        /// Evaluates if a property contains a value type.
        /// </summary>
        /// <param name="property">Property to evaluate.</param>
        /// <returns>True if the property is a value type.</returns>
        public static bool IsValueProperty(PropertyInfo property)
        {
            return (property.PropertyType.IsValueType || property.PropertyType == typeof(string)) && !property.PropertyType.IsInterface;
        }

        #endregion

        #endregion

        #region GET

        #region GetProperties

        /// <summary>
        /// Returns a list of properties that meet the argument criteria.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="options">Options specifying which properties to get.</param>
        /// <returns>A list of <see cref="PropertyInfo"/>.</returns>
        public static List<PropertyInfo> GetProperties<T>(GetPropertiesOptions options)
        {
            return GetProperties(typeof(T), options);
        }

        /// <summary>
        /// Returns a list of properties that meet the argument criteria.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="obj">Object to get properties from.</param>
        /// <param name="options">Options specifying which properties to get.</param>
        /// <returns>A list of <see cref="PropertyInfo"/>.</returns>
        public static List<PropertyInfo> GetProperties<T>(T obj, GetPropertiesOptions options)
        {
            return GetProperties(obj.GetType(), options);
        }

        /// <summary>
        /// Returns a list of properties that meet the argument criteria.
        /// </summary>
        /// <param name="type">Type to get the properties from.</param>
        /// <param name="options">Options specifying which properties to get.</param>
        /// <returns>A list of <see cref="PropertyInfo"/>.</returns>
        public static List<PropertyInfo> GetProperties(Type type, GetPropertiesOptions options)
        {
            List<PropertyInfo> properties = new();

            foreach (PropertyInfo property in type.GetProperties())
            {
                bool access = false;

                if (options.ReadWriteProperties && IsReadWriteProperty(property))
                {
                    access = true;
                }
                else if (options.ReadOnlyProperties && IsReadOnlyProperty(property))
                {
                    access = true;
                }
                else if (options.WriteOnlyProperties && IsWriteOnlyProperty(property))
                {
                    access = true;
                }

                if (access)
                {
                    if (options.ValueProperties && IsValueProperty(property))
                    {
                        properties.Add(property);
                        continue;
                    }

                    if (options.ClassProperties && IsClassProperty(property))
                    {
                        properties.Add(property);
                        continue;
                    }

                    if (options.ListProperties && IsListProperty(property))
                    {
                        properties.Add(property);
                        continue;
                    }

                    if (options.InterfaceProperties && IsInterfaceProperty(property))
                    {
                        properties.Add(property);
                        continue;
                    }
                }
            }

            return properties;
        }

        #endregion

        #region GetPropertyNames

        /// <summary>
        /// Returns a list of properties names that meet the argument criteria.
        /// </summary>
        /// <typeparam name="T">Type to get property names from.</typeparam>
        /// <param name="options">Options specifying which properties to get.</param>
        /// <returns>A list of property names.</returns>
        public static List<string> GetPropertyNames<T>(GetPropertiesOptions options)
        {
            return GetPropertyNames(typeof(T), options);
        }

        /// <summary>
        /// Returns a list of properties names that meet the argument criteria.
        /// </summary>
        /// <typeparam name="T">Type to get property names from.</typeparam>
        /// <param name="obj">Object to get property names from.</param>
        /// <param name="options">Options specifying which properties to get.</param>
        /// <returns>A list of property names.</returns>
        public static List<string> GetPropertyNames<T>(T obj, GetPropertiesOptions options)
        {
            return GetPropertyNames(obj.GetType(), options);
        }

        /// <summary>
        /// Returns a list of property names that meet the argument criteria.
        /// </summary>
        /// <param name="type">Type to get property names from.</param>
        /// <param name="options">Options specifying which properties to get.</param>
        /// <returns>A list of property names.</returns>
        public static List<string> GetPropertyNames(Type type, GetPropertiesOptions options)
        {
            return new List<string>(GetProperties(type, options).Select(property => property.Name));
        }

        #endregion

        #region GetReadWriteProperties

        /// <summary>
        /// Get a list of all the readwrite properties.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <returns>List of readwrite properties.</returns>
        public static List<PropertyInfo> GetReadWriteProperties<T>()
        {
            return GetReadWriteProperties(typeof(T));
        }

        /// <summary>
        /// Get a list of all the readwrite properties.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <param name="obj">Object with properties.</param>
        /// <returns>List of readwrite properties.</returns>
        public static List<PropertyInfo> GetReadWriteProperties<T>(T obj)
        {
            return GetReadWriteProperties(obj.GetType());
        }

        /// <summary>
        /// Get a list of all the readwrite properties.
        /// </summary>
        /// <param name="type">Type with properties.</param>
        /// <returns>List of readwrite properties.</returns>
        public static List<PropertyInfo> GetReadWriteProperties(Type type)
        {
            return new List<PropertyInfo>(type.GetProperties().Where(property => IsReadWriteProperty(property)));
        }

        /// <summary>
        /// Get a list of all the readwrite properties.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other accessability choices, and only select readwrite properties.</param>
        /// <returns>List of readwrite properties.</returns>
        public static List<PropertyInfo> GetReadWriteProperties<T>(GetPropertiesOptions options)
        {
            return GetReadWriteProperties(typeof(T), options);
        }

        /// <summary>
        /// Get a list of all the readwrite properties.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <param name="obj">Object with properties.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other accessability choices, and only select readwrite properties.</param>
        /// <returns>List of readwrite properties.</returns>
        public static List<PropertyInfo> GetReadWriteProperties<T>(T obj, GetPropertiesOptions options)
        {
            return GetReadWriteProperties(obj.GetType(), options);
        }

        /// <summary>
        /// Get a list of all the readwrite properties.
        /// </summary>
        /// <param name="type">Type with properties.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other accessability choices, and only select readwrite properties.</param>
        /// <returns>List of readwrite properties.</returns>
        public static List<PropertyInfo> GetReadWriteProperties(Type type, GetPropertiesOptions options)
        {
            options.ReadWriteProperties = true;
            options.ReadOnlyProperties = false;
            options.WriteOnlyProperties = false;

            return GetProperties(type, options);
        }

        #endregion

        #region GetReadWritePropertyNames

        /// <summary>
        /// Get a list of all the readwrite property names.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <returns>List of readwrite property names.</returns>
        public static List<string> GetReadWritePropertyNames<T>()
        {
            return GetReadWritePropertyNames(typeof(T));
        }

        /// <summary>
        /// Get a list of all the readwrite property names.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <param name="obj">Object with properties.</param>
        /// <returns>List of readwrite property names.</returns>
        public static List<string> GetReadWritePropertyNames<T>(T obj)
        {
            return GetReadWritePropertyNames(obj.GetType());
        }

        /// <summary>
        /// Get a list of all the readwrite property names.
        /// </summary>
        /// <param name="type">Type with properties.</param>
        /// <returns>List of readwrite property names.</returns>
        public static List<string> GetReadWritePropertyNames(Type type)
        {
            return new List<string>(GetReadWriteProperties(type).Select(property => property.Name));
        }

        /// <summary>
        /// Get a list of all the readwrite property names.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other accessability choices, and only select readwrite properties.</param>
        /// <returns>List of readwrite property names.</returns>
        public static List<string> GetReadWritePropertyNames<T>(GetPropertiesOptions options)
        {
            return GetReadWritePropertyNames(typeof(T), options);
        }

        /// <summary>
        /// Get a list of all the readwrite property names.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <param name="obj">Object with properties.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other accessability choices, and only select readwrite properties.</param>
        /// <returns>List of readwrite property names.</returns>
        public static List<string> GetReadWritePropertyNames<T>(T obj, GetPropertiesOptions options)
        {
            return GetReadWritePropertyNames(obj.GetType(), options);
        }

        /// <summary>
        /// Get a list of all the readwrite property names.
        /// </summary>
        /// <param name="type">Type with properties.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other accessability choices, and only select readwrite properties.</param>
        /// <returns>List of readwrite property names.</returns>
        public static List<string> GetReadWritePropertyNames(Type type, GetPropertiesOptions options)
        {
            return new List<string>(GetReadWriteProperties(type, options).Select(property => property.Name));
        }

        #endregion

        #region GetReadOnlyProperties

        /// <summary>
        /// Get a list of all the readonly properties.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <returns>List of readonly properties.</returns>
        public static List<PropertyInfo> GetReadOnlyProperties<T>()
        {
            return GetReadOnlyProperties(typeof(T));
        }

        /// <summary>
        /// Get a list of all the readonly properties.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <param name="obj">Object with properties.</param>
        /// <returns>List of readonly properties.</returns>
        public static List<PropertyInfo> GetReadOnlyProperties<T>(T obj)
        {
            return GetReadOnlyProperties(obj.GetType());
        }

        /// <summary>
        /// Get a list of all the readonly properties.
        /// </summary>
        /// <param name="type">Type with properties.</param>
        /// <returns>List of readonly properties.</returns>
        public static List<PropertyInfo> GetReadOnlyProperties(Type type)
        {
            return new List<PropertyInfo>(type.GetProperties().Where(property => IsReadOnlyProperty(property)));
        }

        /// <summary>
        /// Get a list of all the readonly properties.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other accessability choices, and only select readonly properties.</param>
        /// <returns>List of readonly properties.</returns>
        public static List<PropertyInfo> GetReadOnlyProperties<T>(GetPropertiesOptions options)
        {
            return GetReadOnlyProperties(typeof(T), options);
        }

        /// <summary>
        /// Get a list of all the readonly properties.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <param name="obj">Object with properties.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other accessability choices, and only select readonly properties.</param>
        /// <returns>List of readonly properties.</returns>
        public static List<PropertyInfo> GetReadOnlyProperties<T>(T obj, GetPropertiesOptions options)
        {
            return GetReadOnlyProperties(obj.GetType(), options);
        }

        /// <summary>
        /// Get a list of all the readonly properties.
        /// </summary>
        /// <param name="type">Type with properties.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other accessability choices, and only select readonly properties.</param>
        /// <returns>List of readonly properties.</returns>
        public static List<PropertyInfo> GetReadOnlyProperties(Type type, GetPropertiesOptions options)
        {
            options.ReadWriteProperties = false;
            options.ReadOnlyProperties = true;
            options.WriteOnlyProperties = false;

            return GetProperties(type, options);
        }

        #endregion

        #region GetReadOnlyPropertyNames

        /// <summary>
        /// Get a list of all the readonly property names.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <returns>List of readonly property names.</returns>
        public static List<string> GetReadOnlyPropertyNames<T>()
        {
            return GetReadOnlyPropertyNames(typeof(T));
        }

        /// <summary>
        /// Get a list of all the readonly property names.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <param name="obj">Object with properties.</param>
        /// <returns>List of readonly property names.</returns>
        public static List<string> GetReadOnlyPropertyNames<T>(T obj)
        {
            return GetReadOnlyPropertyNames(obj.GetType());
        }

        /// <summary>
        /// get a list of all the readonly property names.
        /// </summary>
        /// <param name="type">Type with properties.</param>
        /// <returns>List of readonly property names.</returns>
        public static List<string> GetReadOnlyPropertyNames(Type type)
        {
            return new List<string>(GetReadOnlyProperties(type).Select(property => property.Name));
        }

        /// <summary>
        /// Get a list of all the readonly property names.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other accessability choices, and only select readonly properties.</param>
        /// <returns>List of readonly property names.</returns>
        public static List<string> GetReadOnlyPropertyNames<T>(GetPropertiesOptions options)
        {
            return GetReadOnlyPropertyNames(typeof(T), options);
        }

        /// <summary>
        /// Get a list of all the readonly property names.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <param name="obj">Object with properties.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other accessability choices, and only select readonly properties.</param>
        /// <returns>List of readonly property names.</returns>
        public static List<string> GetReadOnlyPropertyNames<T>(T obj, GetPropertiesOptions options)
        {
            return GetReadOnlyPropertyNames(obj.GetType(), options);
        }

        /// <summary>
        /// get a list of all the readonly property names.
        /// </summary>
        /// <param name="type">Type with properties.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other accessability choices, and only select readonly properties.</param>
        /// <returns>List of readonly property names.</returns>
        public static List<string> GetReadOnlyPropertyNames(Type type, GetPropertiesOptions options)
        {
            return new List<string>(GetReadOnlyProperties(type, options).Select(property => property.Name));
        }

        #endregion

        #region GetWriteOnlyProperties

        /// <summary>
        /// Get a list of all the writeonly properties.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <returns>List of writeonly properties.</returns>
        public static List<PropertyInfo> GetWriteOnlyProperties<T>()
        {
            return GetWriteOnlyProperties(typeof(T));
        }

        /// <summary>
        /// Get a list of all the writeonly properties.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <param name="obj">Object with properties.</param>
        /// <returns>List of writeonly properties.</returns>
        public static List<PropertyInfo> GetWriteOnlyProperties<T>(T obj)
        {
            return GetWriteOnlyProperties(obj.GetType());
        }

        /// <summary>
        /// Get a list of all the writeonly properties.
        /// </summary>
        /// <param name="type">Type with properties.</param>
        /// <returns>List of writeonly properties.</returns>
        public static List<PropertyInfo> GetWriteOnlyProperties(Type type)
        {
            return new List<PropertyInfo>(type.GetProperties().Where(property => IsWriteOnlyProperty(property)));
        }

        /// <summary>
        /// Get a list of all the writeonly properties.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other accessability choices, and only select writeonly properties.</param>
        /// <returns>List of writeonly properties.</returns>
        public static List<PropertyInfo> GetWriteOnlyProperties<T>(GetPropertiesOptions options)
        {
            return GetWriteOnlyProperties(typeof(T), options);
        }

        /// <summary>
        /// Get a list of all the writeonly properties.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <param name="obj">Object with properties.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other accessability choices, and only select writeonly properties.</param>
        /// <returns>List of writeonly properties.</returns>
        public static List<PropertyInfo> GetWriteOnlyProperties<T>(T obj, GetPropertiesOptions options)
        {
            return GetWriteOnlyProperties(obj.GetType(), options);
        }

        /// <summary>
        /// Get a list of all the writeonly properties.
        /// </summary>
        /// <param name="type">Type with properties.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other accessability choices, and only select writeonly properties.</param>
        /// <returns>List of writeonly properties.</returns>
        public static List<PropertyInfo> GetWriteOnlyProperties(Type type, GetPropertiesOptions options)
        {
            options.ReadWriteProperties = false;
            options.ReadOnlyProperties = false;
            options.WriteOnlyProperties = true;

            return GetProperties(type, options);
        }

        #endregion

        #region GetWriteOnlyPropertyNames

        /// <summary>
        /// Get a list of all the writeonly property names.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <returns>List of writeonly property names.</returns>
        public static List<string> GetWriteOnlyPropertyNames<T>()
        {
            return GetWriteOnlyPropertyNames(typeof(T));
        }

        /// <summary>
        /// Get a list of all the writeonly property names.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <param name="obj">Object with properties.</param>
        /// <returns>List of writeonly property names.</returns>
        public static List<string> GetWriteOnlyPropertyNames<T>(T obj)
        {
            return GetWriteOnlyPropertyNames(obj.GetType());
        }

        /// <summary>
        /// Get a list of all the writeonly property names.
        /// </summary>
        /// <param name="type">Type with properties.</param>
        /// <returns>List of writeonly property names.</returns>
        public static List<string> GetWriteOnlyPropertyNames(Type type)
        {
            return new List<string>(GetWriteOnlyProperties(type).Select(property => property.Name));
        }

        /// <summary>
        /// Get a list of all the writeonly property names.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other accessability choices, and only select writeonly properties.</param>
        /// <returns>List of writeonly property names.</returns>
        public static List<string> GetWriteOnlyPropertyNames<T>(GetPropertiesOptions options)
        {
            return GetWriteOnlyPropertyNames(typeof(T), options);
        }

        /// <summary>
        /// Get a list of all the writeonly property names.
        /// </summary>
        /// <typeparam name="T">Type with properties.</typeparam>
        /// <param name="obj">Object with properties.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other accessability choices, and only select writeonly properties.</param>
        /// <returns>List of writeonly property names.</returns>
        public static List<string> GetWriteOnlyPropertyNames<T>(T obj, GetPropertiesOptions options)
        {
            return GetWriteOnlyPropertyNames(obj.GetType(), options);
        }

        /// <summary>
        /// Get a list of all the writeonly property names.
        /// </summary>
        /// <param name="type">Type with properties.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other accessability choices, and only select writeonly properties.</param>
        /// <returns>List of writeonly property names.</returns>
        public static List<string> GetWriteOnlyPropertyNames(Type type, GetPropertiesOptions options)
        {
            return new List<string>(GetWriteOnlyProperties(type, options).Select(property => property.Name));
        }

        #endregion

        #region GetClassProperties

        /// <summary>
        /// Gets a list of Properties with class values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <returns>A list of properties with class values.</returns>
        public static List<PropertyInfo> GetClassProperties<T>()
        {
            return GetClassProperties(typeof(T));
        }

        /// <summary>
        /// Gets a list of Properties with class values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="obj">Object to get the properties from.</param>
        /// <returns>A list of properties with class values.</returns>
        public static List<PropertyInfo> GetClassProperties<T>(T obj)
        {
            return GetClassProperties(obj.GetType());
        }

        /// <summary>
        /// Gets a list of Properties with class values.
        /// </summary>
        /// <param name="type">Type to get the properties from.</param>
        /// <returns>A list of properties with class values.</returns>
        public static List<PropertyInfo> GetClassProperties(Type type)
        {
            return new List<PropertyInfo>(type.GetProperties().Where(property => IsClassProperty(property)));
        }

        /// <summary>
        /// Gets a list of Properties with class values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select class properties.</param>
        /// <returns>A list of properties with class values.</returns>
        public static List<PropertyInfo> GetClassProperties<T>(GetPropertiesOptions options)
        {
            return GetClassProperties(typeof(T), options);
        }

        /// <summary>
        /// Gets a list of Properties with class values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="obj">Object to get the properties from.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select class properties.</param>
        /// <returns>A list of properties with class values.</returns>
        public static List<PropertyInfo> GetClassProperties<T>(T obj, GetPropertiesOptions options)
        {
            return GetClassProperties(obj.GetType(), options);
        }

        /// <summary>
        /// Gets a list of Properties with class values.
        /// </summary>
        /// <param name="type">Type to get the properties from.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select class properties.</param>
        /// <returns>A list of properties with class values.</returns>
        public static List<PropertyInfo> GetClassProperties(Type type, GetPropertiesOptions options)
        {
            options.ClassProperties = true;
            options.InterfaceProperties = false;
            options.ValueProperties = false;
            options.ListProperties = false;

            return GetProperties(type, options);
        }

        #endregion

        #region GetClassPropertyNames

        /// <summary>
        /// Gets a list of property names with class values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <returns>A list of property names with class values.</returns>
        public static List<string> GetClassPropertyNames<T>()
        {
            return GetClassPropertyNames(typeof(T));
        }

        /// <summary>
        /// Gets a list of property names with class values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="obj">Object to get the properties from.</param>
        /// <returns>A list of property names with class values.</returns>
        public static List<string> GetClassPropertyNames<T>(T obj)
        {
            return GetClassPropertyNames(obj.GetType());
        }

        /// <summary>
        /// Gets a list of property names with class values.
        /// </summary>
        /// <param name="type">Type to get the properties from.</param>
        /// <returns>A list of property names with class values.</returns>
        public static List<string> GetClassPropertyNames(Type type)
        {
            return new List<string>(GetClassProperties(type).Select(property => property.Name));
        }

        /// <summary>
        /// Gets a list of property names with class values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select class type properties.</param>
        /// <returns>A list of property names with class values.</returns>
        public static List<string> GetClassPropertyNames<T>(GetPropertiesOptions options)
        {
            return GetClassPropertyNames(typeof(T), options);
        }

        /// <summary>
        /// Gets a list of property names with class values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="obj">Object to get the properties from.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select class type properties.</param>
        /// <returns>A list of property names with class values.</returns>
        public static List<string> GetClassPropertyNames<T>(T obj, GetPropertiesOptions options)
        {
            return GetClassPropertyNames(obj.GetType(), options);
        }

        /// <summary>
        /// Gets a list of property names with class values.
        /// </summary>
        /// <param name="type">Type to get the properties from.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select class type properties.</param>
        /// <returns>A list of property names with class values.</returns>
        public static List<string> GetClassPropertyNames(Type type, GetPropertiesOptions options)
        {
            return new List<string>(GetClassProperties(type, options).Select(p => p.Name));
        }

        #endregion

        #region GetInterfaceProperties

        /// <summary>
        /// Gets a list of properties with interface values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <returns>A list of properties with interface values.</returns>
        public static List<PropertyInfo> GetInterfaceProperties<T>()
        {
            return GetInterfaceProperties(typeof(T));
        }

        /// <summary>
        /// Gets a list of properties with interface values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="obj">Object to get properties from.</param>
        /// <returns>A list of properties with interface values.</returns>
        public static List<PropertyInfo> GetInterfaceProperties<T>(T obj)
        {
            return GetInterfaceProperties(obj.GetType());
        }

        /// <summary>
        /// Gets a list of properties with interface values.
        /// </summary>
        /// <param name="type">Type to get the properties from.</param>
        /// <returns>A list of properties with interface values.</returns>
        public static List<PropertyInfo> GetInterfaceProperties(Type type)
        {
            return new List<PropertyInfo>(type.GetProperties().Where(p => IsInterfaceProperty(p)));
        }

        /// <summary>
        /// Gets a list of properties with interface values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select interface type properties.</param>
        /// <returns>A list of properties with interface values.</returns>
        public static List<PropertyInfo> GetInterfaceProperties<T>(GetPropertiesOptions options)
        {
            return GetInterfaceProperties(typeof(T), options);
        }

        /// <summary>
        /// Gets a list of properties with interface values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="obj">Object to get the properties from.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select interface type properties.</param>
        /// <returns>A list of properties with interface values.</returns>
        public static List<PropertyInfo> GetInterfaceProperties<T>(T obj, GetPropertiesOptions options)
        {
            return GetInterfaceProperties(obj.GetType(), options);
        }

        /// <summary>
        /// Gets a list of properties with interface values.
        /// </summary>
        /// <param name="type">Type to get the properties from.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select interface type properties.</param>
        /// <returns>A list of properties with interface values.</returns>
        public static List<PropertyInfo> GetInterfaceProperties(Type type, GetPropertiesOptions options)
        {
            options.InterfaceProperties = true;
            options.ClassProperties = false;
            options.ValueProperties = false;
            options.ListProperties = false;

            return GetProperties(type, options);
        }

        #endregion

        #region GetInterfacePropertyNames

        /// <summary>
        /// Gets a list of property names with interface values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <returns>A list of property names with interface values.</returns>
        public static List<string> GetInterfacePropertyNames<T>()
        {
            return GetInterfacePropertyNames(typeof(T));
        }

        /// <summary>
        /// Gets a list of property names with interface values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="obj">Object to get the properties from.</param>
        /// <returns>A list of property names with interface values.</returns>
        public static List<string> GetInterfacePropertyNames<T>(T obj)
        {
            return GetInterfacePropertyNames(obj.GetType());
        }

        /// <summary>
        /// Gets a list of property names with interface values.
        /// </summary>
        /// <param name="type">Type to get the properties from.</param>
        /// <returns>A list of property names with interface values.</returns>
        public static List<string> GetInterfacePropertyNames(Type type)
        {
            return new List<string>(GetInterfaceProperties(type).Select(property => property.Name));
        }

        /// <summary>
        /// Gets a list of property names with interface values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select interface type properties.</param>
        /// <returns>A list of property names with interface values.</returns>
        public static List<string> GetInterfacePropertyNames<T>(GetPropertiesOptions options)
        {
            return GetInterfacePropertyNames(typeof(T), options);
        }

        /// <summary>
        /// Gets a list of property names with interface values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="obj">Object to get the properties from.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select interface type properties.</param>
        /// <returns>A list of property names with interface values.</returns>
        public static List<string> GetInterfacePropertyNames<T>(T obj, GetPropertiesOptions options)
        {
            return GetInterfacePropertyNames(obj.GetType(), options);
        }

        /// <summary>
        /// Gets a list of property names with interface values.
        /// </summary>
        /// <param name="type">Type to get the properties from.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select interface type properties.</param>
        /// <returns>A list of property names with interface values.</returns>
        public static List<string> GetInterfacePropertyNames(Type type, GetPropertiesOptions options)
        {
            return new List<string>(GetInterfaceProperties(type, options).Select(property => property.Name));
        }

        #endregion

        #region GetListProperties

        /// <summary>
        /// Gets a list of properties with list values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <returns>A list of properties with list values.</returns>
        public static List<PropertyInfo> GetListProperties<T>()
        {
            return GetListProperties(typeof(T));
        }

        /// <summary>
        /// Gets a list of properties with list values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="obj">Object to get the properties from.</param>
        /// <returns>A list of properties with lists values.</returns>
        public static List<PropertyInfo> GetListProperties<T>(T obj)
        {
            return GetListProperties(obj.GetType());
        }

        /// <summary>
        /// Gets a list of properties with list values.
        /// </summary>
        /// <param name="type">Type to get the properties from.</param>
        /// <returns>A list of properties with lists values.</returns>
        public static List<PropertyInfo> GetListProperties(Type type)
        {
            return new List<PropertyInfo>(type.GetProperties().Where(property => IsListProperty(property)));
        }

        /// <summary>
        /// Gets a list of properties with list values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select list type properties.</param>
        /// <returns>A list of properties with list values.</returns>
        public static List<PropertyInfo> GetListProperties<T>(GetPropertiesOptions options)
        {
            return GetListProperties(typeof(T), options);
        }

        /// <summary>
        /// Gets a list of properties with list values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="obj">Object to get the properties from.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select list type properties.</param>
        /// <returns>A list of properties with lists values.</returns>
        public static List<PropertyInfo> GetListProperties<T>(T obj, GetPropertiesOptions options)
        {
            return GetListProperties(obj.GetType(), options);
        }

        /// <summary>
        /// Gets a list of properties with list values.
        /// </summary>
        /// <param name="type">Type to get the properties from.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select list type properties.</param>
        /// <returns>A list of properties with lists values.</returns>
        public static List<PropertyInfo> GetListProperties(Type type, GetPropertiesOptions options)
        {
            options.ListProperties = true;
            options.ValueProperties = false;
            options.ClassProperties = false;
            options.InterfaceProperties = false;

            return GetProperties(type, options);
        }

        #endregion

        #region GetListPropertyNames

        /// <summary>
        /// Gets a list of property names with list values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <returns>A list of property names with list values.</returns>
        public static List<string> GetListPropertyNames<T>()
        {
            return GetListPropertyNames(typeof(T));
        }

        /// <summary>
        /// Gets a list of property names with list values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="obj">An Object to get the properties from.</param>
        /// <returns>A list of property names with list values.</returns>
        public static List<string> GetListPropertyNames<T>(T obj)
        {
            return GetListPropertyNames(obj.GetType());
        }

        /// <summary>
        /// Gets a list of property names with list values.
        /// </summary>
        /// <param name="type">Type to get the properties from.</param>
        /// <returns>A list of property names with list values.</returns>
        public static List<string> GetListPropertyNames(Type type)
        {
            return new List<string>(GetListProperties(type).Select(property => property.Name));
        }

        /// <summary>
        /// Gets a list of property names with list values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select list type properties.</param>
        /// <returns>A list of property names with list values.</returns>
        public static List<string> GetListPropertyNames<T>(GetPropertiesOptions options)
        {
            return GetListPropertyNames(typeof(T), options);
        }

        /// <summary>
        /// Gets a list of property names with list values.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="obj">An Object to get the properties from.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select list type properties.</param>
        /// <returns>A list of property names with list values.</returns>
        public static List<string> GetListPropertyNames<T>(T obj, GetPropertiesOptions options)
        {
            return GetListPropertyNames(obj.GetType(), options);
        }

        /// <summary>
        /// Gets a list of property names with list values.
        /// </summary>
        /// <param name="type">Type to get the properties from.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select list type properties.</param>
        /// <returns>A list of property names with list values.</returns>
        public static List<string> GetListPropertyNames(Type type, GetPropertiesOptions options)
        {
            return new List<string>(GetListProperties(type, options).Select(property => property.Name));
        }

        #endregion

        #region GetValueTypeProperties

        /// <summary>
        /// Get a list of properties with value type values.
        /// </summary>
        /// <typeparam name="T">Type with properties to evaluate.</typeparam>
        /// <returns>A list of properties with value types.</returns>
        public static List<PropertyInfo> GetValueTypeProperties<T>()
        {
            return GetValueTypeProperties(typeof(T));
        }

        /// <summary>
        /// Get a list of properties with value type values.
        /// </summary>
        /// <typeparam name="T">Type with properties to evaluate.</typeparam>
        /// <param name="obj">Object to get the properties from.</param>
        /// <returns>A list of properties with value types.</returns>
        public static List<PropertyInfo> GetValueTypeProperties<T>(T obj)
        {
            return GetValueTypeProperties(obj.GetType());
        }

        /// <summary>
        /// Get a list of properties with value type values.
        /// </summary>
        /// <param name="type">Type with properties to evaluate.</param>
        /// <returns>A list of properties with value types.</returns>
        public static List<PropertyInfo> GetValueTypeProperties(Type type)
        {
            return new List<PropertyInfo>(type.GetProperties().Where(property => IsValueProperty(property)));
        }

        /// <summary>
        /// Get a list of properties with value types.
        /// </summary>
        /// <typeparam name="T">Type with properties to evaluate.</typeparam>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select value type properties.</param>
        /// <returns>A list of properties with value types.</returns>
        public static List<PropertyInfo> GetValueTypeProperties<T>(GetPropertiesOptions options)
        {
            return GetValueTypeProperties(typeof(T), options);
        }

        /// <summary>
        /// Get a list of properties with value type values.
        /// </summary>
        /// <typeparam name="T">Type with properties to evaluate.</typeparam>
        /// <param name="obj">Object to get the properties from.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select value type properties.</param>
        /// <returns>A list of properties with value types.</returns>
        public static List<PropertyInfo> GetValueTypeProperties<T>(T obj, GetPropertiesOptions options)
        {
            return GetValueTypeProperties(obj.GetType(), options);
        }

        /// <summary>
        /// Get a list of properties with value type values.
        /// </summary>
        /// <param name="type">Type with properties to evaluate.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select value type properties.</param>
        /// <returns>A list of properties with value types.</returns>
        public static List<PropertyInfo> GetValueTypeProperties(Type type, GetPropertiesOptions options)
        {
            options.ValueProperties = true;
            options.ClassProperties = false;
            options.InterfaceProperties = false;
            options.ListProperties = false;

            return GetProperties(type, options);
        }

        #endregion

        #region GetValueTypePropertyNames

        /// <summary>
        /// Get a list of property names with value type values.
        /// </summary>
        /// <typeparam name="T">Type with properties to evaluate.</typeparam>
        /// <returns>A list of property names with value types.</returns>
        public static List<string> GetValueTypePropertyNames<T>()
        {
            return GetValueTypePropertyNames(typeof(T));
        }

        /// <summary>
        /// Get a list of property names with value type values.
        /// </summary>
        /// <typeparam name="T">Type with properties to evaluate.</typeparam>
        /// <param name="obj">Object to get the properties from.</param>
        /// <returns>A list of property names with value types.</returns>
        public static List<string> GetValueTypePropertyNames<T>(T obj)
        {
            return GetValueTypePropertyNames(obj.GetType());
        }

        /// <summary>
        /// Get a list of property names with value type values.
        /// </summary>
        /// <param name="type">Type with properties to evaluate.</param>
        /// <returns>A list of property names with value types.</returns>
        public static List<string> GetValueTypePropertyNames(Type type)
        {
            return new List<string>(GetValueTypeProperties(type).Select(property => property.Name));
        }

        /// <summary>
        /// Get a list of property names with value type values.
        /// </summary>
        /// <typeparam name="T">Type with properties to evaluate.</typeparam>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select value type properties.</param>
        /// <returns>A list of property names with value types.</returns>
        public static List<string> GetValueTypePropertyNames<T>(GetPropertiesOptions options)
        {
            return GetValueTypePropertyNames(typeof(T), options);
        }

        /// <summary>
        /// Get a list of property names with value type values.
        /// </summary>
        /// <typeparam name="T">Type with properties to evaluate.</typeparam>
        /// <param name="obj">Object to get the properties from.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select value type properties.</param>
        /// <returns>A list of property names with value types.</returns>
        public static List<string> GetValueTypePropertyNames<T>(T obj, GetPropertiesOptions options)
        {
            return GetValueTypePropertyNames(obj.GetType(), options);
        }

        /// <summary>
        /// Get a list of property names with value type values.
        /// </summary>
        /// <param name="type">Type with properties to evaluate.</param>
        /// <param name="options">Options specifying which properties to get.
        /// These options ignore other value type choices, and only select value type properties.</param>
        /// <returns>A list of property names with value types.</returns>
        public static List<string> GetValueTypePropertyNames(Type type, GetPropertiesOptions options)
        {
            return new List<string>(GetValueTypeProperties(type, options).Select(property => property.Name));
        }

        #endregion

        #endregion
    }
}
