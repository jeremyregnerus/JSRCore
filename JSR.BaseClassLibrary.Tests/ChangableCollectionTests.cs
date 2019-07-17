// <copyright file="ChangableCollectionTests.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JSR.BaseClassLibrary.Tests.Mocks;
using JSR.TestAsserts;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.BaseClassLibrary.Tests
{
    [TestClass]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Unit Test.")]
    public class ChangableCollectionTests
    {
        [TestMethod]
        public void Initializes()
        {
            ChangableCollection<MockChangableObject> list = new ChangableCollection<MockChangableObject>();

            Assert.IsFalse(list.IsChanged);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void SerializesAndDeserializes()
        {
            SerializationAssert.SerializesAndDeserializes<ChangableCollection<MockChangableObject>>();
            SerializationAssert.IsNotChangedAfterDeserialized<ChangableCollection<MockChangableObject>>();
        }

        [TestMethod]
        public void NotifiesIsChangedOnEachItemAdded()
        {
            ChangableCollection<MockChangableObject> list = new ChangableCollection<MockChangableObject>();

            List<string> propertiesChanged = new List<string>();
            ((INotifyPropertyChanged)list).PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<bool> wasChanged = new List<bool>();
            list.OnChanged += (sender, changed) => wasChanged.Add(changed);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                list.AcceptChanges();
                propertiesChanged.Clear();
                wasChanged.Clear();

                MockChangableObject obj = ObjectUtilities.CreateInstanceWithRandomValues<MockChangableObject>();
                list.Add(obj);

                Assert.IsTrue(list.IsChanged);
                CollectionAssert.Contains(propertiesChanged, nameof(list.IsChanged));
                Assert.AreEqual(1, wasChanged.Count(x => x == true));
            }
        }

        [TestMethod]
        public void NotifiesIsChangedOnMultipleItemsAdded()
        {
            ChangableCollection<MockChangableObject> list = new ChangableCollection<MockChangableObject>();

            List<string> propertiesChanged = new List<string>();
            ((INotifyPropertyChanged)list).PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<bool> wasChanged = new List<bool>();
            list.OnChanged += (sender, changed) => wasChanged.Add(changed);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                MockChangableObject obj = ObjectUtilities.CreateInstanceWithRandomValues<MockChangableObject>();
                list.Add(obj);
            }

            Assert.IsTrue(list.IsChanged);
            Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(IChangeTracking.IsChanged)));
            Assert.AreEqual(1, wasChanged.Count(x => x == true));
        }

        [TestMethod]
        public void NotifiesIsChangedOnEachItemRemoved()
        {
            ChangableCollection<MockChangableObject> list = ObjectUtilities.CreateInstanceWithRandomValues<ChangableCollection<MockChangableObject>>();

            List<string> propertiesChanged = new List<string>();
            ((INotifyPropertyChanged)list).PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<bool> wasChagned = new List<bool>();
            list.OnChanged += (sender, changed) => wasChagned.Add(changed);

            while (list.Count > 0)
            {
                list.AcceptChanges();
                propertiesChanged.Clear();
                wasChagned.Clear();

                list.RemoveAt(list.Count - 1);

                Assert.IsTrue(list.IsChanged);
                CollectionAssert.Contains(propertiesChanged, nameof(list.IsChanged));
                Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(list.IsChanged)));
                Assert.AreEqual(1, wasChagned.Count(x => x == true));
            }
        }

        [TestMethod]
        public void NotifiesIsChangedOnMultipleItemsRemoved()
        {
            ChangableCollection<MockChangableObject> list = ObjectUtilities.CreateInstanceWithRandomValues<ChangableCollection<MockChangableObject>>();
            list.AcceptChanges();

            List<string> propertiesChanged = new List<string>();
            ((INotifyPropertyChanged)list).PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<bool> wasChanged = new List<bool>();
            list.OnChanged += (sender, changed) => wasChanged.Add(changed);

            while (list.Count > 0)
            {
                list.RemoveAt(list.Count - 1);
            }

            Assert.IsTrue(list.IsChanged);
            Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(IChangeTracking.IsChanged)));
            Assert.AreEqual(1, wasChanged.Count(x => x == true));
        }

        [TestMethod]
        public void NotifiesIsChangedOnEachItemChanged()
        {
            ChangableCollection<MockChangableObject> list = ObjectUtilities.CreateInstanceWithRandomValues<ChangableCollection<MockChangableObject>>();

            List<string> propertiesChanged = new List<string>();
            ((INotifyPropertyChanged)list).PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<bool> wasChanged = new List<bool>();
            list.OnChanged += (sender, changed) => wasChanged.Add(changed);

            foreach (MockChangableObject item in list)
            {
                list.AcceptChanges();
                propertiesChanged.Clear();
                wasChanged.Clear();

                ObjectUtilities.PopulateObjectWithRandomValues(item);

                Assert.IsTrue(list.IsChanged);
                CollectionAssert.Contains(propertiesChanged, nameof(IChangeTracking.IsChanged));
                Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(IChangeTracking.IsChanged)));
                Assert.AreEqual(1, wasChanged.Count(x => x == true));
            }
        }

        [TestMethod]
        public void NotifiesIsChangedOnMultipleItemsChanged()
        {
            ChangableCollection<MockChangableObject> list = ObjectUtilities.CreateInstanceWithRandomValues<ChangableCollection<MockChangableObject>>();
            list.AcceptChanges();

            List<string> propertiesChanged = new List<string>();
            ((INotifyPropertyChanged)list).PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<bool> wasChanged = new List<bool>();
            list.OnChanged += (sender, changed) => wasChanged.Add(changed);

            foreach (MockChangableObject item in list)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(item);
            }

            Assert.IsTrue(list.IsChanged);
            Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(list.IsChanged)));
            Assert.AreEqual(1, wasChanged.Count(x => x == true));
        }

        [TestMethod]
        public void AllListItemsAcceptChanges()
        {
            ChangableCollection<MockChangableObject> list = ObjectUtilities.CreateInstanceWithRandomValues<ChangableCollection<MockChangableObject>>();
            Assert.IsTrue(list.IsChanged);

            foreach (IChangeTracking item in list)
            {
                Assert.IsTrue(item.IsChanged);
            }

            list.AcceptChanges();

            Assert.IsFalse(list.IsChanged);

            foreach (IChangeTracking item in list)
            {
                Assert.IsFalse(item.IsChanged);
            }
        }

        [TestMethod]
        public void NotifiesIsChangedOnAcceptChanges()
        {
            ChangableCollection<MockChangableObject> list = ObjectUtilities.CreateInstanceWithRandomValues<ChangableCollection<MockChangableObject>>();
            Assert.IsTrue(list.IsChanged);

            foreach (IChangeTracking item in list)
            {
                Assert.IsTrue(item.IsChanged);
            }

            List<string> propertiesChanged = new List<string>();
            ((INotifyPropertyChanged)list).PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<bool> wasChanged = new List<bool>();
            list.OnChanged += (sender, changed) => wasChanged.Add(changed);

            list.AcceptChanges();

            Assert.IsFalse(list.IsChanged);
            Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(list.IsChanged)));
            Assert.AreEqual(1, wasChanged.Count(x => x == false));
        }
    }
}
