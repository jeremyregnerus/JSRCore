using System.Diagnostics.CodeAnalysis;

namespace JSR.Utilities.Tests.Mocks
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Unit test")]
    public class ChildClassMock
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
