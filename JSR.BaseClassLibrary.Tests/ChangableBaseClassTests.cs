// <copyright file="ChangableBaseClassTests.cs" company="Jeremy Regnerus">
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
    /// <summary>
    /// Tests the <see cref="IChangable"/> and <see cref="IChangableCollection{T}"/> interface implementations.
    /// </summary>
    [TestClass]
    public class ChangableBaseClassTests
    {
        /// <summary>
        /// Tests that <see cref="IChangable"/> initializes without setting is changed.
        /// </summary>
        [TestMethod]
        public void Initializes()
        {
            Assert.IsFalse(new ChangableWithChildrenMock().IsChanged);
        }

        /// <summary>
        /// Tests that an implementation of <see cref="IChangable"/> can serialize and deserialize.
        /// </summary>
        [TestMethod]
        public void SerializesAndDeserializes()
        {
            SerializationAssert.SerializesAndDeserializes<ChangableWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="IChangable"/> is not changed after deserialized.
        /// </summary>
        [TestMethod]
        public void IsNotChangedAfterDeserialization()
        {
            SerializationAssert.IsNotChangedAfterDeserialized<ChangableWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="IChangable"/> can change values.
        /// </summary>
        [TestMethod]
        public void ChangesValues()
        {
            PropertyValueChangeAssert.ChangesValues<ChangableWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="IChangable"/> notifies properties changed.
        /// </summary>
        [TestMethod]
        public void NotifiesPropertiesChanged()
        {
            NotifyPropertyChangedAssert.NotifiesPropertiesChanged<ChangableWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="IChangable"/> is changed when the properties, classes and lists within it are changed.
        /// </summary>
        [TestMethod]
        public void ChangesOnPropertiesChanged()
        {
            ChangeTrackingAssert.IsChangedWhenChanged<ChangableWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="IChangable"/> notifies when changes occur to properties, classes and lists within the object change.
        /// </summary>
        [TestMethod]
        public void NotifiesIsChanged()
        {
            ChangableAssert.NotifiesIsChangedWhenChanged<ChangableWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="IChangable"/> properly implements AcceptChanges.
        /// </summary>
        [TestMethod]
        public void AcceptsChanges()
        {
            ChangeTrackingAssert.AcceptsChanges<ChangableWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="IChangable"/> notifies IsChanged once when accepting changes.
        /// </summary>
        [TestMethod]
        public void NotifiesIsChangedOnAcceptChanges()
        {
            ChangableAssert.NotifiesIsChangedOnAcceptChanges<ChangableWithChildrenMock>();
        }
    }
}
