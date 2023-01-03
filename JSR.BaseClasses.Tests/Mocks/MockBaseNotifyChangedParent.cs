// <copyright file="MockBaseNotifyChangedParent.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

namespace JSR.BaseClasses.Tests.Mocks
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Mock")]
    public class MockBaseNotifyChangedParent : MockBaseNotifyChanged
    {
        private MockBaseNotifyChanged child = new();

        public MockBaseNotifyChangedParent()
        {
            AddChildChangeTracking(child);
        }

        public MockBaseNotifyChanged Child { get => child; set => SetValue(ref child, value); }
    }
}
