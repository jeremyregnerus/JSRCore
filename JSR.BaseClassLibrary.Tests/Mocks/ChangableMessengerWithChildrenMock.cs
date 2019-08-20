// <copyright file="ChangableMessengerWithChildrenMock.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text;

namespace JSR.BaseClassLibrary.Tests.Mocks
{
    [DataContract]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Mock Object.")]
    public class ChangableMessengerWithChildrenMock : ChangableMessengerBaseClass
    {
        [DataMember]
        private readonly ChangableMessengerMock childReadOnly = new ChangableMessengerMock();

        [DataMember]
        private readonly ChangableMessengerCollection<ChangableMessengerMock> childCollectionReadOnly = new ChangableMessengerCollection<ChangableMessengerMock>();

        [DataMember]
        private ChangableMessengerMock child = new ChangableMessengerMock();

        [DataMember]
        private ChangableMessengerCollection<ChangableMessengerMock> childCollection = new ChangableMessengerCollection<ChangableMessengerMock>();

        public ChangableMessengerWithChildrenMock()
        {
            OnCreated();
        }

        public ChangableMessengerMock Child { get => child; set => SetValue(value, ref child); }

        public ChangableMessengerMock ChildReadOnly { get => childReadOnly; }

        public ChangableMessengerCollection<ChangableMessengerMock> ChildCollection { get => childCollection; set => SetValue(value, ref childCollection); }

        public ChangableMessengerCollection<ChangableMessengerMock> ChildCollectionReadOnly { get => childCollectionReadOnly; }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext s)
        {
            OnCreated();
        }

        public override void AcceptChanges()
        {
            child.AcceptChanges();
            childReadOnly.AcceptChanges();
            childCollection.AcceptChanges();
            childCollectionReadOnly.AcceptChanges();

            base.AcceptChanges();
        }

        private void OnCreated()
        {
            AddChangeTracking(child);
            AddChangeTracking(childReadOnly);
            AddChangeTracking(childCollection);
            AddChangeTracking(childCollectionReadOnly);
        }
    }
}
