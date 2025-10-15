static class LocationData    // A static class is a class that cannot be created as an object.
{

    public static List<Location> AllLocations = new List<Location>    // Create a public static List that stores all available locations

    {

        new Location("Halmstad Hospital", Region.Hallands),
        new Location("Varberg Clinic", Region.Hallands),
        new Location("Lund Hospital", Region.Skane),
        new Location("Malmö Clinic", Region.Skane)
    };
     static void Main(string[] args)
    {
        Console.WriteLine("=== List of All Locations ===");
        Console.WriteLine();

        // Loop through all items in the list and print them
        foreach (var location in LocationData.AllLocations)
        {
           Console.WriteLine(location.Name + " - " + location.BelongsToRegion);
        }

        Console.WriteLine("\nPress Enter to exit...");
        Console.ReadLine();
    }
}
    