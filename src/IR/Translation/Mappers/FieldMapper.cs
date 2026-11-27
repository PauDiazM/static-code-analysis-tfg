using IR.Types;
using IR.Variables;
using Parsers;

namespace Translation.Mappers;

public class FieldMapper
{
    private readonly TypeReferenceMapper _typeReferenceMapper;

    public FieldMapper(TypeReferenceMapper typeReferenceMapper)
    {
        _typeReferenceMapper = typeReferenceMapper
            ?? throw new ArgumentNullException(nameof(typeReferenceMapper));
    }

    public IEnumerable<FieldBlueprint> Map(
        DeclarationElement element,
        CompoundTypeBlueprint container)
    {
        ArgumentNullException.ThrowIfNull(element);
        ArgumentNullException.ThrowIfNull(container);

        if (element.Kind != ElementKind.Field)
        {
            throw new ArgumentException(
                "Element must represent a field declaration.",
                nameof(element));
        }

        ParsedElement? variableDeclaration = element.Children
            .FirstOrDefault(child => child.Kind == ElementKind.VariableDeclaration);

        if (variableDeclaration is null)
        {
            yield break;
        }

        foreach (VariableElement variable in GetVariables(variableDeclaration))
        {
            var field = new FieldBlueprint(
                variable.Name,
                container.Id,
                _typeReferenceMapper.Map(variable));

            foreach (string modifier in element.Modifiers)
            {
                field.AddModifier(modifier);
            }

            yield return field;
        }
    }

    private static IEnumerable<VariableElement> GetVariables(ParsedElement element)
    {
        foreach (ParsedElement child in element.Children)
        {
            if (child is VariableElement variable)
            {
                yield return variable;
            }
            else
            {
                foreach (VariableElement descendant in GetVariables(child))
                {
                    yield return descendant;
                }
            }
        }
    }
}