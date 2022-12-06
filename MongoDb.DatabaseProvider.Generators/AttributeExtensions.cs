using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDb.DatabaseProvider.Generators;
public static class AttributeExtensions
{
    public static Dictionary<string, object> GetAttributeProperties(this AttributeArgumentListSyntax argumentList)
    {
        var dic = new Dictionary<string, object>();

        foreach (var attributeParam in argumentList.Arguments)
        {
            if (attributeParam is AttributeArgumentSyntax ags)
            {
                if (ags.Expression is LiteralExpressionSyntax les)
                {
                    var kind = les.Kind();
                    object value = kind switch
                    {
                        Microsoft.CodeAnalysis.CSharp.SyntaxKind.FalseLiteralExpression => false,
                        Microsoft.CodeAnalysis.CSharp.SyntaxKind.TrueLiteralExpression => true,
                        Microsoft.CodeAnalysis.CSharp.SyntaxKind.NumericLiteralExpression => ParseNumericLiteral(ags.Expression.GetText()),
                        _ => ags.Expression.GetText().ToString().Trim('"'),
                    };

                    dic.Add(ags.NameEquals?.ToFullString().Trim().TrimEnd('=').Trim() ?? string.Empty, value);
                }
            }
        }

        return dic;

        static object ParseNumericLiteral(Microsoft.CodeAnalysis.Text.SourceText text)
        {
            if (int.TryParse(text.ToString(), out var intResult))
            {
                return intResult;
            }

            if (decimal.TryParse(text.ToString(), out var decimalResult))
            {
                return decimalResult;
            }

            return 0;
        }
    }

    public static object? GetAttributeProperty(this AttributeArgumentListSyntax argumentList, string propertyName)
    {
        var list = argumentList.GetAttributeProperties();

        if (list.ContainsKey(propertyName))
        {
            return list[propertyName];
        }

        return null;
    }

    public static T? GetAttributeProperty<T>(this AttributeArgumentListSyntax argumentList, string propertyName)
    {
        var obj = argumentList.GetAttributeProperty(propertyName);

        if (obj == null)
        {
            return default;
        }

        return (T)obj;
    }
}