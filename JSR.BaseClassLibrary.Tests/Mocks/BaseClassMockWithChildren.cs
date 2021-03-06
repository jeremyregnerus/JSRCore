﻿// <copyright file="BaseClassMockWithChildren.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Runtime.Serialization;

namespace JSR.BaseClassLibrary.Tests.Mocks
{
    [DataContract]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Mock Class.")]
    public class BaseClassMockWithChildren : BaseClassMock
    {
        [DataMember]
        private readonly BaseClassMock childReadOnly = new BaseClassMock();

        [DataMember]
        private readonly BaseCollection<BaseClassMock> childCollectionReadOnly = new BaseCollection<BaseClassMock>();

        [DataMember]
        private BaseClassMock child;

        [DataMember]
        private BaseCollection<BaseClassMock> childCollection;

        public BaseClassMockWithChildren()
        {
            OnCreated();
        }

        public BaseClassMock ChildReadOnly { get => childReadOnly; }

        public BaseCollection<BaseClassMock> ChildCollectionReadOnly { get => childCollectionReadOnly; }

        public BaseClassMock Child { get => child; set => SetValue(value, ref child); }

        public BaseCollection<BaseClassMock> ChildCollection { get => childCollection; set => SetValue(value, ref childCollection); }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext streamingContext)
        {
            OnCreated();
        }

        public override void AcceptChanges()
        {
            childReadOnly.AcceptChanges();
            childCollectionReadOnly.AcceptChanges();
            child?.AcceptChanges();
            childCollection?.AcceptChanges();

            base.AcceptChanges();
        }

        private void OnCreated()
        {
            AddChildNotifications(childReadOnly);
            AddChildNotifications(childCollectionReadOnly);
            AddChildNotifications(child);
            AddChildNotifications(childCollection);
        }
    }
}
