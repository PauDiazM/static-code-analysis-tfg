public class ConditionalExample
{
    public static void Main()
    {
        int score = 75;
        string result = "Unknown";

        if (score >= 90)
        {
            result = "Excellent";
        }
        else if (score >= 60)
        {
            result = "Passed";
        }
        else
        {
            result = "Try again";
        }

        Console.WriteLine(result);
    }
}