using IR.Types;
using Parsers;

namespace Translation.Mappers;

public class CompoundTypeMapper
{
    public CompoundTypeBlueprint Map(ParsedElement element)
    {
        if (element is not DeclarationElement declaration)
        {
            throw new ArgumentException(
                "Expected a DeclarationElement.",
                nameof(element));
        }

        CompoundTypeKind kind = element.Kind switch
        {
            ElementKind.Class => CompoundTypeKind.Class,
            ElementKind.Struct => CompoundTypeKind.Struct,
            ElementKind.Interface => CompoundTypeKind.Interface,
            ElementKind.Record => CompoundTypeKind.Record,
            ElementKind.Enum => CompoundTypeKind.Enum,

            _ => throw new ArgumentOutOfRangeException(
                nameof(element),
                element.Kind,
                "Unsupported compound type kind.")
        };

        return new CompoundTypeBlueprint(declaration.Name, kind);
    }
}
