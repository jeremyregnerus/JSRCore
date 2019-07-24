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
        /// <summary>
        /// Checks if a property is readwrite.
        /// </summary>
        /// <typeparam name="T">Type with property to check.</typeparam>
        /// <param name="propertyName">Name of property to check.</param>
        /// <returns>True if the property is readwrite.</returns>
        public static bool CheckIfPropertyIsReadWrite<T>(string propertyName)
        {
            return CheckIfPropertyIsReadWrite(typeof(T).GetProperty(propertyName));
        }

        /// <summary>
        /// Check if a property is readwrite.
        /// </summary>
        /// <typeparam name="T">Type with property to check.</typeparam>
        /// <param name="objectWithProperty">Object with property to check.</param>
        /// <param name="propertyName">Name of property to check.</param>
        /// <returns>True if the property is readwrite.</returns>
        public static bool CheckIfPropertyIsReadWrite<T>(T objectWithProperty, string propertyName)
        {
            return CheckIfPropertyIsReadWrite(objectWithProperty.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Checks if a property is readwrite.
        /// </summary>
        /// <param name="type">Type with property to check.</param>
        /// <param name="propertyName">Name of property to check.</param>
        /// <returns>True if the property is readwrite.</returns>
        public static bool CheckIfPropertyIsReadWrite(Type type, string propertyName)
        {
            return CheckIfPropertyIsReadWrite(type.GetProperty(propertyName));
        }

        /// <summary>
        /// Checks if a property is readwrite.
        /// </summary>
        /// <param name="property">Property to check.</param>
        /// <returns>True if the property is readwrite.</returns>
        public static bool CheckIfPropertyIsReadWrite(PropertyInfo property)
        {
            return property.GetMethod != null && property.GetMethod.IsPublic && property.SetMethod != null && property.SetMethod.IsPublic;
        }

        /// <summary>
        /// Checks if a property is readonly.
        /// </summary>
        /// <typeparam name="T">Type with property to check.</typeparam>
        /// <param name="propertyName">Name of property to check.</param>
        /// <returns>True if the property is readonly.</returns>
        public static bool CheckIfPropertyIsReadOnly<T>(string propertyName)
        {
            return CheckIfPropertyIsReadOnly(typeof(T).GetProperty(propertyName));
        }

        /// <summary>
        /// Checks if a property is readonly.
        /// </summary>
        /// <typeparam name="T">Type with property to check.</typeparam>
        /// <param name="objectWithProperty">Object with property to check.</param>
        /// <param name="propertyName">Name of property to check.</param>
        /// <returns>True if the property is readonly.</returns>
        public static bool CheckIfPropertyIsReadOnly<T>(T objectWithProperty, string propertyName)
        {
            return CheckIfPropertyIsReadOnly(objectWithProperty.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Checks if a property is readonly.
        /// </summary>
        /// <param name="type">Type with property to check.</param>
        /// <param name="propertyName">Name of property to check.</param>
        /// <returns>True if the property is readonly.</returns>
        public static bool CheckIfPropertyIsReadOnly(Type type, string propertyName)
        {
            return CheckIfPropertyIsReadOnly(type.GetProperty(propertyName));
        }

        /// <summary>
        /// Checks if a property is readonly.
        /// </summary>
        /// <param name="property">Property to check.</param>
        /// <returns>True if the property is readonly.</returns>
        public static bool CheckIfPropertyIsReadOnly(PropertyInfo property)
        {
            return property.GetMethod != null && property.GetMethod.IsPublic && (property.SetMethod == null || !property.SetMethod.IsPublic);
        }

        /// <summary>
        /// Checks if a property is writeonly.
        /// </summary>
        /// <typeparam name="T">Type with property to check.</typeparam>
        /// <param name="propertyName">Name of property to check.</param>
        /// <returns>True if the property is writeonly.</returns>
        public static bool CheckIfPropertyIsWriteOnly<T>(string propertyName)
        {
            return CheckIfPropertyIsWriteOnly(typeof(T).GetProperty(propertyName));
        }

        /// <summary>
        /// Checks if a property is writeonly.
        /// </summary>
        /// <typeparam name="T">Type with property to check.</typeparam>
        /// <param name="objectWithProperty">Object with property to check.</param>
        /// <param name="propertyName">Name of property to check.</param>
        /// <returns>True if the property is writeonly.</returns>
        public static bool CheckIfPropertyIsWriteOnly<T>(T objectWithProperty, string propertyName)
        {
            return CheckIfPropertyIsWriteOnly(objectWithProperty.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Checks if a property is writeonly.
        /// </summary>
        /// <param name="type">Type with property to check.</param>
        /// <param name="propertyName">Name of property to check.</param>
        /// <returns>True if the property is writeonly.</returns>
        public static bool CheckIfPropertyIsWriteOnly(Type type, string propertyName)
        {
            return CheckIfPropertyIsWriteOnly(type.GetProperty(propertyName));
        }

        /// <summary>
        /// Checks if a property is writeonly.
        /// </summary>
        /// <param name="property">Property to check.</param>
        /// <returns>True if the property is writeonly.</returns>
        public static bool CheckIfPropertyIsWriteOnly(PropertyInfo property)
        {
            return property.SetMethod != null && property.SetMethod.IsPublic && (property.GetMethod == null || !property.GetMethod.IsPublic);
        }

        /// <summary>
        /// Checks if a property contains a class type.
        /// </summary>
        /// <typeparam name="T">Type with property to check.</typeparam>
        /// <param name="propertyName">Property name to check.</param>
        /// <returns>True if the property is a class.</returns>
        public static bool CheckIfPropertyIsClass<T>(string propertyName)
        {
            return CheckIfPropertyIsClass(typeof(T), propertyName);
        }

        /// <summary>
        /// Checks if a property contains a class type.
        /// </summary>
        /// <typeparam name="T">Type with property to check.</typeparam>
        /// <param name="objectWithProperty">Object with property to check.</param>
        /// <param name="propertyName">Property name to check.</param>
        /// <returns>True if the property is a class.</returns>
        public static bool CheckIfPropertyIsClass<T>(T objectWithProperty, string propertyName)
        {
            return CheckIfPropertyIsClass(objectWithProperty.GetType(), propertyName);
        }

        /// <summary>
        /// Checks if a property contains a class type.
        /// </summary>
        /// <param name="type">Type with property to check.</param>
        /// <param name="propertyName">Property name to check.</param>
        /// <returns>True if the property is a class.</returns>
        public static bool CheckIfPropertyIsClass(Type type, string propertyName)
        {
            return CheckIfPropertyIsClass(type.GetProperty(propertyName));
        }

        /// <summary>
        /// Checks if a property contains class type.
        /// </summary>
        /// <param name="property">Property to check.</param>
        /// <returns>True if the property is a class.</returns>
        public static bool CheckIfPropertyIsClass(PropertyInfo property)
        {
            return property.PropertyType.IsClass && property.PropertyType != typeof(string);
        }

        /// <summary>
        /// Checks if the property is a list.
        /// </summary>
        /// <typeparam name="T">Type with property to check.</typeparam>
        /// <param name="propertyName">Name of property to check.</param>
        /// <returns>True if the property is a list.</returns>
        public static bool CheckIfPropertyIsList<T>(string propertyName)
        {
            return CheckIfPropertyIsList(typeof(T).GetProperty(propertyName));
        }

        /// <summary>
        /// Checks if the property is a list.
        /// </summary>
        /// <typeparam name="T">Type with property to check.</typeparam>
        /// <param name="objectWithProperty">Object with property to check.</param>
        /// <param name="propertyName">Name of property to check.</param>
        /// <returns>True if the property is a list.</returns>
        public static bool CheckIfPropertyIsList<T>(T objectWithProperty, string propertyName)
        {
            return CheckIfPropertyIsList(objectWithProperty.GetType().GetProperty(propertyName));
        }

        /// <summary>
        /// Checks if a property contains a list type.
        /// </summary>
        /// <param name="type">Type with property to check.</param>
        /// <param name="propertyName">Property Name to check.</param>
        /// <returns>True if the property is a list.</returns>
        public static bool CheckIfPropertyIsList(Type type, string propertyName)
        {
            return CheckIfPropertyIsList(type.GetProperty(propertyName));
        }

        /// <summary>
        /// Checks if a property contains a list type.
        /// </summary>
        /// <param name="property">Property to check.</param>
        /// <returns>True if the property is a list.</returns>
        public static bool CheckIfPropertyIsList(PropertyInfo property)
        {
            return typeof(IList).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string);
        }

        /// <summary>
        /// Returns a list of properties that meet the argument criteria.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <param name="valueTypes">Include properties with value types.</param>
        /// <param name="classTypes">Include properties with class types.</param>
        /// <param name="listTypes">Include properties with list types.</param>
        /// <returns>A list of <see cref="PropertyInfo"/>.</returns>
        public static List<PropertyInfo> GetListOfProperties<T>(bool readWrite, bool readOnly, bool writeOnly, bool valueTypes, bool classTypes, bool listTypes)
        {
            return GetListOfProperties(typeof(T), readWrite, readOnly, writeOnly, valueTypes, classTypes, listTypes);
        }

        /// <summary>
        /// Returns a list of properties that meet the argument criteria.
        /// </summary>
        /// <typeparam name="T">Type to get the properties from.</typeparam>
        /// <param name="objectWithProperties">Object to get properties from.</param>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <param name="valueTypes">Include properties with value types.</param>
        /// <param name="classTypes">Include properties with class types.</param>
        /// <param name="listTypes">Include properties with list types.</param>
        /// <returns>A list of <see cref="PropertyInfo"/>.</returns>
        public static List<PropertyInfo> GetListOfProperties<T>(T objectWithProperties, bool readWrite, bool readOnly, bool writeOnly, bool valueTypes, bool classTypes, bool listTypes)
        {
            return GetListOfProperties(objectWithProperties.GetType(), readWrite, readOnly, writeOnly, valueTypes, classTypes, listTypes);
        }

        /// <summary>
        /// Returns a list of properties that meet the argument criteria.
        /// </summary>
        /// <param name="type">Type to get the properties from.</param>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <param name="valueTypes">Include properties with value types.</param>
        /// <param name="classTypes">Include properties with class types.</param>
        /// <param name="listTypes">Include properties with list types.</param>
        /// <returns>A list of <see cref="PropertyInfo"/>.</returns>
        public static List<PropertyInfo> GetListOfProperties(Type type, bool readWrite, bool readOnly, bool writeOnly, bool valueTypes, bool classTypes, bool listTypes)
        {
            List<PropertyInfo> returnProperties = new List<PropertyInfo>();

            if (valueTypes)
            {
                returnProperties.AddRange(GetListOfPropertiesWithValueTypes(type, readWrite, readOnly, writeOnly));
            }

            if (classTypes)
            {
                returnProperties.AddRange(GetListOfPropertiesWithClassTypes(type, readWrite, readOnly, writeOnly));
            }

            if (listTypes)
            {
                returnProperties.AddRange(GetListOfPropertiesWithListTypes(type, readWrite, readOnly, writeOnly));
            }

            return returnProperties;
        }

        /// <summary>
        /// Returns a list of properties names that meet the argument criteria.
        /// </summary>
        /// <typeparam name="T">Type to get property names from.</typeparam>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <param name="valueTypes">Include properties with value types.</param>
        /// <param name="classTypes">Include properties with class types.</param>
        /// <param name="listTypes">Include properties with list types.</param>
        /// <returns>A list of property names.</returns>
        public static List<string> GetListOfPropertyNames<T>(bool readWrite, bool readOnly, bool writeOnly, bool valueTypes, bool classTypes, bool listTypes)
        {
            return GetListOfPropertyNames(typeof(T), readWrite, readOnly, writeOnly, valueTypes, classTypes, listTypes);
        }

        /// <summary>
        /// Returns a list of properties names that meet the argument criteria.
        /// </summary>
        /// <typeparam name="T">Type to get property names from.</typeparam>
        /// <param name="objectWithProperties">Object to get property names from.</param>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <param name="valueTypes">Include properties with value types.</param>
        /// <param name="classTypes">Include properties with class types.</param>
        /// <param name="listTypes">Include properties with list types.</param>
        /// <returns>A list of property names.</returns>
        public static List<string> GetListOfPropertyNames<T>(T objectWithProperties, bool readWrite, bool readOnly, bool writeOnly, bool valueTypes, bool classTypes, bool listTypes)
        {
            return GetListOfPropertyNames(objectWithProperties.GetType(), readWrite, readOnly, writeOnly, valueTypes, classTypes, listTypes);
        }

        /// <summary>
        /// Returns a list of property names that meet the argument criteria.
        /// </summary>
        /// <param name="type">Type to get property names from.</param>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <param name="valueTypes">Include properties with value types.</param>
        /// <param name="classTypes">Include properties with class types.</param>
        /// <param name="listTypes">Include properties with list types.</param>
        /// <returns>A list of property names.</returns>
        public static List<string> GetListOfPropertyNames(Type type, bool readWrite, bool readOnly, bool writeOnly, bool valueTypes, bool classTypes, bool listTypes)
        {
            return new List<string>(GetListOfProperties(type, readWrite, readOnly, writeOnly, valueTypes, classTypes, listTypes).Select(property => property.Name));
        }

        /// <summary>
        /// Gets a list of Properties with class values.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to evaluate.</typeparam>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <returns>A list of properties with class values.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithClassTypes<T>(bool readWrite, bool readOnly, bool writeOnly)
        {
            return GetListOfPropertiesWithClassTypes(typeof(T), readWrite, readOnly, writeOnly);
        }

        /// <summary>
        /// Gets a list of Properties with class values.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to evaluate.</typeparam>
        /// <param name="objectWithProperties">Object with properties to evaluate.</param>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <returns>A list of properties with class values.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithClassTypes<T>(T objectWithProperties, bool readWrite, bool readOnly, bool writeOnly)
        {
            return GetListOfPropertiesWithClassTypes(objectWithProperties.GetType(), readWrite, readOnly, writeOnly);
        }

        /// <summary>
        /// Gets a list of Properties with class values.
        /// </summary>
        /// <param name="type">Type of object with properties to evaluate.</param>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <returns>A list of properties with class values.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithClassTypes(Type type, bool readWrite, bool readOnly, bool writeOnly)
        {
            List<PropertyInfo> classProperties = new List<PropertyInfo>(type.GetProperties().Where(property => property.PropertyType.IsClass && property.PropertyType != typeof(string)));

            List<PropertyInfo> properties = new List<PropertyInfo>();

            if (readWrite)
            {
                properties.AddRange(classProperties.Where(property => CheckIfPropertyIsReadWrite(property)));
            }

            if (readOnly)
            {
                properties.AddRange(classProperties.Where(property => CheckIfPropertyIsReadOnly(property)));
            }

            if (writeOnly)
            {
                properties.AddRange(classProperties.Where(property => CheckIfPropertyIsWriteOnly(property)));
            }

            return properties;
        }

        /// <summary>
        /// Gets a list of property names with class values.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to evaluate.</typeparam>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <returns>A list of property names with class values.</returns>
        public static List<string> GetListOfPropertyNamesWithClassTypes<T>(bool readWrite, bool readOnly, bool writeOnly)
        {
            return GetListOfPropertyNamesWithClassTypes(typeof(T), readWrite, readOnly, writeOnly);
        }

        /// <summary>
        /// Gets a list of property names with class values.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to evaluate.</typeparam>
        /// <param name="objectWithProperties">Object with properties to evaluate.</param>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <returns>A list of property names with class values.</returns>
        public static List<string> GetListOfPropertyNamesWithClassTypes<T>(T objectWithProperties, bool readWrite, bool readOnly, bool writeOnly)
        {
            return GetListOfPropertyNamesWithClassTypes(objectWithProperties.GetType(), readWrite, readOnly, writeOnly);
        }

        /// <summary>
        /// Gets a list of property names with class values.
        /// </summary>
        /// <param name="type">Type of object with properties to evaluate.</param>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <returns>A list of property names with class values.</returns>
        public static List<string> GetListOfPropertyNamesWithClassTypes(Type type, bool readWrite, bool readOnly, bool writeOnly)
        {
            return new List<string>(GetListOfPropertiesWithClassTypes(type, readWrite, readOnly, writeOnly).Select(p => p.Name));
        }

        /// <summary>
        /// Gets a list of properties with list values.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to evaluate.</typeparam>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <returns>A list of properties with list values.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithListTypes<T>(bool readWrite, bool readOnly, bool writeOnly)
        {
            return GetListOfPropertiesWithListTypes(typeof(T), readWrite, readOnly, writeOnly);
        }

        /// <summary>
        /// Gets a list of properties with list values.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to evaluate.</typeparam>
        /// <param name="objectWithProperties">Object with properties to evaluate.</param>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <returns>A list of properties with lists values.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithListTypes<T>(T objectWithProperties, bool readWrite, bool readOnly, bool writeOnly)
        {
            return GetListOfPropertiesWithListTypes(objectWithProperties.GetType(), readWrite, readOnly, writeOnly);
        }

        /// <summary>
        /// Gets a list of properties with list values.
        /// </summary>
        /// <param name="type">Type with properties to evaluate.</param>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <returns>A list of properties with lists values.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithListTypes(Type type, bool readWrite, bool readOnly, bool writeOnly)
        {
            List<PropertyInfo> listProperties = new List<PropertyInfo>(type.GetProperties().Where(property => typeof(IList).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string)));

            List<PropertyInfo> properties = new List<PropertyInfo>();

            if (readWrite)
            {
                properties.AddRange(listProperties.Where(property => CheckIfPropertyIsReadWrite(property)));
            }

            if (readOnly)
            {
                properties.AddRange(listProperties.Where(property => CheckIfPropertyIsReadOnly(property)));
            }

            if (writeOnly)
            {
                properties.AddRange(listProperties.Where(property => CheckIfPropertyIsWriteOnly(property)));
            }

            return properties;
        }

        /// <summary>
        /// Gets a list of property names with list values.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to evaluate.</typeparam>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <returns>A list of property names with list values.</returns>
        public static List<string> GetListOfPropertyNamesWithListTypes<T>(bool readWrite, bool readOnly, bool writeOnly)
        {
            return GetListOfPropertyNamesWithListTypes(typeof(T), readWrite, readOnly, writeOnly);
        }

        /// <summary>
        /// Gets a list of property names with list values.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to evaluate.</typeparam>
        /// <param name="objectWithProperties">An object with properties to evaluate.</param>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <returns>A list of property names with list values.</returns>
        public static List<string> GetListOfPropertyNamesWithListTypes<T>(T objectWithProperties, bool readWrite, bool readOnly, bool writeOnly)
        {
            return GetListOfPropertyNamesWithListTypes(objectWithProperties.GetType(), readWrite, readOnly, writeOnly);
        }

        /// <summary>
        /// Gets a list of property names with list values.
        /// </summary>
        /// <param name="type">Type of object with properties to evaluate.</param>
        /// <param name="readWrite">Include ReadWrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <returns>A list of property names with list values.</returns>
        public static List<string> GetListOfPropertyNamesWithListTypes(Type type, bool readWrite, bool readOnly, bool writeOnly)
        {
            return new List<string>(GetListOfPropertiesWithListTypes(type, readWrite, readOnly, writeOnly).Select(p => p.Name));
        }

        /// <summary>
        /// Get a list of properties with value types.
        /// </summary>
        /// <typeparam name="T">Type with properties to evaluate.</typeparam>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <returns>A list of properties with value types.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithValueTypes<T>(bool readWrite, bool readOnly, bool writeOnly)
        {
            return GetListOfPropertiesWithValueTypes(typeof(T), readWrite, readOnly, writeOnly);
        }

        /// <summary>
        /// Get a list of properties with value types.
        /// </summary>
        /// <typeparam name="T">Type with properties to evaluate.</typeparam>
        /// <param name="objectWithProperties">Object with properties to evaluate.</param>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <returns>A list of properties with value types.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithValueTypes<T>(T objectWithProperties, bool readWrite, bool readOnly, bool writeOnly)
        {
            return GetListOfPropertiesWithValueTypes(objectWithProperties.GetType(), readWrite, readOnly, writeOnly);
        }

        /// <summary>
        /// Get a list of properties with value types.
        /// </summary>
        /// <param name="type">Type with properties to evaluate.</param>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">include writeonly properties.</param>
        /// <returns>A list of properties with value types.</returns>
        public static List<PropertyInfo> GetListOfPropertiesWithValueTypes(Type type, bool readWrite, bool readOnly, bool writeOnly)
        {
            List<PropertyInfo> allValueProperties = new List<PropertyInfo>(type.GetProperties().Where(property => !CheckIfPropertyIsClass(property) && !CheckIfPropertyIsList(property)));

            List<PropertyInfo> returnProperties = new List<PropertyInfo>();

            if (readWrite)
            {
                returnProperties.AddRange(allValueProperties.Where(property => CheckIfPropertyIsReadWrite(property)));
            }

            if (readOnly)
            {
                returnProperties.AddRange(allValueProperties.Where(property => CheckIfPropertyIsReadOnly(property)));
            }

            if (writeOnly)
            {
                returnProperties.AddRange(allValueProperties.Where(property => CheckIfPropertyIsWriteOnly(property)));
            }

            return returnProperties;
        }

        /// <summary>
        /// Get a list of property names with value types.
        /// </summary>
        /// <typeparam name="T">Type with properties to evaluate.</typeparam>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <returns>A list of property names with value types.</returns>
        public static List<string> GetListOfPropertyNamesWithValueTypes<T>(bool readWrite, bool readOnly, bool writeOnly)
        {
            return GetListOfPropertyNamesWithValueTypes(typeof(T), readWrite, readOnly, writeOnly);
        }

        /// <summary>
        /// Get a list of property names with value types.
        /// </summary>
        /// <typeparam name="T">Type with properties to evaluate.</typeparam>
        /// <param name="objectWithProperties">Object with properties to evaluate.</param>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <returns>A list of property names with value types.</returns>
        public static List<string> GetListOfPropertyNamesWithValueTypes<T>(T objectWithProperties, bool readWrite, bool readOnly, bool writeOnly)
        {
            return GetListOfPropertyNamesWithValueTypes(objectWithProperties.GetType(), readWrite, readOnly, writeOnly);
        }

        /// <summary>
        /// Get a list of property names with value types.
        /// </summary>
        /// <param name="type">Type with properties to evaluate.</param>
        /// <param name="readWrite">Include readwrite properties.</param>
        /// <param name="readOnly">Include readonly properties.</param>
        /// <param name="writeOnly">Include writeonly properties.</param>
        /// <returns>A list of property names with value types.</returns>
        public static List<string> GetListOfPropertyNamesWithValueTypes(Type type, bool readWrite, bool readOnly, bool writeOnly)
        {
            return new List<string>(GetListOfPropertiesWithValueTypes(type, readWrite, readOnly, writeOnly).Select(property => property.Name));
        }
    }
}
