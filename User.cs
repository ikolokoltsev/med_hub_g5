namespace App;

enum Role
{
    Patient,
    Personnel,
    Admin,
}
public class User
{
    public string FirstName;
    public string LastName;
    public int DateOfBirth;
    public int SocialSecurityNumber;
    public string Email;
    public string _password;
    public string Role; // "Admin", "Personnel", "Patient".

    public User(string firstName, string lastName, int dateOfBirth, int socialSecurityNumber, string email, string password, string role)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        SocialSecurityNumber = socialSecurityNumber;
        Email = email;
        _password = password;
        Role = role;
    }

    public bool TryLogin(string email, string password)
    {
        return email == Email && password == _password;
    }

    public string ToSaveString()
    {
        return $"{FirstName}, {LastName}; {DateOfBirth}; {SocialSecurityNumber}; {Role}; {Email};{_password}";
    }

}


