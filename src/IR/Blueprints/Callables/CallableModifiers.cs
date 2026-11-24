namespace IR.Callables;

[Flags]
public enum CallableModifiers
{
    None = 0,
    Static = 1 << 0,
    Abstract = 1 << 1,
    Virtual = 1 << 2
}
