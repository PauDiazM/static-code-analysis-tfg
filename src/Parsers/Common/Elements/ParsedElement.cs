namespace Parsers;

public abstract class ParsedElement
{

    public ElementKind Kind { get; set; } = new();
    public string Text { get; set; } = string.Empty;

    public List<ParsedElement> Children { get; } = new();

    public void PrintInformation(int depth = 0)
    {
        string indent = new(' ', depth * 2);

        Console.WriteLine($"{indent}Node Type: {Kind}, Children: {Children.Count}");

        PrintSpecificInformation(indent);

        Console.WriteLine($"{indent}Text: {Text}");
    }

    protected virtual void PrintSpecificInformation(string indent){}
}


public class ImportElement : ParsedElement
{
    public string KeyWord { get; set; } = string.Empty;
    public string Package { get; set; } = string.Empty;
    protected override void PrintSpecificInformation(string indent)
    {
       Console.WriteLine($"{indent}Keyword: {KeyWord}, Package: {Package}");
    }
}

public class ArgumentElement : ParsedElement
{
}

public class UnknownElement : ParsedElement
{
    public UnknownElement(ElementKind kind)
    {
        Kind = kind;
    }
}