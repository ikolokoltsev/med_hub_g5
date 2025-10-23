namespace App;

public enum AppointmentStatusEnum 
{
    Pending,
    Completed,
    Cancelled,
    Accepted,
    Rejected,
}

class Appointment
{
    public User BookedBy;
    public string LocationName;
    public DateTime DateAndTime;
    public AppointmentStatusEnum Status;
    //public List<Appointment> Appointments = new List<Appointment>();

    public Appointment(User bookedBy, string location, DateTime dateAndTime, User bookBy)
    {
        BookedBy = bookBy;
        LocationName = location;
        DateAndTime = dateAndTime;
        Status = AppointmentStatusEnum.Pending;


        EventLog.AddEvent(bookBy.Email, EventTypeEnum.AppointmentRegistered,
            $"Appointment registered at {location} on {dateAndTime}.");
    }

    public void Accept(User approver)
    {
        Status = AppointmentStatusEnum.Accepted;
        EventLog.AddEvent(approver.SocialSecurityNumber, EventTypeEnum.AppointmentApproved,
            $"Appointment registered by {BookedBy.SocialSecurityNumber} at {LocationName}.");
    }

    public void Deny(User approver)
    {
        Status = AppointmentStatusEnum.Rejected;
        EventLog.AddEvent(approver.SocialSecurityNumber, EventTypeEnum.AppointmentDenied,
            $"Denied appointment for {BookedBy.SocialSecurityNumber} at {LocationName}.");
    }

    public string ToDisplayString()
    {
        return $"{BookedBy.SocialSecurityNumber} | {LocationName} | {DateAndTime} | {Status}";
    }
}