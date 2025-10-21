namespace App;
class RegistrationRequest
{
    public string Email;
    public string Region;
    public string Password;
    public string ManegeRegistrationRequest;
    public string Name;
    public string LastName;
    public string SocialNumber;

    public RegistrationRequest(string email, string name, string lname, string password, string snumber, string region)
    {
        Email = email;
        Name = name;
        Password = password;
        Region = region;
        LastName = lname;
        SocialNumber = snumber;
        
    }
    
}