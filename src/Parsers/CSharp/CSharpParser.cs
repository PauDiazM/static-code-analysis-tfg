namespace Parsers.Roslyn;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Parsers;

public class CSharpParser(string filepath) : IParser
{
    public ParseResult Parse()
    {
        string source = File.ReadAllText(filepath);
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);
        ParsedElement root = new CSharpElementMapper().Map(syntaxTree.GetRoot());
        ParseResult result = new(root);

        foreach (Diagnostic diagnostic in syntaxTree.GetDiagnostics())
        {
            SourceSpan? span = diagnostic.Location.IsInSource
                ? new SourceSpan(
                    diagnostic.Location.SourceSpan.Start,
                    diagnostic.Location.SourceSpan.Length)
                : null;

            result.Diagnostics.Add(new ParseDiagnostic(
                diagnostic.GetMessage(),
                diagnostic.Severity switch
                {
                    DiagnosticSeverity.Error => ParseDiagnosticSeverity.Error,
                    DiagnosticSeverity.Warning => ParseDiagnosticSeverity.Warning,
                    _ => ParseDiagnosticSeverity.Info
                },
                diagnostic.Id,
                span));
        }

        return result;
    }
}
