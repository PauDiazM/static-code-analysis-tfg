using IR.Organization;
using Parsers;

namespace Translation.Mappers;

public class NamespaceMapper
{
    public NamespaceBlueprint Map(ParsedElement element)
    {
        if (element is not DeclarationElement declaration ||
            element.Kind != ElementKind.Namespace)
        {
            throw new ArgumentException(
                "Expected a namespace DeclarationElement.",
                nameof(element));
        }

        return new NamespaceBlueprint(declaration.Name);
    }
}
