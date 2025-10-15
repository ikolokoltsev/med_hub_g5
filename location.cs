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

}



    
   


  

