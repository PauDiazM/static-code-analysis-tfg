namespace Parsers;

public interface IElementTranslator
{
    ParsedElement Translate(ElementKind kind);
}
