namespace App;

  public enum AppointmentStatusEnum   // Define possible appointment states
{
    Pending,   // The patient awaits 
    Completed,   // The appointment has happened
    Cancelled    // The appointment was cancelled
}

public enum AppointmentRequestStatusEnum
{
    Pending, Accept, Denied
}
class Appointment
{
    public string PatientName;     // Who the appointment is for
    public string PersonnelName;   // Which doctor/staff will handle it
    public string LocationName;    // Where it will take place
    public DateTime DateAndTime;   // When the appointment is scheduled
    public AppointmentStatusEnum Status; // The status of the appointment
    public List<Appointment> Appointments = new List<Appointment>();

    // Constructor (used when creating a new appointment)
    public Appointment(string patient, string personnel, string location, DateTime dateAndTime)
    {
        PatientName = patient;
        PersonnelName = personnel;
        LocationName = location;
        DateAndTime = dateAndTime;
        Status = AppointmentStatusEnum.Pending;  // Default when created
    }
}

class AppointmentRequest

{
    public User PatientName;    // Who is asking for the appointment
    public string LocationName;   // Where they want to go (clinic/hospital)
    public User RegionName;   // Which region they want to go (clinic/hospital)

    public AppointmentRequestStatusEnum AppointmentRequestStatus;       // Has this request been approved yet?

    // Constructor - runs when we create a new appointment request
    public AppointmentRequest(User patientName, string locationName, User regionName)
    {
        PatientName = patientName;
        LocationName = locationName;
        RegionName = regionName;
    }

    public string ToSaveString()
    {
        string result = $"{PatientName},{PatientName.Email},{PatientName.SocialSecurityNumber},{PatientName.RegionName},{LocationName}";
        return result;
    }

}