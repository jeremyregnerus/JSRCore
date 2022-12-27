// <copyright file="BaseClassTests.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.ComponentModel;
using JSR.Asserts;
using JSR.BaseClasses.Tests.Mocks;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.BaseClasses.Tests
{
    /// <summary>
    /// Performs testing on <see cref="BaseClass"/> and <see cref="BaseCollection{T}"/> using <see cref="BaseClassMock"/> and <see cref="BaseClassMockWithChildren"/>.
    /// </summary>
    [TestClass]
    public class BaseClassTests
    {
        /// <summary>
        /// Tests that <see cref="BaseClass"/> serializes and deserializes.
        /// </summary>
        [TestMethod]
        public void SerializesAndDeserializes()
        {
            Assert.That.SerializesAndDeserializes<BaseClassMock>();
            Assert.That.SerializesAndDeserializes<BaseClassMockWithChildren>();
        }

        /// <summary>
        /// Tests that <see cref="BaseClass.IsChanged"/> is false when deserialized.
        /// </summary>
        [TestMethod]
        public void IsNotChangedAfterDeserialization()
        {
            Assert.That.IsNotChangedAfterDeserialized<BaseClassMock>();
            Assert.That.IsNotChangedAfterDeserialized<BaseClassMockWithChildren>();
        }

        /// <summary>
        /// Tests that <see cref="BaseClass"/> does not set the <see cref="BaseClass.Message"/> value when deserializing.
        /// </summary>
        [TestMethod]
        public void HasNoMessageWhenDeserialized()
        {
            BaseClassMock bcm = ObjectUtilities.CreateInstanceWithRandomValues<BaseClassMock>();
            BaseClassMockWithChildren bcmwc = ObjectUtilities.CreateInstanceWithRandomValues<BaseClassMockWithChildren>();

            bcm.ChangeMessage(RandomUtilities.GetRandomString());
            bcmwc.ChangeMessage(RandomUtilities.GetRandomString());

            Assert.IsTrue(string.IsNullOrEmpty(ObjectUtilities.GetSerializedCopyOfObject(bcm).Message));
            Assert.IsTrue(string.IsNullOrEmpty(ObjectUtilities.GetSerializedCopyOfObject(bcmwc).Message));
        }

        /// <summary>
        /// Tests that <see cref="BaseClass"/> initializes without setting <see cref="BaseClass.IsChanged"/> to true.
        /// </summary>
        [TestMethod]
        public void IsChangedWhenCreated()
        {
            Assert.IsTrue(new BaseClassMock().IsChanged);
            Assert.IsTrue(new BaseClassMockWithChildren().IsChanged);
        }

        /// <summary>
        /// Tests that <see cref="BaseClass"/> initializes without setting <see cref="BaseClass.Message"/> value.
        /// </summary>
        [TestMethod]
        public void HasNoMessageWhenInitialized()
        {
            Assert.IsTrue(string.IsNullOrEmpty(new BaseClassMock().Message));
            Assert.IsTrue(string.IsNullOrEmpty(new BaseClassMockWithChildren().Message));
        }

        /// <summary>
        /// Tests that <see cref="BaseClass"/> updates the values of its properties.
        /// </summary>
        [TestMethod]
        public void ChangesValues()
        {
            Assert.That.PropertiesChangeValues<BaseClassMock>();
            Assert.That.PropertiesChangeValues<BaseClassMockWithChildren>();
        }

        /// <summary>
        /// Tests that <see cref="BaseClass"/> raises the <see cref="PropertyChangedEventHandler"/> when property values change.
        /// </summary>
        [TestMethod]
        public void NotifiesPropertiesChange()
        {
            NotifyPropertyChangedAssert.NotifiesPropertiesChanged<BaseClassMockWithChildren>();
            NotifyPropertyChangedAssert.NotifiesPropertiesChanged(GetSerializedBaseClassMockWithChildren());
        }

        /// <summary>
        /// Tests that <see cref="BaseClass"/> change <see cref="BaseClass.IsChanged"/> to true when its property values change.
        /// </summary>
        [TestMethod]
        public void IsChangedWhenHiearchyChanges()
        {
            ChangeTrackingAssert.IsChangedWhenHierarchyChanges<BaseClassMockWithChildren>();
            ChangeTrackingAssert.IsChangedWhenHierarchyChanges(GetSerializedBaseClassMockWithChildren());
        }

        /// <summary>
        /// Tests that <see cref="BaseClass"/> raises the <see cref="OnChangedEventHandler"/> when its property values change.
        /// </summary>
        [TestMethod]
        public void NotifiesIsChangedWhenHiearchyChanges()
        {
            NotifyChangeAssert.NotifiesIsChangedWhenHierarchyChanges<BaseClassMockWithChildren>();
            NotifyChangeAssert.NotifiesIsChangedWhenHierarchyChanges(GetSerializedBaseClassMockWithChildren());
        }

        /// <summary>
        /// Tests that <see cref="BaseClass"/> sets <see cref="BaseClass.IsChanged"/> to false when executing <see cref="BaseClass.AcceptChanges"/>.
        /// </summary>
        [TestMethod]
        public void AcceptsChanges()
        {
            ChangeTrackingAssert.AcceptsChanges<BaseClassMockWithChildren>();
            ChangeTrackingAssert.AcceptsChanges(GetSerializedBaseClassMockWithChildren());
        }

        /// <summary>
        /// Tests that <see cref="BaseClass"/> raises the <see cref="OnChangedEventHandler"/> once when executing <see cref="BaseClass.AcceptChanges"/>.
        /// </summary>
        [TestMethod]
        public void NotifiesIsChangedOnAcceptChanges()
        {
            NotifyChangeAssert.NotifiesIsChangedOnAcceptChanges<BaseClassMockWithChildren>();
            NotifyChangeAssert.NotifiesIsChangedOnAcceptChanges(GetSerializedBaseClassMockWithChildren());
        }

        /// <summary>
        /// Tests that <see cref="BaseClass"/> raises the <see cref="OnMessageEventHandler"/>.
        /// </summary>
        [TestMethod]
        public void RaisesMessages()
        {
            BaseClassMockWithChildren obj = new BaseClassMockWithChildren();
            MessengerMonitor<BaseClassMockWithChildren> monitor = new MessengerMonitor<BaseClassMockWithChildren>(obj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                string message = RandomUtilities.GetRandomString(obj.Message);
                obj.ChangeMessage(message);

                monitor.AssertMessageNotification(message, false);
                monitor.AssertMessageCount(i + 1, false);
            }
        }

        /// <summary>
        /// Tests that <see cref="BaseClass"/> does not raise the <see cref="OnMessageEventHandler"/> when the same value is set for <see cref="BaseClass.Message"/> multiple times.
        /// </summary>
        [TestMethod]
        public void DoesNotRaiseMessageNotificationOnSameMessage()
        {
            BaseClassMockWithChildren obj = new BaseClassMockWithChildren();
            MessengerMonitor<BaseClassMockWithChildren> monitor = new MessengerMonitor<BaseClassMockWithChildren>(obj);

            string message = RandomUtilities.GetRandomString();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.ChangeMessage(message);
            }

            monitor.AssertMessageNotification(message, false);
            monitor.AssertMessageCount(1, false);
        }

        /// <summary>
        /// Tests that <see cref="BaseClass"/> raises the <see cref="OnMessageEventHandler"/> when it's children raise messages.
        /// </summary>
        [TestMethod]
        public void RaisesChildMessages()
        {
            BaseClassMockWithChildren obj = ObjectUtilities.CreateInstanceWithRandomValues<BaseClassMockWithChildren>();
            MessengerMonitor<BaseClassMockWithChildren> monitor = new MessengerMonitor<BaseClassMockWithChildren>(obj);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                string message = RandomUtilities.GetRandomString(obj.Child.Message);
                obj.Child.ChangeMessage(message);

                monitor.AssertMessageNotification(message, false);
                monitor.AssertMessageCount(i + 1, false);
            }

            monitor.ClearNotifications();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                string message = RandomUtilities.GetRandomString(obj.ChildReadOnly.Message);
                obj.ChildReadOnly.ChangeMessage(message);

                monitor.AssertMessageNotification(message, false);
                monitor.AssertMessageCount(i + 1, false);
            }

            monitor.ClearNotifications();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                string message = RandomUtilities.GetRandomString(obj.Message);

                obj.Child.ChangeMessage(message);
                obj.ChildReadOnly.ChangeMessage(message);

                monitor.AssertMessageNotification(message, false);
                monitor.AssertMessageCount(i + 1, false);
            }
        }

        /// <summary>
        /// Tests that <see cref="BaseClass"/> raises the <see cref="OnMessageEventHandler"/> when items in a list raise a message.
        /// </summary>
        [TestMethod]
        public void RaisesListItemMessages()
        {
            BaseClassMockWithChildren obj = ObjectUtilities.CreateInstanceWithRandomValues<BaseClassMockWithChildren>();
            MessengerMonitor<BaseClassMockWithChildren> monitor = new MessengerMonitor<BaseClassMockWithChildren>(obj);

            int count = 0;

            foreach (BaseClassMock item in obj.ChildCollection)
            {
                string message = RandomUtilities.GetRandomString(item.Message);
                item.ChangeMessage(message);

                count++;

                monitor.AssertMessageNotification(message, false);
                monitor.AssertMessageCount(count, false);
            }

            monitor.ClearNotifications();
            count = 0;

            foreach (BaseClassMock item in obj.ChildCollectionReadOnly)
            {
                string message = RandomUtilities.GetRandomString(item.Message);
                item.ChangeMessage(message);

                count++;

                monitor.AssertMessageNotification(message, false);
                monitor.AssertMessageCount(count, false);
            }
        }

        /// <summary>
        /// Tests that <see cref="BaseClass"/> no longer notifies messages when Child is removed.
        /// </summary>
        [TestMethod]
        public void RemovesChildMessageNotification()
        {
            BaseClassMockWithChildren obj = ObjectUtilities.CreateInstanceWithRandomValues<BaseClassMockWithChildren>();
            obj.ChangeMessage();

            MessengerMonitor<BaseClassMockWithChildren> monitor = new MessengerMonitor<BaseClassMockWithChildren>(obj);

            BaseClassMock child = obj.Child;
            obj.Child = new BaseClassMock();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                string message = RandomUtilities.GetRandomString(obj.Message);
                child.ChangeMessage(message);

                Assert.AreNotEqual(message, obj.Message);
            }

            monitor.AssertMessageCount(0, false);
        }

        /// <summary>
        /// Tests that <see cref="BaseClass"/> no longer notifies messages for items that are removed from a list.
        /// </summary>
        [TestMethod]
        public void RemovesListItemMessageNotification()
        {
            BaseClassMockWithChildren obj = ObjectUtilities.CreateInstanceWithRandomValues<BaseClassMockWithChildren>();
            obj.ChangeMessage();

            MessengerMonitor<BaseClassMockWithChildren> monitor = new MessengerMonitor<BaseClassMockWithChildren>(obj);

            while (obj.ChildCollection.Count > 0)
            {
                BaseClassMock child = obj.ChildCollection.Last();
                obj.ChildCollection.Remove(child);

                child.ChangeMessage(RandomUtilities.GetRandomString(obj.Message));

                Assert.AreNotEqual(child.Message, obj.Message);
                monitor.AssertMessageCount(0, false);
            }

            while (obj.ChildCollectionReadOnly.Count > 0)
            {
                BaseClassMock child = obj.ChildCollectionReadOnly.Last();
                obj.ChildCollectionReadOnly.Remove(child);

                child.ChangeMessage(RandomUtilities.GetRandomString(obj.Message));

                Assert.AreNotEqual(child.Message, obj.Message);
                monitor.AssertMessageCount(0, false);
            }
        }

        private static BaseClassMockWithChildren GetSerializedBaseClassMockWithChildren()
        {
            return ObjectUtilities.GetSerializedCopyOfObject(ObjectUtilities.CreateInstanceWithRandomValues<BaseClassMockWithChildren>());
        }
    }
}
