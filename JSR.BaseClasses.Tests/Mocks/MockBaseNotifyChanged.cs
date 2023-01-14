namespace JSR.BaseClasses.Tests.Mocks
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Mock")]
    public class MockBaseNotifyChanged : Changeable
    {
        private int integerProperty;
        private string stringProperty = string.Empty;

        public int IntegetProperty { get => integerProperty; set => SetProperty(ref integerProperty, value); }

        public string StringProperty { get => stringProperty; set => SetProperty(ref stringProperty, value); }
    }
}
