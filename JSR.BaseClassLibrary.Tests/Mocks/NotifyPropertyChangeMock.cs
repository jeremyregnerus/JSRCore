// <copyright file="NotifyPropertyChangeMock.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using JSR.Utilities;

namespace JSR.BaseClassLibrary.Tests.Mocks
{
    [DataContract]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Mock Object.")]
    public class NotifyPropertyChangeMock : NotifyPropertyChangeBaseClass
    {
        [DataMember]
        private string stringProperty;

        [DataMember]
        private int integerProperty;

        [DataMember]
        private DateTime dateTimeProperty;

        [DataMember]
        private double doubleProperty;

        public string StringProperty { get => stringProperty; set => SetValue(value, ref stringProperty); }

        public string StringReadOnlyProperty { get; } = RandomUtilities.GetRandomString();

        public int IntegerProperty { get => integerProperty; set => SetValue(value, ref integerProperty); }

        public int IntegerReadOnlyProperty { get; } = RandomUtilities.GetRandomInteger();

        public DateTime DateTimeProperty { get => dateTimeProperty; set => SetValue(value, ref dateTimeProperty); }

        public DateTime DateTimeReadOnlyProperty { get; } = RandomUtilities.GetRandomDateTime();

        public double DoubleProperty { get => doubleProperty; set => SetValue(value, ref doubleProperty); }

        public double DoubleReadOnlyProperty { get; } = RandomUtilities.GetRandomDouble();
    }
}
