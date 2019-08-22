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
        /// Tests that <see cref="ChangableBaseClass"/> initializes without setting <see cref="ChangableBaseClass.IsChanged"/> to true.
        /// </summary>
        [TestMethod]
        public void Initializes()
        {
            Assert.IsFalse(new ChangableWithChildrenMock().IsChanged);
        }

        /// <summary>
        /// Tests that an implementation of <see cref="ChangableBaseClass"/> serializes and deserializes properly.
        /// </summary>
        [TestMethod]
        public void SerializesAndDeserializes()
        {
            SerializationAssert.SerializesAndDeserializes<ChangableWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="ChangableBaseClass"/> does not set <see cref="ChangableBaseClass.IsChanged"/> to true after being deserialized.
        /// </summary>
        [TestMethod]
        public void IsNotChangedAfterDeserialization()
        {
            SerializationAssert.IsNotChangedAfterDeserialized<ChangableWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="ChangableBaseClass"/> can change property values.
        /// </summary>
        [TestMethod]
        public void ChangesValues()
        {
            PropertyValueChangeAssert.ChangesValues<ChangableWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="ChangableBaseClass"/> raises <see cref="PropertyChangedEventHandler"/> when property values change.
        /// </summary>
        [TestMethod]
        public void NotifiesPropertiesChanged()
        {
            NotifyPropertyChangedAssert.NotifiesPropertiesChanged<ChangableWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="ChangableBaseClass"/> changes <see cref="ChangableBaseClass.IsChanged"/> to true when properties are changed.
        /// </summary>
        [TestMethod]
        public void ChangesOnPropertiesChanged()
        {
            ChangeTrackingAssert.IsChangedWhenChanged<ChangableWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="ChangableBaseClass"/> raises <see cref="OnChangedEventHandler"/> correctly when properties are changed.
        /// </summary>
        [TestMethod]
        public void NotifiesIsChangedWhenChanged()
        {
            ChangableAssert.NotifiesIsChangedWhenChanged<ChangableWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="ChangableBaseClass"/> sets <see cref="ChangableBaseClass.IsChanged"/> to false when executing <see cref="ChangableBaseClass.AcceptChanges"/>.
        /// </summary>
        [TestMethod]
        public void AcceptsChanges()
        {
            ChangeTrackingAssert.AcceptsChanges<ChangableWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="ChangableBaseClass"/> raises <see cref="OnChangedEventHandler"/> once when executing <see cref="ChangableBaseClass.AcceptChanges"/>.
        /// </summary>
        [TestMethod]
        public void NotifiesIsChangedOnAcceptChanges()
        {
            ChangableAssert.NotifiesIsChangedOnAcceptChanges<ChangableWithChildrenMock>();
        }
    }
}
