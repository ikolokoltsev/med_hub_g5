// To create users and bind them to a specific region, we need a region class.

// Work to be done
// Create a Region class that will contain fields:

// RegionEnum Name
// List locations
// Region enum:

// Skane
// Halland
// The thing is, we won't allow the creation of a new Region; it should be hard-coded from the very beginning. BUT, we need to add new locations to the Region.

namespace Regions
{
  public enum RegionEnum
  {
    Skane,
    Halland
  }

  public class Location
  {
    public string Name;

    public Location(string name)
    {
      Name = name;
    }
  }

  public class Region
  {
    public RegionEnum Name;
    public List<Location> Locations;

    // Konstruktor
    public Region(RegionEnum name)
    {
      Name = name;
      Locations = new List<Location>();
    }

    public void AddLocation(string locationName)
    {
      Location newLocation = new Location(locationName);
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
}