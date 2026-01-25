namespace DISourceGenerator.Tests
{
    public class DISourceGeneratorSnapshotTests
    {
        [Fact]
        public Task GetGeneratedString()
        {
            var source = @"namespace ConsoleApp_Tests
                           {
                               [Singleton]
                               public partial class WithAttribute
                               {
                               }
                           }
                           ";

            return TestHelper.Verify(source);
        }
    }
}
