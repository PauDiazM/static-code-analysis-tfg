using IR.Organization;
using Parsers;
using Translation.Mappers;

namespace Translation;

public class ParsedResultTranslator
{
    private readonly DeclarationMapper _declarationMapper;
    private readonly ImportMapper _importMapper;

    public ParsedResultTranslator()
    {
        NamespaceMapper namespaceMapper = new();
        CompoundTypeMapper compoundTypeMapper = new();

        _declarationMapper = new DeclarationMapper(
            namespaceMapper,
            compoundTypeMapper);

        _importMapper = new ImportMapper();
    }

    public FileBlueprint Translate(
        ParseResult result,
        string filePath,
        string languageId)
    {
        ArgumentNullException.ThrowIfNull(result);

        if (result.Root.Kind != ElementKind.CompilationUnit)
        {
            throw new ArgumentException(
                "Expected a CompilationUnit as the root element.",
                nameof(result));
        }

        FileBlueprint file = new(filePath, languageId);

        foreach (ParsedElement child in result.Root.Children)
        {
            switch (child.Kind)
            {
                case ElementKind.Import:
                    file.AddImport(_importMapper.Map(child));
                    break;

                case ElementKind.Identifier:
                    break;

                default:
                    file.AddDeclaration(_declarationMapper.Map(child));
                    break;
            }
        }

        return file;
    }
}
