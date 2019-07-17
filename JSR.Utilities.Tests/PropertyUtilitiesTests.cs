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
        public void CheckIfPropertyHasPublicGetAndSetMethod()
        {
            Assert.IsTrue(PropertyUtilities.CheckIfPropertyHasPublicGetAndSetMethod<UtilityMock>(nameof(UtilityMock.PublicStringProperty)));
            Assert.IsFalse(PropertyUtilities.CheckIfPropertyHasPublicGetAndSetMethod<UtilityMock>(nameof(UtilityMock.ReadOnlyStringProperty)));
            Assert.IsFalse(PropertyUtilities.CheckIfPropertyHasPublicGetAndSetMethod<UtilityMock>(nameof(UtilityMock.WriteOnlyStringProperty)));

            UtilityMock obj = new UtilityMock();
            Assert.IsTrue(PropertyUtilities.CheckIfPropertyHasPublicGetAndSetMethod(nameof(obj.PublicIntProperty), obj));
            Assert.IsFalse(PropertyUtilities.CheckIfPropertyHasPublicGetAndSetMethod(nameof(obj.ReadOnlyStringProperty), obj));
            Assert.IsFalse(PropertyUtilities.CheckIfPropertyHasPublicGetAndSetMethod(nameof(obj.WriteOnlyStringProperty), obj));

            Assert.IsTrue(PropertyUtilities.CheckIfPropertyHasPublicGetAndSetMethod(nameof(UtilityMock.PublicIntProperty), typeof(UtilityMock)));
            Assert.IsFalse(PropertyUtilities.CheckIfPropertyHasPublicGetAndSetMethod(nameof(UtilityMock.ReadOnlyStringProperty), typeof(UtilityMock)));
            Assert.IsFalse(PropertyUtilities.CheckIfPropertyHasPublicGetAndSetMethod(nameof(UtilityMock.WriteOnlyStringProperty), typeof(UtilityMock)));

            Assert.IsTrue(PropertyUtilities.CheckIfPropertyHasPublicGetAndSetMethod(typeof(UtilityMock).GetProperty(nameof(UtilityMock.PublicStringProperty))));
            Assert.IsFalse(PropertyUtilities.CheckIfPropertyHasPublicGetAndSetMethod(typeof(UtilityMock).GetProperty(nameof(UtilityMock.ReadOnlyStringProperty))));
            Assert.IsFalse(PropertyUtilities.CheckIfPropertyHasPublicGetAndSetMethod(typeof(UtilityMock).GetProperty(nameof(UtilityMock.WriteOnlyStringProperty))));
        }

        /// <summary>
        /// Tests if the utility gets the public properties with get and set methods.
        /// </summary>
        [TestMethod]
        public void GetListOfPropertiesWithPubligGetAndSetMethods()
        {
            List<string> propertyNames = new List<string> { nameof(UtilityMock.PublicBoolProperty), nameof(UtilityMock.PublicIntProperty), nameof(UtilityMock.PublicStringProperty) };

            List<PropertyInfo> properties = PropertyUtilities.GetListOfPropertiesWithPublicGetAndSetMethods<UtilityMock>();

            foreach (string propertyName in propertyNames)
            {
                CollectionAssert.Contains(properties.Select(x => x.Name).ToList(), propertyName);
            }
        }
    }
}
