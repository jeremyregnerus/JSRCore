using JSR.BaseClasses.Tests.Mocks;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.BaseClasses.Tests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Unit tests")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit tests")]
    public class BaseMessengerUnitTests
    {
        [TestMethod]
        public void Message_RaisesNotification()
        {
            MockBaseMessenger messenger = new();
            List<string> messages = new();
            messenger.OnMessage += (sender, message) => messages.Add(message);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                string newMessage = new Random().NewString(messenger.Message, 8);
                messenger.ChangeMessage(newMessage);

                CollectionAssert.Contains(messages, newMessage);
                Assert.AreEqual(i + 1, messages.Count);
            }
        }

        [TestMethod]
        public void Message_DoesNotRaiseNotificationOnSameMessage()
        {
            MockBaseMessenger messenger = new();
            string message = new Random().NextString(8);
            messenger.ChangeMessage(message);

            List<string> messages = new();
            messenger.OnMessage += (sender, message) => messages.Add(message);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                messenger.ChangeMessage(message);
            }

            Assert.AreEqual(0, messages.Count);
        }

        [TestMethod]
        public void Message_RaisesChildMessage()
        {
            MockBaseMessengerParent messenger = new();
            List<string> messages = new();
            messenger.OnMessage += (sender, message) => messages.Add(message);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                string message = new Random().NewString(messenger.Child.Message, 8);
                messenger.Child.ChangeMessage(message);

                CollectionAssert.Contains(messages, message);
                Assert.AreEqual(i + 1, messages.Count);
            }
        }

        [TestMethod]
        public void Message_DoesNotRaiseNotificationWhenChildMessagesMatch()
        {
            MockBaseMessengerParent parent = new();
            string message = new Random().NextString(8);
            parent.Child.ChangeMessage(message);

            Assert.AreEqual(message, parent.Message);

            List<string> messages = new();

            parent.OnMessage += (sender, message) => messages.Add(message);

            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                parent.Child.ChangeMessage(message);
            }

            Assert.AreEqual(0, messages.Count);
        }

        [TestMethod]
        public void Message_UpdatesForNewChildren()
        {
            MockBaseMessengerParent parent = new();
            List<string> messages = new();
            parent.OnMessage += (sender, message) => messages.Add(message);

            MockBaseMessenger child = new();
            parent.Child = child;

            string message = new Random().NextString();

            child.ChangeMessage(message);

            Assert.AreEqual(message, parent.Message);
        }

        [TestMethod]
        public void Message_DoesNotUpdateForOldChildren()
        {
            MockBaseMessengerParent parent = new();
            List<string> messages = new();
            parent.OnMessage += (sender, message) => messages.Add(message);

            MockBaseMessenger oldChild = parent.Child;
            parent.Child = new();

            string message = new Random().NextString();
            oldChild.ChangeMessage(message);

            Assert.AreNotEqual(message, parent.Message);
        }
    }
}
