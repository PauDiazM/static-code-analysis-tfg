using Parsers;

public enum ContractKind
{
    Interface,
    Template
}
public abstract class DeclarationElement : ParsedElement
{
    public string Name { get; set; } = string.Empty;
    protected override void PrintSpecificInformation(string indent)
    {
        Console.WriteLine($"{indent}Name: {Name}");
        PrintDeclarationInformation(indent);
    }

    protected virtual void PrintDeclarationInformation(string indent){}

}

public class ClassElement : DeclarationElement
{
}

public class ContractElement : DeclarationElement
{
}

public class CallableElement : DeclarationElement
{
    public List<string> Modifiers { get; set; } = new();

    public string? ReturnType { get; set; }

    protected override void PrintDeclarationInformation(string indent)
    {
        Console.WriteLine($"{indent}Modifiers: {string.Join(" ", Modifiers)}");
        Console.WriteLine($"{indent}Return type: {ReturnType}");
    }
}

public class ParameterElement : DeclarationElement
{
    public string Type { get; set; } = string.Empty;

    protected override void PrintDeclarationInformation(string indent)
    {
        Console.WriteLine($"{indent}Type: {Type}");
    }
}