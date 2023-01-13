namespace JSR.BaseClasses.Tests.Mocks
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Mock")]
    public class MockBaseMessenger : BaseMessenger
    {
        public void ChangeMessage(string message)
        {
            Message = message;
        }
    }
}
