using Factories;
using Parsers;
using Presentation;

string filepath = Path.GetFullPath(
    Path.Combine(AppContext.BaseDirectory, "../../../../code_samples/examples/HelloWorld.cs"));
ParserFactory factory = new ParserFactory();
IParser parser = factory.Create(filepath);
ParseResult result = parser.Parse();
new ParseResultConsolePrinter().Print(result);
