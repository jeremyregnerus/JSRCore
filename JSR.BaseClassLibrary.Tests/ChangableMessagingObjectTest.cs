// <copyright file="ChangableMessagingObjectTest.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using JSR.BaseClassLibrary.Tests.Mocks;
using JSR.TestAsserts;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.BaseClassLibrary.Tests
{
    [TestClass]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Unit Test.")]
    public class ChangableMessagingObjectTest
    {
        [TestMethod]
        public void Initializes()
        {
            Assert.IsFalse(new MockChangableMessagingObject().IsChanged);
        }

        [TestMethod]
        public void SerializesAndDeserializes()
        {
            SerializationAssert.SerializesAndDeserializes<MockChangableMessagingObject>();
            SerializationAssert.IsNotChangedAfterDeserialized<MockChangableMessagingObject>();
        }

        [TestMethod]
        public void ChangesValues()
        {
            PropertyChangeAssert.ChangesValues<MockChangableMessagingObject>();
        }

        [TestMethod]
        public void NotifiesPropertyChanges()
        {
            PropertyChangeAssert.NotifiesEachPropertyChanges<MockChangableMessagingObject>();
        }

        [TestMethod]
        public void ChangesOnPropertiesChanged()
        {
            ChangeTrackingAssert.IsChangedWhenEachPropertyChanges<MockChangableMessagingObject>();
            ChangeTrackingAssert.IsChangedWhenAllPropertiesChange<MockChangableMessagingObject>();
        }

        [TestMethod]
        public void NotifiesIsChanged()
        {
            ChangableObjectAssert.NotifiesIsChangedWhenEachPropertyChanges<MockChangableMessagingObject>();
            ChangableObjectAssert.NotifiesIsChangedWhenAllPropertiesChange<MockChangableMessagingObject>();
        }

        [TestMethod]
        public void NotifiesIsChangedOnChildChanges()
        {
            MockChangableMessagingObjectWithChildren obj = new MockChangableMessagingObjectWithChildren();
            obj.Child = new MockChangableMessagingObject();
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
                    Assert.AreEqual(2, wasChanged.Count);

                    propertiesChanged.Clear();
                    wasChanged.Clear();
                }

                obj.Child = new MockChangableMessagingObject();

                Assert.IsTrue(obj.IsChanged);

                obj.AcceptChanges();

                propertiesChanged.Clear();
                wasChanged.Clear();
            }

            foreach (PropertyInfo property in PropertyUtilities.GetListOfPropertiesWithPublicGetAndSetMethods(obj.ChildReadOnly))
            {
                ObjectUtilities.PopulatePropertyWithRandomValue(obj.ChildReadOnly, property);

                Assert.IsTrue(obj.IsChanged);
                Assert.IsTrue(obj.ChildReadOnly.IsChanged);

                Assert.AreEqual(1, propertiesChanged.Count(x => x == nameof(IChangeTracking.IsChanged)));
                Assert.AreEqual(1, wasChanged.Count);

                obj.AcceptChanges();

                Assert.IsFalse(obj.IsChanged);
                Assert.IsFalse(obj.ChildReadOnly.IsChanged);

                Assert.AreEqual(2, propertiesChanged.Count(x => x == nameof(IChangeTracking.IsChanged)));
                Assert.AreEqual(2, wasChanged.Count);

                propertiesChanged.Clear();
                wasChanged.Clear();
            }
        }

        [TestMethod]
        public void AcceptsChanges()
        {
            ChangeTrackingAssert.AcceptsChanges<MockChangableMessagingObject>();
        }

        [TestMethod]
        public void NotifiesIsChangeOnEachListItemAdded()
        {

        }
    }
}
