public class LoopTests
{
    public static void Main(string[] args)
    {
        // Common C# loop
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"Iteration {i + 1}");
        }

        // Foreach loop
        string[] fruits = { "Apple", "Banana", "Cherry" };
        Console.WriteLine(fruits[0]);
        foreach (string fruit in fruits)
        {
            Console.WriteLine(fruit);
        }         

        // While loop
        int count = 0;
        while (count < 3)
        {
            count++;
        }

        // Do-while loop
        int doCount = 0;
        do
        {
            doCount++;
        } while (doCount < 2);
    }
}