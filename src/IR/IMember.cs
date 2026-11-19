namespace IR;

// Used for elements which are children of a parent element
// References the parents' ID.
public interface IMember
{
    Guid ContainerId { get; }
}
