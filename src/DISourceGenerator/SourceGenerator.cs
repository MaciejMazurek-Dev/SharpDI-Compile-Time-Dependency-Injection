using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading;

namespace DISourceGenerator
{
    [Generator]
    public class SourceGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            IncrementalValueProvider prov = context.SyntaxProvider.ForAttributeWithMetadataName(
                fullyQualifiedMetadataName: "SharpDILibrary.RegisterDependencyAttribute",
                predicate: CouldBeDependency,
                transform: );
                
        }

        private static bool CouldBeDependency(SyntaxNode syntaxNode, CancellationToken cancellationToken)
        {
            if(syntaxNode is not AttributeSyntax attribute)
            {
                return false;
            }

            var name = GetName(attribute.Name);
        }

        private static string? GetName(NameSyntax name)
        {
            return name switch
            {
                SimpleNameSyntax sns => sns.Identifier.Text,
                QualifiedNameSyntax qns => qns.Right.Identifier.Text,
                _ => null
            };
        }
    }
}
