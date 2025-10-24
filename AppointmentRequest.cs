namespace App;

public enum AppointmentRequestEnum
{
    Pending,
    Confirmed,
    Rejected,
}

class AppointmentRequest
{
    public string SocialSecurityNumber;
    public RegionEnum Region;
    public string Location;
    public AppointmentRequestEnum Status;

    public AppointmentRequest(string socialSecurityNumber, RegionEnum region, string location,
        AppointmentRequestEnum status = AppointmentRequestEnum.Pending)
    {
        SocialSecurityNumber = socialSecurityNumber;
        Region = region;
        Location = location;
        Status = status;
    }
}