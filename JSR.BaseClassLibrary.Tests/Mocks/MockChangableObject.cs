// <copyright file="MockChangableObject.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace JSR.BaseClassLibrary.Tests.Mocks
{
    [DataContract]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Mock Object.")]
    public class MockChangableObject : ChangableObject
    {
        [DataMember]
        private string stringProperty;

        [DataMember]
        private int integerProperty;

        [DataMember]
        private DateTime dateTimeProperty;

        [DataMember]
        private double doubleProperty;

        [DataMember]
        private decimal decimalProperty;

        public string StringProperty { get => stringProperty; set => SetValue(value, ref stringProperty); }

        public int IntegerProperty { get => integerProperty; set => SetValue(value, ref integerProperty); }

        public DateTime DateTimeProperty { get => dateTimeProperty; set => SetValue(value, ref dateTimeProperty); }

        public double DoubleProperty { get => doubleProperty; set => SetValue(value, ref doubleProperty); }

        public decimal DecimalProperty { get => decimalProperty; set => SetValue(value, ref decimalProperty); }
    }
}
