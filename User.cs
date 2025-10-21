namespace App;

[Flags]
public enum PermissionEnum
{
    None = 0,
    MenagePermissions = 1 << 0,
    AssignToTheRegions = 1 << 1,
    ManegeRegions = 1 << 2,
    AddLocations = 1 << 3,
    CreatePersonnel = 1 << 4,
    ViewPermissionList = 1 << 5,
    ManegeRegistrationRequest = 1 << 6,
    ViewAllUsers = 1 << 7,
    AddNewUser = 1 << 8,
    ManageLocations = 1 << 9,
}

// TODO: implement setting the locale role depending on the context. 
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
    private PermissionEnum _permission;
    // public string? UserRole = RoleEnum.Patient.ToString(); // "Admin", "Personnel", "Patient".

    public User(string firstName, string lastName, int dateOfBirth, int socialSecurityNumber, string email,
        string password, PermissionEnum initialPermission = PermissionEnum.None)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        SocialSecurityNumber = socialSecurityNumber;
        Email = email;
        _password = password;
        _permission = initialPermission;
        // UserRole = role;
    }

    public bool TryLogin(string email, string password)
    {
        return email == Email && password == _password;
    }

    // TODO: AddPermission and RemovePermission probably should be moved into the PermissionManager class(?)
    public void AddPermission(PermissionEnum permission)
    {
        _permission |= permission;
    }

    public void RemovePermission(PermissionEnum permission)
    {
        _permission &= ~permission;
    }

    public bool HasPermission(PermissionEnum permission)
    {
        return _permission.HasFlag(permission);
    }

    public string ToSaveString()
    {
        return $"{FirstName}, {LastName}; {DateOfBirth}; {SocialSecurityNumber}; {Email};{_password}";
    }
}