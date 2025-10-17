using App;
using System.Diagnostics;
using static IOUtilsApp.IOUtils;

List<User> users = new();
users.Add(new User("Loyd", "Lastname", 26, 19992208, "email@gmail.com", "pass"));
users.Add(new User("Max", "Lastname", 26, 19992208, "gmail@gmail.com", "pass"));
users.Add(new User("Lina", "Lastname", 26, 19992208, "lina@gmail.com", "pass"));
users.Add(new User("Nick", "Lastname", 26, 19992208, "none@gmail.com", "pass"));

List<Region> regions = new List<Region>();
regions.Add(new Region(RegionEnum.Skane));
regions.Add(new Region(RegionEnum.Halland));

static void InitiateRegionWithLocations(List<Region> regions, List<Location> locations)
{
    foreach (Region region in regions)
    {
        List<Location> region_locations = locations.FindAll(location => location.BelongsToRegion == region.Name);
        region.InitLocations(region_locations);
    }
}

List<Location> locations = new List<Location>();

locations.Add(new Location("Halmstad Hospital", RegionEnum.Halland.ToString()));
locations.Add(new Location("Varberg Clinic", RegionEnum.Halland.ToString()));
locations.Add(new Location("Lund Hospital", RegionEnum.Skane.ToString()));
locations.Add(new Location("Malmö Clinic", RegionEnum.Skane.ToString()));

InitiateRegionWithLocations(regions, locations);

User? active_user = null;
Menu menu = Menu.None;

bool running = true;

while (running)
{
    Console.Clear();
    switch (menu)
    {
        case Menu.None:
        {
            if (active_user == null)
            {
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Quit");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1": menu = Menu.Register; break;
                    case "2": menu = Menu.Login; break;
                    case "3": running = false; break;
                }
            }
            else
            {
                switch (Console.ReadLine())
                {
                    case "1":
                    case "Add Location":
                        Console.Clear();
                        Console.Write("Chose a region: ");


                        break;
                }
            }
        }
            break;
        case Menu.Login:
        {
            Console.Clear();
            Console.Write("Enter email: ");
            string? email = Console.ReadLine();
            Console.Write("Enter password: ");
            string? password = Console.ReadLine();

            Console.Clear();
            Debug.Assert(email != null);
            Debug.Assert(password != null);

            foreach (User user in users)
            {
                if (user.TryLogin(email, password))
                {
                    active_user = user;
                    break;
                }
            }

            menu = Menu.None;
        }
            break;

        case Menu.Register:
        {
            Console.Clear();
            Console.Write("Enter firstname: ");
            string? firstName = Console.ReadLine();
            Console.Write("Enter lastname: ");
            string? lastName = Console.ReadLine();
            Console.Write("Enter Date of birth: ");
            int dateOfBirth = int.Parse(Console.ReadLine());
            Console.Write("Enter Social Security Number: ");
            int socialSecurityNumber = int.Parse(Console.ReadLine());
            Console.Write("Enter role/status: ");
            string? role = Console.ReadLine();
            Console.Write("Enter email: ");
            string? email = Console.ReadLine();
            Console.Write("Enter password: ");
            string? password = Console.ReadLine();

            Console.Clear();
            Debug.Assert(firstName != null);
            Debug.Assert(lastName != null);
            Debug.Assert(dateOfBirth != null);
            Debug.Assert(socialSecurityNumber != null);
            // Debug.Assert(role != null);
            Debug.Assert(email != null);
            Debug.Assert(password != null);

            users.Add(new User(firstName, lastName, dateOfBirth, socialSecurityNumber, email, password));

            // TODO: add saving file code here

            menu = Menu.None;
        }
            break;
    }
}

// TODO: Implement location menu
/*
Console.WriteLine("=== List of All Locations ===");
Console.WriteLine();

// Loop through all items in the list and print them
foreach (Location location in locations)
     {
        Console.WriteLine(location.Name + " - " + location.BelongsToRegion);
     }

     Console.WriteLine("\nPress Enter to exit...");
     Console.ReadLine();
*/