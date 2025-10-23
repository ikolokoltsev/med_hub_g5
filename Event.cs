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
    RegistrationModified,
    RegistrationRequested,
    RegistrationAccepted,
    AppointmentDenied,
    AppointmentModified,
    AppointmentApproved,
    PermissionGranted,
    LocationAdded,
    JournalViewed,
    Login,
    Logout,
}
// Represents one single event record
class EventLog
{
    string User;
    EventTypeEnum EventType;
    string Description;

    public EventLog(string user, EventTypeEnum eventType, string description)
    {
        User = user;
        EventType = eventType;
        Description = description;
    }

    private static List<EventRecord> events = new List<EventRecord>();

    // Add an event to the list
    public static void AddEvent(string user, EventTypeEnum eventType, string description)
    {
        events.Add(new EventRecord(user, eventType, description, DateTime.Now));
    }

    // Show all events in the console
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


    // Small helper class to store one event record
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