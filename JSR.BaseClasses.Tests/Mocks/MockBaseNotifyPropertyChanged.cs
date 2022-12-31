// <copyright file="MockBaseNotifyPropertyChanged.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

namespace JSR.BaseClasses.Tests.Mocks
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Mock class")]
    public class MockBaseNotifyPropertyChanged : BaseNotifyPropertyChanged
    {
        private string stringProperty = string.Empty;

        private int intProperty;

        public string StringProperty { get => stringProperty; set => SetValue(ref stringProperty, value); }

        public int IntegerProperty { get => intProperty; set => SetValue(ref intProperty, value); }
    }
}
