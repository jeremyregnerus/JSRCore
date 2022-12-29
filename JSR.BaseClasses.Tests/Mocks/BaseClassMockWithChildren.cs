// <copyright file="BaseClassMockWithChildren.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Runtime.Serialization;

namespace JSR.BaseClasses.Tests.Mocks
{
    [DataContract]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Mock Class.")]
    public class BaseClassMockWithChildren : BaseClassMock
    {
        [DataMember]
        private readonly BaseClassMock childReadOnly = new();

        [DataMember]
        private readonly BaseCollection<BaseClassMock> childCollectionReadOnly = new();

        [DataMember]
        private BaseClassMock child = new();

        [DataMember]
        private BaseCollection<BaseClassMock> childCollection = new();

        public BaseClassMockWithChildren()
        {
            OnCreated();
        }

        public BaseClassMock ChildReadOnly { get => childReadOnly; }

        public BaseCollection<BaseClassMock> ChildCollectionReadOnly { get => childCollectionReadOnly; }

        public BaseClassMock Child { get => child; set => SetValue(ref child, value); }

        public BaseCollection<BaseClassMock> ChildCollection { get => childCollection; set => SetValue(ref childCollection, value); }

        public override void AcceptChanges()
        {
            childReadOnly.AcceptChanges();
            childCollectionReadOnly.AcceptChanges();
            child?.AcceptChanges();
            childCollection?.AcceptChanges();

            base.AcceptChanges();
        }

        [OnDeserialized]
        protected override void OnDeserialized(StreamingContext s)
        {
            OnCreated();
        }

        protected override void OnCreated()
        {
            AddChildNotifications(childReadOnly);
            AddChildNotifications(childCollectionReadOnly);
            AddChildNotifications(child);
            AddChildNotifications(childCollection);
        }
    }
}
