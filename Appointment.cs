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
    public User? BookedBy;  // Who the appointment is for
    public User? ManagedBy;  // Who manages or handles the appointment
    public string LocationName;    // Where it will take place
    public DateTime DateAndTime;   // When the appointment is scheduled
    public AppointmentStatusEnum Status; // The status of the appointment

    // Constructor (used when creating a new appointment)
    public Appointment(User bookedBy, User managedBy, string location, DateTime dateAndTime)
    {
        BookedBy = bookedBy;
        ManagedBy = managedBy;
        LocationName = location;
        DateAndTime = dateAndTime;
        Status = AppointmentStatusEnum.Pending;  // Default when created


        EventLog.AddEvent(bookedBy.Email, EventTypeEnum.RegistrationRequested,
        $"Appointment registered at {location} for {bookedBy.SocialSecurityNumber}, managed by {managedBy.SocialSecurityNumber} on {dateAndTime}.");
    }

    public void Accept(User approver)
    {
        Status = AppointmentStatusEnum.Completed;
        EventLog.AddEvent(approver.Email, EventTypeEnum.AppointmentApproved, $"Approved appointment for {BookedBy.SocialSecurityNumber} at {LocationName}.");
    }

    public void Deny (User approver)
    {
        Status = AppointmentStatusEnum.Cancelled;
        EventLog.AddEvent(approver.Email, EventTypeEnum.AppointmentDenied, $"Denied appointment for {BookedBy.SocialSecurityNumber} at {LocationName}.");
    }

    public string ToDisplayString()
    {
        return $"{BookedBy.Email} | {ManagedBy.Email} | {LocationName} | {DateAndTime} | {Status}";
    }

}