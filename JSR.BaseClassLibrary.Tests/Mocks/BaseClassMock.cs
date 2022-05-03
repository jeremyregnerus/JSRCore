// <copyright file="BaseClassMock.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using JSR.Utilities;

namespace JSR.BaseClassLibrary.Tests.Mocks
{
    [DataContract]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Mock Class.")]
    public class BaseClassMock : BaseClass
    {
        [DataMember]
        private string stringProperty;

        [DataMember]
        private int integerProperty;

        [DataMember]
        private DateTime dateTimeProperty;

        [DataMember]
        private double doubleProperty;

        public string StringProperty { get => stringProperty; set => SetValue(ref stringProperty, value); }

        public string StringReadOnlyProperty { get; } = RandomUtilities.GetRandomString();

        public int IntegerProperty { get => integerProperty; set => SetValue(ref integerProperty, value); }

        public int IntegerReadOnlyProperty { get; } = RandomUtilities.GetRandomInteger();

        public DateTime DateTimeProperty { get => dateTimeProperty; set => SetValue(ref dateTimeProperty, value); }

        public DateTime DateTimeReadOnlyProperty { get; } = RandomUtilities.GetRandomDateTime();

        public double DoubleProperty { get => doubleProperty; set => SetValue(ref doubleProperty, value); }

        public double DoubleReadOnlyProperty { get; } = RandomUtilities.GetRandomDouble();

        public void ChangeMessage()
        {
            ChangeMessage(RandomUtilities.GetRandomString(Message));
        }

        public void ChangeMessage(string message)
        {
            Message = message;
        }
    }
}
