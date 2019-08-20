// <copyright file="ChangableWithChildrenMock.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace JSR.BaseClassLibrary.Tests.Mocks
{
    [DataContract]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Mock Object.")]
    public class ChangableWithChildrenMock : ChangableMock
    {
        [DataMember]
        private readonly ChangableMock childReadOnly = new ChangableMock();

        [DataMember]
        private readonly ChangableCollection<ChangableMock> childCollectionReadOnly = new ChangableCollection<ChangableMock>();

        [DataMember]
        private ChangableMock child = new ChangableMock();

        [DataMember]
        private ChangableCollection<ChangableMock> childCollection = new ChangableCollection<ChangableMock>();

        public ChangableWithChildrenMock()
        {
            OnCreated();
        }

        public ChangableMock Child { get => child; set => SetValue(value, ref child); }

        public ChangableMock ChildReadOnly { get => childReadOnly; }

        public ChangableCollection<ChangableMock> ChildCollection { get => childCollection; set => SetValue(value, ref childCollection); }

        public ChangableCollection<ChangableMock> ChildCollectionReadOnly { get => childCollectionReadOnly; }

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
