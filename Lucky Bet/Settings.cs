using System.Text.Json;
class Settings
{
    // Must be properties to work
    public string language {get; set;} = "en";
    public bool askBet {get; set;} = true;
}