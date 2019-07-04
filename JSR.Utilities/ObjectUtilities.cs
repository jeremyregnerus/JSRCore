// <copyright file="ObjectUtilities.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace JSR.Utilities
{
    /// <summary>
    /// Creates and manipulates objects and their properties.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:DoNotUseRegions", Justification = "Document uses regions to separate Method Signatures.")]
    public static class ObjectUtilities
    {
        #region Copy Objects

        /// <summary>
        /// Copies the values from one object to another.
        /// </summary>
        /// <typeparam name="T">Type of object to perform the copy.</typeparam>
        /// <param name="objectToCopyFrom">Object to copy values from.</param>
        /// <param name="objectToCopyTo">Object to copy values to.</param>
        public static void CopyValuesFromObjectToObject<T>(T objectToCopyFrom, T objectToCopyTo)
        {
            // TODO: 2: Determine if this check is required.
            if (typeof(T) != objectToCopyFrom.GetType())
            {
                throw new Exception($"The generic type {typeof(T)} does not match the object {nameof(objectToCopyFrom)} type of {objectToCopyFrom.GetType()}.");
            }

            foreach (PropertyInfo property in PropertyUtilities.GetListOfPropertiesWithPublicSetMethod(objectToCopyFrom))
            {
                property.SetValue(objectToCopyTo, property.GetValue(objectToCopyFrom));
            }

            foreach (PropertyInfo property in PropertyUtilities.GetListOfReadOnlyProperties(typeof(T)).Where(p => typeof(IList).IsAssignableFrom(p.PropertyType)))
            {
                IList objectFromList = (IList)property.GetValue(objectToCopyFrom);
                IList objectToList = (IList)property.GetValue(objectToCopyTo);

                objectToList.Clear();

                foreach (var item in objectFromList)
                {
                    objectToList.Add(item);
                }
            }
        }

        /// <summary>
        /// Creates a copy of an object using serialization.
        /// </summary>
        /// <typeparam name="T">Type of object to serialize.</typeparam>
        /// <param name="objectToCopy">Object to copy.</param>
        /// <returns>A copy of <paramref name="objectToCopy"/>.</returns>
        public static T GetSerializedCopyOfObject<T>(T objectToCopy)
        {
            // TODO: 2: Determine if this check is required.
            if (typeof(T) != objectToCopy.GetType())
            {
                throw new Exception($"The generic type {typeof(T)} does not match the object {nameof(objectToCopy)} type of {objectToCopy.GetType()}.");
            }

            DataContractSerializer s = new DataContractSerializer(typeof(T), new DataContractSerializerSettings() { PreserveObjectReferences = true });

            using (MemoryStream stream = new MemoryStream())
            {
                s.WriteObject(stream, objectToCopy);
                stream.Position = 0;
                T retVal = (T)s.ReadObject(stream);

                if (retVal is IChangeTracking)
                {
                    ((IChangeTracking)retVal).AcceptChanges();
                }

                return retVal;
            }
        }

        #endregion

        #region Instance Creation

        /// <summary>
        /// Creates a new Instance of a specified Type and populates all of its public properties.
        /// </summary>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <returns>A new object of the specified type with populated values.</returns>
        public static T CreateInstanceWithRandomValues<T>()
        {
            T obj = Activator.CreateInstance<T>();

            if (typeof(IList).IsAssignableFrom(obj.GetType()))
            {
                PopulateListWithRandomValues((IList)obj);
            }
            else
            {
                PopulateObjectWithRandomValues(obj);
            }

            return obj;
        }

        /// <summary>
        /// Creates a new Instance of a specified Type and populates all of its public set properties.
        /// </summary>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <param name="propertyNamesToPopulate">List of property names to add random values to.</param>
        /// <returns>A new object of the specified type with populated values.</returns>
        public static T CreateInstanceWithRandomValues<T>(List<string> propertyNamesToPopulate)
        {
            T obj = Activator.CreateInstance<T>();
            PopulatePropertiesWithRandomValues(obj, propertyNamesToPopulate);
            return obj;
        }

        /// <summary>
        /// Creates a new instance of a specified Type and populates specific properties with random values.
        /// </summary>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <param name="propertiesToPopulate">List of <see cref="PropertyInfo"/> to populate with random values.</param>
        /// <returns>A new object of the specified Type.</returns>
        public static T CreateInstanceWithRandomValues<T>(List<PropertyInfo> propertiesToPopulate)
        {
            T obj = Activator.CreateInstance<T>();
            PopulatePropertiesWithRandomValues(obj, propertiesToPopulate);
            return obj;
        }

        /// <summary>
        /// Creates a new Instance of a specified Type and populates all of its public set properties.
        /// </summary>
        /// <param name="type">Type of object to create.</param>
        /// <returns>A new object of the specified type with populated values.</returns>
        public static object CreateInstanceWithRandomValues(Type type)
        {
            var obj = Activator.CreateInstance(type);

            if (typeof(IList).IsAssignableFrom(type))
            {
                PopulateListWithRandomValues((IList)obj);
            }
            else
            {
                PopulateObjectWithRandomValues(obj);
            }

            return obj;
        }

        /// <summary>
        /// Creates a new Instance of a specified Type and populates all of its public set properties.
        /// </summary>
        /// <param name="type">Type of object to create.</param>
        /// <param name="propertyNamesToPopulate">List of property names to add random values to.</param>
        /// <returns>A new object of the defined type with populated values.</returns>
        public static object CreateInstanceWithRandomValues(Type type, List<string> propertyNamesToPopulate)
        {
            var obj = Activator.CreateInstance(type);
            PopulatePropertiesWithRandomValues(obj, propertyNamesToPopulate);
            return obj;
        }

        /// <summary>
        /// Creates a new Instance of a specified Type and populates specific properties with random values.
        /// </summary>
        /// <param name="type">Type of object to create.</param>
        /// <param name="propertiesToPopulate">List of <see cref="PropertyInfo"/> to populate with random values.</param>
        /// <returns>A new object of the specified type with populated values is specific properties.</returns>
        public static object CreateInstanceWithRandomValues(Type type, List<PropertyInfo> propertiesToPopulate)
        {
            var obj = Activator.CreateInstance(type);
            PopulatePropertiesWithRandomValues(obj, propertiesToPopulate);
            return obj;
        }

        #endregion

        #region PopulateObjectWithRandomValues

        /// <summary>
        /// Populates an object's properties with random values.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> to populate.</typeparam>
        /// <param name="objectToPopulate"><see cref="object"/> to populate with random values.</param>
        public static void PopulateObjectWithRandomValues<T>(T objectToPopulate)
        {
            Type type = objectToPopulate.GetType();

            if (typeof(IList).IsAssignableFrom(type))
            {
                RemoveRandomItemsFromList((IList)objectToPopulate);
                PopulateListWithRandomValues((IList)objectToPopulate);
            }
            else
            {
                PopulatePropertiesWithRandomValues(objectToPopulate, PropertyUtilities.GetListOfPropertiesWithPublicSetMethod(objectToPopulate));

                foreach (PropertyInfo propertyInfo in PropertyUtilities.GetListOfReadOnlyProperties(objectToPopulate).Where(p => p.PropertyType.IsClass || typeof(IList).IsAssignableFrom(p.PropertyType)))
                {
                    PopulateObjectWithRandomValues(propertyInfo.GetValue(objectToPopulate));
                }
            }
        }

        #endregion

        #region PopulatePropertiesWithRandomValues

        /// <summary>
        /// Adds random values to the specified properties of an object.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to populate.</typeparam>
        /// <param name="objectWithProperties">Object to populate properties to.</param>
        /// <param name="propertyNames">List of Property Names specifying which properties to populate values.</param>
        public static void PopulatePropertiesWithRandomValues<T>(T objectWithProperties, List<string> propertyNames)
        {
            foreach (string propertyName in propertyNames)
            {
                PopulatePropertyWithRandomValue(objectWithProperties, propertyName);
            }
        }

        /// <summary>
        /// Adds random values to the specified properties of an object.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to populate.</typeparam>
        /// <param name="objectWithProperties">Object to populate properties to.</param>
        /// <param name="properties">List of <see cref="PropertyInfo"/> specifying which properties to populate values.</param>
        public static void PopulatePropertiesWithRandomValues<T>(T objectWithProperties, List<PropertyInfo> properties)
        {
            foreach (PropertyInfo property in properties)
            {
                PopulatePropertyWithRandomValue(objectWithProperties, property);
            }
        }

        #endregion

        #region PopulatePropertyWithRandomValue

        /// <summary>
        /// Adds a random value to a property.
        /// </summary>
        /// <typeparam name="T">Type to add the property value to.</typeparam>
        /// <param name="objectWithProperty">Object to add the property value to.</param>
        /// <param name="propertyName">Name of the property to add a random value to.</param>
        public static void PopulatePropertyWithRandomValue<T>(T objectWithProperty, string propertyName)
        {
            // TODO: 2: Determine if this check is required.
            if (typeof(T) != objectWithProperty.GetType())
            {
                throw new Exception($"The generic type {typeof(T)} does not match the object {nameof(objectWithProperty)} type of {objectWithProperty.GetType()}.");
            }

            PopulatePropertyWithRandomValue(objectWithProperty, typeof(T).GetRuntimeProperty(propertyName));
        }

        /// <summary>
        /// Adds a random value to a property.
        /// </summary>
        /// <typeparam name="T">Type to add the property value to.</typeparam>
        /// <param name="objectWithProperty">Object to add the property value to.</param>
        /// <param name="property">Property to add the value to.</param>
        public static void PopulatePropertyWithRandomValue<T>(T objectWithProperty, PropertyInfo property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property), $"{nameof(property)}cannot be null. Check that the property exists.");
            }

            if (PropertyUtilities.CheckIfPropertyHasPublicGetAndSetMethod(property))
            {
                switch (property.PropertyType)
                {
                    case Type t when t == typeof(string):
                        property.SetValue(objectWithProperty, RandomUtilities.GetRandomString((string)property.GetValue(objectWithProperty)));
                        break;
                    case Type t when t.IsEnum:
                        // TODO: 1: Need to verify this works.
                        property.SetValue(objectWithProperty, RandomUtilities.GetRandomEnum((Enum)property.GetValue(objectWithProperty)));
                        break;
                    case Type t when t == typeof(bool):
                        property.SetValue(objectWithProperty, RandomUtilities.GetRandomBoolean((bool)property.GetValue(objectWithProperty)));
                        break;
                    case Type t when t == typeof(int):
                        property.SetValue(objectWithProperty, RandomUtilities.GetRandomInteger((int)property.GetValue(objectWithProperty)));
                        break;
                    case Type t when t == typeof(DateTime):
                        property.SetValue(objectWithProperty, RandomUtilities.GetRandomDateTime((DateTime)property.GetValue(objectWithProperty)));
                        break;
                    case Type t when t == typeof(double):
                        property.SetValue(objectWithProperty, RandomUtilities.GetRandomDouble((double)property.GetValue(objectWithProperty)));
                        break;
                    case Type t when t == typeof(decimal):
                        property.SetValue(objectWithProperty, RandomUtilities.GetRandomDecimal((decimal)property.GetValue(objectWithProperty)));
                        break;
                    case Type t when typeof(IList).IsAssignableFrom(t) && property.PropertyType != typeof(string):
                        property.SetValue(objectWithProperty, CreateInstanceWithRandomValues(property.PropertyType));
                        break;
                    case Type t when t.IsClass || t.IsValueType:
                        property.SetValue(objectWithProperty, CreateInstanceWithRandomValues(property.PropertyType));
                        break;
                    default:
                        throw new ArgumentException($"The property {property.Name} uses an unsupported value type of {property.PropertyType} for this method.", nameof(property));
                }
            }
        }

        #endregion

        #region List Tools

        /// <summary>
        /// Populates a list with Random Values.
        /// </summary>
        /// <typeparam name="T">Type of list to populate.</typeparam>
        /// <param name="listToPopulate">List to populate with random values.</param>
        /// <param name="parentObjectType">Type of the object that contains the list.</param>
        public static void PopulateListWithRandomValues<T>(T listToPopulate, Type parentObjectType = null) where T : IList
        {
            if (listToPopulate == null)
            {
                throw new ArgumentNullException(nameof(listToPopulate));
            }

            Type listType = listToPopulate.GetType().GenericTypeArguments[0];

            if (listType == parentObjectType)
            {
                return;
            }

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                PopulateListWithRandomValue(listToPopulate, parentObjectType);
            }
        }

        /// <summary>
        /// Adds a random value to a list.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of list to add the value to.</typeparam>
        /// <param name="listToPopulate"><see cref="List{T}"/> to add the new random value to.</param>
        /// <param name="parentObjectType">Type of the object that contains the list.</param>
        public static void PopulateListWithRandomValue<T>(T listToPopulate, Type parentObjectType = null) where T : IList
        {
            if (listToPopulate == null)
            {
                throw new ArgumentNullException(nameof(listToPopulate));
            }

            Type listType = listToPopulate.GetType().GenericTypeArguments[0];

            if (listType == parentObjectType)
            {
                return;
            }

            if (listType == typeof(string))
            {
                listToPopulate.Add(RandomUtilities.GetRandomString());
            }
            else
            {
                listToPopulate.Add(CreateInstanceWithRandomValues(listType));
            }
        }

        /// <summary>
        /// Changes the items within a list to populate them with new random values.
        /// </summary>
        /// <typeparam name="T">Type of list with items to change.</typeparam>
        /// <param name="listToChange">List with items to change.</param>
        public static void ChangeListItems<T>(T listToChange) where T : IList
        {
            foreach (var item in listToChange)
            {
                PopulateObjectWithRandomValues(item);
            }
        }

        /// <summary>
        /// Randomly removes items from a list.
        /// </summary>
        /// <typeparam name="T">Type of list to remove items from.</typeparam>
        /// <param name="listToRemoveItems">List to remove items from.</param>
        public static void RemoveRandomItemsFromList<T>(T listToRemoveItems) where T : IList
        {
            int count = new Random().Next(listToRemoveItems.Count);

            for (int i = 0; i < count; i++)
            {
                listToRemoveItems.RemoveAt(new Random().Next(0, listToRemoveItems.Count));
            }
        }

        #endregion

        /// <summary>
        /// Gets a list from an object using the property name where the list is.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> that contains the <see cref="IList"/> property.</typeparam>
        /// <param name="objectWithList"><see cref="object"/> that contains a property of type <see cref="IList"/>.</param>
        /// <param name="listPropertyName">Name of the property that contains the <see cref="IList"/>.</param>
        /// <returns>The <see cref="IList"/> contained within the named property.</returns>
        public static IList GetObjectListByPropertyName<T>(T objectWithList, string listPropertyName)
        {
            PropertyInfo propertyInfo = objectWithList.GetType().GetRuntimeProperty(listPropertyName);

            if (!typeof(IList).IsAssignableFrom(propertyInfo.PropertyType))
            {
                throw new ArgumentException("{0} is not of type IList", propertyInfo.Name);
            }

            return (IList)propertyInfo.GetValue(objectWithList);
        }
    }
}
