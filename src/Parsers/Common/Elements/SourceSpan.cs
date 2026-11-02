namespace Parsers;

/// <summary>
/// A range in the source text, measured in UTF-16 code units, with an exclusive end.
/// </summary>
public sealed record SourceSpan
{
    public int StartOffset { get; }
    public int Length { get; }
    public int EndOffset => checked(StartOffset + Length);

    public SourceSpan(int startOffset, int length)
    {
        if (startOffset < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(startOffset));
        }

        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        if (length > int.MaxValue - startOffset)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        StartOffset = startOffset;
        Length = length;
    }
}
