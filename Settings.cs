using System.Text.Json;
using System.IO;

class Settings
{
    private Settings settings;
    private Language lang;
    // Must be properties to work
    public string language {get; set;} = "en";
    public bool askBet {get; set;} = true;

    public void save()
    {
        lang = new Language($"lang/{settings.language}.json");
        File.WriteAllText("settings.json", JsonSerializer.Serialize(settings));
    }
    public void load(){
    if(!File.Exists("settings.json")){
        settings = new Settings();
        File.WriteAllText("settings.json", JsonSerializer.Serialize(settings));
    }
    settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText("settings.json"));
    }
}