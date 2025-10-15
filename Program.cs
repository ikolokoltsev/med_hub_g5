// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!")

using App;
using System.Collections;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Transactions;


List<User> users = new();

/*
Be able to create a new user with full name, date of birth, email, password.
To check if the user already exists by email and password.
Show the list of the users by their role.
*/

if (File.Exists("users.save"))
{
    string[] lines = File.ReadAllLines("users.save");
    foreach (string line in lines)
    {
        string[] data = line.Split(",");
        users.Add(new (data[0], data[1], data[2], data[3], data[4], data[5], data[6]));
    }
}

User? active_user = null;
Menu menu = Menu.None;

bool running = true;

while (running)
{
    try { Console.Clear(); } catch { }
    switch(menu)
    {
        case Menu.None:
        {
            if (active_user = null)
            {
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Quit");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1": menu = Menu.Login; break;
                    case "2": menu = Menu.Register; break;
                    case "3": running = false; break;

                }
            }
            else
            {
                Console.WriteLine("Invalid option");
            }
        }break;

        case Menu.Login:
        {
            try { Console.Clear(); } catch { }
            Console.Write("Enter email: ");
            string? email = Console.ReadLine();
            Console.Write("Enter password: ");
            string? password = Console.ReadLine();

            try { Console.Clear(); } catch { }
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

        }break;

        case Menu.Register:
        {
            try { Console.Clear; } catch { }
            Console.Write("Enter firstname: ");
            string? firstName = Console.ReadLine();
            Console.Write("Enter lastname: ");
            string? lastName = Console.ReadLine();
            Console.Write("Enter Date of birth: ");
            int? dateOfBirth = int.Parse(Console.ReadLine());
            Console.Write("Enter Social Security Number: ");
            int? socialSecurityNumber = int.Parse(Console.ReadLine());
            Console.Write("Enter role/status: ");
            string? role = Console.ReadLine();
            Console.Write("Enter email: ");
            string? email = Console.ReadLine();
            Console.Write("Enter password: ");
            string? password = Console.ReadLine();

            try { Console.Clear(); } catch { }
            Debug.Assert(firstName != null);
            Debug.Assert(lastName != null);
            Debug.Assert(dateOfBirth != null);
            Debug.Assert(socialSecurityNumber != null);
            Debug.Assert(role != null);
            Debug.Assert(email != null);
            Debug.Assert(password != null);

            users.Add(new User(firstName, lastName, dateOfBirth, socialSecurityNumber, role, email, password));

            // save user to file
            string[] users_save_string = new string[users.Count];
            for (int i = 0; i < users.Count; i++)
            {
                users_save_string[i] = users[i].ToSaveString();
            }
            File.WriteAllLines("users.save", users_save_string);

            menu = menu.None;
            
        }break;
        

        
    }
}



enum ParticipantStatus
{
    Patient,
    Personnel,
    Admin,
}

