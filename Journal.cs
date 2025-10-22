namespace App;

public class JournalEntry
{
    public string OwnerEmail;
    public string AuthorEmail;
    public DateTime Timestamp;
    public string Content;

    public JournalEntry(string ownerEmail, string authorEmail, string content)
    {
        OwnerEmail = ownerEmail;
        AuthorEmail = authorEmail;
        Timestamp = DateTime.Now;
        Content = content;
    }

    public override string ToString()
    {
        return $"{OwnerEmail};{AuthorEmail};{Timestamp};{Content}";
    }
}