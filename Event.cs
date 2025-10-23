/*
registration request,
accept patient registration,
register appointments,
modify appointment,
approve appointment request, and
List<Event>
*/

namespace App;

enum EventTypeEnum
{
    RegistrationRequested,
    RegistrationAccepted,
    AppointmentDenied,
    AppointmentModified,
    AppointmentApproved,
    AppointmentRegistered,
    PermissionGranted,
    LocationAdded,
    JournalViewed,
    Login,
    Logout,
}
// Represents one single event record
class EventLog
{
    string User; // we will use SSN
    EventTypeEnum EventType;
    string Description;

    public EventLog(string user, EventTypeEnum eventType, string description)
    {
        User = user;
        EventType = eventType;
        Description = description;
    }

    private static List<EventRecord> events = new List<EventRecord>();
    
    public static void AddEvent(string user, EventTypeEnum type, string description)
    {
        events.Add(new EventRecord(user, type, description, DateTime.Now));
    }

    public static void ShowAllEvents()

    {
        if (events.Count == 0)
        {
            Console.WriteLine("No events logged yet.");
            return;
        }

        foreach (var e in events)
        {
            Console.WriteLine($"{e.Timestamp} - {e.EventType} - {e.Description}");
        }
    }

    class EventRecord
    {
        public string User;
        public EventTypeEnum EventType;
        public string Description;
        public DateTime Timestamp;

        public EventRecord(string user, EventTypeEnum eventType, string description, DateTime timestamp)
        {
            User = user;
            EventType = eventType;
            Description = description;
            Timestamp = timestamp;
        }
    }
}