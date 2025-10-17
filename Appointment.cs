namespace App;

enum AppointmentStatusEnum   // Define possible appointment states
{
    Pending,   // The patient awaits 
    Completed,   // The appointment has happened
    Cancelled    // The appointment was cancelled



}

enum AppointmentRequestStatusEnum
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

    // Constructor (used when creating a new appointment)
    public Appointment(string patient, string personnel, string location, DateTime dateAndTime)
    {
        PatientName = patient;
        PersonnelName = personnel;
        LocationName = location;
        DateAndTime = dateAndTime;
        Status = AppointmentStatus.Requested;  // Default when created
    }
}

class AppointmentRequest

{
    public string PatientName;    // Who is asking for the appointment
    public string LocationName;   // Where they want to go (clinic/hospital)
    public string RegionName;   // Which region they want to go (clinic/hospital)

    public AppointmentRequestStatusEnum AppointmentRequestStatus;       // Has this request been approved yet?

    // Constructor - runs when we create a new appointment request
    public AppointmentRequest(string patientName, string locationName, DateTime requestedDate);

}