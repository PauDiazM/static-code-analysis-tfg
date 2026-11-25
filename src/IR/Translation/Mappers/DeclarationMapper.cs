using IR;
using IR.Organization;
using Parsers;

namespace Translation.Mappers;

public class DeclarationMapper
{
    private readonly NamespaceMapper _namespaceMapper;
    private readonly CompoundTypeMapper _compoundTypeMapper;

    public DeclarationMapper(
        NamespaceMapper namespaceMapper,
        CompoundTypeMapper compoundTypeMapper)
    {
        _namespaceMapper = namespaceMapper;
        _compoundTypeMapper = compoundTypeMapper;
    }

    public Blueprint Map(ParsedElement element)
    {
        return element.Kind switch
        {
            ElementKind.Namespace => MapNamespace(element),

            ElementKind.Class or
            ElementKind.Struct or
            ElementKind.Interface or
            ElementKind.Record or
            ElementKind.Enum =>
                _compoundTypeMapper.Map(element),

            _ => throw new ArgumentOutOfRangeException(
                nameof(element),
                element.Kind,
                "Unsupported declaration kind.")
        };
    }

    private NamespaceBlueprint MapNamespace(ParsedElement element)
    {
        NamespaceBlueprint namespaceBlueprint =
            _namespaceMapper.Map(element);

        foreach (ParsedElement child in element.Children)
        {
            // Identifiers are metadata, not declarations.
            if (child.Kind == ElementKind.Identifier)
            {
                continue;
            }

            Blueprint declaration = Map(child);
            namespaceBlueprint.AddDeclaration(declaration);
        }

        return namespaceBlueprint;
    }
}
