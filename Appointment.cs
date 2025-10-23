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
    public User BookedBy;  // Who the appointment is for
    public User ManagedBy;  // Who managed the appointment
    public string LocationName;    // Where it will take place
    public DateTime DateAndTime;   // When the appointment is scheduled
    public AppointmentStatusEnum Status; // The status of the appointment

    // Constructor (used when creating a new appointment)
    public Appointment(User bookedBy, User managedBy, string location, DateTime dateAndTime)
    {
        BookedBy = bookBy;
        ManagedBy = managedBy;
        LocationName = location;
        DateAndTime = dateAndTime;
        Status = AppointmentStatusEnum.Pending;  // Default when created


        EventLog.AddEvent(bookBy.SocialSecurityNumber, managedBy.SocialSecurityNumber, EventTypeEnum.AppointmentRegistered, $"Appointment registered at {location} on {dateAndTime}.");
    }

    public void Accept(User approver)
    {
        Status = AppointmemtStatusEnum.Accepted;
        EventLog.AddEvent(approver.SocialSecurityNumber, EventTypeEnum.AppointmentApproved, $"Approved appointment for {bookedBy.SocialSecurityNumber} at {location}.");
    }

    public void Deny (User approver)
    {
        Status = AppointmentStatusEnum.Denied;
        EventLog.AddEvent(approver.SocialSecurityNumber, EventTypeEnum.AppointmentDenied, $"Denied appointment for {bookedBy.SocialSecurityNumber} at {location}.");
    }

    public string ToDisplayString()
    {
        return $"{BookedBy.SocialSecurityNumber} | {ManagedBy.SocialSecurityNumber} | {LocationName.Name} | {DateAndTime} | {Status}";
    }

}