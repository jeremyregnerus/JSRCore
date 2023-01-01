// <copyright file="ObjectUtilities.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Collections;
using System.Diagnostics.CodeAnalysis;
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
        public static void CopyValuesFromObjectToObject<T>([DisallowNull] T objectToCopyFrom, [DisallowNull] T objectToCopyTo)
        {
            Type type = objectToCopyFrom.GetType();

            foreach (PropertyInfo property in type.GetProperties().Where(p => p.SetMethod != null && p.SetMethod.IsPublic))
            {
                property.SetValue(objectToCopyTo, property.GetValue(objectToCopyFrom));
            }

            foreach (PropertyInfo property in type.GetProperties().Where(p => (p.SetMethod == null || !p.SetMethod.IsPublic) && typeof(IList).IsAssignableFrom(p.PropertyType)))
            {
                IList objectFromList = (IList)property.GetValue(objectToCopyFrom)!;
                IList objectToList = (IList)property.GetValue(objectToCopyTo)!;

                objectToList.Clear();

                foreach (var item in objectFromList)
                {
                    objectToList.Add(item);
                }
            }

            foreach (PropertyInfo property in type.GetProperties().Where(x => (x.SetMethod == null || !x.SetMethod.IsPublic) && x.PropertyType.IsClass && x.PropertyType != typeof(string)))
            {
                CopyValuesFromObjectToObject((dynamic)property.GetValue(objectToCopyFrom)!, (dynamic)property.GetValue(objectToCopyTo)!);
            }
        }

        /// <summary>
        /// Creates a copy of an object using serialization.
        /// </summary>
        /// <typeparam name="T">Type of object to serialize.</typeparam>
        /// <param name="objectToCopy">Object to copy.</param>
        /// <returns>A copy of <paramref name="objectToCopy"/>.</returns>
        public static T GetSerializedCopyOfObject<T>([DisallowNull] T objectToCopy)
        {
            DataContractSerializer s = new(typeof(T), new DataContractSerializerSettings() { PreserveObjectReferences = true });

            using MemoryStream stream = new();
            s.WriteObject(stream, objectToCopy);
            stream.Position = 0;

            var copy = s.ReadObject(stream);

            if (copy is null)
            {
                throw new SerializationException($"Failed to deserialize {nameof(objectToCopy)}");
            }

            return (T)copy;
        }

        /// <summary>
        /// Creates a new instance of a specified type and populates all of its properties with random values.
        /// </summary>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <returns>A new object with random property values.</returns>
        public static T CreateInstanceWithRandomValues<T>()
        {
            T obj = Activator.CreateInstance<T>()!;
            PopulateObjectWithRandomValues(obj);
            return obj;
        }

        /// <summary>
        /// Creates a new instance of a specified type and populates specified properties with random values.
        /// </summary>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <param name="propertyNames">List of property names to add random values to.</param>
        /// <returns>A new object with random property values for the provided property names.</returns>
        public static T CreateInstanceWithRandomValues<T>(List<string> propertyNames)
        {
            T obj = Activator.CreateInstance<T>()!;
            PopulatePropertiesWithRandomValues(obj, propertyNames);
            return obj;
        }

        /// <summary>
        /// Creates a new instance of a specified Type and populates specified properties with random values.
        /// </summary>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <param name="properties">List of <see cref="PropertyInfo"/> to populate with random values.</param>
        /// <returns>A new object of the specified Type.</returns>
        public static T CreateInstanceWithRandomValues<T>(List<PropertyInfo> properties)
        {
            T obj = Activator.CreateInstance<T>()!;
            PopulatePropertiesWithRandomValues(obj, properties);
            return obj;
        }

        /// <summary>
        /// Creates a new Instance of a specified Type and populates all of its properties with random values.
        /// </summary>
        /// <param name="type">Type of object to create.</param>
        /// <returns>A new object of the specified type with populated values.</returns>
        public static dynamic CreateInstanceWithRandomValues(Type type)
        {
            var obj = Activator.CreateInstance(type)!;
            PopulateObjectWithRandomValues(obj);
            return obj;
        }

        /// <summary>
        /// Creates a new Instance of a specified Type and populates all of its public set properties.
        /// </summary>
        /// <param name="type">Type of object to create.</param>
        /// <param name="propertyNames">List of property names to add random values to.</param>
        /// <returns>A new object of the defined type with populated values.</returns>
        public static dynamic CreateInstanceWithRandomValues(Type type, List<string> propertyNames)
        {
            dynamic obj = Activator.CreateInstance(type)!;
            PopulatePropertiesWithRandomValues(obj, propertyNames);
            return obj;
        }

        /// <summary>
        /// Creates a new Instance of a specified Type and populates specific properties with random values.
        /// </summary>
        /// <param name="type">Type of object to create.</param>
        /// <param name="properties">List of <see cref="PropertyInfo"/> to populate with random values.</param>
        /// <returns>A new object of the specified type with populated values is specific properties.</returns>
        public static dynamic CreateInstanceWithRandomValues(Type type, List<PropertyInfo> properties)
        {
            dynamic obj = Activator.CreateInstance(type)!;
            PopulatePropertiesWithRandomValues(obj, properties);
            return obj;
        }

        /// <summary>
        /// Populates an object's properties with random values.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> to populate.</typeparam>
        /// <param name="obj"><see cref="object"/> to populate with random values.</param>
        public static void PopulateObjectWithRandomValues<T>([DisallowNull] T obj)
        {
            if (typeof(IList).IsAssignableFrom(obj.GetType()))
            {
                PopulateListWithRandomValues((IList)obj);
            }
            else
            {
                // Populates all of the readwrite properties including values, classes and lists with values.
                PopulatePropertiesWithRandomValues(obj, PropertyUtilities.GetReadWriteProperties(obj));

                // populates all of the readonly classes and lists with values.
                PopulatePropertiesWithRandomValues(obj, PropertyUtilities.GetListProperties(obj.GetType(), new GetPropertiesOptions(false) { ReadOnlyProperties = true }));
            }
        }

        /// <summary>
        /// Adds random values to the specified properties of an object.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to populate.</typeparam>
        /// <param name="obj">Object to populate properties to.</param>
        /// <param name="propertyNames">List of Property Names specifying which properties to populate values.</param>
        public static void PopulatePropertiesWithRandomValues<T>([DisallowNull] T obj, List<string> propertyNames)
        {
            if (propertyNames != null)
            {
                foreach (string propertyName in propertyNames)
                {
                    PopulatePropertyWithRandomValue(obj, propertyName);
                }
            }
        }

        /// <summary>
        /// Adds random values to the specified properties of an object.
        /// </summary>
        /// <typeparam name="T">Type of object with properties to populate.</typeparam>
        /// <param name="obj">Object to populate properties to.</param>
        /// <param name="properties">List of <see cref="PropertyInfo"/> specifying which properties to populate values.</param>
        public static void PopulatePropertiesWithRandomValues<T>([DisallowNull] T obj, List<PropertyInfo> properties)
        {
            if (properties != null)
            {
                foreach (PropertyInfo property in properties)
                {
                    PopulatePropertyWithRandomValue(obj, property);
                }
            }
        }

        /// <summary>
        /// Adds a random value to a property.
        /// </summary>
        /// <typeparam name="T">Type to add the property value to.</typeparam>
        /// <param name="obj">Object to add the property value to.</param>
        /// <param name="propertyName">Name of the property to add a random value to.</param>
        public static void PopulatePropertyWithRandomValue<T>([DisallowNull] T obj, string propertyName)
        {
            PopulatePropertyWithRandomValue(obj, obj.GetType().GetProperty(propertyName)!);
        }

        /// <summary>
        /// Adds a random value to a property.
        /// </summary>
        /// <typeparam name="T">Type to add the property value to.</typeparam>
        /// <param name="obj">Object to add the property value to.</param>
        /// <param name="property">Property to add the value to.</param>
        public static void PopulatePropertyWithRandomValue<T>([DisallowNull] T obj, PropertyInfo property)
        {
            if (PropertyUtilities.IsListProperty(property) || PropertyUtilities.IsClassProperty(property))
            {
                if (obj.GetType() == property.PropertyType)
                {
                    throw new ArgumentException($"The property {property.Name} is trying to add the type {property.PropertyType}, which is the same type.", nameof(property));
                }

                if (PropertyUtilities.IsReadWriteProperty(property))
                {
                    property.SetValue(obj, Activator.CreateInstance(property.PropertyType));
                }

                PopulateObjectWithRandomValues(property.GetValue(obj)!);

                return;
            }

            if (!PropertyUtilities.IsReadWriteProperty(property))
            {
                return;
            }

            switch (property.PropertyType)
            {
                case Type t when t == typeof(string):
                    property.SetValue(obj, RandomUtilities.GetRandomString((string)property.GetValue(obj)!));
                    break;
                case Type t when t.IsEnum:
                    property.SetValue(obj, RandomUtilities.GetRandomEnum((Enum)property.GetValue(obj)!));
                    break;
                case Type t when t == typeof(bool):
                    property.SetValue(obj, RandomUtilities.GetRandomBoolean((bool)property.GetValue(obj)!));
                    break;
                case Type t when t == typeof(int):
                    property.SetValue(obj, RandomUtilities.GetRandomInteger((int)property.GetValue(obj)!));
                    break;
                case Type t when t == typeof(DateTime):
                    property.SetValue(obj, RandomUtilities.GetRandomDateTime((DateTime)property.GetValue(obj)!));
                    break;
                case Type t when t == typeof(double):
                    property.SetValue(obj, RandomUtilities.GetRandomDouble((double)property.GetValue(obj)!));
                    break;
                case Type t when t == typeof(decimal):
                    property.SetValue(obj, RandomUtilities.GetRandomDecimal((decimal)property.GetValue(obj)!));
                    break;
                default:
                    throw new ArgumentException($"The property {property.Name} uses an unsupported value type of {property.PropertyType} for this method.", nameof(property));
            }
        }

        /// <summary>
        /// Populates a list with Random Values.
        /// </summary>
        /// <typeparam name="T">Type of list to populate.</typeparam>
        /// <param name="list">List to populate with random values.</param>
        /// <param name="parentType">Type of the object that contains the list.</param>
        public static void PopulateListWithRandomValues<T>(T list, Type? parentType = null) where T : IList
        {
            RemoveRandomItemsFromList(list);

            if (list.GetType().GenericTypeArguments[0] == parentType)
            {
                throw new ArgumentException($"The type {parentType} is the same as the parent object.", nameof(parentType));
            }

            AddRandomItemsToList(list);
        }

        /// <summary>
        /// Add a random number of new items to a list.
        /// </summary>
        /// <typeparam name="T">Type of list.</typeparam>
        /// <param name="list">List to add items to.</param>
        public static void AddRandomItemsToList<T>(T list) where T : IList
        {
            Type listType = list.GetType().GenericTypeArguments[0];

            if ((listType.IsClass || typeof(IList).IsAssignableFrom(listType)) && listType != typeof(string))
            {
                for (int i = 0; i < new Random().Next(5, 20); i++)
                {
                    list.Add(CreateInstanceWithRandomValues(listType));
                }
            }
            else
            {
                for (int i = 0; i < new Random().Next(5, 20); i++)
                {
                    list.Add(RandomUtilities.GetRandom(listType));
                }
            }
        }

        /// <summary>
        /// Changes the items within a list to populate them with new random values.
        /// </summary>
        /// <typeparam name="T">Type of list with items to change.</typeparam>
        /// <param name="list">List with items to change.</param>
        /// <param name="allItems">Change all items in list. False changes random items.</param>
        public static void ChangeListItems<T>(T list, bool allItems) where T : IList
        {
            foreach (var item in list)
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
        /// <param name="list">List to remove items from.</param>
        public static void RemoveRandomItemsFromList<T>(T list) where T : IList
        {
            int count = new Random().Next(list.Count);

            for (int i = 0; i < count; i++)
            {
                list.RemoveAt(new Random().Next(0, list.Count));
            }
        }
    }
}
