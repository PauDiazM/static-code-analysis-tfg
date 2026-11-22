using IR.References;

namespace IR.Types;

public class ArrayTypeBlueprint : TypeBlueprint
{
    public ArrayTypeBlueprint(string name, TypeReference elementType)
        : base(name)
    {
        ElementType = elementType ?? throw new ArgumentNullException(nameof(elementType));
    }

    public TypeReference ElementType { get; }

    public int Rank { get; private set; } = 1;

    public void SetRank(int rank)
    {
        Rank = rank > 0 ? rank : throw new ArgumentOutOfRangeException(nameof(rank));
    }
}