namespace Parsers;

public enum ParseDiagnosticSeverity
{
    Info,
    Warning,
    Error
}

public sealed record ParseDiagnostic(
    string Message,
    ParseDiagnosticSeverity Severity,
    string? Code = null,
    SourceSpan? Span = null);