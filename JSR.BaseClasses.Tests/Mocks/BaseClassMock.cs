// <copyright file="BaseClassMock.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using JSR.Utilities;

namespace JSR.BaseClasses.Tests.Mocks
{
    [DataContract]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Mock Class.")]
    public class BaseClassMock : BaseClass
    {
        [DataMember]
        private string stringProperty = string.Empty;

        [DataMember]
        private string readOnlyStringProperty = new Random().NextString(8);

        [DataMember]
        private int integerProperty;

        [DataMember]
        private int readOnlyIntegerProperty = new Random().Next();

        [DataMember]
        private DateTime dateTimeProperty;

        [DataMember]
        private DateTime readOnlyDateTimeProperty = new Random().NextDateTime();

        [DataMember]
        private double doubleProperty;

        [DataMember]
        private double readOnlyDoubleProperty = new Random().NextDouble();

        public string StringProperty { get => stringProperty; set => SetValue(ref stringProperty, value); }

        public string StringReadOnlyProperty { get => readOnlyStringProperty; }

        public int IntegerProperty { get => integerProperty; set => SetValue(ref integerProperty, value); }

        public int IntegerReadOnlyProperty { get => readOnlyIntegerProperty; }

        public DateTime DateTimeProperty { get => dateTimeProperty; set => SetValue(ref dateTimeProperty, value); }

        public DateTime DateTimeReadOnlyProperty { get => readOnlyDateTimeProperty; }

        public double DoubleProperty { get => doubleProperty; set => SetValue(ref doubleProperty, value); }

        public double DoubleReadOnlyProperty { get => readOnlyDoubleProperty; }

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
