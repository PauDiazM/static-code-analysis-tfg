namespace IR.Types;

public class ArrayTypeBlueprint : TypeBlueprint
{
    public ArrayTypeBlueprint(string name)
        : base(name)
    {
    }

    public TypeBlueprint? ElementType { get; set; }

    public int Rank { get; private set; } = 1;

    public void SetRank(int rank)
    {
        Rank = rank > 0 ? rank : throw new ArgumentOutOfRangeException(nameof(rank));
    }
}