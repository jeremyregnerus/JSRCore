// <copyright file="ObjectUtilities.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace JSR.Utilities
{
    /// <summary>
    /// Creates and manipulates objects and their properties.
    /// </summary>
    public static class ObjectUtilities
    {
        /// <summary>
        /// Copies the values from one object to another.
        /// </summary>
        /// <typeparam name="T">Type of object to perform the copy.</typeparam>
        /// <param name="objectToCopyFrom">Object to copy values from.</param>
        /// <param name="objectToCopyTo">Object to copy values to.</param>
        public static void CopyValuesFromObjectToObject<T>(T objectToCopyFrom, T objectToCopyTo)
        {
            Type type = objectToCopyFrom.GetType();

            foreach (PropertyInfo property in type.GetProperties().Where(p => p.SetMethod != null && p.SetMethod.IsPublic))
            {
                property.SetValue(objectToCopyTo, property.GetValue(objectToCopyFrom));
            }

            foreach (PropertyInfo property in type.GetProperties().Where(p => (p.SetMethod == null || !p.SetMethod.IsPublic) && typeof(IList).IsAssignableFrom(p.PropertyType)))
            {
                IList objectFromList = (IList)property.GetValue(objectToCopyFrom);
                IList objectToList = (IList)property.GetValue(objectToCopyTo);

                objectToList.Clear();

                foreach (var item in objectFromList)
                {
                    objectToList.Add(item);
                }
            }

            foreach (PropertyInfo property in type.GetProperties().Where(x => (x.SetMethod == null || !x.SetMethod.IsPublic) && x.PropertyType.IsClass && x.PropertyType != typeof(string)))
            {
                CopyValuesFromObjectToObject((dynamic)property.GetValue(objectToCopyFrom), (dynamic)property.GetValue(objectToCopyTo));
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

        /// <summary>
        /// Creates a new instance of a specified type and populates all of its properties with random values.
        /// </summary>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <returns>A new object with random property values.</returns>
        public static T CreateInstanceWithRandomValues<T>()
        {
            T obj = Activator.CreateInstance<T>();
            PopulateObjectWithRandomValues(obj);
            return obj;
        }

        /// <summary>
        /// Creates a new instance of a specified type and populates specified properties with random values.
        /// </summary>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <param name="propertyNamesToPopulate">List of property names to add random values to.</param>
        /// <returns>A new object with random property values for the provided property names.</returns>
        public static T CreateInstanceWithRandomValues<T>(List<string> propertyNamesToPopulate)
        {
            T obj = Activator.CreateInstance<T>();
            PopulatePropertiesWithRandomValues(obj, propertyNamesToPopulate);
            return obj;
        }

        /// <summary>
        /// Creates a new instance of a specified Type and populates specified properties with random values.
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
        /// Creates a new Instance of a specified Type and populates all of its properties with random values.
        /// </summary>
        /// <param name="type">Type of object to create.</param>
        /// <returns>A new object of the specified type with populated values.</returns>
        public static dynamic CreateInstanceWithRandomValues(Type type)
        {
            dynamic obj = Activator.CreateInstance(type);
            PopulateObjectWithRandomValues(obj);
            return obj;
        }

        /// <summary>
        /// Creates a new Instance of a specified Type and populates all of its public set properties.
        /// </summary>
        /// <param name="type">Type of object to create.</param>
        /// <param name="propertyNamesToPopulate">List of property names to add random values to.</param>
        /// <returns>A new object of the defined type with populated values.</returns>
        public static dynamic CreateInstanceWithRandomValues(Type type, List<string> propertyNamesToPopulate)
        {
            dynamic obj = Activator.CreateInstance(type);
            PopulatePropertiesWithRandomValues(obj, propertyNamesToPopulate);
            return obj;
        }

        /// <summary>
        /// Creates a new Instance of a specified Type and populates specific properties with random values.
        /// </summary>
        /// <param name="type">Type of object to create.</param>
        /// <param name="propertiesToPopulate">List of <see cref="PropertyInfo"/> to populate with random values.</param>
        /// <returns>A new object of the specified type with populated values is specific properties.</returns>
        public static dynamic CreateInstanceWithRandomValues(Type type, List<PropertyInfo> propertiesToPopulate)
        {
            dynamic obj = Activator.CreateInstance(type);
            PopulatePropertiesWithRandomValues(obj, propertiesToPopulate);
            return obj;
        }

        /// <summary>
        /// Populates an object's properties with random values.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> to populate.</typeparam>
        /// <param name="objectToPopulate"><see cref="object"/> to populate with random values.</param>
        public static void PopulateObjectWithRandomValues<T>(T objectToPopulate)
        {
            if (typeof(IList).IsAssignableFrom(objectToPopulate.GetType()))
            {
                PopulateListWithRandomValues((IList)objectToPopulate);
            }
            else
            {
                PopulatePropertiesWithRandomValues(objectToPopulate, PropertyUtilities.GetListOfPropertiesWithValueTypes(objectToPopulate, true, false, false));
                PopulatePropertiesWithRandomValues(objectToPopulate, PropertyUtilities.GetListOfPropertiesWithClassTypes(objectToPopulate, true, true, false));
                PopulatePropertiesWithRandomValues(objectToPopulate, PropertyUtilities.GetListOfPropertiesWithListTypes(objectToPopulate, true, true, false));

                PopulatePropertiesWithRandomValues(objectToPopulate, PropertyUtilities.GetListOfProperties(objectToPopulate.GetType(), true, false, false, true, true, true));
                PopulatePropertiesWithRandomValues(objectToPopulate, PropertyUtilities.GetListOfProperties(objectToPopulate.GetType(), false, true, false, false, true, true));
            }
        }

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

        /// <summary>
        /// Adds a random value to a property.
        /// </summary>
        /// <typeparam name="T">Type to add the property value to.</typeparam>
        /// <param name="objectWithProperty">Object to add the property value to.</param>
        /// <param name="propertyName">Name of the property to add a random value to.</param>
        public static void PopulatePropertyWithRandomValue<T>(T objectWithProperty, string propertyName)
        {
            PopulatePropertyWithRandomValue(objectWithProperty, objectWithProperty.GetType().GetRuntimeProperty(propertyName));
        }

        /// <summary>
        /// Adds a random value to a property.
        /// </summary>
        /// <typeparam name="T">Type to add the property value to.</typeparam>
        /// <param name="objectWithProperty">Object to add the property value to.</param>
        /// <param name="property">Property to add the value to.</param>
        public static void PopulatePropertyWithRandomValue<T>(T objectWithProperty, PropertyInfo property)
        {
            if (PropertyUtilities.CheckIfPropertyIsList(property) || PropertyUtilities.CheckIfPropertyIsClass(property))
            {
                if (PropertyUtilities.CheckIfPropertyIsReadWrite(property))
                {
                    property.SetValue(objectWithProperty, CreateInstanceWithRandomValues(property.PropertyType));
                }
                else if (PropertyUtilities.CheckIfPropertyIsReadOnly(property))
                {
                    PopulateObjectWithRandomValues(property.GetValue(objectWithProperty));
                }

                return;
            }

            switch (property.PropertyType)
            {
                case Type t when t == typeof(string):
                    property.SetValue(objectWithProperty, RandomUtilities.GetRandomString((string)property.GetValue(objectWithProperty)));
                    break;
                case Type t when t.IsEnum:
                    // TODO: 1 - Need to verify Enum provides the correct enum range of values.
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
                default:
                    throw new ArgumentException($"The property {property.Name} uses an unsupported value type of {property.PropertyType} for this method.", nameof(property));
            }
        }

        /// <summary>
        /// Populates a list with Random Values.
        /// </summary>
        /// <typeparam name="T">Type of list to populate.</typeparam>
        /// <param name="listToPopulate">List to populate with random values.</param>
        /// <param name="parentObjectType">Type of the object that contains the list.</param>
        public static void PopulateListWithRandomValues<T>(T listToPopulate, Type parentObjectType = null) where T : IList
        {
            RemoveRandomItemsFromList(listToPopulate);

            if (listToPopulate.GetType().GenericTypeArguments[0] == parentObjectType)
            {
                return;
            }

            AddRandomItemsToList(listToPopulate);
        }

        /// <summary>
        /// Adds a random value to a list.
        /// </summary>
        /// <typeparam name="T">Type of list.</typeparam>
        /// <param name="listToAddItem">List to add item to.</param>
        public static void AddRandomItemToList<T>(T listToAddItem) where T : IList
        {
            AddRandomItemToList(listToAddItem, listToAddItem.GetType().GenericTypeArguments[0]);
        }

        /// <summary>
        /// Adds a random value to a list.
        /// </summary>
        /// <typeparam name="T">Type of list.</typeparam>
        /// <param name="listToAddItem">List to add item to.</param>
        /// <param name="listObjectType">Type of item to add to list.</param>
        public static void AddRandomItemToList<T>(T listToAddItem, Type listObjectType) where T : IList
        {
            if (listObjectType != typeof(string) && (listObjectType.IsClass || typeof(IList).IsAssignableFrom(listObjectType)))
            {
                listToAddItem.Add(CreateInstanceWithRandomValues(listObjectType));
            }
            else
            {
                listToAddItem.Add(RandomUtilities.GetRandom(listObjectType));
            }
        }

        /// <summary>
        /// Add a random number of new items to a list.
        /// </summary>
        /// <typeparam name="T">Type of list.</typeparam>
        /// <param name="listToAddItemsTo">List to add items to.</param>
        public static void AddRandomItemsToList<T>(T listToAddItemsTo) where T : IList
        {
            AddRandomItemsToList(listToAddItemsTo, listToAddItemsTo.GetType().GenericTypeArguments[0]);
        }

        /// <summary>
        /// Add a random number of new items to a list.
        /// </summary>
        /// <typeparam name="T">Type of list.</typeparam>
        /// <param name="listToAddItems">List to add items to.</param>
        /// <param name="listObjectType">Type of items to add to list.</param>
        public static void AddRandomItemsToList<T>(T listToAddItems, Type listObjectType) where T : IList
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                AddRandomItemToList(listToAddItems, listObjectType);
            }
        }

        /// <summary>
        /// Changes the items within a list to populate them with new random values.
        /// </summary>
        /// <typeparam name="T">Type of list with items to change.</typeparam>
        /// <param name="listToChange">List with items to change.</param>
        /// <param name="allItems">Change all items in list. False changes random items.</param>
        public static void ChangeListItems<T>(T listToChange, bool allItems) where T : IList
        {
            foreach (var item in listToChange)
            {
                if (allItems || RandomUtilities.GetRandomBoolean())
                {
                    PopulateObjectWithRandomValues(item);
                }
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
    }
}
