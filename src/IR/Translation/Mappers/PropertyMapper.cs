using IR.Members;
using IR.Types;
using Parsers;

namespace Translation.Mappers;

public class PropertyMapper
{
    private readonly TypeReferenceMapper _typeReferenceMapper;
    private readonly PropertyAccessorMapper _accessorMapper;

    public PropertyMapper(
        TypeReferenceMapper typeReferenceMapper,
        PropertyAccessorMapper accessorMapper)
    {
        _typeReferenceMapper = typeReferenceMapper
            ?? throw new ArgumentNullException(nameof(typeReferenceMapper));

        _accessorMapper = accessorMapper
            ?? throw new ArgumentNullException(nameof(accessorMapper));
    }

    public PropertyBlueprint Map(
        PropertyElement element,
        CompoundTypeBlueprint container)
    {
        ArgumentNullException.ThrowIfNull(element);
        ArgumentNullException.ThrowIfNull(container);

        var property = new PropertyBlueprint(
            element.Name,
            container.Id,
            _typeReferenceMapper.Map(element));

        foreach (string modifier in element.Modifiers)
        {
            property.AddModifier(modifier);
        }

        foreach (AccessorElement accessor in GetAccessors(element))
        {
            PropertyAccessorBlueprint accessorBlueprint =
                _accessorMapper.Map(accessor);

            switch (accessorBlueprint.AccessorKind)
            {
                case PropertyAccessorKind.Get:
                    property.SetGetter(accessorBlueprint);
                    break;

                case PropertyAccessorKind.Set:
                    property.SetSetter(accessorBlueprint);
                    break;
            }
        }

        return property;
    }

    private static IEnumerable<AccessorElement> GetAccessors(
        ParsedElement element)
    {
        foreach (ParsedElement child in element.Children)
        {
            if (child is AccessorElement accessor)
            {
                yield return accessor;
            }
            else
            {
                foreach (AccessorElement descendant in GetAccessors(child))
                {
                    yield return descendant;
                }
            }
        }
    }
}