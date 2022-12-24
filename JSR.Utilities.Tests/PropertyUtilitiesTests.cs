// <copyright file="PropertyUtilitiesTests.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using JSR.Utilities.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.Utilities.Tests
{
    [TestClass]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Unit test")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit test")]
    public class PropertyUtilitiesTests
    {
        #region EvaluatePropertyAccessability

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), true)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), true)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), true)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), true)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), true)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), true)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), true)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), true)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), true)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), true)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), true)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), true)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void IsReadWriteProperty_EvaluatesPropertyAccessability(string propertyName, bool expected)
        {
            Assert.AreEqual(expected, PropertyUtilities.IsReadWriteProperty(typeof(PropertyMock).GetProperty(propertyName)));
            Assert.AreEqual(expected, PropertyUtilities.IsReadWriteProperty(typeof(PropertyMock), propertyName));
            Assert.AreEqual(expected, PropertyUtilities.IsReadWriteProperty(new PropertyMock(), propertyName));
            Assert.AreEqual(expected, PropertyUtilities.IsReadWriteProperty<PropertyMock>(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), true)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), true)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), true)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), true)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), true)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), true)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), true)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), true)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), true)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void IsReadOnlyProperty_EvaluatesPropertyAccessability(string propertyName, bool expected)
        {
            Assert.AreEqual(expected, PropertyUtilities.IsReadOnlyProperty(typeof(PropertyMock).GetProperty(propertyName)));
            Assert.AreEqual(expected, PropertyUtilities.IsReadOnlyProperty(typeof(PropertyMock), propertyName));
            Assert.AreEqual(expected, PropertyUtilities.IsReadOnlyProperty(new PropertyMock(), propertyName));
            Assert.AreEqual(expected, PropertyUtilities.IsReadOnlyProperty<PropertyMock>(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), true)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), true)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), true)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), true)]
        public void IsWriteOnlyProperty_EvaluatesPropertyAccessability(string propertyName, bool expected)
        {
            Assert.AreEqual(expected, PropertyUtilities.IsWriteOnlyProperty(typeof(PropertyMock).GetProperty(propertyName)));
            Assert.AreEqual(expected, PropertyUtilities.IsWriteOnlyProperty(typeof(PropertyMock), propertyName));
            Assert.AreEqual(expected, PropertyUtilities.IsWriteOnlyProperty(new PropertyMock(), propertyName));
            Assert.AreEqual(expected, PropertyUtilities.IsWriteOnlyProperty<PropertyMock>(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), true)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), true)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), true)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void IsClassProperty_EvaluatesPropertyType(string propertyName, bool expected)
        {
            Assert.AreEqual(expected, PropertyUtilities.IsClassProperty(typeof(PropertyMock).GetProperty(propertyName)));
            Assert.AreEqual(expected, PropertyUtilities.IsClassProperty(typeof(PropertyMock), propertyName));
            Assert.AreEqual(expected, PropertyUtilities.IsClassProperty(new PropertyMock(), propertyName));
            Assert.AreEqual(expected, PropertyUtilities.IsClassProperty<PropertyMock>(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void IsInterfaceProperty_EvaluatesPropertyType(string propertyName, bool expected)
        {
            Assert.AreEqual(expected, PropertyUtilities.IsInterfaceProperty(typeof(PropertyMock).GetProperty(propertyName)));
            Assert.AreEqual(expected, PropertyUtilities.IsInterfaceProperty(typeof(PropertyMock), propertyName));
            Assert.AreEqual(expected, PropertyUtilities.IsInterfaceProperty(new PropertyMock(), propertyName));
            Assert.AreEqual(expected, PropertyUtilities.IsInterfaceProperty<PropertyMock>(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), true)]
        [DataRow(nameof(PropertyMock.ListReadOnly), true)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void IsListProperty_EvaluatesPropertyType(string propertyName, bool expected)
        {
            Assert.AreEqual(expected, PropertyUtilities.IsListProperty(typeof(PropertyMock).GetProperty(propertyName)));
            Assert.AreEqual(expected, PropertyUtilities.IsListProperty(typeof(PropertyMock), propertyName));
            Assert.AreEqual(expected, PropertyUtilities.IsListProperty(new PropertyMock(), propertyName));
            Assert.AreEqual(expected, PropertyUtilities.IsListProperty<PropertyMock>(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), true)]
        [DataRow(nameof(PropertyMock.StringReadOnly), true)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), true)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), true)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), true)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StructReadWrite), true)]
        [DataRow(nameof(PropertyMock.StructReadOnly), true)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), true)]
        [DataRow(nameof(PropertyMock.CharReadWrite), true)]
        [DataRow(nameof(PropertyMock.CharReadOnly), true)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), true)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), true)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), true)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), true)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), true)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), true)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), true)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.IntReadWrite), true)]
        [DataRow(nameof(PropertyMock.IntReadOnly), true)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), true)]
        public void IsValueProperty_EvaluatesPropertyType(string propertyName, bool expected)
        {
            Assert.AreEqual(expected, PropertyUtilities.IsValueProperty(typeof(PropertyMock).GetProperty(propertyName)));
            Assert.AreEqual(expected, PropertyUtilities.IsValueProperty(typeof(PropertyMock), propertyName));
            Assert.AreEqual(expected, PropertyUtilities.IsValueProperty(new PropertyMock(), propertyName));
            Assert.AreEqual(expected, PropertyUtilities.IsValueProperty<PropertyMock>(propertyName));
        }

        #endregion

        #region GetProperties

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite))]
        [DataRow(nameof(PropertyMock.ClassReadOnly))]
        [DataRow(nameof(PropertyMock.ClassWriteOnly))]
        [DataRow(nameof(PropertyMock.ObjectReadWrite))]
        [DataRow(nameof(PropertyMock.ObjectReadOnly))]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly))]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite))]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly))]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly))]
        [DataRow(nameof(PropertyMock.ListReadWrite))]
        [DataRow(nameof(PropertyMock.ListReadOnly))]
        [DataRow(nameof(PropertyMock.ListWriteOnly))]
        [DataRow(nameof(PropertyMock.StringReadWrite))]
        [DataRow(nameof(PropertyMock.StringReadOnly))]
        [DataRow(nameof(PropertyMock.StringWriteOnly))]
        [DataRow(nameof(PropertyMock.EnumReadWrite))]
        [DataRow(nameof(PropertyMock.EnumReadOnly))]
        [DataRow(nameof(PropertyMock.EnumWriteOnly))]
        [DataRow(nameof(PropertyMock.StructReadWrite))]
        [DataRow(nameof(PropertyMock.StructReadOnly))]
        [DataRow(nameof(PropertyMock.StructWriteOnly))]
        [DataRow(nameof(PropertyMock.CharReadWrite))]
        [DataRow(nameof(PropertyMock.CharReadOnly))]
        [DataRow(nameof(PropertyMock.CharWriteOnly))]
        [DataRow(nameof(PropertyMock.TupleReadWrite))]
        [DataRow(nameof(PropertyMock.TupleReadOnly))]
        [DataRow(nameof(PropertyMock.TupleWriteOnly))]
        [DataRow(nameof(PropertyMock.BoolReadWrite))]
        [DataRow(nameof(PropertyMock.BoolReadOnly))]
        [DataRow(nameof(PropertyMock.BoolWriteOnly))]
        [DataRow(nameof(PropertyMock.DecimalReadWrite))]
        [DataRow(nameof(PropertyMock.DecimalReadOnly))]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly))]
        [DataRow(nameof(PropertyMock.DoubleReadWrite))]
        [DataRow(nameof(PropertyMock.DoubleReadOnly))]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly))]
        [DataRow(nameof(PropertyMock.IntReadWrite))]
        [DataRow(nameof(PropertyMock.IntReadOnly))]
        [DataRow(nameof(PropertyMock.IntWriteOnly))]
        public void GetProperties_GetsAllProperties(string propertyName)
        {
            GetPropertiesOptions options = new(true);

            List<PropertyInfo> properties = PropertyUtilities.GetProperties(typeof(PropertyMock), options);
            Assert.IsTrue(properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetProperties(new PropertyMock(), options);
            Assert.IsTrue(properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetProperties<PropertyMock>(options);
            Assert.IsTrue(properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), true)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), true)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), true)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), true)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), true)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), true)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), true)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), true)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), true)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), true)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), true)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), true)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetProperties_Filters_ReadWriteProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(true)
            {
                ReadOnlyProperties = false,
                WriteOnlyProperties = false,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), true)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), true)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), true)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), true)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), true)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), true)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), true)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), true)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), true)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetProperties_Filters_ReadOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(true)
            {
                ReadWriteProperties = false,
                WriteOnlyProperties = false,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), true)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), true)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), true)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), true)]
        public void GetProperties_Filters_WriteOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(true)
            {
                ReadWriteProperties = false,
                ReadOnlyProperties = false,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), true)]
        [DataRow(nameof(PropertyMock.StringReadOnly), true)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), true)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), true)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), true)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StructReadWrite), true)]
        [DataRow(nameof(PropertyMock.StructReadOnly), true)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), true)]
        [DataRow(nameof(PropertyMock.CharReadWrite), true)]
        [DataRow(nameof(PropertyMock.CharReadOnly), true)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), true)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), true)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), true)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), true)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), true)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), true)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), true)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.IntReadWrite), true)]
        [DataRow(nameof(PropertyMock.IntReadOnly), true)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), true)]
        public void GetProperties_Filters_ValueTypeProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadWriteProperties = true,
                ReadOnlyProperties = true,
                WriteOnlyProperties = true,
                ValueProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), true)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), true)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), true)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetProperties_Filters_ClassProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadWriteProperties = true,
                ReadOnlyProperties = true,
                WriteOnlyProperties = true,
                ClassProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), true)]
        [DataRow(nameof(PropertyMock.ListReadOnly), true)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetProperties_Filters_ListProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadWriteProperties = true,
                ReadOnlyProperties = true,
                WriteOnlyProperties = true,
                ListProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetProperties_Filters_InterfaceProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadWriteProperties = true,
                ReadOnlyProperties = true,
                WriteOnlyProperties = true,
                InterfaceProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        #endregion

        #region GetPropertyNames

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite))]
        [DataRow(nameof(PropertyMock.ClassReadOnly))]
        [DataRow(nameof(PropertyMock.ClassWriteOnly))]
        [DataRow(nameof(PropertyMock.ObjectReadWrite))]
        [DataRow(nameof(PropertyMock.ObjectReadOnly))]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly))]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite))]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly))]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly))]
        [DataRow(nameof(PropertyMock.ListReadWrite))]
        [DataRow(nameof(PropertyMock.ListReadOnly))]
        [DataRow(nameof(PropertyMock.ListWriteOnly))]
        [DataRow(nameof(PropertyMock.StringReadWrite))]
        [DataRow(nameof(PropertyMock.StringReadOnly))]
        [DataRow(nameof(PropertyMock.StringWriteOnly))]
        [DataRow(nameof(PropertyMock.EnumReadWrite))]
        [DataRow(nameof(PropertyMock.EnumReadOnly))]
        [DataRow(nameof(PropertyMock.EnumWriteOnly))]
        [DataRow(nameof(PropertyMock.StructReadWrite))]
        [DataRow(nameof(PropertyMock.StructReadOnly))]
        [DataRow(nameof(PropertyMock.StructWriteOnly))]
        [DataRow(nameof(PropertyMock.CharReadWrite))]
        [DataRow(nameof(PropertyMock.CharReadOnly))]
        [DataRow(nameof(PropertyMock.CharWriteOnly))]
        [DataRow(nameof(PropertyMock.TupleReadWrite))]
        [DataRow(nameof(PropertyMock.TupleReadOnly))]
        [DataRow(nameof(PropertyMock.TupleWriteOnly))]
        [DataRow(nameof(PropertyMock.BoolReadWrite))]
        [DataRow(nameof(PropertyMock.BoolReadOnly))]
        [DataRow(nameof(PropertyMock.BoolWriteOnly))]
        [DataRow(nameof(PropertyMock.DecimalReadWrite))]
        [DataRow(nameof(PropertyMock.DecimalReadOnly))]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly))]
        [DataRow(nameof(PropertyMock.DoubleReadWrite))]
        [DataRow(nameof(PropertyMock.DoubleReadOnly))]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly))]
        [DataRow(nameof(PropertyMock.IntReadWrite))]
        [DataRow(nameof(PropertyMock.IntReadOnly))]
        [DataRow(nameof(PropertyMock.IntWriteOnly))]
        public void GetPropertyNames_GetsAllProperties(string propertyName)
        {
            GetPropertiesOptions options = new(true);

            List<string> properties = PropertyUtilities.GetPropertyNames(typeof(PropertyMock), options);
            Assert.IsTrue(properties.Contains(propertyName));

            properties = PropertyUtilities.GetPropertyNames(new PropertyMock(), options);
            Assert.IsTrue(properties.Contains(propertyName));

            properties = PropertyUtilities.GetPropertyNames<PropertyMock>(options);
            Assert.IsTrue(properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), true)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), true)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), true)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), true)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), true)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), true)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), true)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), true)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), true)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), true)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), true)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), true)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetPropertyNames_Filters_ReadWriteProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(true)
            {
                ReadOnlyProperties = false,
                WriteOnlyProperties = false,
            };

            List<string> properties = PropertyUtilities.GetPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), true)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), true)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), true)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), true)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), true)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), true)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), true)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), true)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), true)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetPropertyNames_Filters_ReadOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(true)
            {
                ReadWriteProperties = false,
                WriteOnlyProperties = false,
            };

            List<string> properties = PropertyUtilities.GetPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), true)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), true)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), true)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), true)]
        public void GetPropertyNames_Filters_WriteOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(true)
            {
                ReadWriteProperties = false,
                ReadOnlyProperties = false,
            };

            List<string> properties = PropertyUtilities.GetPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), true)]
        [DataRow(nameof(PropertyMock.StringReadOnly), true)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), true)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), true)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), true)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StructReadWrite), true)]
        [DataRow(nameof(PropertyMock.StructReadOnly), true)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), true)]
        [DataRow(nameof(PropertyMock.CharReadWrite), true)]
        [DataRow(nameof(PropertyMock.CharReadOnly), true)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), true)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), true)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), true)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), true)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), true)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), true)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), true)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.IntReadWrite), true)]
        [DataRow(nameof(PropertyMock.IntReadOnly), true)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), true)]
        public void GetPropertyNames_Filters_ValueTypeProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadWriteProperties = true,
                ReadOnlyProperties = true,
                WriteOnlyProperties = true,
                ValueProperties = true,
            };

            List<string> properties = PropertyUtilities.GetPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), true)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), true)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), true)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetPropertyNames_Filters_ClassProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadWriteProperties = true,
                ReadOnlyProperties = true,
                WriteOnlyProperties = true,
                ClassProperties = true,
            };

            List<string> properties = PropertyUtilities.GetPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), true)]
        [DataRow(nameof(PropertyMock.ListReadOnly), true)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetPropertyNames_Filters_ListProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadWriteProperties = true,
                ReadOnlyProperties = true,
                WriteOnlyProperties = true,
                ListProperties = true,
            };

            List<string> properties = PropertyUtilities.GetPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetPropertyNames_Filters_InterfaceProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadWriteProperties = true,
                ReadOnlyProperties = true,
                WriteOnlyProperties = true,
                InterfaceProperties = true,
            };

            List<string> properties = PropertyUtilities.GetPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        #endregion

        #region GetReadWriteProperties

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), true)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), true)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), true)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), true)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), true)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), true)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), true)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), true)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), true)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), true)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), true)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), true)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadWriteProperties_GetsAllReadWriteProperties(string propertyName, bool expected)
        {
            List<PropertyInfo> properties = PropertyUtilities.GetReadWriteProperties(typeof(PropertyMock));
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadWriteProperties(new PropertyMock());
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadWriteProperties<PropertyMock>();
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), true)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), true)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadWriteProprerties_FiltersClassProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ClassProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetReadWriteProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadWriteProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadWriteProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadWriteProprerties_FiltersInterfaceProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                InterfaceProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetReadWriteProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadWriteProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadWriteProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), true)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadWriteProprerties_FiltersListProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ListProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetReadWriteProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadWriteProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadWriteProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), true)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), true)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), true)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), true)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), true)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), true)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), true)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), true)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), true)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadWriteProprerties_FiltersValueProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ValueProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetReadWriteProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadWriteProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadWriteProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        #endregion

        #region GetReadWritePropertyNames

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), true)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), true)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), true)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), true)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), true)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), true)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), true)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), true)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), true)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), true)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), true)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), true)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadWritePropertyNames_GetsAllReadWriteProperties(string propertyName, bool expected)
        {
            List<string> properties = PropertyUtilities.GetReadWritePropertyNames(typeof(PropertyMock));
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadWritePropertyNames(new PropertyMock());
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadWritePropertyNames<PropertyMock>();
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), true)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), true)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadWriteProprertyNames_FiltersClassProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ClassProperties = true,
            };

            List<string> properties = PropertyUtilities.GetReadWritePropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadWritePropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadWritePropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadWriteProprertyNames_FiltersInterfaceProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                InterfaceProperties = true,
            };

            List<string> properties = PropertyUtilities.GetReadWritePropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadWritePropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadWritePropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), true)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadWriteProprertyNames_FiltersListProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ListProperties = true,
            };

            List<string> properties = PropertyUtilities.GetReadWritePropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadWritePropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadWritePropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), true)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), true)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), true)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), true)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), true)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), true)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), true)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), true)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), true)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadWriteProprertyNames_FiltersValueProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ValueProperties = true,
            };

            List<string> properties = PropertyUtilities.GetReadWritePropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadWritePropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadWritePropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        #endregion

        #region GetReadOnlyProperties

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), true)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), true)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), true)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), true)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), true)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), true)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), true)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), true)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), true)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadOnlyProperties_GetsAllReadWriteProperties(string propertyName, bool expected)
        {
            List<PropertyInfo> properties = PropertyUtilities.GetReadOnlyProperties(typeof(PropertyMock));
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadOnlyProperties(new PropertyMock());
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadOnlyProperties<PropertyMock>();
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), true)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadOnlyProprerties_FiltersClassProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ClassProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetReadOnlyProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadOnlyProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadOnlyProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadOnlyProprerties_FiltersInterfaceProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                InterfaceProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetReadOnlyProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadOnlyProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadOnlyProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), true)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadOnlyProprerties_FiltersListProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ListProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetReadOnlyProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadOnlyProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadOnlyProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), true)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), true)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), true)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), true)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), true)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), true)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), true)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadOnlyProprerties_FiltersValueProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ValueProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetReadOnlyProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadOnlyProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetReadOnlyProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        #endregion

        #region GetReadOnlyPropertyNames

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), true)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), true)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), true)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), true)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), true)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), true)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), true)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), true)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), true)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadOnlyPropertyNames_GetsAllReadWriteProperties(string propertyName, bool expected)
        {
            List<string> properties = PropertyUtilities.GetReadOnlyPropertyNames(typeof(PropertyMock));
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadOnlyPropertyNames(new PropertyMock());
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadOnlyPropertyNames<PropertyMock>();
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), true)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadOnlyProprertyNames_FiltersClassProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ClassProperties = true,
            };

            List<string> properties = PropertyUtilities.GetReadOnlyPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadOnlyPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadOnlyPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadOnlyProprertyNames_FiltersInterfaceProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                InterfaceProperties = true,
            };

            List<string> properties = PropertyUtilities.GetReadOnlyPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadOnlyPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadOnlyPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), true)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadOnlyProprertyNames_FiltersListProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ListProperties = true,
            };

            List<string> properties = PropertyUtilities.GetReadOnlyPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadOnlyPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadOnlyPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), true)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), true)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), true)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), true)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), true)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), true)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), true)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetReadOnlyProprertyNames_FiltersValueProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ValueProperties = true,
            };

            List<string> properties = PropertyUtilities.GetReadOnlyPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadOnlyPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetReadOnlyPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        #endregion

        #region GetWriteOnlyProperties

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), true)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), true)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), true)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), true)]
        public void GetWriteOnlyProperties_GetsAllReadWriteProperties(string propertyName, bool expected)
        {
            List<PropertyInfo> properties = PropertyUtilities.GetWriteOnlyProperties(typeof(PropertyMock));
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetWriteOnlyProperties(new PropertyMock());
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetWriteOnlyProperties<PropertyMock>();
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetWriteOnlyProprerties_FiltersClassProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ClassProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetWriteOnlyProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetWriteOnlyProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetWriteOnlyProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetWriteOnlyProprerties_FiltersInterfaceProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                InterfaceProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetWriteOnlyProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetWriteOnlyProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetWriteOnlyProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetWriteOnlyProprerties_FiltersListProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ListProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetWriteOnlyProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetWriteOnlyProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetWriteOnlyProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), true)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), true)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), true)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), true)]
        public void GetWriteOnlyProprerties_FiltersValueProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ValueProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetWriteOnlyProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetWriteOnlyProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetWriteOnlyProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        #endregion

        #region GetWriteOnlyPropertyNames

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), true)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), true)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), true)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), true)]
        public void GetWriteOnlyPropertyNames_GetsAllReadWriteProperties(string propertyName, bool expected)
        {
            List<string> properties = PropertyUtilities.GetWriteOnlyPropertyNames(typeof(PropertyMock));
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetWriteOnlyPropertyNames(new PropertyMock());
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetWriteOnlyPropertyNames<PropertyMock>();
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetWriteOnlyProprertyNames_FiltersClassProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ClassProperties = true,
            };

            List<string> properties = PropertyUtilities.GetWriteOnlyPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetWriteOnlyPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetWriteOnlyPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetWriteOnlyProprertyNames_FiltersInterfaceProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                InterfaceProperties = true,
            };

            List<string> properties = PropertyUtilities.GetWriteOnlyPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetWriteOnlyPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetWriteOnlyPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetWriteOnlyProprertyNames_FiltersListProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ListProperties = true,
            };

            List<string> properties = PropertyUtilities.GetWriteOnlyPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetWriteOnlyPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetWriteOnlyPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), true)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), true)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), true)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), true)]
        public void GetWriteOnlyProprertyNames_FiltersValueProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ValueProperties = true,
            };

            List<string> properties = PropertyUtilities.GetWriteOnlyPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetWriteOnlyPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetWriteOnlyPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        #endregion

        #region GetClassProperties

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), true)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), true)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), true)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetClassProperties_GetsAllClassProperties(string propertyName, bool expected)
        {
            List<PropertyInfo> properties = PropertyUtilities.GetClassProperties(typeof(PropertyMock));
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetClassProperties(new PropertyMock());
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetClassProperties<PropertyMock>();
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), true)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), true)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetClassProprerties_FiltersReadWriteProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadWriteProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetClassProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetClassProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetClassProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), true)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetClassProprerties_FiltersReadOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadOnlyProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetClassProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetClassProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetClassProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetClassProprerties_FiltersWriteOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                WriteOnlyProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetClassProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetClassProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetClassProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        #endregion

        #region GetClassPropertyNames

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), true)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), true)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), true)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetClassPropertyNames_GetsAllClassProperties(string propertyName, bool expected)
        {
            List<string> properties = PropertyUtilities.GetClassPropertyNames(typeof(PropertyMock));
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetClassPropertyNames(new PropertyMock());
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetClassPropertyNames<PropertyMock>();
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), true)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), true)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetClassProprertyNames_FiltersReadWriteProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadWriteProperties = true,
            };

            List<string> properties = PropertyUtilities.GetClassPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetClassPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetClassPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), true)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetClassProprertyNames_FiltersReadOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadOnlyProperties = true,
            };

            List<string> properties = PropertyUtilities.GetClassPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetClassPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetClassPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetClassProprertyNames_FiltersWriteOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                WriteOnlyProperties = true,
            };

            List<string> properties = PropertyUtilities.GetClassPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetClassPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetClassPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        #endregion

        #region GetInterfaceProperties

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetInterfaceProperties_GetsAllInterfaceProperties(string propertyName, bool expected)
        {
            List<PropertyInfo> properties = PropertyUtilities.GetInterfaceProperties(typeof(PropertyMock));
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetInterfaceProperties(new PropertyMock());
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetInterfaceProperties<PropertyMock>();
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetInterfaceProprerties_FiltersReadWriteProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadWriteProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetInterfaceProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetInterfaceProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetInterfaceProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetInterfaceProprerties_FiltersReadOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadOnlyProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetInterfaceProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetInterfaceProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetInterfaceProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetInterfaceProprerties_FiltersWriteOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                WriteOnlyProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetInterfaceProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetInterfaceProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetInterfaceProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        #endregion

        #region GetInterfacePropertyNames

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetInterfacePropertyNames_GetsAllInterfaceProperties(string propertyName, bool expected)
        {
            List<string> properties = PropertyUtilities.GetInterfacePropertyNames(typeof(PropertyMock));
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetInterfacePropertyNames(new PropertyMock());
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetInterfacePropertyNames<PropertyMock>();
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), true)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetInterfaceProprertyNames_FiltersReadWriteProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadWriteProperties = true,
            };

            List<string> properties = PropertyUtilities.GetInterfacePropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetInterfacePropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetInterfacePropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), true)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetInterfaceProprertyNames_FiltersReadOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadOnlyProperties = true,
            };

            List<string> properties = PropertyUtilities.GetInterfacePropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetInterfacePropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetInterfacePropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), true)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetInterfaceProprertyNames_FiltersWriteOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                WriteOnlyProperties = true,
            };

            List<string> properties = PropertyUtilities.GetInterfacePropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetInterfacePropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetInterfacePropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        #endregion

        #region GetListProperties

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), true)]
        [DataRow(nameof(PropertyMock.ListReadOnly), true)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetListProperties_GetsAllListProperties(string propertyName, bool expected)
        {
            List<PropertyInfo> properties = PropertyUtilities.GetListProperties(typeof(PropertyMock));
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetListProperties(new PropertyMock());
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetListProperties<PropertyMock>();
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), true)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetListProprerties_FiltersReadWriteProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadWriteProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetListProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetListProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetListProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), true)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetListProprerties_FiltersReadOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadOnlyProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetListProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetListProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetListProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetListProprerties_FiltersWriteOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                WriteOnlyProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetListProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetListProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetListProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        #endregion

        #region GetListPropertyNames

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), true)]
        [DataRow(nameof(PropertyMock.ListReadOnly), true)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetListPropertyNames_GetsAllListProperties(string propertyName, bool expected)
        {
            List<string> properties = PropertyUtilities.GetListPropertyNames(typeof(PropertyMock));
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetListPropertyNames(new PropertyMock());
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetListPropertyNames<PropertyMock>();
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), true)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetListProprertyNames_FiltersReadWriteProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadWriteProperties = true,
            };

            List<string> properties = PropertyUtilities.GetListPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetListPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetListPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), true)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetListProprertyNames_FiltersReadOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadOnlyProperties = true,
            };

            List<string> properties = PropertyUtilities.GetListPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetListPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetListPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetListProprertyNames_FiltersWriteOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                WriteOnlyProperties = true,
            };

            List<string> properties = PropertyUtilities.GetListPropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetListPropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetListPropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        #endregion

        #region GetValueProperties

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), true)]
        [DataRow(nameof(PropertyMock.StringReadOnly), true)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), true)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), true)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), true)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StructReadWrite), true)]
        [DataRow(nameof(PropertyMock.StructReadOnly), true)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), true)]
        [DataRow(nameof(PropertyMock.CharReadWrite), true)]
        [DataRow(nameof(PropertyMock.CharReadOnly), true)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), true)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), true)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), true)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), true)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), true)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), true)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), true)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.IntReadWrite), true)]
        [DataRow(nameof(PropertyMock.IntReadOnly), true)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), true)]
        public void GetValueTypeProperties_GetsAllValueTypeProperties(string propertyName, bool expected)
        {
            List<PropertyInfo> properties = PropertyUtilities.GetValueTypeProperties(typeof(PropertyMock));
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetValueTypeProperties(new PropertyMock());
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetValueTypeProperties<PropertyMock>();
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), true)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), true)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), true)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), true)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), true)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), true)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), true)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), true)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), true)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetValutTypeProprerties_FiltersReadWriteProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadWriteProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetValueTypeProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetValueTypeProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetValueTypeProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), true)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), true)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), true)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), true)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), true)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), true)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), true)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetValueTypeProprerties_FiltersReadOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadOnlyProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetValueTypeProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetValueTypeProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetValueTypeProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), true)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), true)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), true)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), true)]
        public void GetValueTypeProprerties_FiltersWriteOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                WriteOnlyProperties = true,
            };

            List<PropertyInfo> properties = PropertyUtilities.GetValueTypeProperties(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetValueTypeProperties(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));

            properties = PropertyUtilities.GetValueTypeProperties<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Any(p => p.Name == propertyName));
        }

        #endregion

        #region GetValueProperties

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), true)]
        [DataRow(nameof(PropertyMock.StringReadOnly), true)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), true)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), true)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), true)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StructReadWrite), true)]
        [DataRow(nameof(PropertyMock.StructReadOnly), true)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), true)]
        [DataRow(nameof(PropertyMock.CharReadWrite), true)]
        [DataRow(nameof(PropertyMock.CharReadOnly), true)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), true)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), true)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), true)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), true)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), true)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), true)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), true)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.IntReadWrite), true)]
        [DataRow(nameof(PropertyMock.IntReadOnly), true)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), true)]
        public void GetValueTypePropertyNames_GetsAllValueTypeProperties(string propertyName, bool expected)
        {
            List<string> properties = PropertyUtilities.GetValueTypePropertyNames(typeof(PropertyMock));
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetValueTypePropertyNames(new PropertyMock());
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetValueTypePropertyNames<PropertyMock>();
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), true)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), true)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), true)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), true)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), true)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), true)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), true)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), true)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), true)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetValutTypeProprertyNames_FiltersReadWriteProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadWriteProperties = true,
            };

            List<string> properties = PropertyUtilities.GetValueTypePropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetValueTypePropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetValueTypePropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), true)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), false)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), true)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), true)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), false)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), true)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), false)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), true)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), true)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), false)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), true)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), false)]
        public void GetValueTypeProprertyNames_FiltersReadOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                ReadOnlyProperties = true,
            };

            List<string> properties = PropertyUtilities.GetValueTypePropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetValueTypePropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetValueTypePropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        [TestMethod]
        [DataRow(nameof(PropertyMock.ClassReadWrite), false)]
        [DataRow(nameof(PropertyMock.ClassReadOnly), false)]
        [DataRow(nameof(PropertyMock.ClassWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectReadWrite), false)]
        [DataRow(nameof(PropertyMock.ObjectReadOnly), false)]
        [DataRow(nameof(PropertyMock.ObjectWriteOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadWrite), false)]
        [DataRow(nameof(PropertyMock.InterfaceReadOnly), false)]
        [DataRow(nameof(PropertyMock.InterfaceWriteOnly), false)]
        [DataRow(nameof(PropertyMock.ListReadWrite), false)]
        [DataRow(nameof(PropertyMock.ListReadOnly), false)]
        [DataRow(nameof(PropertyMock.ListWriteOnly), false)]
        [DataRow(nameof(PropertyMock.StringReadWrite), false)]
        [DataRow(nameof(PropertyMock.StringReadOnly), false)]
        [DataRow(nameof(PropertyMock.StringWriteOnly), true)]
        [DataRow(nameof(PropertyMock.EnumReadWrite), false)]
        [DataRow(nameof(PropertyMock.EnumReadOnly), false)]
        [DataRow(nameof(PropertyMock.EnumWriteOnly), true)]
        [DataRow(nameof(PropertyMock.StructReadWrite), false)]
        [DataRow(nameof(PropertyMock.StructReadOnly), false)]
        [DataRow(nameof(PropertyMock.StructWriteOnly), true)]
        [DataRow(nameof(PropertyMock.CharReadWrite), false)]
        [DataRow(nameof(PropertyMock.CharReadOnly), false)]
        [DataRow(nameof(PropertyMock.CharWriteOnly), true)]
        [DataRow(nameof(PropertyMock.TupleReadWrite), false)]
        [DataRow(nameof(PropertyMock.TupleReadOnly), false)]
        [DataRow(nameof(PropertyMock.TupleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.BoolReadWrite), false)]
        [DataRow(nameof(PropertyMock.BoolReadOnly), false)]
        [DataRow(nameof(PropertyMock.BoolWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DecimalReadWrite), false)]
        [DataRow(nameof(PropertyMock.DecimalReadOnly), false)]
        [DataRow(nameof(PropertyMock.DecimalWriteOnly), true)]
        [DataRow(nameof(PropertyMock.DoubleReadWrite), false)]
        [DataRow(nameof(PropertyMock.DoubleReadOnly), false)]
        [DataRow(nameof(PropertyMock.DoubleWriteOnly), true)]
        [DataRow(nameof(PropertyMock.IntReadWrite), false)]
        [DataRow(nameof(PropertyMock.IntReadOnly), false)]
        [DataRow(nameof(PropertyMock.IntWriteOnly), true)]
        public void GetValueTypeProprertyNames_FiltersWriteOnlyProperties(string propertyName, bool expected)
        {
            GetPropertiesOptions options = new(false)
            {
                WriteOnlyProperties = true,
            };

            List<string> properties = PropertyUtilities.GetValueTypePropertyNames(typeof(PropertyMock), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetValueTypePropertyNames(new PropertyMock(), options);
            Assert.AreEqual(expected, properties.Contains(propertyName));

            properties = PropertyUtilities.GetValueTypePropertyNames<PropertyMock>(options);
            Assert.AreEqual(expected, properties.Contains(propertyName));
        }

        #endregion
    }
}
