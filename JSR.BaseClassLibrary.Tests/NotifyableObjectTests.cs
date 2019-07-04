// <copyright file="NotifyableObjectTests.cs" company="Jeremy Regnerus">
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
    [TestClass]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Unit Test.")]
    public class NotifyableObjectTests
    {
        [TestMethod]
        public void SerializesAndDeserializes()
        {
            SerializationAssert.SerializesAndDeserializes<MockNotifyableObject>();
        }

        [TestMethod]
        public void NotifiesPropertyChanges()
        {
            PropertyChangeAssert.NotifiesEachPropertyChanges<MockNotifyableObject>();
        }

        [TestMethod]
        public void UpdatesPropertyValues()
        {
            PropertyChangeAssert.ChangesValues<MockNotifyableObject>();
        }

        [TestMethod]
        public void DoesNotNotifyOnSameValues()
        {
            MockNotifyableObject obj = ObjectUtilities.CreateInstanceWithRandomValues<MockNotifyableObject>();
            MockNotifyableObject objCopy = ObjectUtilities.GetSerializedCopyOfObject(obj);

            List<string> propertiesChanged = new List<string>();
            obj.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            int count = new Random().Next(5, 20);

            for (int i = 0; i < count; i++)
            {
                obj.DateTimeProperty = objCopy.DateTimeProperty;
                obj.IntegerProperty = objCopy.IntegerProperty;
                obj.StringProperty = objCopy.StringProperty;

                Assert.AreEqual(0, propertiesChanged.Count);

                ObjectUtilities.PopulateObjectWithRandomValues(obj);
                objCopy = ObjectUtilities.GetSerializedCopyOfObject(obj);

                propertiesChanged.Clear();
            }
        }
    }
}
