enum Region  // This defines "regions" that locations can belong to,
{
    Hallands,
    Skane    // we use enum values like Region,Skane — which is safer than Skåne
}

class Location    //This represents one healthcare location (like a hospital )
{
    public string Name;     // Field to store the name of the location (name of Hospital in each region)
    public Region BelongsToRegion;     // Field to store which region this location belongs to

    public Location(string name, Region region) // Constructor: runs automatically when we create a new Location
    {
        Name = name;      // assign the name we pass in to the field
        BelongsToRegion = region;  // assign the region we pass in

    }

    public override string ToString()    // Override the ToString() method from base class Object
    {
        return $"{Name} ({BelongsToRegion})";   // Return a readable string showing both name and region
    }
}
static class LocationData    // A static class is a class that cannot be created as an object.
{

    public static List<Location> AllLocations = new List<Location>    // Create a public static List that stores all available locations
    {


    new Location("Halmstad Hospital", Region.Hallands),
    new Location("Varberg Clinic", Region.Hallands),
    new Location("Lund Hospital", Region.Skane),
    new Location("Malmö Clinic", Region.Skane)
    };
}

    
   


  

