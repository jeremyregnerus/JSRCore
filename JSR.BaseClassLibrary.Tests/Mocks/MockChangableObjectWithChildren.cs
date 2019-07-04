// <copyright file="MockChangableObjectWithChildren.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace JSR.BaseClassLibrary.Tests.Mocks
{
    [DataContract]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Mock Object.")]
    internal class MockChangableObjectWithChildren : MockChangableObject
    {
        [DataMember]
        private readonly MockChangableObject childReadOnly = new MockChangableObject();

        [DataMember]
        private readonly ChangableCollection<MockChangableObject> childCollectionReadOnly = new ChangableCollection<MockChangableObject>();

        [DataMember]
        private MockChangableObject child = new MockChangableObject();

        [DataMember]
        private ChangableCollection<MockChangableObject> childCollection = new ChangableCollection<MockChangableObject>();

        public MockChangableObjectWithChildren()
        {
            OnCreated();
        }

        public MockChangableObject Child { get => child; set => SetValue(value, ref child); }

        public MockChangableObject ChildReadOnly { get => childReadOnly; }

        public ChangableCollection<MockChangableObject> ChildCollection { get => childCollection; set => SetValue(value, ref childCollection); }

        public ChangableCollection<MockChangableObject> ChildCollectionReadOnly { get => childCollectionReadOnly; }

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
