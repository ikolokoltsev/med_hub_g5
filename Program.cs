using Regions;

Region skane = new Region(RegionEnum.Skane);
Region halland = new Region(RegionEnum.Halland);

halland.AddLocation("Nyhems vårdcentral");
halland.AddLocation("Halmstad Akut");

skane.AddLocation("Lund Vårdcentral");
skane.AddLocation("Malmö Sjukhus");

skane.ShowLocations();
halland.ShowLocations();

Console.WriteLine("\nTryck på valfri tangent för att avsluta...");
Console.ReadKey();