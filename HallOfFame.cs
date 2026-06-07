using System.Linq;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;

class HallOfFame
{
    private List<PlayerRecord> records = new List<PlayerRecord>(); // New list name records

    public void addRecord(PlayerRecord record)
    {
        records.Add(record);
        save();
    }
    public void clearRecords()
    {
        records.Clear();
        save();
    }
    // Array's fixed; List's dynamic 
    public List<PlayerRecord> getTop5(int difficulty)
    {
        return records.Where(r => r.difficultyLevel == difficulty) // filters results by difficulty
        .OrderBy(r => r.attempts) // sorts results by number of tries
        .ThenBy(r => r.timeInSeconds)
        .Take(5) // limits results
        .ToList(); // changes table into list (vector in C++)
    }
    public bool hasAnyRecords()
    {
        return records.Any();
    }
    public void save()
    {
        File.WriteAllText("scoreboard.json", JsonSerializer.Serialize(records));
    }
    public void load()
    {
        if (!File.Exists("scoreboard.json"))
        {
            Console.WriteLine("[DEBUG] no scoreboard file found");
            records = new List<PlayerRecord>();
            return;
        }
        records = JsonSerializer.Deserialize<List<PlayerRecord>>(File.ReadAllText("scoreboard.json"));
    }
}