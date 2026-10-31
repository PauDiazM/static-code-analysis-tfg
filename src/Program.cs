using Factories;
using Parsers;

//string filepath = "../code_samples/examples/HelloWorld.cs";
string filepath = "./Parsers/CSharp/RoslynRules/RoslynRules.cs";
ParserFactory factory = new ParserFactory();
Console.WriteLine("Created parser factory.");
IParser parser = factory.Create(filepath);
Console.WriteLine("Created C# parser.");
parser.Parse();
