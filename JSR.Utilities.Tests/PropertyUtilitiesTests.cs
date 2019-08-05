// <copyright file="PropertyUtilitiesTests.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.Utilities.Tests
{
    /// <summary>
    /// Tests for the PropertyUtilities Class.
    /// </summary>
    [TestClass]
    public class PropertyUtilitiesTests
    {
        /// <summary>
        /// Tests if the utility validates a property has public get and set methods.
        /// </summary>
        [TestMethod]
        public void ChecksPropertyAccessability()
        {
            Assert.IsTrue(PropertyUtilities.CheckIfPropertyIsReadWrite<UtilityMock>(nameof(UtilityMock.PublicStringProperty)));
            Assert.IsTrue(PropertyUtilities.CheckIfPropertyIsReadOnly<UtilityMock>(nameof(UtilityMock.ReadOnlyStringProperty)));
            Assert.IsTrue(PropertyUtilities.CheckIfPropertyIsWriteOnly<UtilityMock>(nameof(UtilityMock.WriteOnlyStringProperty)));

            UtilityMock obj = new UtilityMock();

            Assert.IsTrue(PropertyUtilities.CheckIfPropertyIsReadWrite(obj, nameof(obj.PublicStringProperty)));
            Assert.IsTrue(PropertyUtilities.CheckIfPropertyIsReadOnly(obj, nameof(obj.ReadOnlyStringProperty)));
            Assert.IsTrue(PropertyUtilities.CheckIfPropertyIsWriteOnly(obj, nameof(obj.WriteOnlyStringProperty)));

            Assert.IsTrue(PropertyUtilities.CheckIfPropertyIsReadWrite(typeof(UtilityMock), nameof(UtilityMock.PublicStringProperty)));
            Assert.IsTrue(PropertyUtilities.CheckIfPropertyIsReadOnly(typeof(UtilityMock), nameof(UtilityMock.ReadOnlyStringProperty)));
            Assert.IsTrue(PropertyUtilities.CheckIfPropertyIsWriteOnly(typeof(UtilityMock), nameof(UtilityMock.WriteOnlyStringProperty)));

            Assert.IsTrue(PropertyUtilities.CheckIfPropertyIsReadWrite(typeof(UtilityMock).GetProperty(nameof(UtilityMock.PublicStringProperty))));
            Assert.IsTrue(PropertyUtilities.CheckIfPropertyIsReadOnly(typeof(UtilityMock).GetProperty(nameof(UtilityMock.ReadOnlyStringProperty))));
            Assert.IsTrue(PropertyUtilities.CheckIfPropertyIsWriteOnly(typeof(UtilityMock).GetProperty(nameof(UtilityMock.WriteOnlyStringProperty))));
        }
    }
}
