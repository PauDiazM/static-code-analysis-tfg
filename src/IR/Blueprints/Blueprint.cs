namespace IR;

public abstract class Blueprint
{
    public Guid Id { get; } = Guid.NewGuid();

    public SourceLocation? SourceLocation { get; set; }
    
}