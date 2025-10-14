// To create users and bind them to a specific region, we need a region class.

// Work to be done
// Create a Region class that will contain fields:

// RegionEnum Name
// List locations
// Region enum:

// Skane
// Halland
// The thing is, we won't allow the creation of a new Region; it should be hard-coded from the very beginning. BUT, we need to add new locations to the Region.




namespace Region

{
  public class Regions
  {
    enum region
    {
      Halland,
      Skane,
    }
  }
}


class Region
{
  string region = "Halland";
  string region = "Skane";
  {
 Region MyRegion = new region();
  Console.WriteLine(MyRegion);
  }
}