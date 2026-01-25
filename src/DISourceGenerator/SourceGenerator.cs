using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Text;
using System.Threading;

namespace DISourceGenerator
{
    [Generator]
    public class SourceGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            IncrementalValuesProvider<INamedTypeSymbol?> result = context.SyntaxProvider.CreateSyntaxProvider(
                                                                        AttributePredicate, 
                                                                        GetClassesToGenerate);

            context.RegisterSourceOutput(result, GenerateCode);
        }

        private bool AttributePredicate(SyntaxNode syntaxNode, CancellationToken cancellationToken)
        {
            if(syntaxNode is not AttributeSyntax attribute)
            {
                return false;
            }

            return attribute.Name.ToString() == "Singleton";
        }

        private INamedTypeSymbol? GetClassesToGenerate(GeneratorSyntaxContext context, CancellationToken cancellationToken)
        {
            AttributeSyntax attributeSyntax = (AttributeSyntax)context.Node;
            if(attributeSyntax.Parent.Parent is not ClassDeclarationSyntax classDeclaration)
            {
                return null;
            }

            INamedTypeSymbol? result = context.SemanticModel.GetDeclaredSymbol(classDeclaration);
            return result;
        }

        private void GenerateCode(SourceProductionContext context, INamedTypeSymbol typeSymbol)
        {
            context.AddSource($"{typeSymbol.Name}.g.cs", "Test");
        }

    }
}
