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
    /// Performs testing on <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger"/> and <see cref="BaseCollection{T}"/> using <see cref="BaseClassMock"/> and <see cref="BaseClassMockWithChildren"/>.
    /// </summary>
    [TestClass]
    public class BaseClassTests
    {
        /// <summary>
        /// Tests that <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger"/> serializes and deserializes.
        /// </summary>
        [TestMethod]
        public void SerializesAndDeserializes()
        {
            Assert.That.SerializesAndDeserializes<BaseClassMock>();
            Assert.That.SerializesAndDeserializes<BaseClassMockWithChildren>();
        }

        /// <summary>
        /// Tests that <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger.IsChanged"/> is false when deserialized.
        /// </summary>
        [TestMethod]
        public void IsNotChangedAfterDeserialization()
        {
            Assert.That.IsNotChangedAfterDeserialized<BaseClassMock>();
            Assert.That.IsNotChangedAfterDeserialized<BaseClassMockWithChildren>();
        }

        /// <summary>
        /// Tests that <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger"/> does not set the <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger.Message"/> value when deserializing.
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
        /// Tests that <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger"/> initializes without setting <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger.IsChanged"/> to true.
        /// </summary>
        [TestMethod]
        public void IsChangedWhenCreated()
        {
            Assert.IsTrue(new BaseClassMock().IsChanged);
            Assert.IsTrue(new BaseClassMockWithChildren().IsChanged);
        }

        /// <summary>
        /// Tests that <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger"/> initializes without setting <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger.Message"/> value.
        /// </summary>
        [TestMethod]
        public void HasNoMessageWhenInitialized()
        {
            Assert.IsTrue(string.IsNullOrEmpty(new BaseClassMock().Message));
            Assert.IsTrue(string.IsNullOrEmpty(new BaseClassMockWithChildren().Message));
        }

        /// <summary>
        /// Tests that <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger"/> updates the values of its properties.
        /// </summary>
        [TestMethod]
        public void ChangesValues()
        {
            Assert.That.PropertiesChangeValues<BaseClassMock>();
            Assert.That.PropertiesChangeValues<BaseClassMockWithChildren>();
        }

        /// <summary>
        /// Tests that <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger"/> raises the <see cref="PropertyChangedEventHandler"/> when property values change.
        /// </summary>
        [TestMethod]
        public void NotifiesPropertiesChange()
        {
            Assert.That.NotifiesPropertiesChanged<BaseClassMockWithChildren>();
            Assert.That.NotifiesPropertiesChanged(GetSerializedBaseClassMockWithChildren());
        }

        /// <summary>
        /// Tests that <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger"/> change <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger.IsChanged"/> to true when its property values change.
        /// </summary>
        [TestMethod]
        public void IsChangedWhenHiearchyChanges()
        {
            Assert.That.IsChangedWhenHierarchyChanges<BaseClassMockWithChildren>();
            Assert.That.IsChangedWhenHierarchyChanges(GetSerializedBaseClassMockWithChildren());
        }

        /// <summary>
        /// Tests that <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger"/> raises the <see cref="OnChangedEventHandler"/> when its property values change.
        /// </summary>
        [TestMethod]
        public void NotifiesIsChangedWhenHiearchyChanges()
        {
            Assert.That.NotifiesIsChangedWhenHierarchyChanges<BaseClassMockWithChildren>();
            Assert.That.NotifiesIsChangedWhenHierarchyChanges(GetSerializedBaseClassMockWithChildren());
        }

        /// <summary>
        /// Tests that <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger"/> sets <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger.IsChanged"/> to false when executing <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger.AcceptChanges"/>.
        /// </summary>
        [TestMethod]
        public void AcceptsChanges()
        {
            Assert.That.AcceptsChanges<BaseClassMockWithChildren>();
            Assert.That.AcceptsChanges(GetSerializedBaseClassMockWithChildren());
        }

        /// <summary>
        /// Tests that <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger"/> raises the <see cref="OnChangedEventHandler"/> once when executing <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger.AcceptChanges"/>.
        /// </summary>
        [TestMethod]
        public void NotifiesIsChangedOnAcceptChanges()
        {
            Assert.That.NotifiesIsChangedOnAcceptChanges<BaseClassMockWithChildren>();
            Assert.That.NotifiesIsChangedOnAcceptChanges(GetSerializedBaseClassMockWithChildren());
        }

        /// <summary>
        /// Tests that <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger"/> raises the <see cref="OnMessageEventHandler"/>.
        /// </summary>
        [TestMethod]
        public void RaisesMessages()
        {
            BaseClassMockWithChildren obj = new();
            List<string> messages = new();
            obj.OnMessage += (s, m) => messages.Add(m);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                string message = RandomUtilities.GetRandomString(obj.Message);
                obj.ChangeMessage(message);

                Assert.AreEqual(message, messages.LastOrDefault());
                Assert.AreEqual(i + 1, messages.Count);
            }
        }

        /// <summary>
        /// Tests that <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger"/> does not raise the <see cref="OnMessageEventHandler"/> when the same value is set for <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger.Message"/> multiple times.
        /// </summary>
        [TestMethod]
        public void DoesNotRaiseMessageNotificationOnSameMessage()
        {
            BaseClassMockWithChildren obj = new();
            List<string> messages = new();
            obj.OnMessage += (s, m) => messages.Add(m);

            string message = RandomUtilities.GetRandomString();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.ChangeMessage(message);
            }

            Assert.AreEqual(message, messages.LastOrDefault());
            Assert.AreEqual(1, messages.Count);
        }

        /// <summary>
        /// Tests that <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger"/> raises the <see cref="OnMessageEventHandler"/> when it's children raise messages.
        /// </summary>
        [TestMethod]
        public void RaisesChildMessages()
        {
            BaseClassMockWithChildren obj = ObjectUtilities.CreateInstanceWithRandomValues<BaseClassMockWithChildren>();
            List<string> messages = new();
            obj.OnMessage += (s, m) => messages.Add(m);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                string message = RandomUtilities.GetRandomString(obj.Child.Message);
                obj.Child.ChangeMessage(message);

                Assert.AreEqual(message, messages.LastOrDefault());
                Assert.AreEqual(i + 1, messages.Count);
            }

            messages.Clear();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                string message = RandomUtilities.GetRandomString(obj.ChildReadOnly.Message);
                obj.ChildReadOnly.ChangeMessage(message);

                Assert.AreEqual(message, messages.LastOrDefault());
                Assert.AreEqual(i + 1, messages.Count);
            }

            messages.Clear();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                string message = RandomUtilities.GetRandomString(obj.Message);

                obj.Child.ChangeMessage(message);
                obj.ChildReadOnly.ChangeMessage(message);

                Assert.AreEqual(message, messages.LastOrDefault());
                Assert.AreEqual(i + 1, messages.Count);
            }
        }

        /// <summary>
        /// Tests that <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger"/> raises the <see cref="OnMessageEventHandler"/> when items in a list raise a message.
        /// </summary>
        [TestMethod]
        public void RaisesListItemMessages()
        {
            BaseClassMockWithChildren obj = ObjectUtilities.CreateInstanceWithRandomValues<BaseClassMockWithChildren>();

            List<string> messages = new();
            obj.OnMessage += (s, m) => messages.Add(m);

            int count = 0;

            foreach (BaseClassMock item in obj.ChildCollection)
            {
                string message = RandomUtilities.GetRandomString(item.Message);
                item.ChangeMessage(message);

                count++;

                Assert.AreEqual(message, messages.LastOrDefault());
                Assert.AreEqual(count, messages.Count);
            }

            messages.Clear();
            count = 0;

            foreach (BaseClassMock item in obj.ChildCollectionReadOnly)
            {
                string message = RandomUtilities.GetRandomString(item.Message);
                item.ChangeMessage(message);

                count++;

                Assert.AreEqual(message, messages.LastOrDefault());
                Assert.AreEqual(count, messages.Count);
            }
        }

        /// <summary>
        /// Tests that <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger"/> no longer notifies messages when Child is removed.
        /// </summary>
        [TestMethod]
        public void RemovesChildMessageNotification()
        {
            BaseClassMockWithChildren obj = ObjectUtilities.CreateInstanceWithRandomValues<BaseClassMockWithChildren>();
            obj.ChangeMessage();

            List<string> messages = new();
            obj.OnMessage += (s, m) => messages.Add(m);

            BaseClassMock child = obj.Child;
            obj.Child = new BaseClassMock();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                string message = RandomUtilities.GetRandomString(obj.Message);
                child.ChangeMessage(message);

                Assert.AreNotEqual(message, messages.LastOrDefault());
                Assert.AreEqual(0, messages.Count);
            }
        }

        /// <summary>
        /// Tests that <see cref="BaseNotifyPropertyChangedNotifyChangedMessenger"/> no longer notifies messages for items that are removed from a list.
        /// </summary>
        [TestMethod]
        public void RemovesListItemMessageNotification()
        {
            BaseClassMockWithChildren obj = ObjectUtilities.CreateInstanceWithRandomValues<BaseClassMockWithChildren>();
            obj.ChangeMessage();

            List<string> messages = new();
            obj.OnMessage += (s, m) => messages.Add(m);

            while (obj.ChildCollection.Count > 0)
            {
                BaseClassMock child = obj.ChildCollection.Last();
                obj.ChildCollection.Remove(child);

                child.ChangeMessage(RandomUtilities.GetRandomString(obj.Message));

                Assert.AreNotEqual(child.Message, messages.LastOrDefault());
                Assert.AreEqual(0, messages.Count);
            }

            while (obj.ChildCollectionReadOnly.Count > 0)
            {
                BaseClassMock child = obj.ChildCollectionReadOnly.Last();
                obj.ChildCollectionReadOnly.Remove(child);

                child.ChangeMessage(RandomUtilities.GetRandomString(obj.Message));

                Assert.AreNotEqual(child.Message, messages.LastOrDefault());
                Assert.AreEqual(0, messages.Count);
            }
        }

        private static BaseClassMockWithChildren GetSerializedBaseClassMockWithChildren()
        {
            return ObjectUtilities.GetSerializedCopyOfObject(ObjectUtilities.CreateInstanceWithRandomValues<BaseClassMockWithChildren>());
        }
    }
}
