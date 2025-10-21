namespace App;
{
enum RoleEnum
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
    // public string? UserRole = RoleEnum.Patient.ToString(); // "Admin", "Personnel", "Patient".


    public User(string firstName, string lastName, int dateOfBirth, int socialSecurityNumber, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        SocialSecurityNumber = socialSecurityNumber;
        Email = email;
        _password = password;
        // UserRole = role;

    }


    public bool TryLogin(string email, string password)
    {
        return email == Email && password == _password;
    }

    public string ToSaveString()
    {
        return $"{FirstName}, {LastName}; {DateOfBirth}; {SocialSecurityNumber}; {Email};{_password}"}
        ;
}
}



