using App;
using System.Diagnostics;
using static IOUtilsApp.IOUtils;

List<User> users = new List<User>();

users.Add(new User("Loyd", "Lastname", 26, 19992208, "email@gmail.com", "pass", RegionEnum.Halland.ToString(),
    PermissionEnum.MenagePermissions | PermissionEnum.AssignToTheRegions));
users.Add(new User("Max", "Lastname", 26, 19992208, "gmail@gmail.com", "pass", RegionEnum.Halland.ToString(), PermissionEnum.ManegeRegistrationRequest | PermissionEnum.AddLocations));

users.Add(new User("Lina", "Lastname", 26, 19992208, "lina@gmail.com", "pass", RegionEnum.Halland.ToString()));
users.Add(new User("Nick", "Lastname", 26, 19992208, "none@gmail.com", "pass", RegionEnum.Halland.ToString()));

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

// Permissions perm = new Permissions("Max", PermissionEnum.AssignToTheRegions | PermissionEnum.AssignToTheRegions | PermissionEnum.AssignToTheRegions);

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
                Console.Write("Enter roleTitle: ");
                string? roleTitle = Console.ReadLine();

                Console.Clear();
                Debug.Assert(email != null);
                Debug.Assert(password != null);
                Debug.Assert(roleTitle != null);

                foreach (User user in users)
                {
                    if (user.TryLogin(email, password))
                    {
                        active_user = user;

                        break;
                    }
                }

                menu = Menu.Main;
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
                Console.Write("Enter email: ");
                string? email = Console.ReadLine();
                Console.Write("Enter password: ");
                string? password = Console.ReadLine();
                Console.Write("Enter regionName: ");
                string? regionName = Console.ReadLine();
                Console.Write("Enter roleTitle: ");
                string? roleInput = Console.ReadLine();
                /*if (Enum.TryParse<Role>(roleInput, true, out Role roleTitle))
                {
                    Console.WriteLine($"You are {roleTitle}");
                }
                else
                {
                    Console.WriteLine("Invalid role entered.");
                    roleTitle = Role.Patient;
                }*/

                Console.Clear();
                Debug.Assert(firstName != null);
                Debug.Assert(lastName != null);
                Debug.Assert(dateOfBirth != null);
                Debug.Assert(socialSecurityNumber != null);
                Debug.Assert(email != null);
                Debug.Assert(password != null);
                Debug.Assert(regionName != null);

                users.Add(new User(firstName, lastName, dateOfBirth, socialSecurityNumber, email, password, regionName));


                // TODO: add saving file code here

                menu = Menu.Main;
            }
            break;

        case Menu.Main:
            {

            }
            break;
    }
}
/*
// Appointment system

// ResquestAppointment

List<Appointment> appointments = new();

static void RequestAppointment(string patientName, string locationName, string regionName)
{
    
}


Console.Clear();
            Console.WriteLine("\nWelcome! What would you like to do?");
            Console.WriteLine("....Appointment System....");
            Console.WriteLine("1. Request Appointment");
            Console.WriteLine("2. Register Appointment");
            Console.WriteLine("3. Modify Appointment");
            Console.WriteLine("4. Approve Appointment");
            Console.WriteLine("5. Logout");
            Console.WriteLine("6. Quit");
            Console.Write("Select one option and ENTER its number: ");

            switch(Console.ReadLine())
            {
                case "1":
                    {
                        Console.Clear();
                        Console.WriteLine("\nRegister an appointment as a patient");
                        if (CheckUserPermissions(active_user, Permission.RequestAppointment))
                        {
                            Console.WriteLine("Access Denied");
                            Console.ReadLine();
                        }

                        
                    }break;
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