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
    