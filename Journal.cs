namespace App;

// Vad jag behöver är koppla detta till patient, kunna spara det. Permission to to watch. Permisssion to change.
// Information structure, date time, projections/prognoses perhaps


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

    public static JournalEntry FromSaveString(string line)
    {
        string[] parts = line.Split(';', 4, StringSplitOptions.TrimEntries);
        return new JournalEntry(parts[0], parts[1], parts[3])
        {
            Timestamp = DateTime.Parse(parts[2])
        };
    }
}

// public class JournalHandler
// {
//     private const string FileName = "Journal.txt";
//     private readonly List<JournalEntry> _entries = new List<JournalEntry>();

//     public JournalHandler()
//     {
//         LoadFromFile();
//     }

//     private void LoadFromFile()
//     {
//         if (!File.Exists(FileName))
//         {
//             File.Create(FileName).Close();
//             return;
//         }

//         string[] lines = File.ReadAllLines(FileName);
//         foreach (string line in lines)
//         {
//             if (!string.IsNullOrWhiteSpace(line))
//             {
//                 _entries.Add(JournalEntry.FromSaveString(line));
//             }
//         }
//     }

//     private void SaveToFile()
//     {
//         List<string> lines = new List<string>();
//         foreach (JournalEntry entry in _entries)
//         {
//             lines.Add(entry.ToString());
//         }
//         File.WriteAllLines(FileName, lines);
//     }

// Endast ägaren kan läsa sin egen journal
//     public void ShowUserJournal(User user)
//     {
//         Console.WriteLine($"Journal för {user.Email}:");
//         bool found = false;

//         foreach (JournalEntry entry in _entries)
//         {
//             if (entry.OwnerEmail.Equals(user.Email, StringComparison.OrdinalIgnoreCase))
//             {
//                 Console.WriteLine($"[{entry.Timestamp}] Skapad av: {entry.AuthorEmail}");
//                 Console.WriteLine(entry.Content);
//                 Console.WriteLine("----------------------------");
//                 found = true;
//             }
//         }

//         if (!found)
//         {
//             Console.WriteLine("Ingen journal hittades för denna användare.");
//         }
//     }

//     // Endast personal med behörighet får skapa nya anteckningar
//     public void AddJournalEntry(User author, string ownerEmail, string content)
//     {
//         if (!author.HasPermission(Permission.HanteraJournal))
//         {
//             Console.WriteLine("Du har inte behörighet att skapa journalanteckningar.");
//             return;
//         }

//         JournalEntry entry = new JournalEntry(ownerEmail, author.Email, content);
//         _entries.Add(entry);
//         SaveToFile();

//         Console.WriteLine($" Journal för {ownerEmail} uppdaterad av {author.Email}.");
//     }
// }
// }