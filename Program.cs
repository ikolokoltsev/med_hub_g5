using App;
using System.Diagnostics;

List<User> users = new List<User>();
List<Appointment> appointments = new List<Appointment>();

users.Add(new User("Loyd", "Lastname", 26, 19992208, "email@gmail.com", "pass", RegionEnum.Halland.ToString(),
    PermissionEnum.MenagePermissions | PermissionEnum.AssignToTheRegions | PermissionEnum.CreatePersonnelAccount | PermissionEnum.ShowPermissionsList));
users.Add(new User("Max", "Lastname", 26, 19992208, "gmail@gmail.com", "pass", RegionEnum.Halland.ToString(), 
    PermissionEnum.ManegeRegistrationRequest | PermissionEnum.AddLocations | PermissionEnum.ShowPatiensJournalEntries));
users.Add(new User("Lina", "Lastname", 26, 19992208, "lina@gmail.com", "pass", RegionEnum.Halland.ToString(), 
    PermissionEnum.ViewTheSchedule | PermissionEnum.ManageAppointments | PermissionEnum.ManegeRegistrationRequest));
users.Add(new User("Nick", "Lastname", 26, 19992208, "none@gmail.com", "pass", RegionEnum.Halland.ToString(), 
    PermissionEnum.ViewTheSchedule | PermissionEnum.ManageAppointments | PermissionEnum.ManegeRegistrationRequest));

// Events record that new registration happened
EventLog eventLog = new EventLog();

EventLog.AddEvent(firstName, EventType.RegistrationRequested, $"New user {firstName} registered in {regionName}.");


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

                Console.Clear();
                Debug.Assert(email != null);
                Debug.Assert(password != null);

                foreach (User user in users)
                {
                    if (user.TryLogin(email, password))
                    {
                        active_user = user;

                     //Eventslog to record that user logged in
                     EventLog.AddEvent(active_user.FirstName, EventType.Login, $"{active_user.FirstName} logged in.");              

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

                // Event record that new registration happened

                EventLog.AddEvent(firstName, EventType.RegistrationRequested, $"New user {firstName} registered in {regionName}.");
               
               
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
                if (active_user.HasPermission(PermissionEnum.MenagePermissions))
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
                            Console.WriteLine(".....Manage Appointments.....")
                            Console.WriteLine("ENTER one option: "Accept" or "Deny"?)");
                            string? userAction = Console.WriteLine;

                            Appointment appointment = new Appointment(active_user, LocationName, DateAndTime);
                            if (userAction?.Equals("Accept", StringComparison.OrdinalIgnoreCase) == true)
                            {
                            appointment.Approve(active_user);
                            Console.WriteLine("Appointment approved!");
                            }
                            else if (userAction?.Equals("Deny", StringComparison.OrdinalIgnoreCase) == true)
                            {
                                appointment.Deny(active_user);
                                Console.WriteLine("Appointment denied!");
                            }
                            else
                            {
                                Console.WriteLine("Please choose one of the options provided.")
                            }
                            Console.WriteLine("Press ENTER to continue...");
                            Console.ReadLine();
                        }break;
                
                    case "2":
                        if (active_user.HasPermission(PermissionEnum.ViewTheSchedule))
                        {
                            Console.Clear();
                            Console.WriteLine(".....View a Location List.....");
                            foreach (Location location in locations)
                            {
                                Console.WriteLine(location);
                            }
                            Console.WriteLine("Press ENTER to continue...");
                            Console.ReadLine();
                        }break;
                    
                    case "3":
                        if (active_user.HasPermission(PermissionEnum.ManageRegistrationRequest))
                        {
                            Console.Clear();
                            Console.WriteLine(".....Manage Patients Registration ==> Modify, Accept or Deny.....")
                            Console.WriteLine("ENTER one option: "Modify", Accept" or "Deny"?)");
                            string? userAction = Console.ReadLine();

                            // Create a "pending registration" user
                            User pendingUser = new User(
                                firstName: "Sarah",
                                lastName: "Lastname",
                                dateOfBirth: 26,
                                socialSecuriyNumber: 19951208,
                                email: "sara@gmail.com",
                                password: "pass22"
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
                            EventLog.AddEvent(active_user.Email, EventTypeEnum.RegistrationDenied, $"Denied registration request for {pendingUser.Email}.");
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
                            else
                            {
                                Console.WriteLine("Please choose one of the options provided.");
                            }
                            Console.WriteLine("Press ENTER to continue...");
                            Console.ReadLine();
                        else
                        {
                            Console.WriteLine("Access denied.");
                        }break;

                    case "4":
                        if (active_user.HasPermission(PermissionEnum.MenagePermissions))
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
                            Console.WriteLine($"Current permissions: {selectedUser.HasPermission}\n");");
                        }break;
               }
            }break;
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