// See https://aka.ms/new-console-template for more information

using App;
using System.Diagnostics;


List<User> users = new();

User? active_user = null;
Menu menu = Menu.None;

bool running = true;

while (running)
{
    Console.Clear();
    switch(menu)
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

        }break;

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
            Debug.Assert(role != null);
            Debug.Assert(email != null);
            Debug.Assert(password != null);

            users.Add(new User(firstName, lastName, dateOfBirth, socialSecurityNumber, role, email, password));

            // TODO: add saving file code here

            menu = Menu.None;
            
        }break;
           
    }
}
