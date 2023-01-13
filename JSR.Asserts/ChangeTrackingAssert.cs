using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.Asserts
{
    /// <summary>
    /// Asserts and checks objects that implement IChangeTracking.
    /// </summary>
    public static class ChangeTrackingAssert
    {
        #region IsChangedWhenCreated

        /// <summary>
        /// Asserts that a type of <see cref="IChangeTracking"/> is flagged <see cref="IChangeTracking.IsChanged"/> when initialized.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type to test.</param>
        public static void IsChangedWhenCreated(this Assert assert, Type type)
        {
            IsChangedWhenCreated(assert, CheckTypeIsIChangeTracking(type));
        }

        /// <summary>
        /// Asserts that a type of <see cref="IChangeTracking"/> is flagged <see cref="IChangeTracking.IsChanged"/> when initialized.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="IChangeTracking"/> to test.</typeparam>
        /// <param name="assert">Assert extension.</param>
        public static void IsChangedWhenCreated<T>(this Assert assert) where T : IChangeTracking
        {
            IsChangedWhenCreated(assert, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Asserts that a type of <see cref="IChangeTracking"/> is flagged <see cref="IChangeTracking.IsChanged"/> when initialized.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="IChangeTracking"/> to test.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object to test. This object should be a new object for this test.</param>
        public static void IsChangedWhenCreated<T>(this Assert assert, T obj) where T : IChangeTracking
        {
            _ = assert;
            Assert.IsTrue(obj.IsChanged);
        }

        #endregion

        #region AcceptsChanges

        /// <summary>
        /// Asserts that an object and it's class and list properties can accept changes.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        public static void AcceptsChanges(this Assert assert, Type type)
        {
            AcceptsChanges(assert, CheckTypeIsIChangeTracking(type));
        }

        /// <summary>
        /// Asserts that an object and it's class and list properties can accept changes.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        public static void AcceptsChanges<T>(this Assert assert) where T : IChangeTracking
        {
            AcceptsChanges(assert, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Asserts that an object and it's class and list properties can accept changes.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object to test.</param>
        public static void AcceptsChanges<T>(this Assert assert, T obj) where T : IChangeTracking
        {
            _ = assert;

            ObjectUtilities.PopulateObjectWithRandomValues(obj);

            obj.AcceptChanges();
            Assert.IsFalse(obj.IsChanged);

            foreach (PropertyInfo property in PropertyUtilities.GetProperties(obj, new(true) { WriteOnlyProperties = false }))
            {
                if (property.GetValue(obj) is IChangeTracking tracking)
                {
                    Assert.IsFalse(tracking.IsChanged);
                }
            }
        }

        #endregion

        #region IsChangedWhenHierarchyChanges

        /// <summary>
        /// Asserts that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        public static void IsChangedWhenHierarchyChanges(this Assert assert, Type type)
        {
            IsChangedWhenHierarchyChanges(assert, CheckTypeIsIChangeTracking(type));
        }

        /// <summary>
        /// Asserts that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void IsChangedWhenHierarchyChanges(this Assert assert, Type type, List<string> propertyNames)
        {
            IsChangedWhenHierarchyChanges(assert, CheckTypeIsIChangeTracking(type), propertyNames);
        }

        /// <summary>
        /// Asserts that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void IsChangedWhenHierarchyChanges(this Assert assert, Type type, List<PropertyInfo> properties)
        {
            IsChangedWhenHierarchyChanges(assert, CheckTypeIsIChangeTracking(type), properties);
        }

        /// <summary>
        /// Asserts that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void IsChangedWhenHierarchyChanges(this Assert assert, Type type, string propertyName)
        {
            IsChangedWhenHierarchyChanges(assert, CheckTypeIsIChangeTracking(type), propertyName);
        }

        /// <summary>
        /// Asserts that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="property">Property to test.</param>
        public static void IsChangedWhenHierarchyChanges(this Assert assert, Type type, PropertyInfo property)
        {
            IsChangedWhenHierarchyChanges(assert, CheckTypeIsIChangeTracking(type), property);
        }

        /// <summary>
        /// Asserts that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        public static void IsChangedWhenHierarchyChanges<T>(this Assert assert) where T : IChangeTracking
        {
            IsChangedWhenHierarchyChanges(assert, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Asserts that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void IsChangedWhenHierarchyChanges<T>(this Assert assert, List<string> propertyNames) where T : IChangeTracking
        {
            IsChangedWhenHierarchyChanges(assert, Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Asserts that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void IsChangedWhenHierarchyChanges<T>(this Assert assert, List<PropertyInfo> properties) where T : IChangeTracking
        {
            IsChangedWhenHierarchyChanges(assert, Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Asserts that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="propertyName">Property name to test.</param>
        public static void IsChangedWhenHierarchyChanges<T>(this Assert assert, string propertyName) where T : IChangeTracking
        {
            IsChangedWhenHierarchyChanges(assert, Activator.CreateInstance<T>(), propertyName);
        }

        /// <summary>
        /// Asserts that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="property">Property to test.</param>
        public static void IsChangedWhenHierarchyChanges<T>(this Assert assert, PropertyInfo property) where T : IChangeTracking
        {
            IsChangedWhenHierarchyChanges(assert, Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Asserts that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with properties to test.</param>
        public static void IsChangedWhenHierarchyChanges<T>(this Assert assert, T obj) where T : IChangeTracking
        {
            IsChangedWhenHierarchyChanges(assert, obj, PropertyUtilities.GetProperties(obj, new GetPropertiesOptions(true) { WriteOnlyProperties = false }));
        }

        /// <summary>
        /// Asserts that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void IsChangedWhenHierarchyChanges<T>(this Assert assert, T obj, List<string> propertyNames) where T : IChangeTracking
        {
            foreach (string propertyName in propertyNames)
            {
                IsChangedWhenHierarchyChanges(assert, obj, propertyName);
            }
        }

        /// <summary>
        /// Asserts that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void IsChangedWhenHierarchyChanges<T>(this Assert assert, T obj, List<PropertyInfo> properties) where T : IChangeTracking
        {
            foreach (PropertyInfo property in properties)
            {
                IsChangedWhenHierarchyChanges(assert, obj, property);
            }
        }

        /// <summary>
        /// Asserts that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyName">Name of property to test.</param>
        public static void IsChangedWhenHierarchyChanges<T>(this Assert assert, T obj, string propertyName) where T : IChangeTracking
        {
            IsChangedWhenHierarchyChanges(assert, obj, obj.GetType().GetProperty(propertyName)!);
        }

        /// <summary>
        /// Asserts that when properties, lists and classes change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="property">Property to test.</param>
        public static void IsChangedWhenHierarchyChanges<T>(this Assert assert, T obj, PropertyInfo property) where T : IChangeTracking
        {
            if (PropertyUtilities.IsReadWriteProperty(property))
            {
                IsChangedWhenPropertyChanges(assert, obj, property);
            }

            if (PropertyUtilities.IsClassProperty(property))
            {
                IsChangedWhenClassPropertyChanges(assert, obj, property);
            }

            if (PropertyUtilities.IsListProperty(property))
            {
                IsChangedWhenListPropertyChanges(assert, obj, property, true, true, true);
            }
        }

        #endregion

        #region IsChangedWhenPropertiesChange

        /// <summary>
        /// Asserts that each property changes the IsChanged state of an object when their values change.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenPropertiesChange(this Assert assert, Type type)
        {
            IsChangedWhenPropertiesChange(assert, CheckTypeIsIChangeTracking(type));
        }

        /// <summary>
        /// Asserts that each property changes the IsChanged state of an object when their values change.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> to test.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void IsChangedWhenPropertiesChange(this Assert assert, Type type, List<string> propertyNames)
        {
            IsChangedWhenPropertiesChange(assert, CheckTypeIsIChangeTracking(type), propertyNames);
        }

        /// <summary>
        /// Asserts that each property changes the IsChanged state of an object when their values change.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> to test.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void IsChangedWhenPropertiesChange(this Assert assert, Type type, List<PropertyInfo> properties)
        {
            IsChangedWhenPropertiesChange(assert, CheckTypeIsIChangeTracking(type), properties);
        }

        /// <summary>
        /// Asserts that each property changes the IsChanged state of an object when their values changed.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> to test.</typeparam>
        /// <param name="assert">Assert extension.</param>
        public static void IsChangedWhenPropertiesChange<T>(this Assert assert) where T : IChangeTracking
        {
            IsChangedWhenPropertiesChange(assert, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Asserts that each property changes the IsChanged state of an object when their values change.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> to test.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="propertyNames">List of property names to test.</param>
        public static void IsChangedWhenPropertiesChange<T>(this Assert assert, List<string> propertyNames) where T : IChangeTracking
        {
            IsChangedWhenPropertiesChange(assert, Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Asserts that each property changes the IsChanged state of an object when their values change.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> to test.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="properties">List of properties to test.</param>
        public static void IsChangedWhenPropertiesChange<T>(this Assert assert, List<PropertyInfo> properties) where T : IChangeTracking
        {
            IsChangedWhenPropertiesChange(assert, Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Asserts that each property changes the IsChanged state of an object when their values change.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> to test.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenPropertiesChange<T>(this Assert assert, T obj) where T : IChangeTracking
        {
            IsChangedWhenPropertiesChange(assert, obj, PropertyUtilities.GetReadWriteProperties(obj));
        }

        /// <summary>
        /// Asserts that each property changes the IsChanged state of an object when their values change.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> to test.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object that implements <see cref="IChangeTracking"/> to test.</param>
        /// <param name="propertyNames">List of property names to change.</param>
        public static void IsChangedWhenPropertiesChange<T>(this Assert assert, T obj, List<string> propertyNames) where T : IChangeTracking
        {
            foreach (string propertyName in propertyNames)
            {
                IsChangedWhenPropertyChanges(assert, obj, propertyName);
            }
        }

        /// <summary>
        /// Asserts that each property changes the IsChanged state of an object when their values change.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> to test.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object that implements <see cref="IChangeTracking"/> to test.</param>
        /// <param name="properties">List of properties to change.</param>
        public static void IsChangedWhenPropertiesChange<T>(this Assert assert, T obj, List<PropertyInfo> properties) where T : IChangeTracking
        {
            foreach (PropertyInfo property in properties)
            {
                IsChangedWhenPropertyChanges(assert, obj, property);
            }
        }

        #endregion

        #region IsChangedWhenPropertyChanges

        /// <summary>
        /// Asserts that a property changes the IsChanged state of an object when its value changes.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> to test.</param>
        /// <param name="propertyName">Name of property to change.</param>
        public static void IsChangedWhenPropertyChanges(this Assert assert, Type type, string propertyName)
        {
            IsChangedWhenPropertyChanges(assert, CheckTypeIsIChangeTracking(type), propertyName);
        }

        /// <summary>
        /// Asserts that a property changes the IsChanged state of an object when its value changes.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> to test.</param>
        /// <param name="property">Property to change.</param>
        public static void IsChangedWhenPropertyChanges(this Assert assert, Type type, PropertyInfo property)
        {
            IsChangedWhenPropertyChanges(assert, CheckTypeIsIChangeTracking(type), property);
        }

        /// <summary>
        /// Asserts that a property changes the IsChanged state of an object when its value changes.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="propertyName">Name of property to change.</param>
        public static void IsChangedWhenPropertyChanges<T>(this Assert assert, string propertyName) where T : IChangeTracking
        {
            IsChangedWhenPropertyChanges(assert, Activator.CreateInstance<T>(), propertyName);
        }

        /// <summary>
        /// Asserts that a property changes the IsChanged state of an object when its value changes.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="property">Property to change.</param>
        public static void IsChangedWhenPropertyChanges<T>(this Assert assert, PropertyInfo property) where T : IChangeTracking
        {
            IsChangedWhenPropertyChanges(assert, Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Asserts that an object's IsChanged property is true when a specific property changes.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object that implements <see cref="IChangeTracking"/> to test.</param>
        /// <param name="propertyName">Name of property to change.</param>
        public static void IsChangedWhenPropertyChanges<T>(this Assert assert, T obj, string propertyName) where T : IChangeTracking
        {
            IsChangedWhenPropertyChanges(assert, obj, obj.GetType().GetProperty(propertyName)!);
        }

        /// <summary>
        /// Asserts that a property changes the IsChanged state of an object when its value changes.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object that implements <see cref="IChangeTracking"/> to test.</param>
        /// <param name="property">Property to change.</param>
        public static void IsChangedWhenPropertyChanges<T>(this Assert assert, T obj, PropertyInfo property) where T : IChangeTracking
        {
            _ = assert;

            if (PropertyUtilities.IsReadWriteProperty(property))
            {
                obj.AcceptChanges();
                ObjectUtilities.PopulatePropertyWithRandomValue(obj, property);

                Assert.IsTrue(obj.IsChanged);
            }
        }

        #endregion

        #region IsChangedWhenChildClassPropertiesChange

        /// <summary>
        /// Asserts that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> with class properties to test.</param>
        public static void IsChangedWhenClassPropertiesChange(this Assert assert, Type type)
        {
            IsChangedWhenClassPropertiesChange(assert, CheckTypeIsIChangeTracking(type));
        }

        /// <summary>
        /// Asserts that when the child class properties change, the parent's Ischanged property is changed to true.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> with class properties.</param>
        /// <param name="propertyNames">List of property names that implement <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertiesChange(this Assert assert, Type type, List<string> propertyNames)
        {
            IsChangedWhenClassPropertiesChange(assert, CheckTypeIsIChangeTracking(type), propertyNames);
        }

        /// <summary>
        /// Asserts that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/> with class properties.</param>
        /// <param name="properties">List of properties that implement <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertiesChange(this Assert assert, Type type, List<PropertyInfo> properties)
        {
            IsChangedWhenClassPropertiesChange(assert, CheckTypeIsIChangeTracking(type), properties);
        }

        /// <summary>
        /// Asserts that when the child class properties change, the parent's IsChanged state is changed to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> with class properties.</typeparam>
        /// <param name="assert">Assert extension.</param>
        public static void IsChangedWhenClassPropertiesChange<T>(this Assert assert) where T : IChangeTracking
        {
            IsChangedWhenClassPropertiesChange(assert, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Asserts that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> with class properties to test.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="propertyNames">List of property names that implement <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertiesChange<T>(this Assert assert, List<string> propertyNames) where T : IChangeTracking
        {
            IsChangedWhenClassPropertiesChange(assert, Activator.CreateInstance<T>(), propertyNames);
        }

        /// <summary>
        /// Asserts that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> with class properties to test.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="properties">List of properties that implement <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertiesChange<T>(this Assert assert, List<PropertyInfo> properties) where T : IChangeTracking
        {
            IsChangedWhenClassPropertiesChange(assert, Activator.CreateInstance<T>(), properties);
        }

        /// <summary>
        /// Asserts that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/> with class properties.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object to test.</param>
        public static void IsChangedWhenClassPropertiesChange<T>(this Assert assert, T obj) where T : IChangeTracking
        {
            IsChangedWhenClassPropertiesChange(assert, obj, PropertyUtilities.GetClassProperties(obj, new GetPropertiesOptions(true) { WriteOnlyProperties = false }));
        }

        /// <summary>
        /// Asserts that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="propertyNames">List of class property names to test. Each class should implement <see cref="IChangeTracking"/>.</param>
        public static void IsChangedWhenClassPropertiesChange<T>(this Assert assert, T obj, List<string> propertyNames) where T : IChangeTracking
        {
            foreach (string propertyName in propertyNames)
            {
                IsChangedWhenClassPropertyChanges(assert, obj, obj.GetType().GetProperty(propertyName)!);
            }
        }

        /// <summary>
        /// Asserts that when the child class properties change, the parent's IsChanged property is changed to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with properties to test.</param>
        /// <param name="properties">List of class properties to test. Each class should implement <see cref="IChangeTracking"/>.</param>
        public static void IsChangedWhenClassPropertiesChange<T>(this Assert assert, T obj, List<PropertyInfo> properties) where T : IChangeTracking
        {
            foreach (PropertyInfo property in properties)
            {
                IsChangedWhenClassPropertyChanges(assert, obj, property);
            }
        }

        #endregion

        #region IsChangedWhenClassPropertyChanges

        /// <summary>
        /// Asserts that when a property with a class value changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="propertyName">Name of property that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertyChanges(this Assert assert, Type type, string propertyName)
        {
            IsChangedWhenClassPropertyChanges(assert, CheckTypeIsIChangeTracking(type), propertyName);
        }

        /// <summary>
        /// Asserts that when a property with a class value changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="property">Property that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertyChanges(this Assert assert, Type type, PropertyInfo property)
        {
            IsChangedWhenClassPropertyChanges(assert, CheckTypeIsIChangeTracking(type), property);
        }

        /// <summary>
        /// Asserts that when a property with a class value changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="propertyName">Name of property that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertyChanges<T>(this Assert assert, string propertyName) where T : IChangeTracking
        {
            IsChangedWhenClassPropertyChanges<T>(assert, typeof(T).GetProperty(propertyName)!);
        }

        /// <summary>
        /// Asserts that when a property with a class value changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="property">Property that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertyChanges<T>(this Assert assert, PropertyInfo property) where T : IChangeTracking
        {
            IsChangedWhenClassPropertyChanges(assert, Activator.CreateInstance<T>(), property);
        }

        /// <summary>
        /// Asserts that when a property with a class value changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="propertyName">Name of property that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertyChanges<T>(this Assert assert, T obj, string propertyName) where T : IChangeTracking
        {
            IsChangedWhenClassPropertyChanges(assert, obj, obj.GetType().GetProperty(propertyName)!);
        }

        /// <summary>
        /// Asserts that when a property with a class value changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with property to test.</param>
        /// <param name="property">Property that implements <see cref="IChangeTracking"/> to test.</param>
        public static void IsChangedWhenClassPropertyChanges<T>(this Assert assert, T obj, PropertyInfo property) where T : IChangeTracking
        {
            IsChangedWhenChildClassChanges(assert, obj, property.GetValue(obj)!);
        }

        #endregion

        /// <summary>
        /// Asserts that when the child classes change, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="IChangeTracking"/> with child classes.</typeparam>
        /// <typeparam name="TChild">Type that implements <see cref="IChangeTracking"/> as a child class of the parent.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="parent">Parent object that contains child classes contained within the list.</param>
        /// <param name="children">Class objects contained within the properties of the object being tested.</param>
        public static void IsChangedWhenChildClassesChange<TParent, TChild>(this Assert assert, TParent parent, List<TChild> children) where TParent : IChangeTracking
        {
            foreach (TChild child in children)
            {
                IsChangedWhenChildClassChanges(assert, parent, child ?? throw new ArgumentNullException(nameof(children), $"{nameof(child)} object of {nameof(children)} is null."));
            }
        }

        /// <summary>
        /// Asserts that when a child class changes, it changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="IChangeTracking"/> and contains the TChild type.</typeparam>
        /// <typeparam name="TChild">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="parentObj">Parent object to test.</param>
        /// <param name="childObj">Child object to change.</param>
        public static void IsChangedWhenChildClassChanges<TParent, TChild>(this Assert assert, TParent parentObj, [DisallowNull] TChild childObj) where TParent : IChangeTracking
        {
            _ = assert;

            parentObj.AcceptChanges();
            ObjectUtilities.PopulateObjectWithRandomValues(childObj);

            Assert.IsTrue(parentObj.IsChanged);

            if (childObj is IChangeTracking tracking)
            {
                Assert.IsTrue(tracking.IsChanged);
            }
        }

        #region IsChangedWhenListPropertiesChange

        /// <summary>
        /// Asserts that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListPropertiesChange(this Assert assert, Type type, bool addItems, bool removeItems, bool changeItems)
        {
            IsChangedWhenListPropertiesChange(assert, CheckTypeIsIChangeTracking(type), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="propertyNames">List of property names that implement <see cref="IList"/> and <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListPropertiesChange(this Assert assert, Type type, List<string> propertyNames, bool addItems, bool removeItems, bool changeItems)
        {
            IsChangedWhenListPropertiesChange(assert, CheckTypeIsIChangeTracking(type), propertyNames, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="properties">List of properties that implement <see cref="IList"/> and <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListPropertiesChange(this Assert assert, Type type, List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems)
        {
            IsChangedWhenListPropertiesChange(assert, CheckTypeIsIChangeTracking(type), properties, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListPropertiesChange<T>(this Assert assert, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListPropertiesChange(assert, Activator.CreateInstance<T>(), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="propertyNames">List of property names that implement <see cref="IList"/> and <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListPropertiesChange<T>(this Assert assert, List<string> propertyNames, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListPropertiesChange(assert, Activator.CreateInstance<T>(), propertyNames, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="properties">List of properties that implement <see cref="IList"/> and <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListPropertiesChange<T>(this Assert assert, List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListPropertiesChange(assert, Activator.CreateInstance<T>(), properties, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with list properties to test.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListPropertiesChange<T>(this Assert assert, T obj, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListPropertiesChange(assert, obj, PropertyUtilities.GetListProperties(obj, new GetPropertiesOptions(true) { WriteOnlyProperties = false }), addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with list properties to test.</param>
        /// <param name="propertyNames">List of property names that implement <see cref="IList"/> and <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListPropertiesChange<T>(this Assert assert, T obj, List<string> propertyNames, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            foreach (string propertyName in propertyNames)
            {
                IsChangedWhenListPropertyChanges(assert, obj, obj.GetType().GetProperty(propertyName)!, addItems, removeItems, changeItems);
            }
        }

        /// <summary>
        /// Asserts that when list properties change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="properties">List of properties that implement <see cref="IList"/> and <see cref="IChangeTracking"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListPropertiesChange<T>(this Assert assert, T obj, List<PropertyInfo> properties, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            foreach (PropertyInfo property in properties)
            {
                IsChangedWhenListPropertyChanges(assert, obj, property, addItems, removeItems, changeItems);
            }
        }

        #endregion

        #region IsChangedWhenListPropertyChanges

        /// <summary>
        /// Asserts that when a list property changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="propertyName">Name of list property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListPropertyChanges(this Assert assert, Type type, string propertyName, bool addItems, bool removeItems, bool changeItems)
        {
            IsChangedWhenListPropertyChanges(assert, CheckTypeIsIChangeTracking(type), propertyName, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when a list property changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        /// <param name="property">List property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListPropertyChanges(this Assert assert, Type type, PropertyInfo property, bool addItems, bool removeItems, bool changeItems)
        {
            IsChangedWhenListPropertyChanges(assert, CheckTypeIsIChangeTracking(type), property, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when a list property changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="propertyName">Name of list property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListPropertyChanges<T>(this Assert assert, string propertyName, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListPropertyChanges<T>(assert, typeof(T).GetProperty(propertyName)!, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when a list property changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="property">List property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListPropertyChanges<T>(this Assert assert, PropertyInfo property, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListPropertyChanges(assert, Activator.CreateInstance<T>(), property, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when a list property changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with list property.</param>
        /// <param name="propertyName">Name of list property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListPropertyChanges<T>(this Assert assert, T obj, string propertyName, bool addItems, bool removeItems, bool changeItems) where T : IChangeTracking
        {
            IsChangedWhenListPropertyChanges(assert, obj, obj.GetType().GetProperty(propertyName)!, addItems, removeItems, changeItems);
        }

        /// <summary>
        /// Asserts that when a list property changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with list property.</param>
        /// <param name="property">List property to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListPropertyChanges<TParent>(this Assert assert, TParent obj, PropertyInfo property, bool addItems, bool removeItems, bool changeItems) where TParent : IChangeTracking
        {
            if (property.GetValue(obj) is IList list)
            {
                IsChangedWhenListChanges(assert, obj, list, addItems, removeItems, changeItems);
            }
        }

        #endregion

        /// <summary>
        /// Asserts that when lists change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="TParent">Type implements <see cref="IChangeTracking"/>.</typeparam>
        /// <typeparam name="TList">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with lists to test.</param>
        /// <param name="lists">List of <see cref="IList"/>.</param>
        /// <param name="addItems">Test adding items to lists.</param>
        /// <param name="removeItems">Test removing items from lists.</param>
        /// <param name="changeItems">Test changing items in lists.</param>
        public static void IsChangedWhenListsChange<TParent, TList>(this Assert assert, TParent obj, List<IList> lists, bool addItems, bool removeItems, bool changeItems) where TParent : IChangeTracking where TList : IList
        {
            foreach (IList list in lists)
            {
                IsChangedWhenListChanges(assert, obj, list, addItems, removeItems, changeItems);
            }
        }

        /// <summary>
        /// Asserts that when a list changes, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <typeparam name="TList">Type that implements <see cref="IList"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with list to test.</param>
        /// <param name="list">List to test.</param>
        /// <param name="addItems">Test adding items to list.</param>
        /// <param name="removeItems">Test removing items from list.</param>
        /// <param name="changeItems">Test changing items in list.</param>
        public static void IsChangedWhenListChanges<TParent, TList>(this Assert assert, TParent obj, TList list, bool addItems, bool removeItems, bool changeItems) where TParent : IChangeTracking where TList : IList
        {
            if (addItems)
            {
                IsChangedWhenListItemsAdded(assert, obj, list);
            }

            if (removeItems)
            {
                IsChangedWhenListItemsRemoved(assert, obj, list);
            }

            if (changeItems)
            {
                IsChangedWhenListItemsChange(assert, obj, list);
            }
        }

        /// <summary>
        /// Asserts that when items are added to a list, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <typeparam name="TList">Type that implements <see cref="IList"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with list to add items to.</param>
        /// <param name="list">List to add items to.</param>
        public static void IsChangedWhenListItemsAdded<TParent, TList>(this Assert assert, TParent obj, TList list) where TParent : IChangeTracking where TList : IList
        {
            _ = assert;

            obj.AcceptChanges();

            ObjectUtilities.AddRandomItemsToList(list);

            Assert.IsTrue(obj.IsChanged);

            if (list is IChangeTracking tracking)
            {
                Assert.IsTrue(tracking.IsChanged);
            }
        }

        /// <summary>
        /// Asserts that when items are removed from a list, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <typeparam name="TList">Type that implements <see cref="IList"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with list to remove items from.</param>
        /// <param name="list">List to remove items from.</param>
        public static void IsChangedWhenListItemsRemoved<TParent, TList>(this Assert assert, TParent obj, TList list) where TParent : IChangeTracking where TList : IList
        {
            _ = assert;

            if (list.Count == 0)
            {
                ObjectUtilities.PopulateListWithRandomValues(list);
            }

            while (list.Count > 0)
            {
                obj.AcceptChanges();

                list.RemoveAt(list.Count - 1);

                Assert.IsTrue(obj.IsChanged);

                if (list is IChangeTracking tracking)
                {
                    Assert.IsTrue(tracking.IsChanged);
                }
            }
        }

        /// <summary>
        /// Test that when items in a list change, the parent's IsChanged property is set to true.
        /// </summary>
        /// <typeparam name="TParent">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <typeparam name="TList">Type that implements <see cref="IList"/>.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="obj">Object with list of items to change.</param>
        /// <param name="list">List with items to change.</param>
        public static void IsChangedWhenListItemsChange<TParent, TList>(this Assert assert, TParent obj, TList list) where TParent : IChangeTracking where TList : IList
        {
            _ = assert;

            if (list.Count == 0)
            {
                ObjectUtilities.PopulateListWithRandomValues(list);
            }

            foreach (var item in list)
            {
                obj.AcceptChanges();

                ObjectUtilities.PopulateObjectWithRandomValues(item);

                Assert.IsTrue(obj.IsChanged);

                if (list is IChangeTracking listTracking)
                {
                    Assert.IsTrue(listTracking.IsChanged);
                }

                if (item is IChangeTracking itemTracking)
                {
                    Assert.IsTrue(itemTracking.IsChanged);
                }
            }
        }

        /// <summary>
        /// Checks that the type implements <see cref="IChangeTracking"/> and creates a new instance of that type.
        /// </summary>
        /// <param name="type">Type to test and create.</param>
        /// <returns>A new instance of the type specified that implements <see cref="IChangeTracking"/>.</returns>
        public static IChangeTracking CheckTypeIsIChangeTracking(Type type)
        {
            Assert.IsTrue(typeof(IChangeTracking).IsAssignableFrom(type));
            var obj = Activator.CreateInstance(type);
            Assert.IsNotNull(obj);

            return (IChangeTracking)obj;
        }
    }
}
