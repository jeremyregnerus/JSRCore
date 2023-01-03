// <copyright file="MockBaseNotifyChanged.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

namespace JSR.BaseClasses.Tests.Mocks
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Mock")]
    public class MockBaseNotifyChanged : BaseNotifyChanged
    {
        private int integerProperty;
        private string stringProperty = string.Empty;

        public int IntegetProperty { get => integerProperty; set => SetValue(ref integerProperty, value); }

        public string StringProperty { get => stringProperty; set => SetValue(ref stringProperty, value); }
    }
}
