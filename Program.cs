using App;
List<User> requests = new();
List<User> users = new List<User>();



if (File.Exists("UserReg.txt"))
{
    foreach (var line in File.ReadAllLines("UserReg.txt"))
    {
        string[] parts = line.Split('-');
        if (parts.Length == 3)
        {
            requests.Add(new User(parts[0], parts[1], parts[2]));
        }
    }
}

bool is_running = true;

while (is_running)
{
    Console.Clear();

    Console.WriteLine("[2] - register");
    switch(Console.ReadLine())
    {
        case "2":
        {
          Console.WriteLine("E-mail Adress used for registration");
            string email = Console.ReadLine();
            bool EmailTaken = false;
            foreach (User user in users)
            {
                if (user.Email = email)
                {
                    EmailTaken = true;
                    break;
                }
            }
            if (EmailTaken)
            {
                Console.WriteLine("that email adress is taken ");
                break;
            }
            else
            {
                Console.Write("Choose a username: ");
                string username = Console.ReadLine();
                bool UsernameTaken = false;
                foreach (User user in users)
                {
                    if (user.Username = username)
                    {
                        UsernameTaken = true;
                        break;
                    }
                    if (UsernameTaken)
                    {
                        Console.WriteLine("That username is taken");
                        break;
                    }
                }

                Console.WriteLine("choose a password:  ");
                string password = Console.ReadLine();
                users.Add(new User(email, username, password));
            }       
        }break;
    }          
}