using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

public abstract class Language
{
    protected Dictionary<string, string> text;

    protected Language(string path)
    {
        string jsonLocation = File.ReadAllText(path);
        text = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonLocation);
        if(text == null)
        {
            text = new Dictionary<string, string>();
        }
    }
    public virtual string GetText(string translation)
    {
        if (text.ContainsKey(translation))
        {
            return text[translation];
        }
        return "["+ translation + "]";
    }
}
public class Polish : Language
{
    public Polish() : base("lang/pl.json")
    {

    }
}
public class English : Language
{
    public English() : base("lang/en.json")
    {

    }
}