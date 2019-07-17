// <copyright file="ChangableObjectTests.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using JSR.BaseClassLibrary.Tests.Mocks;
using JSR.TestAsserts;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.BaseClassLibrary.Tests
{
    [TestClass]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Unit Test.")]
    public class ChangableObjectTests
    {
        [TestMethod]
        public void Initializes()
        {
            Assert.IsFalse(new MockChangableObjectWithChildren().IsChanged);
        }

        [TestMethod]
        public void SerializesAndDeserializes()
        {
            SerializationAssert.SerializesAndDeserializes<MockChangableObjectWithChildren>();
            SerializationAssert.IsNotChangedAfterDeserialized<MockChangableObjectWithChildren>();
        }

        [TestMethod]
        public void ChangesValues()
        {
            PropertyChangeAssert.ChangesValues<MockChangableObjectWithChildren>();
        }

        [TestMethod]
        public void NotifiesPropertyChanges()
        {
            PropertyNotificationAssert.NotifiesPropertiesChanged<MockChangableObjectWithChildren>();
        }

        [TestMethod]
        public void ChangesOnPropertiesChanged()
        {
            ChangeTrackingAssert.IsChangedWhenEachPropertyChanges<MockChangableObjectWithChildren>();
            ChangeTrackingAssert.IsChangedWhenAllPropertiesChange<MockChangableObjectWithChildren>();
        }

        [TestMethod]
        public void NotifiesIsChanged()
        {
            ChangableObjectAssert.NotifiesIsChangedWhenEachPropertyChanges<MockChangableObjectWithChildren>();
            ChangableObjectAssert.NotifiesIsChangedWhenAllPropertiesChange<MockChangableObjectWithChildren>();
        }

        [TestMethod]
        public void NotifiesIsChangedOnChildChanges()
        {
            MockChangableObjectWithChildren obj = new MockChangableObjectWithChildren
            {
                Child = new MockChangableObject(),
            };
            obj.AcceptChanges();

            List<string> propertiesChanged = new List<string>();
            obj.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<bool> wasChanged = new List<bool>();
            obj.OnChanged += (sender, changed) => wasChanged.Add(changed);

            for (int i = 0; i < new Random().Next(10); i++)
            {
                foreach (PropertyInfo property in PropertyUtilities.GetListOfPropertiesWithPublicGetAndSetMethods(obj.Child))
                {
                    ObjectUtilities.PopulatePropertyWithRandomValue(obj.Child, property);

                    Assert.IsTrue(obj.IsChanged);
                    Assert.IsTrue(obj.Child.IsChanged);

                    Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(IChangeTracking.IsChanged)));
                    Assert.AreEqual(1, wasChanged.Count);

                    obj.AcceptChanges();

                    Assert.IsFalse(obj.IsChanged);
                    Assert.IsFalse(obj.Child.IsChanged);

                    Assert.AreEqual(2, propertiesChanged.Count(x => x == nameof(IChangeTracking.IsChanged)));
                    Assert.AreEqual(2, wasChanged.Count());

                    propertiesChanged.Clear();
                    wasChanged.Clear();
                }

                obj.Child = new MockChangableObject();

                Assert.IsTrue(obj.IsChanged);

                obj.AcceptChanges();

                propertiesChanged.Clear();
                wasChanged.Clear();
            }

            foreach (PropertyInfo property in obj.ChildReadOnly.GetType().GetRuntimeProperties().Where(x => x.SetMethod != null && x.SetMethod.IsPublic))
            {
                ObjectUtilities.PopulatePropertyWithRandomValue(obj.ChildReadOnly, property);

                Assert.IsTrue(obj.IsChanged);
                Assert.IsTrue(obj.ChildReadOnly.IsChanged);

                Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(IChangeTracking.IsChanged)));

                obj.AcceptChanges();

                Assert.IsFalse(obj.IsChanged);
                Assert.IsFalse(obj.ChildReadOnly.IsChanged);

                Assert.AreEqual(2, propertiesChanged.Count(x => x == nameof(IChangeTracking.IsChanged)));
                Assert.AreEqual(2, wasChanged.Count());

                propertiesChanged.Clear();
                wasChanged.Clear();
            }
        }

        [TestMethod]
        public void AcceptsChanges()
        {
            ChangeTrackingAssert.AcceptsChanges<MockChangableObjectWithChildren>();
        }

        [TestMethod]
        public void NotifiesIsChangedOnEachListItemAdded()
        {
            MockChangableObjectWithChildren obj = new MockChangableObjectWithChildren();

            List<string> propertiesChanged = new List<string>();
            obj.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<bool> wasChanged = new List<bool>();
            obj.OnChanged += (sender, changed) => wasChanged.Add(changed);

            for (int i = 0; i < new Random().Next(10, 30); i++)
            {
                MockChangableObject item = ObjectUtilities.CreateInstanceWithRandomValues<MockChangableObject>();
                item.AcceptChanges();

                if (i % 2 == 0)
                {
                    obj.ChildCollection.Add(item);
                }
                else
                {
                    obj.ChildCollectionReadOnly.Add(item);
                }

                Assert.IsTrue(obj.IsChanged);
                Assert.IsTrue(obj.ChildCollection.IsChanged || obj.ChildCollectionReadOnly.IsChanged);

                Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(obj.IsChanged)));
                Assert.AreEqual(1, wasChanged.Count(x => x == true));

                obj.AcceptChanges();

                Assert.IsFalse(obj.IsChanged);
                Assert.IsFalse(obj.ChildCollection.IsChanged && obj.ChildCollectionReadOnly.IsChanged);

                Assert.AreEqual(2, propertiesChanged.Count(x => x == nameof(obj.IsChanged)));
                Assert.AreEqual(2, wasChanged.Count());

                propertiesChanged.Clear();
                wasChanged.Clear();
            }
        }

        [TestMethod]
        public void NotifiesIsChangedOnMultipleListItemsAdded()
        {
            MockChangableObjectWithChildren obj = new MockChangableObjectWithChildren();

            List<string> propertiesChanged = new List<string>();
            obj.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<bool> wasChanged = new List<bool>();
            obj.OnChanged += (sender, changed) => wasChanged.Add(changed);

            for (int i = 0; i < new Random().Next(10, 30); i++)
            {
                MockChangableObject item = ObjectUtilities.CreateInstanceWithRandomValues<MockChangableObject>();
                item.AcceptChanges();

                if (i % 2 == 0)
                {
                    obj.ChildCollection.Add(item);
                }
                else
                {
                    obj.ChildCollectionReadOnly.Add(item);
                }
            }

            Assert.IsTrue(obj.IsChanged);
            Assert.IsTrue(obj.ChildCollection.IsChanged);
            Assert.IsTrue(obj.ChildCollectionReadOnly.IsChanged);

            Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(obj.IsChanged)));
            Assert.AreEqual(1, wasChanged.Count());
        }

        [TestMethod]
        public void NotifiesIsChangedOnEachListItemRemoved()
        {
            MockChangableObjectWithChildren obj = ObjectUtilities.CreateInstanceWithRandomValues<MockChangableObjectWithChildren>();
            obj.AcceptChanges();

            List<string> propertiesChanged = new List<string>();
            obj.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<bool> wasChanged = new List<bool>();
            obj.OnChanged += (sender, changed) => wasChanged.Add(changed);

            while (obj.ChildCollection.Count > 0)
            {
                obj.ChildCollection.RemoveAt(obj.ChildCollection.Count - 1);

                Assert.IsTrue(obj.IsChanged);
                Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(obj.IsChanged)));
                Assert.AreEqual(1, wasChanged.Count());

                obj.AcceptChanges();
                propertiesChanged.Clear();
                wasChanged.Clear();
            }

            while (obj.ChildCollectionReadOnly.Count > 0)
            {
                obj.ChildCollectionReadOnly.RemoveAt(obj.ChildCollectionReadOnly.Count - 1);

                Assert.IsTrue(obj.IsChanged);
                Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(obj.IsChanged)));
                Assert.AreEqual(1, wasChanged.Count());

                obj.AcceptChanges();
                propertiesChanged.Clear();
                wasChanged.Clear();
            }
        }

        [TestMethod]
        public void NotifiesIsChangedOnMultipleListItemsRemoved()
        {
            MockChangableObjectWithChildren obj = ObjectUtilities.CreateInstanceWithRandomValues<MockChangableObjectWithChildren>();
            obj.AcceptChanges();

            List<string> propertiesChanged = new List<string>();
            obj.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<bool> wasChanged = new List<bool>();
            obj.OnChanged += (sender, changed) => wasChanged.Add(changed);

            while (obj.ChildCollection.Count > 0)
            {
                obj.ChildCollection.RemoveAt(obj.ChildCollection.Count - 1);
            }

            Assert.IsTrue(obj.IsChanged);
            Assert.IsTrue(obj.ChildCollection.IsChanged);

            Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(obj.IsChanged)));
            Assert.AreEqual(1, wasChanged.Count());

            obj.AcceptChanges();

            Assert.AreEqual(2, propertiesChanged.Count(x => x == nameof(obj.IsChanged)));
            Assert.AreEqual(2, wasChanged.Count());

            propertiesChanged.Clear();
            wasChanged.Clear();

            while (obj.ChildCollectionReadOnly.Count > 0)
            {
                obj.ChildCollectionReadOnly.RemoveAt(obj.ChildCollectionReadOnly.Count - 1);
            }

            Assert.IsTrue(obj.IsChanged);
            Assert.IsTrue(obj.ChildCollectionReadOnly.IsChanged);

            Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(obj.IsChanged)));
            Assert.AreEqual(1, wasChanged.Count());

            obj.AcceptChanges();

            Assert.AreEqual(2, propertiesChanged.Count(x => x == nameof(obj.IsChanged)));
            Assert.AreEqual(2, wasChanged.Count());
        }

        [TestMethod]
        public void NotifiesIsChangedOnEachListItemChanged()
        {
            MockChangableObjectWithChildren obj = ObjectUtilities.CreateInstanceWithRandomValues<MockChangableObjectWithChildren>();

            List<string> propertiesChanged = new List<string>();
            obj.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<bool> wasChanged = new List<bool>();
            obj.OnChanged += (sender, changed) => wasChanged.Add(changed);

            foreach (MockChangableObject item in obj.ChildCollection)
            {
                obj.AcceptChanges();
                propertiesChanged.Clear();
                wasChanged.Clear();

                ObjectUtilities.PopulateObjectWithRandomValues(item);

                Assert.IsTrue(obj.IsChanged);
                Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(obj.IsChanged)));
                Assert.AreEqual(1, wasChanged.Count);
            }

            foreach (MockChangableObject item in obj.ChildCollectionReadOnly)
            {
                obj.AcceptChanges();
                propertiesChanged.Clear();
                wasChanged.Clear();

                ObjectUtilities.PopulateObjectWithRandomValues(item);

                Assert.IsTrue(obj.IsChanged);
                Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(obj.IsChanged)));
                Assert.AreEqual(1, wasChanged.Count);
            }
        }

        [TestMethod]
        public void NotifiesIsChangedOnMultipleChildrenChanged()
        {
            MockChangableObjectWithChildren obj = ObjectUtilities.CreateInstanceWithRandomValues<MockChangableObjectWithChildren>();
            obj.AcceptChanges();

            List<string> propertiesChanged = new List<string>();
            obj.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<bool> wasChanged = new List<bool>();
            obj.OnChanged += (sender, changed) => wasChanged.Add(changed);

            foreach (dynamic item in obj.ChildCollection)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(item);
            }

            foreach (dynamic item in obj.ChildCollectionReadOnly)
            {
                ObjectUtilities.PopulateObjectWithRandomValues(item);
            }

            Assert.IsTrue(obj.IsChanged);
            Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(obj.IsChanged)));
            Assert.AreEqual(1, wasChanged.Count);
        }
    }
}
