using System.Text.Json;
using System.IO;
using System.Collections.Generic;

public class Language
{
    private Dictionary<string, string> text;

    public Language(string path)
    {
        string jsonLocation = File.ReadAllText(path);
        text = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonLocation);
        if(text == null)
        {
            text = new Dictionary<string, string>();
        }
    }
    public string GetText(string translation)
    {
        if (text.ContainsKey(translation))
        {
            return text[translation];
        }
        return "["+ translation + "]";
    }
}