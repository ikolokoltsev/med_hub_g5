namespace App;

class Location    //This represents one healthcare location (like a hospital )
{
    public string Name;     // Field to store the name of the location (name of Hospital in each region)
    public string BelongsToRegion;     // Field to store which region this location belongs to

    public Location(string name, string region) // Constructor: runs automatically when we create a new Location
    {
        Name = name;      // assign the name we pass in to the field
        BelongsToRegion = region;  // assign the region we pass in

    }

}









