using System;
using System.Collections.Generic;

class UI
{
    Settings settings = new Settings();
    private Language lang;
    private UI ui;
    private string[] randomFailMessage =
    {
        "menu.randomFailMessage.message1",
        "menu.randomFailMessage.message2",
        "menu.randomFailMessage.message3",
        "menu.randomFailMessage.message4",
        "menu.randomFailMessage.message5",
        "menu.randomFailMessage.message6",
        "menu.randomFailMessage.message7"
    };
    public UI()
    {
        settings.load();
        lang = new Language($"lang/{settings.language}.json");
    }
    public void displayMessage(string messageKey)
    {
        Console.WriteLine(lang.GetText(messageKey));
    }
    public int readInt(string prompt)
    {
        Console.WriteLine(lang.GetText(prompt));
        int number;
        while (true)
        {
            string input = Console.ReadLine();
            if(int.TryParse(input, out number))
            {
                return number;
            }
        }
    }
    public string readString(string prompt)
    {
        Console.WriteLine(lang.GetText(prompt));
        return Console.ReadLine();
    }
    public void showTop5(List<PlayerRecord> records)
    {
        for (int i = 0; i < records.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {records[i].playerName} - {records[i].attempts} - {records[i].timeInSeconds}s");
            }
    Pause();
    }
    public void getRandomFeedback(bool isTooSmall)
    {
        Random rand = new Random();
        int index = rand.Next(0, randomFailMessage.Length);
        if (isTooSmall)
        {
            Console.WriteLine(lang.GetText(randomFailMessage[index]));
            Console.WriteLine(lang.GetText("menu.guessing.error.smaller"));
        }
        else
        {
            Console.WriteLine(lang.GetText(randomFailMessage[index]));
            Console.WriteLine(lang.GetText("menu.guessing.error.bigger"));
        }
    }
    public void Pause()
    {
        Console.WriteLine(lang.GetText("menu.pause"));
        Console.ReadKey();
    }
}