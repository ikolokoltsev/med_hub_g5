namespace User
{
    // Class for Users

    public class Users
    {
        public string Username;
        public string Password;

        public Users(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public bool TryLogin(string username, string password)
        {
            return username == Username && password == _password;
        }
    }
}

