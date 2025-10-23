using App;


using System.Diagnostics;
using System.Globalization;

List<User> users = new List<User>();
List<Appointment> appointments = new List<Appointment>();

users.Add(new User("Loyd", "Lastname", 26, 19992208, "email@gmail.com", "pass", RegionEnum.Halland.ToString(),
    PermissionEnum.ManagePermissions | PermissionEnum.AssignToTheRegions | PermissionEnum.CreatePersonnelAccount | PermissionEnum.ShowPermissionList));
users.Add(new User("Max", "Lastname", 26, 19992208, "gmail@gmail.com", "pass", RegionEnum.Halland.ToString(), 
    PermissionEnum.ManegeRegistrationRequest | PermissionEnum.AddLocations | PermissionEnum.ShowPatientJournalEntries));
users.Add(new User("Lina", "Lastname", 26, 19992208, "lina@gmail.com", "pass", RegionEnum.Halland.ToString(), 
    PermissionEnum.ViewTheSchedule | PermissionEnum.ManageAppointments | PermissionEnum.ManegeRegistrationRequest));
users.Add(new User("Nick", "Lastname", 26, 19992208, "none@gmail.com", "pass", RegionEnum.Halland.ToString(), 
    PermissionEnum.ViewTheSchedule | PermissionEnum.ManageAppointments | PermissionEnum.ManegeRegistrationRequest));

// Record registration Event for each user
foreach (var user in users)
{
    EventLog.AddEvent(user.FirstName, EventTypeEnum.RegistrationRequested,
        $"New user {user.FirstName} registered in {user.RegionName}.");
}



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
                     EventLog.AddEvent(active_user.FirstName, EventTypeEnum.Login, $"{active_user.FirstName} logged in.");              

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

                EventLog.AddEvent(firstName, EventTypeEnum.RegistrationRequested, $"New user {firstName} registered in {regionName}.");
               
               
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
                Console.Clear();
                Console.WriteLine($"Welcome {active_user.FirstName}!");
                Console.WriteLine("\n---------------------------------");

                // Display only what this user has permission for
                if (active_user.HasPermission(PermissionEnum.ManageAppointments))
                {
                    Console.WriteLine("\n1. Manage Appointments (Modify, Accept or Deny)");
                }
                if (active_user.HasPermission(PermissionEnum.ViewTheSchedule))
                {
                    Console.WriteLine("\n2. View a Location Schedule");
                }
                if (active_user.HasPermission(PermissionEnum.ManegeRegistrationRequest))
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
                if (active_user.HasPermission(PermissionEnum.ShowPatientJournalEntries))
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
                            Console.Clear();
                            Console.WriteLine(".....Manage Appointments.....");
                            Console.Write("ENTER one option: ");
                            string? userAction = Console.ReadLine();

                            Appointment appointment = new Appointment(bookedBy: patient, managedBy: Doctor,  location: "Varberg Clinic", dateAndTime: new DateTime(2025, 11, 1, 9, 0, 0));
                            if (userAction?.Equals("Accept", StringComparison.OrdinalIgnoreCase) == true)
                            {
                                appointment.Accept(active_user);
                                Console.WriteLine("Appointment approved!");
                            }
                            else if (userAction?.Equals("Deny", StringComparison.OrdinalIgnoreCase) == true)
                            {
                                appointment.Deny(active_user);
                                Console.WriteLine("Appointment denied!");
                            }
                            else
                            {
                                Console.WriteLine("Please choose one of the options provided.");
                            }
                            Console.WriteLine("Press ENTER to continue...");
                            Console.ReadLine();
                        } break;

                    case "2":
                        if (active_user.HasPermission(PermissionEnum.ViewTheSchedule))
                        {
                            Console.Clear();
                            Console.WriteLine(".....View a Location List.....");
                            foreach (Location location in locations)
                            {
                                Console.WriteLine(location);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Access denied.");
                            Console.WriteLine("Press ENTER to continue...");
                            Console.ReadLine();
                        }break;

                    case "3":
                        if (active_user.HasPermission(PermissionEnum.ManegeRegistrationRequest))
                        {
                            Console.Clear();
                            Console.WriteLine(".....Manage Patients Registration ==> Modify, Accept or Deny.....");
                            Console.WriteLine("ENTER one option:");
                            string? userAction = Console.ReadLine();

                            // Create a "pending registration" user
                            User pendingUser = new User(
                                firstName: "Sarah",
                                lastName: "Lastname",
                                dateOfBirth: 26,
                                socialSecuriyNumber: 19951208,
                                email: "sara@gmail.com",
                                password: "pass22",
                                regionName: RegionEnum.Halland.ToString(),
                                permission: PermissionEnum.None
                            );

                            Appointment appointment = new Appointment(active_user, LocationName, DateAndTime);
                            if (userAction?.Equals("Accept", StringComparison.OrdinalIgnoreCase) == true)
                            {
                                Console.WriteLine("Registration for {pendingUser.FirstName} {pendingUser.LastName} has been accepted.");

                                // Event log
                                EventLog.AddEvent(active_user.Email, EventTypeEnum.RegistrationAccepted, $"Accepted registration request for {pendingUser.Email}.");
                            }
                            else if (userAction?.Equals("Deny", StringComparison.OrdinalIgnoreCase) == true)
                            {
                                Console.WriteLine("Registration for {pendingUser.FirstName} {pendingUser.LastName} has been denied.");

                                // Event log
                                EventLog.AddEvent(active_user.Email, EventTypeEnum., $"Denied registration request for {pendingUser.Email}.");
                            }
                            else if (userAction?.Equals("Modify", StringComparison.OrdinalIgnoreCase) == true)
                            {
                                Console.WriteLine("Modify registration details for {pendingUser.FirstName} {pendingUser.LastName}");
                                Console.Write("Enter new email: ");
                                string? newEmail = Console.ReadLine();
                                Console.Write("Enter new region: ");
                                string? newRegion = Console.ReadLine();

                                pendingUser.Email = newEmail ?? pendingUser.Email;
                                pendingUser.RegionName = newRegion ?? pendingUser.RegionName;

                                Console.WriteLine("Registration modified");
                                // Event log
                                EventLog.AddEvent(active_user.Email, EventTypeEnum.RegistrationModified, $"Modified registration for {pendingUser.Email}.");
                            }
                            else
                            {
                                Console.WriteLine("Please choose one of the options provided.");
                            }
                            else
                            {
                                Console.WriteLine("Access denied.");
                                Console.WriteLine("Press ENTER to continue...");
                                Console.ReadLine();
                            }
                        } break;

                    case "4":
                        if (active_user.HasPermission(PermissionEnum.ManagePermissions))
                        {
                            Console.Clear();
                            Console.WriteLine(".....Manage User Perissions.....");
                            Console.WriteLine("--------------------------------");

                            // Show all registered users
                            for (int i = 0; i < users.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {users[i].FirstName} {users[i].LastName} | Current Permission: {users[i].Permission}");
                            }
                            Console.Write("\nSelect a user number to modify permissions: ");
                            if (!int.TryParse(Console.ReadLine(), out int userIndex) || userIndex < 1 || userIndex > users.Count)
                            {
                                Console.WriteLine("Invalid user selection.");
                                Console.ReadLine();
                                break;
                            }
                            User selectedUser = users[userIndex - 1];

                            Console.Clear();
                            Console.WriteLine($"Selected user: {selectedUser.FirstName} {selectedUser.LastName}");
                            Console.WriteLine($"Current permissions: {selectedUser.HasPermission}\n"); ");
                        } break;
                        
                    
                }
           }break;
    } 
}


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
