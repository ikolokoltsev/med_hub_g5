namespace App;

enum RegionEnum
{
    Skane,
    Halland
}

class Region
{
    public string Name;
    public List<Location> Locations;

    // Konstruktor
    public Region(RegionEnum name)
    {
        Name = name.ToString();
        Locations = new List<Location>();
    }

    public void AddLocation(string locationName)
    {
        Location newLocation = new Location(locationName, Name);
        Locations.Add(newLocation);
        Console.WriteLine("Location " + locationName + " has been added to the region " + Name + ".");
    }

    public void ShowLocations()
    {
        Console.WriteLine("Locations in regionen " + Name + ":");
        if (Locations.Count == 0)
        {
            Console.WriteLine("No locations yet");
        }
        else
        {
            foreach (Location loc in Locations)
            {
                Console.WriteLine(" - " + loc.Name);
            }
        }
    }
}