// <copyright file="MessengerWithChildrenMock.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace JSR.BaseClassLibrary.Tests.Mocks
{
    [DataContract]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Mock Object.")]
    public class MessengerWithChildrenMock : MessengerMock
    {
        [DataMember]
        private MessengerMock childMessenger1;

        [DataMember]
        private MessengerMock childMessenger2;

        public MessengerWithChildrenMock()
        {
            AddMessaging(MessengerList);
        }

        public MessengerMock ChildMessenger1 { get => childMessenger1; set => SetValue(value, ref childMessenger1); }

        public MessengerMock ChildMessenger2 { get => childMessenger2; set => SetValue(value, ref childMessenger2); }

        public MessengerCollection<MessengerMock> MessengerList { get; } = new MessengerCollection<MessengerMock>();
    }
}
