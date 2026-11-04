using Factories;
using Parsers;
using Presentation;

string examplesDirectory = Path.GetFullPath(
    Path.Combine(AppContext.BaseDirectory, "../../../../code_samples/examples"));
List<string> filepaths = Directory
    .GetFiles(examplesDirectory, "*.cs", SearchOption.AllDirectories)
    .ToList();

Console.WriteLine("Available C# example files:");
int index = 0;

foreach (string filepath in filepaths)
{
    Console.WriteLine($"{index++} - {Path.GetFileName(filepath)}");
}

Console.Write("Enter the number of the file you want to parse: ");
int choice = int.Parse(Console.ReadLine());
ParserFactory factory = new ParserFactory();
IParser parser = factory.Create(filepaths[choice]);
ParseResult result = parser.Parse();
new ParseResultConsolePrinter().Print(result);