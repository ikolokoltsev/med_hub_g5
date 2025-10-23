using App;
using System.Diagnostics;
using System.Globalization;

List<User> users = new List<User>();
List<Appointment> appointments = new List<Appointment>();

users.Add(new User("19840815-2344", RegionEnum.Halland, "pass", PermissionEnum.ManagePermissions |
                                                                PermissionEnum.AssignToTheRegions |
                                                                PermissionEnum.CreatePersonnelAccount |
                                                                PermissionEnum.ShowPermissionList));

users.Add(new User("19931107-3521", RegionEnum.Halland, "pass", PermissionEnum.ManageRegistrationRequest |
                                                                PermissionEnum.AddLocations |
                                                                PermissionEnum.ShowPatientJournalEntities));

users.Add(new User("20010322-6541", RegionEnum.Skane, "pass",
    PermissionEnum.ViewTheSchedule | PermissionEnum.ManageAppointments | PermissionEnum.ManageRegistrationRequest));

users.Add(new User("19951201-0142", RegionEnum.Skane, "pass",
    PermissionEnum.ViewTheSchedule | PermissionEnum.ManageAppointments | PermissionEnum.ManageRegistrationRequest));
ColorizedPrint("All users:");
ColorizedPrint("------------------", ConsoleColor.Cyan);
int index = 0;
foreach (User user in users)
{
    index++;
    ColorizedPrint($"{user.FirstName}", ConsoleColor.Gray);
    ColorizedPrint($"{user.LastName}", ConsoleColor.Gray);
    ColorizedPrint($"{user.Gender}", ConsoleColor.Gray);
    ColorizedPrint($"{user.BirthDate}", ConsoleColor.Gray);
    ColorizedPrint($"{user.Email}", ConsoleColor.Gray);
    ColorizedPrint($"{user.PhoneNumber}", ConsoleColor.Gray);
    ColorizedPrint($"{user.Region}", ConsoleColor.Gray);
    if (index != users.Count)
    {
        ColorizedPrint("------------------", ConsoleColor.DarkMagenta);
    }
}

ColorizedPrint("------------------", ConsoleColor.Cyan);


// Record registration Event for each user
foreach (var user in users)
{
    EventLog.AddEvent(user.FirstName, EventTypeEnum.RegistrationRequested,
        $"New user {user.FirstName} registered in {user.Region}.");
}


List<Region> regions = new List<Region>();
regions.Add(new Region(RegionEnum.Skane));
regions.Add(new Region(RegionEnum.Halland));

static void InitiateRegionWithLocations(List<Region> regions, List<Location> locations)
{
    foreach (Region region in regions)
    {
        List<Location> region_locations = locations.FindAll(location => location.BelongsToRegion == region.RegionName);
        region.InitLocations(region_locations);
    }
}

List<Location> locations = new List<Location>();

locations.Add(new Location("Halmstad Hospital", RegionEnum.Halland));
locations.Add(new Location("Varberg Clinic", RegionEnum.Halland));
locations.Add(new Location("Lund Hospital", RegionEnum.Skane));
locations.Add(new Location("Malmö Clinic", RegionEnum.Skane));

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

                    //Eventslog to record that user logged in
                    EventLog.AddEvent(active_user.FirstName, EventTypeEnum.Login,
                        $"{active_user.FirstName} logged in.");

                    break;
                }
            }

            menu = Menu.Main;
        }
            break;

        case Menu.Register:
        {
            Console.Clear();
            Console.Write("Enter Social Security Number: ");
            string socialSecurityNumber = StringUserInput();
            bool is_valid_ssn = IsValid(socialSecurityNumber);

            if (!is_valid_ssn)
            {
                Console.WriteLine("Press any key to try again, or press escape to exit");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key.Equals(ConsoleKey.Escape))
                {
                    ColorizedPrint("Escape pressed", ConsoleColor.DarkGreen);
                    menu = Menu.None;
                    break;
                }
                else
                {
                    break;
                }
            }

            Console.Write("Enter password: ");
            string password = PasswordInput();


            EventLog.AddEvent(socialSecurityNumber, EventTypeEnum.RegistrationRequested,
                $"Registration attempt for user with social security number {socialSecurityNumber}.");


            // TODO: add saving file code here

            // menu = Menu.Main;
        }
            break;

        case Menu.Main:
        {
            Console.Clear();
            Console.WriteLine($"Welcome {active_user.FirstName}!");
            Console.WriteLine("\n---------------------------------");

            // Show only the options the Logged-in User has access to
            if (active_user.HasPermission(PermissionEnum.ManageAppointments))
            {
                Console.WriteLine("\n1. Manage Appointments (Modify, Accept or Deny)");
            }

            if (active_user.HasPermission(PermissionEnum.ViewTheSchedule))
            {
                Console.WriteLine("\n2. View a Location Schedule");
            }

            if (active_user.HasPermission(PermissionEnum.ManageRegistrationRequest))
            {
                Console.WriteLine("\n3. Manage Patients Registration (Accept or Deny)");
            }

            if (active_user.HasPermission(PermissionEnum.ManagePermissions))
            {
                Console.WriteLine("\n4. Manage User Permissions");
            }

            if (active_user.HasPermission(PermissionEnum.AddLocations))
            {
                Console.WriteLine("\n5. Add Location");
            }

            if (active_user.HasPermission(PermissionEnum.AssignToTheRegions))
            {
                Console.WriteLine("\n6. Assign to the Regions");
            }

            if (active_user.HasPermission(PermissionEnum.CreatePersonnelAccount))
            {
                Console.WriteLine("\n7. Create Personnel Account");
            }

            if (active_user.HasPermission(PermissionEnum.ShowPermissionList))
            {
                Console.WriteLine("\n8. Show Permissions List");
            }

            if (active_user.HasPermission(PermissionEnum.ShowPatientJournalEntities))
            {
                Console.WriteLine("\n9. Show Patient Journal Enteries");
            }

            Console.WriteLine("10. Logout");
            Console.WriteLine("11. Quit");
            Console.Write("Choose an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    if (active_user.HasPermission(PermissionEnum.ManageAppointments))
                    {
                        Console.WriteLine("ENTER what you want to do: \"Accept\" or \"Deny\"?)");
                        if (Console.ReadLine() == "Accept")
                        {
                        }
                    }

                    break;
            }
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

static bool IsValid(string personalNumber)
{
    return Validate(personalNumber);
}

static bool Validate(string personalNumber)
{
    if (personalNumber.Length != 13)
    {
        ColorizedPrint("Personal number must be in format YYYYMMDD-XXXX.", ConsoleColor.DarkRed);
        return false;
    }

    if (personalNumber[8] != '-')
    {
        ColorizedPrint("Personal number must contain a dash (-) at position 9.", ConsoleColor.DarkRed);
        return false;
    }

    string datePart = personalNumber.Substring(0, 8);
    string lastFourDigits = personalNumber.Substring(9, 4);

    if (!datePart.All(digit => char.IsDigit(digit)))
    {
        ColorizedPrint("The date part (YYYYMMDD) must contain only digits.", ConsoleColor.DarkRed);
        return false;
    }

    if (!lastFourDigits.All(digit => char.IsDigit(digit)))
    {
        ColorizedPrint("The last 4 characters must be digits.", ConsoleColor.DarkRed);
        return false;
    }

    if (!DateTime.TryParseExact(datePart, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None,
            out DateTime birthDate))
    {
        ColorizedPrint("The date part is not a valid date.", ConsoleColor.DarkRed);
        return false;
    }

    if (birthDate > DateTime.Today)
    {
        ColorizedPrint("The birth date cannot be in the future.", ConsoleColor.DarkRed);
        return false;
    }

    DateTime minDate = DateTime.Today.AddYears(-150);
    if (birthDate < minDate)
    {
        ColorizedPrint("The birth date cannot be more than 150 years ago.", ConsoleColor.DarkRed);
        return false;
    }

    return true;
}