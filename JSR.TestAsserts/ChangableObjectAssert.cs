// <copyright file="ChangableObjectAssert.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JSR.BaseClassLibrary;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.TestAsserts
{
    /// <summary>
    /// Tests the implementation of IChangableObject. Primarily focused on raising change events.
    /// </summary>
    public static class ChangableObjectAssert
    {
        /// <summary>
        /// Tests that when all of the properties of a Type change, the IsChanged property value is set to true once and raises the OnChange event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        public static void NotifiesIsChangedWhenAllPropertiesChange<T>() where T : IChangableObject
        {
            NotifiesIsChangedWhenAllPropertiesChange(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when all of the properties of an object change, the IsChanged property value is set to true once and raises the OnChange event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        /// <param name="testObj"><see cref="object"/> that implements <see cref="IChangableObject"/> to test.</param>
        public static void NotifiesIsChangedWhenAllPropertiesChange<T>(T testObj) where T : IChangableObject
        {
            testObj.AcceptChanges();

            List<string> propertiesChanged = new List<string>();
            testObj.PropertyChanged += (o, eventArgs) => propertiesChanged.Add(eventArgs.PropertyName);

            List<bool> wasChanged = new List<bool>();
            testObj.OnChanged += (o, changed) => wasChanged.Add(changed);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(testObj);
            }

            Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(IChangableObject.IsChanged)));
            Assert.AreEqual(1, wasChanged.Count);
            Assert.IsTrue(wasChanged[0]);
        }

        /// <summary>
        /// Tests that when each property on a Type is changed, the IsChanged property value is set to true once and raises the OnChanged event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        public static void NotifiesIsChangedWhenEachPropertyChanges<T>() where T : IChangableObject
        {
            NotifiesIsChangedWhenEachPropertyChanges(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when each property on an object is changed, the IsChanged property value is set to true once and raises the OnChanged event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        /// <param name="testObj"><see cref="object"/> that implements <see cref="IChangableObject"/> to test.</param>
        public static void NotifiesIsChangedWhenEachPropertyChanges<T>(T testObj) where T : IChangableObject
        {
            NotifiesIsChangeForSpecificPropertiesChanged(PropertyUtilities.GetListOfPropertyNamesWithPublicSetMethod(testObj.GetType()), testObj);
        }

        /// <summary>
        /// Tests that when a list of properties on a Type are changed, the IsChanged property value is set to true once and raises the OnChanged event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        /// <param name="propertyNames">List of property names to change.</param>
        public static void NotifiesIsChangeForSpecificPropertiesChanged<T>(List<string> propertyNames) where T : IChangableObject
        {
            NotifiesIsChangeForSpecificPropertiesChanged(propertyNames, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when a list of properties on an object are changed, the IsChanged property value is set to true once and raises the OnChanged event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        /// <param name="propertyNames">List of property names to change.</param>
        /// <param name="testObj"><see cref="object"/> that implements <see cref="IChangableObject"/> to test.</param>
        public static void NotifiesIsChangeForSpecificPropertiesChanged<T>(List<string> propertyNames, T testObj) where T : IChangableObject
        {
            foreach (string property in propertyNames)
            {
                NotifiesIsChangedWhenSpecificPropertyChanges(property, testObj);
            }
        }

        /// <summary>
        /// Tests that when a property on a Type is changed the IsChanged property value is set to true once and raises the OnChanged event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        /// <param name="propertyName">Name of property to change.</param>
        public static void NotifiesIsChangedWhenSpecificPropertyChanges<T>(string propertyName) where T : IChangableObject
        {
            NotifiesIsChangedWhenSpecificPropertyChanges(propertyName, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when a property on an object is changed the IsChanged property value is set to true once and raises the OnChanged event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        /// <param name="propertyName">Name of property to change.</param>
        /// <param name="testObj"><see cref="object"/> that implements <see cref="IChangableObject"/> to test.</param>
        public static void NotifiesIsChangedWhenSpecificPropertyChanges<T>(string propertyName, T testObj) where T : IChangableObject
        {
            testObj.AcceptChanges();

            List<string> propertiesChanged = new List<string>();
            testObj.PropertyChanged += (o, eventArgs) => propertiesChanged.Add(eventArgs.PropertyName);

            List<bool> wasChanged = new List<bool>();
            testObj.OnChanged += (o, changed) => wasChanged.Add(changed);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulatePropertyWithRandomValue(testObj, propertyName);
            }

            Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(IChangableObject.IsChanged)));
            Assert.AreEqual(1, wasChanged.Count);
            Assert.IsTrue(wasChanged[0]);
        }

        /// <summary>
        /// Tests that when a Type Accepts Changes the IsChanged property value is set to false once and raises the OnChanged event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        public static void NotifiesIsChangedOnAcceptChanges<T>() where T : IChangableObject
        {
            NotifiesIsChangedOnAcceptChanges(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when an object Accepts Changes the IsChanged property value is set to false once and raises the OnChanged event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        /// <param name="testObj"><see cref="object"/> that implements <see cref="IChangableObject"/> to test.</param>
        public static void NotifiesIsChangedOnAcceptChanges<T>(T testObj) where T : IChangableObject
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(testObj);

                List<string> propertiesChanged = new List<string>();
                testObj.PropertyChanged += (o, eventArgs) => propertiesChanged.Add(eventArgs.PropertyName);

                List<bool> wasChanged = new List<bool>();
                testObj.OnChanged += (o, changed) => wasChanged.Add(changed);

                for (int x = 0; x < new Random().Next(5, 20); x++)
                {
                    testObj.AcceptChanges();
                }

                Assert.AreEqual(1, propertiesChanged.FindAll(x => x == nameof(IChangableObject.IsChanged)));
                Assert.AreEqual(1, wasChanged.Count);
                Assert.IsFalse(wasChanged[0]);
            }
        }

        /// <summary>
        /// Tests that when items are added to a list property on a Type, the IsChanged property is set to true once and raises the OnChanged event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        /// <param name="listPropertyName">Property name of the list to add items to.</param>
        public static void NotifiesIsChangedWhenListItemsAdded<T>(string listPropertyName) where T : IChangableObject
        {
            NotifiesIsChangedWhenListItemsAdded(listPropertyName, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when items are added to a list property on an object, the IsChanged property is set to true once and raises the OnChanged event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        /// <param name="listPropertyName">Property name of the list to add items to.</param>
        /// <param name="testObj"><see cref="object"/> that implements <see cref="IChangableObject"/> to test.</param>
        public static void NotifiesIsChangedWhenListItemsAdded<T>(string listPropertyName, T testObj) where T : IChangableObject
        {
            NotifiesIsChangedWhenListItemsAdded(ObjectUtilities.GetObjectListByPropertyName(testObj, listPropertyName), testObj);
        }

        /// <summary>
        /// Tests that when items are added to a list property on an object, the IsChanged property is set to true once and raises the OnChanged event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        /// <param name="list">List to add items to.</param>
        /// <param name="testObj"><see cref="object"/> that implements <see cref="IChangableObject"/> to test.</param>
        public static void NotifiesIsChangedWhenListItemsAdded<T>(IList list, T testObj) where T : IChangableObject
        {
            testObj.AcceptChanges();

            List<string> propertiesChanged = new List<string>();
            testObj.PropertyChanged += (o, eventArgs) => propertiesChanged.Add(eventArgs.PropertyName);

            List<bool> wasChanged = new List<bool>();
            testObj.OnChanged += (o, changeState) => wasChanged.Add(changeState);

            ObjectUtilities.PopulateListWithRandomValues(list, testObj.GetType());

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulateListWithRandomValue(list, testObj.GetType());

                Assert.AreEqual(1, propertiesChanged.FindAll(x => x == nameof(IChangableObject.IsChanged)));
                Assert.AreEqual(1, wasChanged.Count);
                Assert.IsTrue(wasChanged[0]);
            }
        }

        /// <summary>
        /// Tests that when items are removed from a list property on a Type, the IsChanged property is set to true once and raises the OnChanged event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        /// <param name="propertyNameOfList">Property name of the list to remove items from.</param>
        public static void NotifiesIsChangedWhenListItemsRemoved<T>(string propertyNameOfList) where T : IChangableObject
        {
            NotifiesIsChangedWhenListItemsRemoved(propertyNameOfList, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when items are removed from a list property on an object, the IsChanged property is set to true once and raises the OnChanged event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        /// <param name="listPropertyName">Property name of the list to remove items from.</param>
        /// <param name="testObj"><see cref="object"/> that implements <see cref="IChangableObject"/> to test.</param>
        public static void NotifiesIsChangedWhenListItemsRemoved<T>(string listPropertyName, T testObj) where T : IChangableObject
        {
            NotifiesIsChangedWhenListItemsRemoved(ObjectUtilities.GetObjectListByPropertyName(testObj, listPropertyName), testObj);
        }

        /// <summary>
        /// Tests that when items are removed from a list property on an object, the IsChanged property is set to true once and raises the OnChanged event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        /// <param name="list">List to remove items from.</param>
        /// <param name="testObj"><see cref="object"/> that implements <see cref="IChangableObject"/> to test.</param>
        public static void NotifiesIsChangedWhenListItemsRemoved<T>(IList list, T testObj) where T : IChangableObject
        {
            if (list.Count == 0)
            {
                ObjectUtilities.PopulateListWithRandomValues(list, testObj.GetType());
            }

            testObj.AcceptChanges();

            List<string> propertiesChanged = new List<string>();
            testObj.PropertyChanged += (o, eventArgs) => propertiesChanged.Add(eventArgs.PropertyName);

            List<bool> wasChanged = new List<bool>();
            testObj.OnChanged += (o, changeState) => wasChanged.Add(changeState);

            while (list.Count > 0)
            {
                list.RemoveAt(list.Count - 1);

                Assert.AreEqual(1, propertiesChanged.FindAll(x => x == nameof(IChangableObject.IsChanged)));
                Assert.AreEqual(1, wasChanged.Count);
                Assert.IsTrue(wasChanged[0]);
            }
        }

        /// <summary>
        /// Tests that when items are changed within a list on a Type, the IsChanged property is set to true once and raises the OnChanged event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        /// <param name="listPropertyName">Property name of the list to change items from.</param>
        public static void NotifiesIsChangedWhenListItemsChange<T>(string listPropertyName) where T : IChangableObject
        {
            NotifiesIsChangedWhenListItemsChange(listPropertyName, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that when items are changed within a list on an object, the IsChanged property is set to true once and raises the OnChanged event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        /// <param name="listPropertyName">Property name of the list to change items from.</param>
        /// <param name="testObj"><see cref="object"/> that implements <see cref="IChangableObject"/> to test.</param>
        public static void NotifiesIsChangedWhenListItemsChange<T>(string listPropertyName, T testObj) where T : IChangableObject
        {
            NotifiesIsChangedWhenListItemsChange(ObjectUtilities.GetObjectListByPropertyName(testObj, listPropertyName), testObj);
        }

        /// <summary>
        /// Tests that when items are changed within a list on an object, the IsChanged property is set to true once and raises the OnChanged event once.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> that implements <see cref="IChangableObject"/> to test.</typeparam>
        /// <param name="list">List to change items from.</param>
        /// <param name="testObj"><see cref="object"/> that implements <see cref="IChangableObject"/> to test.</param>
        public static void NotifiesIsChangedWhenListItemsChange<T>(IList list, T testObj) where T : IChangableObject
        {
            if (list.Count == 0)
            {
                ObjectUtilities.PopulateListWithRandomValues(list, testObj.GetType());
            }

            testObj.AcceptChanges();

            List<string> propertiesChanged = new List<string>();
            testObj.PropertyChanged += (o, eventArgs) => propertiesChanged.Add(eventArgs.PropertyName);

            List<bool> wasChanged = new List<bool>();
            testObj.OnChanged += (o, changeState) => wasChanged.Add(changeState);

            foreach (var item in list)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(item);

                Assert.AreEqual(1, propertiesChanged.FindAll(x => x == nameof(IChangableObject.IsChanged)));
                Assert.AreEqual(1, wasChanged.Count);
                Assert.IsTrue(wasChanged[0]);
            }
        }
    }
}
