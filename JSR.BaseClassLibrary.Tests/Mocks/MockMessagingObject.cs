// <copyright file="MockMessagingObject.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace JSR.BaseClassLibrary.Tests.Mocks
{
    [DataContract]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Mock Object.")]
    internal class MockMessagingObject : MessagingObject
    {
        [DataMember]
        private MockMessagingObject child1;

        [DataMember]
        private MockMessagingObject child2;

        public MockMessagingObject Child1 { get => child1; set => SetValue(value, ref child1); }

        public MockMessagingObject Child2 { get => child2; set => SetValue(value, ref child2); }

        public void RaiseMessage(string message)
        {
            Message = message;
        }
    }
}
