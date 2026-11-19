using System.ComponentModel;
using IR.Callables;
using IR.Members;
using IR.Variables;

namespace IR.Types;

public class CompoundTypeBlueprint : TypeBlueprint, IContainerBlueprint
{
    private readonly List<MethodBlueprint> _methods = [];

    private readonly List<FieldBlueprint> _fields = [];

    private readonly List<PropertyBlueprint> _properties = [];

    private readonly List<ConstructorBlueprint> _constructors = [];

    private readonly List<DestructorBlueprint> _destructors = [];

    private readonly List<TypeBlueprint> _implementedInterfaces = [];

    public CompoundTypeBlueprint(
        string name)
        : base(name)
    {
        Kind = CompoundTypeKind.Unknown;
    }

    public CompoundTypeKind Kind { get; set; }

    public IReadOnlyList<MethodBlueprint> Methods => _methods;

    public IReadOnlyList<FieldBlueprint> Fields => _fields;

    public IReadOnlyList<PropertyBlueprint> Properties => _properties;

    public IReadOnlyList<ConstructorBlueprint> Constructors => _constructors;

    public IReadOnlyList<DestructorBlueprint> Destructors => _destructors;

    public IReadOnlyList<TypeBlueprint> ImplementedInterfaces => _implementedInterfaces;

    public CompoundTypeCharacteristics Characteristics { get; set; }

    public void AddMethod(MethodBlueprint method) => AddMember(_methods, method, nameof(method));

    public void AddField(FieldBlueprint field) => AddMember(_fields, field, nameof(field));

    public void AddProperty(PropertyBlueprint property) => AddMember(_properties, property, nameof(property));

    public void AddConstructor(ConstructorBlueprint constructor) =>
        AddMember(_constructors, constructor, nameof(constructor));

    public void AddDestructor(DestructorBlueprint destructor) =>
        AddMember(_destructors, destructor, nameof(destructor));

    public void AddImplementedInterface(CompoundTypeBlueprint @interface) 
    {
        ArgumentNullException.ThrowIfNull(@interface);

        if (_implementedInterfaces.Any(existing => existing.Id == @interface.Id))
        {
            throw new ArgumentException("The same interface cannot be added more than once, ID: ", @interface.Name);
        }

        _implementedInterfaces.Add(@interface);
    }

    public List<IMember> GetAllMembers()
    {
        var members = new List<IMember>(
            _methods.Count + _fields.Count + _properties.Count + _constructors.Count + _destructors.Count);
        members.AddRange(_methods);
        members.AddRange(_fields);
        members.AddRange(_properties);
        members.AddRange(_constructors);
        members.AddRange(_destructors);
        return members;
    }

    private void AddMember<TMember>(List<TMember> members, TMember member, string parameterName)
        where TMember : class, IMember
    {
        ArgumentNullException.ThrowIfNull(member, parameterName);

        if (member.ContainerId != Id)
        {
            throw new ArgumentException("Member's ContainerId must match this type's ID.", parameterName);
        }

        if (members.Any(existing => ReferenceEquals(existing, member)))
        {
            throw new ArgumentException("The same member cannot be added more than once.", parameterName);
        }

        members.Add(member);
    }

}