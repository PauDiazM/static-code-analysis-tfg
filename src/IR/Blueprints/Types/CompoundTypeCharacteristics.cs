namespace IR.Types;

[Flags]
public enum CompoundTypeCharacteristics
{
    None = 0,
    Abstract = 1 << 0,
    Sealed = 1 << 1,
    Static = 1 << 2,
    Generic = 1 << 3,
    Partial = 1 << 4
}
