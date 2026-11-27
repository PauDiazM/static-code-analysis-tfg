using System;

namespace CodeSamples;

public class Vehicle
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    private string _privateField = "This is a private field";

    public Vehicle(string brand, string model, int year)
    {
        Brand = brand;
        Model = model;
        Year = year;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Vehicle: {Year} {Brand} {Model}");
    }

    public static void Main()
    {
        Vehicle vehicle = new Vehicle("Volvo", "XC60", 2024);
        vehicle.DisplayInfo();
    }
}

public struct VehicleDimensions
{
    public int Length { get; set; }
    public int Width { get; set; }

    public List<Dictionary<string, int>, int> intList;
    Vehicle? vehicleNullable;
    Vehicle[] vehicleArray;
}

public enum VehicleKind
{
    Car = 1,
    Truck = 2,
    Motorcycle = 3
}

public record VehicleSummary(string Brand, string Model);