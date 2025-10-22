using App;
using System.Diagnostics;

List<User> users = new List<User>();

users.Add(new User("Loyd", "Lastname", 26, 19992208, "email@gmail.com", "pass", RegionEnum.Halland.ToString(),
    PermissionEnum.ManagePermissions | PermissionEnum.AssignToTheRegions));
users.Add(new User("Max", "Lastname", 26, 19992208, "gmail@gmail.com", "pass", RegionEnum.Halland.ToString(),
    PermissionEnum.ManegeRegistrationRequest | PermissionEnum.AddLocations));

users.Add(new User("Lina", "Lastname", 26, 19992208, "lina@gmail.com", "pass", RegionEnum.Halland.ToString()));
users.Add(new User("Nick", "Lastname", 26, 19992208, "none@gmail.com", "pass", RegionEnum.Halland.ToString()));

// Events record that new registration happened
// EventLog eventLog = new EventLog();

// EventLog.AddEvent(firstName, EventType.RegistrationRequested, $"New user {firstName} registered in {regionName}.");


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


                            //Eventslog to record that user logged in
                            // EventLog.AddEvent(active_user.FirstName, EventType.Login, $"{active_user.FirstName} logged in.");

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
                string password = PasswordInput();

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
                string password = PasswordInput();
                Console.Write("Enter regionName: ");
                string? regionName = Console.ReadLine();

                // Event record that new registration happened

                // EventLog.AddEvent(firstName, EventType.RegistrationRequested, $"New user {firstName} registered in {regionName}.");


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


static void ColorizedPrint(string print_message, ConsoleColor foreground_color = ConsoleColor.White,
    object background_color = null)
{
    if (background_color is ConsoleColor color)
    {
        Console.BackgroundColor = color;
    }

    Console.ForegroundColor = foreground_color;
    Console.WriteLine(print_message);
    Console.ResetColor();
}

static string StringUserInput()
{
    string? user_input = Console.ReadLine();
    Debug.Assert(user_input != null);
    return user_input;
}

static int IntUserInout()
{
    int.TryParse(Console.ReadLine(), out int user_input);
    Debug.Assert(user_input != null);
    return user_input;
}


static string PasswordInput()
{
    string password_acc = "";
    ConsoleKeyInfo key_pressed;
    while (true)
    {
        key_pressed = Console.ReadKey(true);
        if (key_pressed.Key.Equals(ConsoleKey.Enter))
        {
            ColorizedPrint("Enter pressed", ConsoleColor.Green);
            break;
        }
        else if (key_pressed.Key.Equals(ConsoleKey.Backspace))
        {
            if (password_acc.Length > 0)
            {
                password_acc = password_acc.Substring(0, password_acc.Length - 1);
            }
        }
        else
        {
            password_acc += key_pressed.KeyChar;
        }
    }

    return password_acc;
}

ColorizedPrint("Do the flip", ConsoleColor.DarkMagenta);
string pass = PasswordInput();

ColorizedPrint($"Ha-ha I saw your password {pass}", ConsoleColor.DarkRed);
// ConsoleKeyInfo key_pressed = Console.ReadKey(true);
//
// if (key_pressed.Key == ConsoleKey.Enter)
// {
//     ColorizedPrint("Enter pressed", ConsoleColor.Green);
// }else{
//     ColorizedPrint("Not enter", ConsoleColor.Red);
// }