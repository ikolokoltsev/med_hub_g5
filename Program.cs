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

Menu main_menu = new Menu("Welcome to the MedHub", new List<MenuItem>
{
    new MenuItem("Login", () =>
    {
        // while (true)
        // {
        active_user = null;
        Console.CursorVisible = true;
        Console.Write("Enter Social Security Number: ");
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

        if (active_user != null)
        {
            ShowUserMenu(active_user);
        }
        else
        {
            ColorizedPrint("Invalid credentials", ConsoleColor.DarkRed);
            ColorizedPrint("Press any key to continue...", ConsoleColor.DarkRed);
            Console.ReadKey();
        }
        // }
    }),
    new MenuItem("Register", () =>
    {
        while (true)
        {
            Console.CursorVisible = true;
            Console.Write("Enter Social Security Number: ");
            string socialSecurityNumber = StringUserInput();
            bool is_valid_ssn = IsValid(socialSecurityNumber);

            if (!is_valid_ssn)
            {
                Console.WriteLine("Press any key to try again, or press escape to exit");
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key.Equals(ConsoleKey.Escape))
                {
                    return;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                Console.Write("Enter password: ");
                string password = PasswordInput();

                EventLog.AddEvent(socialSecurityNumber, EventTypeEnum.RegistrationRequested,
                    $"Registration attempt for user with social security number {socialSecurityNumber}.");

                return;
            }
        }
    }),
    new MenuItem("Exit", null
    )
});


void ShowUserMenu(User user)
{
    List<MenuItem> user_menu_items = new List<MenuItem>();
    if (user.HasPermission(PermissionEnum.ManageAppointments))
    {
        user_menu_items.Add(new MenuItem("Manage Appointments",
            () =>
            {
                ColorizedPrint("You can manage appointments for this user");
                Console.ReadKey(true);
            }));
    }

    if (user.HasPermission(PermissionEnum.ViewTheSchedule))
    {
        user_menu_items.Add(new MenuItem("View Schedule",
            () =>
            {
                ColorizedPrint("You can manage view the schedule here");
                Console.ReadKey(true);
            }));
    }

    if (user.HasPermission(PermissionEnum.ManageRegistrationRequest))
    {
        user_menu_items.Add(new MenuItem("Registration requests",
            () =>
            {
                ColorizedPrint("You can manage registrations requests here");
                Console.ReadKey(true);
            }));
    }

    if (user.HasPermission(PermissionEnum.ManagePermissions))
    {
        user_menu_items.Add(
            new MenuItem("Manage Permissions", () =>
            {
                ColorizedPrint("You can manage permissions here");
                Console.ReadKey(true);
            }));
    }

    if (user.HasPermission(PermissionEnum.AddLocations))
    {
        user_menu_items.Add(new MenuItem("Add locations", () =>
        {
            ColorizedPrint("You can add locations here");
            Console.ReadKey(true);
        }));
    }

    if (user.HasPermission(PermissionEnum.AssignToTheRegions))
    {
        user_menu_items.Add(
            new MenuItem("Assign to region", () =>
            {
                ColorizedPrint("You can assign to the region here");
                Console.ReadKey(true);
            }));
    }

    if (user.HasPermission(PermissionEnum.CreatePersonnelAccount))
    {
        user_menu_items.Add(new MenuItem("Create account for the personnel",
            () =>
            {
                ColorizedPrint("You can create an account for the personnel here");
                Console.ReadKey(true);
            }));
    }

    if (user.HasPermission(PermissionEnum.ShowPermissionList))
    {
        user_menu_items.Add(new MenuItem("Show permissions",
            () =>
            {
                ColorizedPrint("You can see other users permissions here here");
                Console.ReadKey(true);
            }));
    }

    if (user.HasPermission(PermissionEnum.ShowPatientJournalEntities))
    {
        user_menu_items.Add(new MenuItem("View journal entities",
            () =>
            {
                ColorizedPrint("You can see journal entities here");
                Console.ReadKey(true);
            }));
    }

    user_menu_items.Add(new MenuItem("Logout", null));


    Menu user_menu = new Menu($"Hello dear {active_user.FirstName} {active_user.LastName}", user_menu_items);
    user_menu.ShowMenu();
}

main_menu.ShowMenu();



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