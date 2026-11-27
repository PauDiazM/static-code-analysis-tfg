using IR.References;
using Parsers;

namespace Translation.Mappers;

public class TypeReferenceMapper
{
    public TypeReference Map(ParsedElement element)
    {
        ArgumentNullException.ThrowIfNull(element);

        string type;
        bool isNullable;

        switch (element)
        {
            case VariableElement variable:
                type = variable.Type;
                isNullable = variable.IsNullable;
                break;

            case PropertyElement property:
                type = property.Type;
                isNullable = property.IsNullable;
                break;

            default:
                throw new ArgumentException(
                    "Element must represent a typed declaration.",
                    nameof(element));
        }

        TypeReference typeReference = MapType(element, type);
        typeReference.IsNullable = isNullable;

        return typeReference;
    }

    private TypeReference MapType(ParsedElement element, string type)
    {
        GenericNameElement? genericName = FindGenericName(element);

        if (genericName is not null)
        {
            return MapGenericType(genericName);
        }

        return new NamedTypeReference(type);
    }

    private TypeReference MapGenericType(GenericNameElement element)
    {
        var genericType = new GenericTypeReference(
            new NamedTypeReference(element.BaseType));

        foreach (ParsedElement argument in element.ArgumentTypes)
        {
            genericType.TypeArguments.Add(MapTypeArgument(argument));
        }

        return genericType;
    }

    private TypeReference MapTypeArgument(ParsedElement element)
    {
        if (element is GenericNameElement genericName)
        {
            return MapGenericType(genericName);
        }

        return new NamedTypeReference(element.Text);
    }

    private static GenericNameElement? FindGenericName(ParsedElement element)
    {
        if (element is GenericNameElement genericName)
        {
            return genericName;
        }

        foreach (ParsedElement child in element.Children)
        {
            GenericNameElement? result = FindGenericName(child);

            if (result is not null)
            {
                return result;
            }
        }

        return null;
    }
}