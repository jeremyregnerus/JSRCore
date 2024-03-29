﻿namespace JSR.BaseClasses.Tests.Mocks
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Mock")]
    public class MockBaseNotifyChangedParent : MockBaseNotifyChanged
    {
        private MockBaseNotifyChanged child = new();

        public MockBaseNotifyChangedParent()
        {
            AddChildChangeTracking(child);
        }

        public MockBaseNotifyChanged Child { get => child; set => SetProperty(ref child, value); }
    }
}
