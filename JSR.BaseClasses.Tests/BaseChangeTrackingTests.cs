// <copyright file="BaseChangeTrackingTests.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using JSR.Asserts;
using JSR.BaseClasses.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.BaseClasses.Tests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Unit tests")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit tests")]
    public class BaseChangeTrackingTests
    {
        [TestMethod]
        public void IsChanged_IsTrue_WhenCreated()
        {
            Assert.That.IsChangedWhenCreated<MockBaseChangeTracking>();
        }

        [TestMethod]
        public void AcceptChanges_AcceptsChanges()
        {
            Assert.That.AcceptsChanges<MockBaseChangeTracking>();
        }

        [TestMethod]
        [DataRow(nameof(MockBaseChangeTracking.IntegerProperty))]
        [DataRow(nameof(MockBaseChangeTracking.StringProperty))]
        public void IsChanged_WhenPropertyChanges(string propertyName)
        {
            Assert.That.IsChangedWhenPropertyChanges<MockBaseChangeTracking>(propertyName);
        }
    }
}
