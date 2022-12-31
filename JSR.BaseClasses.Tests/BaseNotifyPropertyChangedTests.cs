// <copyright file="BaseNotifyPropertyChangedTests.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using JSR.Asserts;
using JSR.BaseClasses.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.BaseClasses.Tests
{
    [TestClass]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Unit tests.")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit tests")]
    public class BaseNotifyPropertyChangedTests
    {
        [TestMethod]
        [DataRow(nameof(MockBaseNotifyPropertyChanged.StringProperty))]
        [DataRow(nameof(MockBaseNotifyPropertyChanged.IntegerProperty))]
        public void SetValue_ChangesPropertyValues(string propertyName)
        {
            Assert.That.PropertyChangesValue<MockBaseNotifyPropertyChanged>(propertyName);
        }

        [TestMethod]
        [DataRow(nameof(MockBaseNotifyPropertyChanged.StringProperty))]
        [DataRow(nameof(MockBaseNotifyPropertyChanged.IntegerProperty))]
        public void NotifiesPropertyChangedWhenChanged(string propertyName)
        {
            Assert.That.NotifiesPropertyChanged<MockBaseNotifyPropertyChanged>(propertyName);
        }
    }
}
