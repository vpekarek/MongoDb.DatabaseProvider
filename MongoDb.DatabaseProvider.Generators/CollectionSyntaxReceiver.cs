using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace MongoDb.DatabaseProvider.Generators;

public class CollectionSyntaxReceiver : ISyntaxReceiver
{
    public List<ClassDeclarationSyntax> Collections { get; set; } = new();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is not ClassDeclarationSyntax cds ||
            !cds.Modifiers.Any(Microsoft.CodeAnalysis.CSharp.SyntaxKind.PublicKeyword) ||
            !cds.AttributeLists.Any(x => x.Attributes.Any(y => y.Name.ToString() == "MongoDbCollection")))
        {
            return;
        }

        Collections.Add(cds);
    }
}
