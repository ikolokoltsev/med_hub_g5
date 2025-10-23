using System.Globalization;

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
    ManageRegistrationRequest = 1 << 6,
    ShowPatientJournalEntities = 1 << 7,
    MarkJournalEntitiesLevel = 1 << 8,
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

public enum GenderEnum
{
    Male,
    Female
}

public class User
{
    public string SocialSecurityNumber;
    public string FirstName;
    public string LastName;
    public DateTime BirthDate;
    public GenderEnum Gender;
    public string Email;
    public string PhoneNumber;
    public RegionEnum Region;
    string _password;
    PermissionEnum _permission;
    // public string? UserRole = RoleEnum.Patient.ToString(); // "Admin", "Personnel", "Patient".

    public User(string socialSecurityNumber, RegionEnum region,
        string password, PermissionEnum initialPermission = PermissionEnum.None)

    {
        SocialSecurityNumber = socialSecurityNumber;
        _password = password;
        _permission = initialPermission;
        Region = region;
        GeneratePersonalUserData();
    }

    // Check if the login credentials match
    public bool TryLogin(string socialSecurityNumber, string password)
    {
        return socialSecurityNumber == SocialSecurityNumber && password == _password;
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
        return $"{SocialSecurityNumber}, {_password}; {_permission}";
    }

    private void GeneratePersonalUserData()
    {
        string[] maleFirstNames =
        {
            "Erik", "Lars", "Karl", "Anders", "Per", "Johan", "Nils", "Olof",
            "Gustav", "Sven", "Mikael", "Peter", "Thomas", "Jan", "Magnus",
            "Fredrik", "Oscar", "Alexander", "Viktor", "Emil"
        };

        string[] femaleFirstNames =
        {
            "Anna", "Maria", "Karin", "Kristina", "Eva", "Lena", "Emma",
            "Sofia", "Ingrid", "Birgitta", "Elisabeth", "Linda", "Sara",
            "Margareta", "Johanna", "Helena", "Maja", "Elin", "Ida", "Astrid"
        };

        string[] lastNames =
        {
            "Andersson", "Johansson", "Karlsson", "Nilsson", "Eriksson",
            "Larsson", "Olsson", "Persson", "Svensson", "Gustafsson",
            "Pettersson", "Jonsson", "Jansson", "Hansson", "Bengtsson",
            "Jönsson", "Lindberg", "Jakobsson", "Magnusson", "Olofsson",
            "Lindström", "Lindqvist", "Lindgren", "Berg", "Axelsson"
        };

        string birthDatePart = SocialSecurityNumber.Substring(0, 8);
        BirthDate = DateTime.ParseExact(birthDatePart, "yyyyMMdd", CultureInfo.InvariantCulture);

        int magicFour = int.Parse(SocialSecurityNumber.Substring(9, 4));

        Gender = (magicFour % 2 == 0) ? GenderEnum.Female : GenderEnum.Male;
        FirstName = Gender == GenderEnum.Male
            ? maleFirstNames[magicFour % maleFirstNames.Length]
            : femaleFirstNames[magicFour % femaleFirstNames.Length];
        LastName = lastNames[magicFour / 10 % lastNames.Length];
        Email = $"{FirstName.ToLower()}{LastName.ToLower()}@gmail.com";
        PhoneNumber = GeneratePhoneNumber(magicFour);
    }

    private static string GeneratePhoneNumber(int number)
    {
        int[] prefixes = { 70, 72, 73, 76, 79 };
        int prefix = prefixes[number / 100 % prefixes.Length];

        int firstThree = 100 + number % 900;
        int middle = 10 + number / 10 % 90;
        int lastTwo = 10 + number * 13 % 90;

        return $"+46 {prefix}-{firstThree}-{middle}-{lastTwo}";
    }
}


/*
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
*/