using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Runtime.CompilerServices;

namespace DISourceGenerator.Tests
{
    public static class TestHelper
    {
        public static Task Verify(string sourceCode)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName: "DISourceGenerator.Tests",
                syntaxTrees: new[] { syntaxTree });

            var generator = new SourceGenerator();

            GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

            driver = driver.RunGenerators(compilation);

            return Verifier.Verify(driver);
        }

        [ModuleInitializer]
        public static void Init() => VerifySourceGenerators.Initialize();
    }
}
