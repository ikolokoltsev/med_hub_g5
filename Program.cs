using App;
using System.Diagnostics;
using System.Globalization;

List<User> users = new List<User>();

users.Add(new User("19840815-2344", RegionEnum.Halland, "pass", PermissionEnum.ManagePermissions |
                                                                PermissionEnum.AssignToTheRegions |
                                                                PermissionEnum.CreatePersonnelAccount |
                                                                PermissionEnum.ShowPermissionList |
                                                                PermissionEnum.AddLocations |
                                                                PermissionEnum.HandleRegions |
                                                                PermissionEnum.ManageAppointments |
                                                                PermissionEnum.ManageRegistrationRequest |
                                                                PermissionEnum.MarkJournalEntitiesLevel |
                                                                PermissionEnum.ShowPatientJournalEntities |
                                                                PermissionEnum.ViewTheSchedule));

users.Add(new User("19931107-3521", RegionEnum.Halland, "pass", PermissionEnum.ManageRegistrationRequest |
                                                                PermissionEnum.AddLocations |
                                                                PermissionEnum.ShowPatientJournalEntities |
                                                                PermissionEnum.ManageAppointments));

users.Add(new User("20010322-6541", RegionEnum.Skane, "pass",
    PermissionEnum.ViewTheSchedule | PermissionEnum.ManageAppointments | PermissionEnum.ManageRegistrationRequest));

users.Add(new User("19951201-0142", RegionEnum.Skane, "pass",
    PermissionEnum.ViewTheSchedule | PermissionEnum.ManageAppointments | PermissionEnum.ManageRegistrationRequest));


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

List<Appointment> appointments = new List<Appointment>();

List<RegistrationRequest> registration_requests = new List<RegistrationRequest>();

List<AppointmentRequest> appointment_requests = new List<AppointmentRequest>();


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
        string social_security_number = StringUserInput();
        Console.Write("Enter password: ");
        string password = PasswordInput();

        Console.Clear();

        foreach (User user in users)
        {
            if (user.TryLogin(social_security_number, password))
            {
                active_user = user;

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
                RegionEnum region = ShowRegionOptions();

                bool is_user_exists = users.Any(user => user.SocialSecurityNumber == socialSecurityNumber);
                bool is_registration_exists = registration_requests.Any(registration_request =>
                    registration_request.SocialSecurityNumber == socialSecurityNumber &&
                    registration_request.Status == RegistrationStatusEnum.Pending);
                if (is_user_exists || is_registration_exists)
                {
                    ColorizedPrint("You can't register with this credentials.", ConsoleColor.DarkRed);
                    ColorizedPrint("Press any key to continue...", ConsoleColor.DarkRed);
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    registration_requests.Add(new RegistrationRequest(socialSecurityNumber, password,
                        region));
                    ColorizedPrint($"Selected region {region}", ConsoleColor.DarkGreen);
                    EventLog.AddEvent(socialSecurityNumber, EventTypeEnum.RegistrationRequested,
                        $"Registration attempt for user with social security number {socialSecurityNumber}.");
                    ColorizedPrint("Congratulations! Your registration request was sent", ConsoleColor.DarkGreen);
                    ColorizedPrint("Press any key to continue...", ConsoleColor.DarkGreen);
                    Console.ReadKey();
                }

                return;
            }
        }
    }),
    new MenuItem("Exit", null)
});

void BookAppointment(string location)
{
    List<MenuItem> book_appointment_options = new List<MenuItem>
    {
        new MenuItem("Yes",
            () =>
            {
                appointment_requests.Add(new AppointmentRequest(active_user.SocialSecurityNumber, active_user.Region,
                    location));
            }),
        new MenuItem("No", null)
    };

    Menu book_appointment_menu =
        new Menu($"Do you want to book an appointment in {location}?", book_appointment_options);
    book_appointment_menu.ShowSelectionMenu();
}

void ChoseAppointmentLocation(RegionEnum user_region)
{
    List<MenuItem> chose_appointment_location_options = new List<MenuItem>();
    List<Location> user_locations = regions.Find(region => region.RegionName == user_region).Locations;
    foreach (Location location in user_locations)
    {
        chose_appointment_location_options.Add(new MenuItem($"{location.Name}",
            () => { BookAppointment(location.Name); }));
    }

    Menu chose_appointment_location_menu = new Menu("Book an appointment", chose_appointment_location_options);
    chose_appointment_location_menu.ShowMenu();
}

void RegistrationRequestManagement(RegistrationRequest registration_request, RegionEnum region)
{
    List<MenuItem> reg_req_management_options = new List<MenuItem>()
    {
        new MenuItem("Confirm", () =>
        {
            registration_request.Status = RegistrationStatusEnum.Confirmed;
            users.Add(new User(registration_request.SocialSecurityNumber, region, registration_request.GetPassword()));
            ColorizedPrint("Confirmed registration", ConsoleColor.DarkGreen);
            ColorizedPrint("Press any key to continue...", ConsoleColor.DarkGreen);
            Console.ReadKey();
        }),
        new MenuItem("Reject", () =>
        {
            registration_request.Status = RegistrationStatusEnum.Rejected;
            ColorizedPrint("Rejected registration", ConsoleColor.DarkRed);
            ColorizedPrint("Press any key to continue...", ConsoleColor.DarkRed);
            Console.ReadKey();
        }),
    };
    Menu reg_req_management_menu = new Menu("Registration Requests management", reg_req_management_options);
    reg_req_management_menu.ShowSelectionMenu();
}

void ShowRegistrationRequestsOptions(RegionEnum region)
{
    List<MenuItem> registration_options = new List<MenuItem>();
    List<RegistrationRequest> registration_requests_by_region =
        registration_requests.FindAll(request =>
            request.Region == region && request.Status == RegistrationStatusEnum.Pending);
    if (registration_requests_by_region.Count != 0)
    {
        foreach (RegistrationRequest request in registration_requests_by_region)
        {
            registration_options.Add(new MenuItem($"{request.SocialSecurityNumber}",
                () => { RegistrationRequestManagement(request, region); }));
        }
    }
    else
    {
        registration_options.Add(new MenuItem("No registration requests found. Go back.", null));
    }

    Menu registrations_request_menu = new Menu("Registration requests", registration_options);
    registrations_request_menu.ShowMenu();
}

RegionEnum ShowRegionOptions()
{
    List<MenuItem> region_options = new List<MenuItem>();
    foreach (RegionEnum region in RegionEnum.GetValues(typeof(RegionEnum)))
    {
        region_options.Add(new MenuItem($"{region}", null));
    }

    Menu region_options_menu = new Menu("Region Options", region_options);
    int selectedIndex = region_options_menu.ShowSelectionMenu();

    if (selectedIndex == -1)
    {
        return RegionEnum.Halland;
    }

    string selectedTitle = region_options[selectedIndex].Title;
    return (RegionEnum)Enum.Parse(typeof(RegionEnum), selectedTitle);
}

void ShowUserMenu(User user)
{
    List<MenuItem> user_menu_items = new List<MenuItem>();
    user_menu_items.Add(new MenuItem("Profile", () =>
    {
        ColorizedPrint("Profile", ConsoleColor.DarkGray);
        ColorizedPrint("------------------", ConsoleColor.DarkCyan);
        ColorizedPrint($"{user.FirstName}", ConsoleColor.Gray);
        ColorizedPrint($"{user.LastName}", ConsoleColor.Gray);
        ColorizedPrint($"{user.Gender}", ConsoleColor.Gray);
        ColorizedPrint($"{user.BirthDate}", ConsoleColor.Gray);
        ColorizedPrint($"{user.Email}", ConsoleColor.Gray);
        ColorizedPrint($"{user.PhoneNumber}", ConsoleColor.Gray);
        ColorizedPrint($"{user.Region}", ConsoleColor.Gray);
        ColorizedPrint("------------------", ConsoleColor.DarkCyan);
        ColorizedPrint("\nPress any key to go back.");
        Console.ReadKey(true);
    }));

    user_menu_items.Add(new MenuItem("View my journal", () =>
    {
        ColorizedPrint("You can view your journal here");
        Console.ReadKey(true);
    }));

    user_menu_items.Add(new MenuItem("Book an appointment", () => { ChoseAppointmentLocation(user.Region); }));

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
            () => { ShowRegistrationRequestsOptions(user.Region); }));
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

static int IntUserInput()
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
        ColorizedPrint("Personal number must be in format yyyyMMdd-XXXX.", ConsoleColor.DarkRed);
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