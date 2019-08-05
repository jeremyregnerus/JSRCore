// <copyright file="ChangableObjectAssert.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using JSR.BaseClassLibrary;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.TestAsserts
{
    /// <summary>
    /// Tests the implementation of IChangableObject. Primarily focused on raising change events.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:DoNotUseRegions", Justification = "Regions used for signatures.")]
    public static class ChangableObjectAssert
    {
        #region NotifiesIsChangedOnAcceptChanges

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

        #endregion

        #region NotifiesIsChangedWhenPropertiesChange

        /// <summary>
        /// Test that all properties raise the <see cref="OnChangedEventHandler"/> and change the IsChanged value of an <see cref="IChangableObject"/> object.
        /// </summary>
        /// <typeparam name="T">Type to test that implements <see cref="IChangableObject"/>.</typeparam>
        public static void NotifiesIsChangedWhenPropertiesChange<T>() where T : IChangableObject
        {
            NotifiesIsChangedWhenPropertiesChange(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Test that properties raise the <see cref="OnChangedEventHandler"/> and change the IsChanged value of an <see cref="IChangableObject"/> object.
        /// </summary>
        /// <typeparam name="T">Type to test that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="propertyNamesToChange">List to property names to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(List<string> propertyNamesToChange) where T : IChangableObject
        {
            NotifiesIsChangedWhenPropertiesChange(propertyNamesToChange, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Test that properties raise the <see cref="OnChangedEventHandler"/> and change the IsChanged value of an <see cref="IChangableObject"/> object.
        /// </summary>
        /// <typeparam name="T">Type to test that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="propertiesToChange">List of properties to test.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(List<PropertyInfo> propertiesToChange) where T : IChangableObject
        {
            NotifiesIsChangedWhenPropertiesChange(propertiesToChange, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Test that properties raise the <see cref="OnChangedEventHandler"/> and change the IsChanged value of an <see cref="IChangableObject"/> object.
        /// </summary>
        /// <typeparam name="T">Type to test that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="objectToTest">Object to test that implements <see cref="IChangableObject"/>.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(T objectToTest) where T : IChangableObject
        {
            // Tests all of the readwrite properties
            NotifiesIsChangedWhenPropertiesChange(PropertyUtilities.GetListOfProperties(objectToTest, true, false, false, true, true, true), objectToTest);

            // Tests the readonly class Properties
            NotifiesIsChangedWhenPropertiesChange(PropertyUtilities.GetListOfPropertiesWithClassTypes(objectToTest, true, true, true), objectToTest);

            // Tests the readonly list Properties
            NotifiesIsChangedWhenPropertiesChange(PropertyUtilities.GetListOfPropertiesWithListTypes(objectToTest, true, true, true), objectToTest);
        }

        /// <summary>
        /// Test that properties raise the <see cref="OnChangedEventHandler"/> and change the IsChanged value of an <see cref="IChangableObject"/> object.
        /// </summary>
        /// <typeparam name="T">Type to test that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="propertyNamesToChange">List of property names to test.</param>
        /// <param name="objectToTest">Object to test that implements <see cref="IChangableObject"/>.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(List<string> propertyNamesToChange, T objectToTest) where T : IChangableObject
        {
            foreach (string propertyName in propertyNamesToChange)
            {
                NotifiesIsChangedWhenPropertyChanges(propertyName, objectToTest);
            }
        }

        /// <summary>
        /// Test that properties raise the <see cref="OnChangedEventHandler"/> and change the IsChanged value of an <see cref="IChangableObject"/> object.
        /// </summary>
        /// <typeparam name="T">Type to test that implements <see cref="IChangableObject"/>.</typeparam>
        /// <param name="propertiesToChange">List of properties to test.</param>
        /// <param name="objectToTest">Object to test that implements <see cref="IChangableObject"/>.</param>
        public static void NotifiesIsChangedWhenPropertiesChange<T>(List<PropertyInfo> propertiesToChange, T objectToTest) where T : IChangableObject
        {
            objectToTest.AcceptChanges();

            List<string> propertiesChanged = new List<string>();
            objectToTest.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<bool> wasChanged = new List<bool>();
            objectToTest.OnChanged += (sender, changed) => wasChanged.Add(changed);

            foreach (PropertyInfo property in propertiesToChange)
            {
                ObjectUtilities.PopulatePropertyWithRandomValue(objectToTest, property);
            }

            Assert.IsTrue(objectToTest.IsChanged);
            Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(IChangableObject.IsChanged)));
            Assert.AreEqual(1, wasChanged.Count);

            foreach (PropertyInfo property in propertiesToChange)
            {
                NotifiesIsChangedWhenPropertyChanges(property, objectToTest);
            }
        }

        #endregion

        #region NotifiesIsChangedWhenPropertyChanges

        /// <summary>
        /// Tests that a property raises the <see cref="OnChangedEventHandler"/> and changes the IsChanged value.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/> with property to test.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(string propertyName) where T : IChangableObject
        {
            NotifiesIsChangedWhenPropertyChanges(propertyName, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that a property raises the <see cref="OnChangedEventHandler"/> and changes the IsChanged value.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/> with property to test.</typeparam>
        /// <param name="property">Property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(PropertyInfo property) where T : IChangableObject
        {
            NotifiesIsChangedWhenPropertyChanges(property, Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that a property raises the <see cref="OnChangedEventHandler"/> and changes the IsChanged value.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/> with property to test.</typeparam>
        /// <param name="propertyName">Name of property to test.</param>
        /// <param name="objectToTest">Object that implements <see cref="IChangableObject"/> with property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(string propertyName, T objectToTest) where T : IChangableObject
        {
            NotifiesIsChangedWhenPropertyChanges(objectToTest.GetType().GetProperty(propertyName), objectToTest);
        }

        /// <summary>
        /// Tests that a property raises the <see cref="OnChangedEventHandler"/> and changes the IsChanged value.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangableObject"/> with property to test.</typeparam>
        /// <param name="property">Property to test.</param>
        /// <param name="objectToTest">Object that implements <see cref="IChangableObject"/> with property to test.</param>
        public static void NotifiesIsChangedWhenPropertyChanges<T>(PropertyInfo property, T objectToTest) where T : IChangableObject
        {
            objectToTest.AcceptChanges();

            List<string> propertiesChanged = new List<string>();
            objectToTest.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<bool> wasChanged = new List<bool>();
            objectToTest.OnChanged += (sender, changed) => wasChanged.Add(changed);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulatePropertyWithRandomValue(objectToTest, property);
            }

            Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(IChangableObject.IsChanged)));
            Assert.AreEqual(1, wasChanged.Count);
            Assert.IsTrue(wasChanged[0]);
        }

        #endregion

        #region NotifiesIsChangedWhenChildChanges

        /// <summary>
        /// Tests that all properties implementing <see cref="IChangableObject"/> raise the <see cref="OnChangedEventHandler"/> and changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="T">Type with properties to test.</typeparam>
        public static void NotifiesIsChangedWhenChildrenChange<T>() where T : IChangableObject
        {
            NotifiesIsChangedWhenChildrenChange(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Tests that all properties implementing <see cref="IChangableObject"/> raise the <see cref="OnChangedEventHandler"/> and changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="T">Type to test.</typeparam>
        /// <param name="objectToTest">Object to test.</param>
        public static void NotifiesIsChangedWhenChildrenChange<T>(T objectToTest) where T : IChangableObject
        {
            NotifiesIsChangedWhenChildrenChange(objectToTest, PropertyUtilities.GetListOfPropertiesWithClassTypes(objectToTest, true, true, true));
        }

        /// <summary>
        /// Tests that properties implementing <see cref="IChangableObject"/> raise the <see cref="OnChangedEventHandler"/> and changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="T">Type to test.</typeparam>
        /// <param name="objectToTest">Object to test.</param>
        /// <param name="propertyNamesToTest">List of property names to test.</param>
        public static void NotifiesIsChangedWhenChildrenChange<T>(T objectToTest, List<string> propertyNamesToTest) where T : IChangableObject
        {
            foreach (string propertyName in propertyNamesToTest)
            {
                NotifiesIsChangedWhenChildChanges(objectToTest, propertyName);
            }
        }

        /// <summary>
        /// Tests that properties implementing <see cref="IChangableObject"/> raise the <see cref="OnChangedEventHandler"/> and changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="T">Type with properties to test.</typeparam>
        /// <param name="objectToTest">Object with properties test.</param>
        /// <param name="propertiesToTest">List of properties to test.</param>
        public static void NotifiesIsChangedWhenChildrenChange<T>(T objectToTest, List<PropertyInfo> propertiesToTest) where T : IChangableObject
        {
            foreach (PropertyInfo property in propertiesToTest)
            {
                NotifiesIsChangedWhenChildChanges(objectToTest, property);
            }
        }

        #endregion

        #region NotifiesIsChangedWhenChildChanges

        /// <summary>
        /// Tests that a property implementing <see cref="IChangableObject"/> raises the <see cref="OnChangedEventHandler"/> and changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="T">Type with property to test.</typeparam>
        /// <param name="objectToTest">Object with property to test.</param>
        /// <param name="propertyNameToTest">Name of property to test.</param>
        public static void NotifiesIsChangedWhenChildChanges<T>(T objectToTest, string propertyNameToTest) where T : IChangableObject
        {
            NotifiesIsChangedWhenChildChanges(objectToTest, objectToTest.GetType().GetProperty(propertyNameToTest));
        }

        /// <summary>
        /// Tests that a property implementing <see cref="IChangableObject"/> raises the <see cref="OnChangedEventHandler"/> and changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="T">Type with property to test.</typeparam>
        /// <param name="objectToTest">Object with property to test.</param>
        /// <param name="propertyToTest">Property to test.</param>
        public static void NotifiesIsChangedWhenChildChanges<T>(T objectToTest, PropertyInfo propertyToTest) where T : IChangableObject
        {
            var value = propertyToTest.GetValue(objectToTest);

            if (PropertyUtilities.CheckIfPropertyIsClass(propertyToTest) && value is IChangableObject)
            {
                NotifiesIsChangedWhenChildChanges(objectToTest, (IChangableObject)value);
            }
        }

        /// <summary>
        /// Tests that a property implementing <see cref="IChangableObject"/> raises the <see cref="OnChangedEventHandler"/> and changes the parent's IsChanged property to true.
        /// </summary>
        /// <typeparam name="TParent">Type of parent object to test.</typeparam>
        /// <typeparam name="TChild">Type of child object to test.</typeparam>
        /// <param name="objectToTest">Parent object to test.</param>
        /// <param name="childToChange">Child object to change as child of parent object.</param>
        public static void NotifiesIsChangedWhenChildChanges<TParent, TChild>(TParent objectToTest, TChild childToChange) where TParent : IChangableObject where TChild : IChangableObject
        {
            List<string> propertiesChanged = new List<string>();
            objectToTest.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<bool> stateChange = new List<bool>();
            objectToTest.OnChanged += (sender, changed) => stateChange.Add(changed);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                objectToTest.AcceptChanges();
                propertiesChanged.Clear();
                stateChange.Clear();

                ObjectUtilities.PopulateObjectWithRandomValues(childToChange);

                Assert.IsTrue(childToChange.IsChanged);
                Assert.IsTrue(objectToTest.IsChanged);

                Assert.AreEqual(1, propertiesChanged.Count(propertyName => propertyName == nameof(IChangableObject.IsChanged)));
                Assert.AreEqual(1, stateChange.Count(wasChanged => wasChanged == true));
            }

            objectToTest.AcceptChanges();
            propertiesChanged.Clear();
            stateChange.Clear();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(childToChange);
            }

            Assert.IsTrue(childToChange.IsChanged);
            Assert.IsTrue(objectToTest.IsChanged);

            Assert.AreEqual(1, propertiesChanged.Count(propertyName => propertyName == nameof(IChangableObject.IsChanged)));
            Assert.AreEqual(1, stateChange.Count(wasChanged => wasChanged == true));
        }

        #endregion

        #region NotifiesIsChangedWhenListsChange

        public static void NotifiesIsChangedWhenListsChange<T>() where T : ChangableObject
        {

        }

        public static void NotifiesIsChangedWhenListsChange<T>(T objectToTest) where T : ChangableObject
        {

        }

        #endregion

        #region NotifiesIsChangedWhenListChanges

        public static void NotifiesIsChangedWhenListChanges<T>(T objectToTest, string listPropertyName) where T : ChangableObject
        {

        }

        #endregion

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

            foreach (dynamic item in list)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(item);

                Assert.AreEqual(1, propertiesChanged.FindAll(x => x == nameof(IChangableObject.IsChanged)));
                Assert.AreEqual(1, wasChanged.Count);
                Assert.IsTrue(wasChanged[0]);
            }
        }
    }
}
