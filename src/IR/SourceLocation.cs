namespace IR;

public sealed record SourceLocation
{
    public SourceLocation(
        string filePath,
        int startLine,
        int startColumn,
        int endLine,
        int endColumn)
    {
        FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        StartLine = startLine;
        StartColumn = startColumn;
        EndLine = endLine;
        EndColumn = endColumn;
    }

    public string FilePath { get; }

    public int StartLine { get; }

    public int StartColumn { get; }

    public int EndLine { get; }

    public int EndColumn { get; }
}
