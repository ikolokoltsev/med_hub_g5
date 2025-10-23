namespace App;

[Flags]
public enum PermissionEnum
{
    None = 0,
    ManagePermissions = 1 << 0,
    AssignToTheRegions = 1 << 1,
    HandleRegions = 1 << 2,
    AddLocations = 1 << 3,
    CreatePersonnelAccount = 1 << 4,
    ShowPermissionList = 1 << 5,
    ManegeRegistrationRequest = 1 << 6,
    ShowPatientJournalEntries = 1 << 7,
    MarkJournalEntriesLevel = 1 << 8,
    ManageAppointments = 1 << 9,
    ViewTheSchedule = 1 << 10,

}

// TODO: implement setting the locale role depending on the context. 
enum RoleEnum
{
    Patient,
    Personnel,
    Admin
}
public class User
{
    public string FirstName;
    public string LastName;
    public int DateOfBirth;
    public int SocialSecurityNumber;
    public string Email;
    public string _password;

    public string RegionName;
    private PermissionEnum _permission;
    // public string? UserRole = RoleEnum.Patient.ToString(); // "Admin", "Personnel", "Patient".

    public User(string firstName, string lastName, int dateOfBirth, int socialSecurityNumber, string email,
        string password, string regionName, PermissionEnum initialPermission = PermissionEnum.None)

    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        SocialSecurityNumber = socialSecurityNumber;
        Email = email;
        _password = password;
        RegionName = regionName;
        _permission = initialPermission;

    }

    // Check if the login credentials match
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
