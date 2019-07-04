// <copyright file="MockChangableMessagingObjectWithChildren.cs" company="Jeremy Regnerus">
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
    public class MockChangableMessagingObjectWithChildren : ChangableMessagingObject
    {
        [DataMember]
        private readonly MockChangableMessagingObject childReadOnly = new MockChangableMessagingObject();

        [DataMember]
        private readonly ChangableMessagingCollection<MockChangableMessagingObject> childCollectionReadOnly = new ChangableMessagingCollection<MockChangableMessagingObject>();

        [DataMember]
        private MockChangableMessagingObject child = new MockChangableMessagingObject();

        [DataMember]
        private ChangableMessagingCollection<MockChangableMessagingObject> childCollection = new ChangableMessagingCollection<MockChangableMessagingObject>();

        public MockChangableMessagingObjectWithChildren()
        {
            OnCreated();
        }

        public MockChangableMessagingObject Child { get => child; set => SetValue(value, ref child); }

        public MockChangableMessagingObject ChildReadOnly { get => childReadOnly; }

        public ChangableMessagingCollection<MockChangableMessagingObject> ChildCollection { get => childCollection; set => SetValue(value, ref childCollection); }

        public ChangableMessagingCollection<MockChangableMessagingObject> ChildCollectionReadOnly { get => childCollectionReadOnly; }

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
            child.OnChanged += ChildChanged;
            ChildReadOnly.OnChanged += ChildChanged;
            ChildCollection.OnChanged += ChildChanged;
            childCollectionReadOnly.OnChanged += ChildChanged;
        }
    }
}
