namespace Factories;
using Parsers;

public enum Language
{
    CSharp
}

public class ParserFactory
{
    private readonly Dictionary<Language, Func<string, IParser>> _parsers = new()
    {
        [Language.CSharp] = (filepath) => new CSharpParser(filepath)
    };

    public IParser Create(string filepath)
    {
        Language language = DetectLanguage(filepath);
        if (!_parsers.TryGetValue(language, out var factory))
        {
            throw new NotSupportedException(
                $"No parser registered for {language}");
        }

        return factory(filepath);
    }

    private Language DetectLanguage(string filepath)
    {
        //TODO: Implement language detection

        return Language.CSharp;
    }

}