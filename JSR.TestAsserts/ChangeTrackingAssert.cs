// <copyright file="ChangeTrackingAssert.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.TestAsserts
{
    /// <summary>
    /// Tests and checks objects that implement IChangeTracking.
    /// </summary>
    public static class ChangeTrackingAssert
    {
        /// <summary>
        /// Tests that a Type that implements <see cref="IChangeTracking"/> property implements the AcceptChanges() method.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> to test implements AcceptChanges().</typeparam>
        public static void AcceptsChanges<T>() where T : IChangeTracking
        {
            AcceptsChanges(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that an object that implements <see cref="IChangeTracking"/> properly implements the AcceptChanges() method.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> to test implements AcceptChanges().</typeparam>
        /// <param name="testObj"><see cref="object"/> to test implements AcceptChanges().</param>
        public static void AcceptsChanges<T>(T testObj) where T : IChangeTracking
        {
            ObjectUtilities.PopulateObjectWithRandomValues(testObj);
            Assert.IsTrue(testObj.IsChanged);

            testObj.AcceptChanges();
            Assert.IsFalse(testObj.IsChanged);
        }

        /// <summary>
        /// Tests that a Type's IsChange property is true when all of the properties are changed.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="IChangeTracking"/> <see cref="object"/> to test for IsChanged.</typeparam>
        public static void IsChangedWhenAllPropertiesChange<T>() where T : IChangeTracking
        {
            IsChangedWhenAllPropertiesChange(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that an object's IsChange property is true when all of the properties are changed.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="IChangeTracking"/> <see cref="object"/> to test for IsChanged.</typeparam>
        /// <param name="testObj">An <see cref="IChangeTracking"/> object to test.</param>
        public static void IsChangedWhenAllPropertiesChange<T>(T testObj) where T : IChangeTracking
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                testObj.AcceptChanges();
                Assert.IsFalse(testObj.IsChanged);

                ObjectUtilities.PopulateObjectWithRandomValues(testObj);
                Assert.IsTrue(testObj.IsChanged);
            }
        }

        /// <summary>
        /// Tests that a Type's IsChange property is true when each property changes.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="IChangeTracking"/> <see cref="object"/> to test for IsChanged.</typeparam>
        public static void IsChangedWhenEachPropertyChanges<T>() where T : IChangeTracking
        {
            IsChangedWhenEachPropertyChanges(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that an object's IsChange property is true when each property changes.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="IChangeTracking"/> <see cref="object"/> to test for IsChanged.</typeparam>
        /// <param name="testObj">An <see cref="IChangeTracking"/> object to test.</param>
        public static void IsChangedWhenEachPropertyChanges<T>(T testObj) where T : IChangeTracking
        {
            IsChangedWhenSpecificPropertiesChange(PropertyUtilities.GetListOfPropertyNamesWithPublicSetMethod(testObj.GetType()), testObj);
        }

        /// <summary>
        /// Tests that a Type's IsChanged property is true when a list of properties change.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="IChangeTracking"/> <see cref="object"/> to test for IsChanged.</typeparam>
        /// <param name="propertyNames">List of property names to change.</param>
        public static void IsChangedWhenSpecificPropertiesChange<T>(List<string> propertyNames) where T : IChangeTracking
        {
            IsChangedWhenSpecificPropertiesChange(propertyNames, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that an object's IsChanged property is true when a list of properties change.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="IChangeTracking"/> <see cref="object"/> to test for IsChanged.</typeparam>
        /// <param name="propertyNames">List of property names to change.</param>
        /// <param name="testObj">An <see cref="IChangeTracking"/> object to test.</param>
        public static void IsChangedWhenSpecificPropertiesChange<T>(List<string> propertyNames, T testObj) where T : IChangeTracking
        {
            foreach (string property in propertyNames)
            {
                IsChangedWhenSpecificPropertyChanges(property, testObj);
            }

            testObj.AcceptChanges();

            foreach (string propertyName in propertyNames)
            {
                for (int i = 0; i < new Random().Next(5, 20); i++)
                {
                    ObjectUtilities.PopulatePropertyWithRandomValue(testObj, propertyName);
                }
            }

            Assert.IsTrue(testObj.IsChanged);
        }

        /// <summary>
        /// Tests that a Type's IsChanged property is true when a named property's value changes.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="IChangeTracking"/> <see cref="object"/> to test for IsChanged.</typeparam>
        /// <param name="propertyName">Name of the property to change.</param>
        public static void IsChangedWhenSpecificPropertyChanges<T>(string propertyName) where T : IChangeTracking
        {
            IsChangedWhenSpecificPropertyChanges(propertyName, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that an object's IsChanged property is true when a named property's value changes.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="IChangeTracking"/> <see cref="object"/> to test for IsChanged.</typeparam>
        /// <param name="propertyName">Name of the property to change.</param>
        /// <param name="testObj">An <see cref="IChangeTracking"/> object to test.</param>
        public static void IsChangedWhenSpecificPropertyChanges<T>(string propertyName, T testObj) where T : IChangeTracking
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                testObj.AcceptChanges();
                Assert.IsFalse(testObj.IsChanged);

                ObjectUtilities.PopulatePropertyWithRandomValue(testObj, propertyName);
                Assert.IsTrue(testObj.IsChanged);
            }
        }

        /// <summary>
        /// Tests that when multiple items are added to a list, the Type's IsChanged property is true.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> to test that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="listPropertyName">Name of the property that contains the list to test.</param>
        public static void IsChangedWhenListItemsAdded<T>(string listPropertyName) where T : IChangeTracking
        {
            IsChangedWhenListItemsAdded(listPropertyName, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when multiple items are added to a list, the object's IsChanged property is true.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> to test that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="listPropertyName">Name of the property that contains the list to test.</param>
        /// <param name="testObj"><see cref="object"/> with the property containing a list to test.</param>
        public static void IsChangedWhenListItemsAdded<T>(string listPropertyName, T testObj) where T : IChangeTracking
        {
            IsChangedWhenListItemsAdded(ObjectUtilities.GetObjectListByPropertyName(testObj, listPropertyName), testObj);
        }

        /// <summary>
        /// Tests that when multiple items are added to a list, the object's IsChanged property is true.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> to test that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="list"><see cref="List{T}"/> contained within the property to test.</param>
        /// <param name="testObj"><see cref="object"/> with the property containing a list to test.</param>
        public static void IsChangedWhenListItemsAdded<T>(IList list, T testObj) where T : IChangeTracking
        {
            testObj.AcceptChanges();
            Assert.IsFalse(testObj.IsChanged);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulateListWithRandomValue(list, testObj.GetType());

                Assert.IsTrue(testObj.IsChanged);
            }
        }

        /// <summary>
        /// Tests that when all of the items from a list are removed, the Type's IsChanged value is true.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> to test that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="listPropertyName">Name of the property that contains the list to test.</param>
        public static void IsChangedWhenListItemsRemoved<T>(string listPropertyName) where T : IChangeTracking
        {
            IsChangedWhenListItemsRemoved(listPropertyName, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when all of the items from a list are removed, the object's IsChanged value is true.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> to test that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="listPropertyName">Name of the property that contains the list to test.</param>
        /// <param name="testObj"><see cref="object"/> with the property containing a list to test.</param>
        public static void IsChangedWhenListItemsRemoved<T>(string listPropertyName, T testObj) where T : IChangeTracking
        {
            IsChangedWhenListItemsRemoved(ObjectUtilities.GetObjectListByPropertyName(testObj, listPropertyName), testObj);
        }

        /// <summary>
        /// Tests that when all of the items from a list are removed, the object's IsChanged value is true.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> to test that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="list"><see cref="List{T}"/> contained within the property to test.</param>
        /// <param name="testObj"><see cref="object"/> with the property containing a list to test.</param>
        public static void IsChangedWhenListItemsRemoved<T>(IList list, T testObj) where T : IChangeTracking
        {
            if (list.Count == 0)
            {
                ObjectUtilities.PopulateListWithRandomValues(list, testObj.GetType());
            }

            testObj.AcceptChanges();
            Assert.IsFalse(testObj.IsChanged);

            while (list.Count > 0)
            {
                list.RemoveAt(list.Count - 1);
                Assert.IsTrue(testObj.IsChanged);
            }
        }

        /// <summary>
        /// Test that when multiple items in a list change, the Type's IsChange property is true.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> to test that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="listPropertyName">Name of the property that contains the list to test.</param>
        public static void IsChangedWhenListItemsChange<T>(string listPropertyName) where T : IChangeTracking
        {
            IsChangedWhenListItemsChange(listPropertyName, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Test that when multiple items in a list change, the object's IsChange property is true.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> to test that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="listPropertyName">Name of the property that contains the list to test.</param>
        /// <param name="testObj"><see cref="object"/> with the property containing a list to test.</param>
        public static void IsChangedWhenListItemsChange<T>(string listPropertyName, T testObj) where T : IChangeTracking
        {
            IsChangedWhenListItemsChange(ObjectUtilities.GetObjectListByPropertyName(testObj, listPropertyName), testObj);
        }

        /// <summary>
        /// Test that when multiple items in a list change, the object's IsChanged property is true.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> to test that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="list"><see cref="List{T}"/> contained within the property to test.</param>
        /// <param name="testObj"><see cref="object"/> with the property containing a list to test.</param>
        public static void IsChangedWhenListItemsChange<T>(IList list, T testObj) where T : IChangeTracking
        {
            if (list.Count == 0)
            {
                ObjectUtilities.PopulateListWithRandomValues(list, testObj.GetType());
            }

            testObj.AcceptChanges();
            Assert.IsFalse(testObj.IsChanged);

            foreach (dynamic item in list)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(item);
                Assert.IsTrue(testObj.IsChanged);
            }
        }
    }
}
