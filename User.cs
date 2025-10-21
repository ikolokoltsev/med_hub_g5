namespace App;

// Defines the roles a user can have in the Health Care System
public enum Role
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
    public Role RoleTitle;

    List<Permission> Permissions = new List<Permission>();

    public User(string firstName, string lastName, int dateOfBirth, int socialSecurityNumber, string email, string password, string regionName, Role roleTitle)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        SocialSecurityNumber = socialSecurityNumber;
        Email = email;
        _password = password;
        RegionName = regionName;
        RoleTitle = roleTitle;

    }
    
// Automatically assign permission to each role 
class RolePermission
{
    public static List<Permission> GetPermissionsForEachRole(Role roleTitle)
    {
        switch (roleTitle)
        {
            case Role.Admin:
            
                return new List<Permission>
                {
                    Permission.HandlePermissions,
                    Permission.AssignRegion,
                    Permission.HandleRegistration,
                    Permission.AddLocation,
                    Permission.CreatePersonnelAccount,
                    Permission.ViewPermissionList,
                    Permission.ViewPatientJournal,
                    Permission.SetJournalReadLevel,
                    Permission.ApproveAppointment,
                    Permission.ModifyAppointment,
                    Permission.ViewLocationSchedule
                };
            case Role.Personnel:
                return new List<Permission>
                {
                    Permission.RegisterAppointment,
                    Permission.ModifyAppointment,
                    Permission.ViewLocationSchedule,
                    Permission.ViewPatientJournal
                };
            case Role.Patient:
                return new List<Permission>
                {
                    Permission.RequestAppointment,
                    Permission.ViewOwnJournal,
                    Permission.ViewOwnSchedule
                };
            default:
                return new List<Permission>();
        }
    }
}

    // Check if the login credentials match
    public bool TryLogin(string email, string password, Role roleTitle)
    {
        return email == Email && password == _password && roleTitle == RoleTitle;
    }

    // Convert user information into a string for saving to file
    public string ToSaveString()
    {
        return $"{FirstName}, {LastName}; {DateOfBirth}; {SocialSecurityNumber}; {Email};{_password}";
    }

    // Check if the user has permission
    static bool CheckUserPermissions(User active_user, Permission requiredPermission)
    {
        return active_user.Permissions.Contains(requiredPermission);
    }

    // Add a permission to a user
    void GrantPermission(Permission permission)
    {
        if (Permissions.Contains(permission))
        {
            Permissions.Add(permission);
        }
    }

}



