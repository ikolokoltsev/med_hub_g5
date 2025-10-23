namespace App;

enum RegistartonstatusEnum
{
    pending, // The User awaits a confirmation
    completed, // The resgitration is approved by admin
    Denied, // the application for an account has been denied,
}

class RegistrationRequest
{
    public string Email;
    public string Region;
    public string Password;
    public string Name;
    public string LastName;


    public RegistrationRequest(string email, string name, string lname, string password, string region)
    {
        Email = email;
        Name = name;
        Password = password;
        Region = region;
        LastName = lname;

    }

} 