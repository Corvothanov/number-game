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
        switch (settings.language)
        {
            case "en":
                {
                    lang = new English();
                    break;
                }
            case "pl":
                {
                    lang = new Polish();
                    break;
                }
        }
        File.WriteAllText("settings.json", JsonSerializer.Serialize(this));
    }
    public void load(){
    if(!File.Exists("settings.json")){
        settings = new Settings();
        File.WriteAllText("settings.json", JsonSerializer.Serialize(this));
    }
    settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText("settings.json"));
    language = settings.language;
    askBet = settings.askBet;
    }
}