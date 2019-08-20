// <copyright file="MessengerBaseClassTests.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using JSR.BaseClassLibrary.Tests.Mocks;
using JSR.TestAsserts;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.BaseClassLibrary.Tests
{
    /// <summary>
    /// Tests the <see cref="MessengerBaseClass"/> and the <see cref="MessengerCollection{T}"/> classes.
    /// </summary>
    [TestClass]
    public class MessengerBaseClassTests
    {
        /// <summary>
        /// Tests that the messaging object initializes without a message value.
        /// </summary>
        [TestMethod]
        public void InitializesWithoutMessage()
        {
            MessengerWithChildrenMock obj = new MessengerWithChildrenMock();

            Assert.IsTrue(string.IsNullOrEmpty(obj.Message));
        }

        /// <summary>
        /// Tests that messaging objects can serialize and deserialize.
        /// </summary>
        [TestMethod]
        public void SerializesAndDeserializes()
        {
            SerializationAssert.SerializesAndDeserializes<ChangableWithChildrenMock>();
        }

        /// <summary>
        /// Tests that the message value is not preserved when the object is deserialized.
        /// </summary>
        [TestMethod]
        public void SerializesWithoutMessage()
        {
            MessengerWithChildrenMock obj = ObjectUtilities.CreateInstanceWithRandomValues<MessengerWithChildrenMock>();

            obj.RaiseMessage(RandomUtilities.GetRandomString());

            obj = ObjectUtilities.GetSerializedCopyOfObject(obj);

            Assert.IsTrue(string.IsNullOrEmpty(obj.Message));
        }

        /// <summary>
        /// Tests that a <see cref="MessengerBaseClass"/> object raises a message.
        /// </summary>
        [TestMethod]
        public void RaisesMessages()
        {
            MessengerMock obj = new MessengerMock();

            List<string> propertyChanges = new List<string>();
            obj.PropertyChanged += (sender, args) => propertyChanges.Add(args.PropertyName);

            List<string> messagesRaised = new List<string>();
            obj.OnMessage += (sender, message) => messagesRaised.Add(message);

            int count = new Random().Next(5, 20);
            string messageToSend = RandomUtilities.GetRandomString(20);

            for (int i = 0; i < count; i++)
            {
                while (messageToSend == obj.Message)
                {
                    messageToSend = RandomUtilities.GetRandomString(20);
                }

                obj.RaiseMessage(messageToSend);

                CollectionAssert.Contains(messagesRaised, messageToSend);
                Assert.AreEqual(messageToSend, obj.Message);
            }

            Assert.AreEqual(count, messagesRaised.Count);
            Assert.AreEqual(count, propertyChanges.Count(propertyName => propertyName == nameof(IMessenger.Message)));
        }

        /// <summary>
        /// Tests that the same message does not raise the <see cref="OnMessageEventHandler"/>.
        /// </summary>
        [TestMethod]
        public void DoesNotRaiseNotificationOnSameMessage()
        {
            MessengerMock obj = new MessengerMock();

            List<string> messagesRaised = new List<string>();
            obj.OnMessage += (sender, message) => messagesRaised.Add(message);

            string newMessage = RandomUtilities.GetRandomString();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                obj.RaiseMessage(newMessage);
            }

            Assert.AreEqual(1, messagesRaised.Count);
        }

        /// <summary>
        /// Tests that an <see cref="MessengerBaseClass"/> object raises its child messeging object's messages.
        /// </summary>
        [TestMethod]
        public void RaisesChildMessages()
        {
            MessengerWithChildrenMock obj = new MessengerWithChildrenMock() { ChildMessenger1 = new MessengerMock(), ChildMessenger2 = new MessengerMock() };

            List<string> propertiesChanged = new List<string>();
            obj.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<string> messagesRaised = new List<string>();
            obj.OnMessage += (sender, message) => messagesRaised.Add(message);

            int count = new Random().Next(5, 20);

            for (int i = 0; i < count; i++)
            {
                string message = RandomUtilities.GetRandomString(obj.ChildMessenger1.Message);
                obj.ChildMessenger1.RaiseMessage(message);

                Assert.AreEqual(obj.Message, message);

                message = RandomUtilities.GetRandomString(obj.ChildMessenger2.Message);
                obj.ChildMessenger2.RaiseMessage(message);

                Assert.AreEqual(obj.Message, message);

                int notificationCount = (i + 1) * 2;

                Assert.AreEqual(notificationCount, propertiesChanged.Count(propertyName => propertyName == nameof(IMessenger.Message)));
                Assert.AreEqual(notificationCount, messagesRaised.Count);
            }
        }

        /// <summary>
        /// Tests that when a child is removed from an <see cref="MessengerBaseClass"/>, the object no longer raises message notification.
        /// </summary>
        [TestMethod]
        public void RemovesChildNotifications()
        {
            MessengerWithChildrenMock obj = ObjectUtilities.CreateInstanceWithRandomValues<MessengerWithChildrenMock>();

            List<string> propertiesChanged = new List<string>();
            obj.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            List<string> messagesRaised = new List<string>();
            obj.OnMessage += (sender, message) => messagesRaised.Add(message);

            MessengerMock child1 = obj.ChildMessenger1;
            MessengerMock child2 = obj.ChildMessenger2;

            obj.ChildMessenger1 = new MessengerMock();
            obj.ChildMessenger2 = new MessengerMock();

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                string message = RandomUtilities.GetRandomString(obj.Message);
                child1.RaiseMessage(message);

                Assert.AreNotEqual(message, obj.Message);

                message = RandomUtilities.GetRandomString(obj.Message);
                child2.RaiseMessage(message);

                Assert.AreNotEqual(message, obj.Message);
            }

            CollectionAssert.DoesNotContain(propertiesChanged, nameof(IMessenger.Message));
            Assert.IsTrue(messagesRaised.Count == 0);
        }

        /// <summary>
        /// Tests that children of a messaging list raise a message to the top of the collection.
        /// </summary>
        [TestMethod]
        public void RaisesMessagesOfListItems()
        {
            MessengerWithChildrenMock parent = ObjectUtilities.CreateInstanceWithRandomValues<MessengerWithChildrenMock>();

            List<string> listMessages = new List<string>();
            parent.MessengerList.OnMessage += (sender, message) => listMessages.Add(message);

            List<string> parentMessages = new List<string>();
            parent.OnMessage += (sender, message) => parentMessages.Add(message);

            foreach (MessengerMock item in parent.MessengerList)
            {
                string newMessage = RandomUtilities.GetRandomString(item.Message);
                item.RaiseMessage(newMessage);

                Assert.AreEqual(newMessage, listMessages[listMessages.Count - 1]);
                Assert.AreEqual(newMessage, parentMessages[parentMessages.Count - 1]);
            }
        }

        /// <summary>
        /// Tests that when items are removed from a MessagingCollection, they no longer raise notifications.
        /// </summary>
        [TestMethod]
        public void ItemsRemoveNotificationsWhenRemoveFromList()
        {
            MessengerWithChildrenMock parent = ObjectUtilities.CreateInstanceWithRandomValues<MessengerWithChildrenMock>();

            List<string> listMessages = new List<string>();
            parent.MessengerList.OnMessage += (sender, message) => listMessages.Add(message);

            List<string> parentMessages = new List<string>();
            parent.OnMessage += (sender, message) => parentMessages.Add(message);

            while (parent.MessengerList.Count > 0)
            {
                MessengerMock child = parent.MessengerList[parent.MessengerList.Count - 1];

                parent.MessengerList.Remove(child);

                string newMessage = RandomUtilities.GetRandomString(parent.Message);
                child.RaiseMessage(newMessage);

                Assert.AreNotEqual(child.Message, parent.MessengerList.Message);
                Assert.AreNotEqual(child.Message, parent.Message);

                Assert.AreEqual(0, listMessages.Count);
                Assert.AreEqual(0, parentMessages.Count);
            }
        }
    }
}
