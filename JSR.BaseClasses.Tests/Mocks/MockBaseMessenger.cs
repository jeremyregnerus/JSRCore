namespace JSR.BaseClasses.Tests.Mocks
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Mock")]
    public class MockBaseMessenger : Messenger
    {
        public void ChangeMessage(string message)
        {
            Message = message;
        }
    }
}
