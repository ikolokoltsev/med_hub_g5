namespace App;

public enum RegionEnum
{
    Skane,
    Halland
}

class Region
{
    public RegionEnum RegionName;
    public List<Location> Locations;

    // Konstruktor
    public Region(RegionEnum region)
    {
        RegionName = region;
    }

    public void AddLocation(string locationName)
    {
        Location newLocation = new Location(locationName, RegionName);
        Locations.Add(newLocation);
        Console.WriteLine("Location " + locationName + " has been added to the region " + RegionName + ".");
    }

    public void InitLocations(List<Location> locations)
    {
        Locations = locations;
    }

    public void ShowLocations()
    {
        Console.WriteLine("Locations in regionen " + RegionName + ":");
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