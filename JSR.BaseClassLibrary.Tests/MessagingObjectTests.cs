// <copyright file="MessagingObjectTests.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JSR.BaseClassLibrary.Tests.Mocks;
using JSR.TestAsserts;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.BaseClassLibrary.Tests
{
    /// <summary>
    /// Tests the <see cref="MessagingObject"/> and the <see cref="MessagingCollection{T}"/> classes.
    /// </summary>
    [TestClass]
    public class MessagingObjectTests
    {
        /// <summary>
        /// Tests that the messaging object initializes without a message value.
        /// </summary>
        [TestMethod]
        public void InitializesWithoutMessage()
        {
            MockMessagingObjectWithChildren obj = new MockMessagingObjectWithChildren();

            Assert.IsTrue(string.IsNullOrEmpty(obj.Message));
        }

        /// <summary>
        /// Tests that messaging objects can serialize and deserialize.
        /// </summary>
        [TestMethod]
        public void SerializesAndDeserializes()
        {
            SerializationAssert.SerializesAndDeserializes<MockChangableObjectWithChildren>();

            MockMessagingObjectWithChildren obj = ObjectUtilities.CreateInstanceWithRandomValues<MockMessagingObjectWithChildren>();

            Assert.IsTrue(string.IsNullOrEmpty(obj.Message));

            obj.RaiseMessage(RandomUtilities.GetRandomString());

            Assert.IsFalse(string.IsNullOrEmpty(obj.Message));

            obj = ObjectUtilities.GetSerializedCopyOfObject(obj);

            Assert.IsTrue(string.IsNullOrEmpty(obj.Message));
        }

        /// <summary>
        /// Tests that a <see cref="MessagingObject"/> object raises a message.
        /// </summary>
        [TestMethod]
        public void RaisesMessages()
        {
            PropertyNotificationAssertTracker<MockMessagingObject> tracker = new PropertyNotificationAssertTracker<MockMessagingObject>();

            int count = new Random().Next(5, 20);
            string messageToSend = RandomUtilities.GetRandomString(20);

            for (int i = 0; i < count; i++)
            {
                while (messageToSend == tracker.TrackedObject.Message)
                {
                    messageToSend = RandomUtilities.GetRandomString(20);
                }

                tracker.TrackedObject.RaiseMessage(messageToSend);

                Assert.AreEqual(messageToSend, tracker.TrackedObject.Message);
            }

            tracker.AssertPropertyCount(nameof(tracker.TrackedObject.Message), count);
        }

        /// <summary>
        /// Tests that the same message does not raise the <see cref="OnMessageEventHandler"/>.
        /// </summary>
        [TestMethod]
        public void DoesNotRaiseNotificationOnSameMessage()
        {
            MockMessagingObject obj = new MockMessagingObject();

            List<string> messages = new List<string>();
            obj.OnMessage += (sender, message) => messages.Add(message);

            string messageValue = RandomUtilities.GetRandomString();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.RaiseMessage(messageValue);
            }

            Assert.AreEqual(1, messages.Count);
        }

        /// <summary>
        /// Tests that an <see cref="MessagingObject"/> object raises its child messeging object's messages.
        /// </summary>
        [TestMethod]
        public void RaisesChildMessages()
        {
            PropertyNotificationAssertTracker<MockMessagingObjectWithChildren> tracker = new PropertyNotificationAssertTracker<MockMessagingObjectWithChildren>(new MockMessagingObjectWithChildren() { Child1 = new MockMessagingObjectWithChildren(), Child2 = new MockMessagingObjectWithChildren() });

            int count = new Random().Next(5, 20);

            string child1Message = RandomUtilities.GetRandomString(20);
            string child2Message = RandomUtilities.GetRandomString(20);

            for (int i = 0; i < count; i++)
            {
                while (child1Message == tracker.TrackedObject.Message || child1Message == tracker.TrackedObject.Child1.Message)
                {
                    child1Message = RandomUtilities.GetRandomString(20);
                }

                tracker.TrackedObject.Child1.RaiseMessage(child1Message);

                Assert.AreEqual(tracker.TrackedObject.Message, child1Message);

                while (child2Message == tracker.TrackedObject.Message || child2Message == tracker.TrackedObject.Child2.Message)
                {
                    child2Message = RandomUtilities.GetRandomString(20);
                }

                tracker.TrackedObject.Child2.RaiseMessage(child2Message);

                Assert.AreEqual(tracker.TrackedObject.Message, child2Message);

                tracker.AssertPropertyCount(nameof(IMessenger.Message), (i + 1) * 2);
            }
        }

        /// <summary>
        /// Tests that when a child is removed from an <see cref="MessagingObject"/>, the object no longer raises message notification.
        /// </summary>
        [TestMethod]
        public void RemovesChildNotifications()
        {
            PropertyNotificationAssertTracker<MockMessagingObjectWithChildren> tracker = new PropertyNotificationAssertTracker<MockMessagingObjectWithChildren>(ObjectUtilities.CreateInstanceWithRandomValues<MockMessagingObjectWithChildren>());

            MockMessagingObject child1 = tracker.TrackedObject.Child1;
            MockMessagingObject child2 = tracker.TrackedObject.Child2;

            child1.RaiseMessage(RandomUtilities.GetRandomString(20));
            child2.RaiseMessage(RandomUtilities.GetRandomString(20));

            tracker.TrackedObject.Child1 = null;
            tracker.TrackedObject.Child2 = null;

            tracker.Reset();

            string child1Message = child1.Message;
            string child2Message = child2.Message;

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                while (child1Message == child1.Message || child1Message == tracker.TrackedObject.Message)
                {
                    child1Message = RandomUtilities.GetRandomString(20);
                }

                child1.RaiseMessage(child1Message);
                Assert.AreNotEqual(child1Message, tracker.TrackedObject.Message);

                while (child2Message == child2.Message || child2Message == tracker.TrackedObject.Message)
                {
                    child2Message = RandomUtilities.GetRandomString(20);
                }

                child2.RaiseMessage(child2Message);
                Assert.AreNotEqual(child2Message, tracker.TrackedObject.Message);
            }

            CollectionAssert.DoesNotContain(tracker.PropertiesChanged, nameof(IMessenger.Message));
        }

        /// <summary>
        /// Tests that children of a messaging list raise a message to the top of the collection.
        /// </summary>
        [TestMethod]
        public void RaisesMessagesOfListItems()
        {
            MockMessagingObjectWithChildren obj = new MockMessagingObjectWithChildren();

            ObjectUtilities.PopulateListWithRandomValues(obj.MessagingList);

            List<string> messages = new List<string>();
            obj.OnMessage += (sender, message) => messages.Add(message);

            foreach (MockMessagingObject item in obj.MessagingList)
            {
                string newMessage = RandomUtilities.GetRandomString(item.Message);
                item.RaiseMessage(newMessage);

                Assert.AreEqual(newMessage, messages[messages.Count - 1]);
            }
        }

        /// <summary>
        /// Tests that when items are removed from a MessagingCollection, they no longer raise notifications.
        /// </summary>
        [TestMethod]
        public void ItemsRemoveNotificationsWhenRemoveFromList()
        {
            MockMessagingObjectWithChildren obj = new MockMessagingObjectWithChildren();

            ObjectUtilities.PopulateListWithRandomValues(obj.MessagingList);

            List<string> messages = new List<string>();
            obj.OnMessage += (sender, message) => messages.Add(message);

            while (obj.MessagingList.Count > 0)
            {
                MockMessagingObject child = obj.MessagingList[obj.MessagingList.Count - 1];

                obj.MessagingList.Remove(child);

                string newMessage = RandomUtilities.GetRandomString(obj.Message);
                child.RaiseMessage(newMessage);

                Assert.AreNotEqual(child.Message, obj.Message);
            }
        }
    }
}
