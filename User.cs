namespace App;
class User
{
    public string Username;
    public string Email;
    public string Password;


    public User(string username, string password)
    {
        Username = username;
        Email = Email;
        Password = password;

    }

    public bool TryLogin(string username, string email, string password)
    {
        return Email == email && Username == username && Password == password;
    }

}