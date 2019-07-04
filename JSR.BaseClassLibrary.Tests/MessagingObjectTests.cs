// <copyright file="MessagingObjectTests.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JSR.BaseClassLibrary.Tests.Mocks;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.BaseClassLibrary.Tests
{
    [TestClass]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Unit Test.")]
    public class MessagingObjectTests
    {
        [TestMethod]
        public void RaisesMessage()
        {
            MockMessagingObject obj = new MockMessagingObject();
            List<string> changes = new List<string>();
            obj.PropertyChanged += (x, y) => changes.Add(y.PropertyName);

            int count = new Random().Next(5, 20);
            string messageToSend = RandomUtilities.GetRandomString(20);

            for (int i = 0; i < count; i++)
            {
                while (messageToSend == obj.Message)
                {
                    messageToSend = RandomUtilities.GetRandomString(20);
                }

                obj.RaiseMessage(messageToSend);

                Assert.AreEqual(messageToSend, obj.Message);
            }

            Assert.AreEqual(count, changes.FindAll(s => s == nameof(obj.Message)).Count);
        }

        [TestMethod]
        public void RaisesChildMessages()
        {
            MockMessagingObject parent = new MockMessagingObject() { Child1 = new MockMessagingObject(), Child2 = new MockMessagingObject() };

            List<string> changes = new List<string>();
            parent.PropertyChanged += (x, y) => changes.Add(y.PropertyName);

            int count = new Random().Next(5, 20);

            string child1Message = RandomUtilities.GetRandomString(20);
            string child2Message = RandomUtilities.GetRandomString(20);

            for (int i = 0; i < count; i++)
            {
                while (child1Message == parent.Message || child1Message == parent.Child1.Message)
                {
                    child1Message = RandomUtilities.GetRandomString(20);
                }

                parent.Child1.RaiseMessage(child1Message);

                Assert.AreEqual(parent.Message, child1Message);

                while (child2Message == parent.Message || child2Message == parent.Child2.Message)
                {
                    child2Message = RandomUtilities.GetRandomString(20);
                }

                parent.Child2.RaiseMessage(child2Message);

                Assert.AreEqual(parent.Message, child2Message);

                Assert.AreEqual((i + 1) * 2, changes.FindAll(s => s == nameof(IMessenger.Message)).Count);
            }
        }

        [TestMethod]
        public void RemovesChildNotifications()
        {
            MockMessagingObject parent = new MockMessagingObject();

            MockMessagingObject child1 = new MockMessagingObject();
            MockMessagingObject child2 = new MockMessagingObject();

            parent.Child1 = child1;
            parent.Child2 = child2;

            child1.RaiseMessage(RandomUtilities.GetRandomString(20));
            child2.RaiseMessage(RandomUtilities.GetRandomString(20));

            parent.Child1 = null;
            parent.Child2 = null;

            List<string> changes = new List<string>();
            parent.PropertyChanged += (x, y) => changes.Add(y.PropertyName);

            string child1Message = child1.Message;
            string child2Message = child2.Message;

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                while (child1Message == child1.Message || child1Message == parent.Message)
                {
                    child1Message = RandomUtilities.GetRandomString(20);
                }

                child1.RaiseMessage(child1Message);
                Assert.AreNotEqual(child1Message, parent.Message);

                while (child2Message == child2.Message || child2Message == parent.Message)
                {
                    child2Message = RandomUtilities.GetRandomString(20);
                }

                child2.RaiseMessage(child2Message);
                Assert.AreNotEqual(child2Message, parent.Message);
            }

            CollectionAssert.DoesNotContain(changes, nameof(IMessenger.Message));
        }
    }
}
