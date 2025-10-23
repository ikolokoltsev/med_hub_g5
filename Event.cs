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
    AppointmentRegistered,
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
    public static void AddEvent(string user, EventTypeEnum type, string description)
    {
        events.Add(new EventRecord(user, type, description, DateTime.Now));
    }

    // Show all events in the console
    public static void ShowAllEvents()
    {
        foreach (var ev in events)
        {
            Console.WriteLine($"{ev.Timestamp}: {ev.User} - {ev.Type} - {ev.Description}");
        }
    }
}

// Small helper class to store one event record
class EventRecord
{
    public string User;
    public EventTypeEnum Type;
    public string Description;
    public DateTime Timestamp;

    public EventRecord(string user, EventTypeEnum type, string description, DateTime timestamp)
    {
        User = user;
        Type = type;
        Description = description;
        Timestamp = timestamp;
    }
}