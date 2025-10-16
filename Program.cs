using App;

List<Location> locations = new List<Location>();

locations.Add(new Location("Halmstad Hospital", Region.Hallands));
locations.Add(new Location("Varberg Clinic", Region.Hallands));
locations.Add(new Location("Lund Hospital", Region.Skane));
locations.Add(new Location("Malmö Clinic", Region.Skane));
Console.WriteLine("=== List of All Locations ===");
Console.WriteLine();

// Loop through all items in the list and print them
     foreach (Location location in locations)
     {
        Console.WriteLine(location.Name + " - " + location.BelongsToRegion);
     }

     Console.WriteLine("\nPress Enter to exit...");
     Console.ReadLine();