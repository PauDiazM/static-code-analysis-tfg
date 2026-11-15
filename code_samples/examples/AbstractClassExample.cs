namespace CodeSamples;

public abstract class Animal
{
    public string Name { get; set; }
    public int ExtremityCount { get; set; }
    public abstract void MakeSound();
}

public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Woof");
    }
}