namespace App;

public enum RegistrationStatusEnum
{
    Pending,
    Completed,
    Denied,
}

class RegistrationRequest
{
    public string SocialSecurityNumber;
    string _password;
    public RegionEnum Region;
    public RegistrationStatusEnum Status;

    public RegistrationRequest(string socialSecurityNumber, string password, RegionEnum region,
        RegistrationStatusEnum status = RegistrationStatusEnum.Pending)
    {
        SocialSecurityNumber = socialSecurityNumber;
        _password = password;
        Region = region;
        Status = status;
    }
}