using Factories;
using Parsers;
using Presentation;

List<string> filepaths = new()
{
    Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "../../../../code_samples/examples/HelloWorld.cs")),
    Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "../../../Parsers/Common/Elements/SourceSpan.cs"))
};
ParserFactory factory = new ParserFactory();
IParser parser = factory.Create(filepaths[1]);
ParseResult result = parser.Parse();
new ParseResultConsolePrinter().Print(result);
