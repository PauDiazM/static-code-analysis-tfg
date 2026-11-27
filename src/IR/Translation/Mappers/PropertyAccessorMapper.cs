using IR.Members;
using Parsers;

namespace Translation.Mappers;

public class PropertyAccessorMapper
{
    public PropertyAccessorBlueprint Map(ParsedElement element)
    {
        if (element is not AccessorElement accessor)
        {
            throw new ArgumentException(
                "Expected an AccessorElement.",
                nameof(element));
        }

        PropertyAccessorKind kind = accessor.Keyword switch
        {
            "get" => PropertyAccessorKind.Get,
            "set" => PropertyAccessorKind.Set,

            _ => throw new ArgumentOutOfRangeException(
                nameof(element),
                accessor.Keyword,
                "Unsupported property accessor.")
        };

        return new PropertyAccessorBlueprint(kind);
    }
}