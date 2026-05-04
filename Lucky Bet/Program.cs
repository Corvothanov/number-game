using System;
using System.IO;
using System.Linq;
using System.Text.Json;
class Program
{
static Language lang;
static Settings settings;
    // Checks if scoreboard.txt exists
static bool isScoreboardAvailable()
{
    return File.Exists("scoreboard.txt");
}
static void LoadSettings()
    {
    if(!File.Exists("settings.json")){
        settings = new Settings();
        File.WriteAllText("settings.json", JsonSerializer.Serialize(settings));
    }
    settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText("settings.json"));
    }
static void pause()
    {
        Console.WriteLine(lang.GetText("menu.pause"));
        Console.ReadKey();
    }
static string startScreen() // method (Static) works only when called
{
    Console.Clear();
    Console.WriteLine(lang.GetText("menu.startscreen.startmess"));
    Console.WriteLine(lang.GetText("menu.startscreen.selectoption"));
    Console.WriteLine(lang.GetText("menu.startscreen.newgame"));
    if (isScoreboardAvailable())
    {
        Console.WriteLine(lang.GetText("menu.startscreen.scoreboard"));
    }
    Console.WriteLine(lang.GetText("menu.startscreen.settings"));
    string menuSelect = Console.ReadLine();
    return menuSelect;
}
static string difficultySelect()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(lang.GetText("menu.difficultySelect.startmess") + Environment.NewLine + lang.GetText("menu.difficultySelect.available"));
            Console.WriteLine(lang.GetText("menu.difficultySelect.easy"));
            Console.WriteLine(lang.GetText("menu.difficultySelect.medium"));
            Console.WriteLine(lang.GetText("menu.difficultySelect.hard"));
            Console.WriteLine(lang.GetText("menu.difficultySelect.quit"));
            Console.Write(lang.GetText("menu.difficultySelect.choice"));
            string diffSelect = Console.ReadLine();
            Console.Clear();
            switch (diffSelect)
            {
                default:
                    Console.Clear();
                    Console.WriteLine(lang.GetText("error.difficultySelect"));
                    continue;
                case "1":
                    Console.WriteLine(lang.GetText("menu.difficultySelect.easy.confirm"));
                    return diffSelect;
                case "2":
                    Console.WriteLine(lang.GetText("menu.difficultySelect.medium.confirm"));
                    return diffSelect;
                case "3":
                    Console.WriteLine(lang.GetText("menu.difficultySelect.hard.confirm"));
                    return diffSelect;
                case "q":
                case "Q":
                    // Console.WriteLine("[DEV] Chosen exit");
                    Console.Clear();
                    Main();
                    return null;
            }
        }
    }
static void changeLanguage()
    {
        Console.WriteLine("[DEV] Chosen option to change language");
        Console.WriteLine(lang.GetText("menu.settings.language.change"));
        while (true)
        {
            string languageSwitch = Console.ReadLine();
            switch (languageSwitch)
            {
                case "1":
                    settings.language = "en";
                    lang = new Language($"lang/{settings.language}.json");
                    File.WriteAllText("settings.json", JsonSerializer.Serialize(settings));
                    return;
                case "2":
                    settings.language = "pl";
                    lang = new Language($"lang/{settings.language}.json");
                    File.WriteAllText("settings.json", JsonSerializer.Serialize(settings));
                    return;
                default:
                    Console.WriteLine(lang.GetText("error.startScreen.menu"));
                    continue;
            }
        }
    }
static void betModeQuestion()
    {
        Console.WriteLine("[DEV] Chosen option to change bet mode behavior");
        Console.WriteLine(lang.GetText("menu.settings.betmode.change"));
        while (true)
        {
            string betModeSwitch = Console.ReadLine();
            switch (betModeSwitch)
            {
                case "1":
                    settings.askBet = true;
                    File.WriteAllText("settings.json", JsonSerializer.Serialize(settings));
                    return;
                case "2":
                    settings.askBet = false;
                    File.WriteAllText("settings.json", JsonSerializer.Serialize(settings));
                    return;
                default:
                    Console.WriteLine(lang.GetText("error.startScreen.menu"));
                    continue;
            }
        }
    }
static void resetScoreboard()
    {
        Console.Write(lang.GetText("menu.setting.resetScoreboard.confirm"));
        while (true)
        {
            var key = Console.ReadKey(true).Key;
            if(key == ConsoleKey.Y)
                File.Delete("scoreboard.txt");
                return;
            if(key == ConsoleKey.N)
                return;
        }
    }
class Scores // Template
    {
        public string PlayerName;
        public int Tries;
        public int Difficulty;
    }
static void showScoreboard(int selectedDifficulty)
{
    var top5 = File.ReadAllLines("scoreboard.txt") // Reads a line and stores it as a table
        .Select(line => line.Split(';')) // Splits a line into parts divided by ;
        .Select(parts => new Scores // changes parts into Scores objects
        {
            PlayerName = parts[0],
            Tries = int.Parse(parts[1]),
            Difficulty = int.Parse(parts[2])
        })
        .Where(s => s.Difficulty == selectedDifficulty) // filters results by difficulty
        .OrderBy(s => s.Tries) // sorts results by number of tries
        .Take(5) // limits results
        .ToList(); // changes table into list (vector in C++)

    if(top5.Count == 0)
            {
                Console.WriteLine(lang.GetText("error.scoreboard.noResults"));
                pause();
                Main();
            }
    Console.WriteLine($"=== Hall of Fame ===");
    for (int i = 0; i < top5.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {top5[i].PlayerName} - {top5[i].Tries}");
    }
    pause();
    Main();
}
static void randomFailMessage()
    {
        List<string> randomFailMessageList = new List<string>{
            lang.GetText("menu.randomFailMessage.message1"),
            lang.GetText("menu.randomFailMessage.message2"),
            lang.GetText("menu.randomFailMessage.message3"),
            lang.GetText("menu.randomFailMessage.message4"),
            lang.GetText("menu.randomFailMessage.message5"),
            lang.GetText("menu.randomFailMessage.message6"),
            lang.GetText("menu.randomFailMessage.message7")
        };
        Random rand = new Random();
        int randomFailMessage = rand.Next(0, randomFailMessageList.Count);
        Console.WriteLine(randomFailMessageList[randomFailMessage]);
    }
static bool isBetModeOn()
    {
        Console.WriteLine(lang.GetText("menu.betmode.question"));
        while (true)
        {
            var key = Console.ReadKey(true).Key; // true hides key in console
            if(key == ConsoleKey.Y)
                return true;
            if(key == ConsoleKey.N)
                return false;
        }
    }
    static void Main()
    {
        // Console.WriteLine("Hello, World!");
        LoadSettings();
        lang = new Language($"lang/{settings.language}.json");
        int maxNum = 0, answer = 0, tries = 0, bet = 0;
        string menuSelect = startScreen();
        // Console.WriteLine("You have chosen option: " + menuSelect);
        switch (menuSelect)
        {
            default:
                Console.WriteLine(lang.GetText("error.startScreen.menu"));
                break;
            case "1":
                string diffSelect = difficultySelect();
                if(diffSelect == null)
                {
                    Console.WriteLine(lang.GetText("menu.menuSelect.exit"));
                    break;
                }
                // Console.WriteLine("Selected difficulty level: " + diffSelect);
                if(diffSelect == "1")
                {
                    maxNum = 50;
                }
                else if(diffSelect == "2")
                {
                    maxNum = 100;
                }
                else if(diffSelect == "3")
                {
                    maxNum = 250;
                }
                bool betMode = false;
                if (settings.askBet)
                {
                    betMode = isBetModeOn();
                }
                if (betMode)
                {
                    Console.WriteLine("[DEV] Bet mode is on");
                    Console.WriteLine(lang.GetText("menu.betmode.maximumNumer"));
                    do
                    {
                        int.TryParse(Console.ReadLine(), out bet);
                    }while(bet < 1 || bet > 10);
                    Console.WriteLine($"[DEV] Bet number: {bet}");
                }
                Random rand = new Random();
                int randomNum = rand.Next(1, maxNum + 1);
                Console.WriteLine("[DEV] Random number: " + randomNum);
                while (answer != randomNum)
                {
                    if(betMode && bet == 0)
                    {
                        Console.WriteLine(lang.GetText("menu.betmode.fail"));
                        Main();
                    }
                    Console.WriteLine(lang.GetText("menu.guessing.selectedNumber"));
                    if(betMode)
                        Console.WriteLine(lang.GetText("menu.betmode.guessing.tries.mess1")+(tries+1)+lang.GetText("menu.betmode.guessing.tries.mess2")+bet+lang.GetText("menu.betmode.guessing.tries.mess3"));
                    else
                        Console.WriteLine(lang.GetText("menu.guessing.tries.mess1")+(tries + 1)+lang.GetText("menu.guessing.tries.mess2"));
                    Console.Write(lang.GetText("menu.guessing.input"));
                    int.TryParse(Console.ReadLine(), out answer);
                    tries++;
                    bet--;

                    if(answer == randomNum)
                    {
                        Console.WriteLine(lang.GetText("menu.guessing.win"));
                        Console.Write(lang.GetText("menu.guessing.win.info.mess1")+tries+lang.GetText("menu.guessing.win.info.mess2"));
                        string playerName = Console.ReadLine();
                        File.AppendAllText(Path.Combine(".", "scoreboard.txt"), playerName + ";" + tries + ";" + diffSelect + Environment.NewLine);
                    }
                    else if(answer < randomNum)
                    {
                        randomFailMessage();
                        Console.WriteLine(lang.GetText("menu.guessing.error.smaller"));
                    }
                    else if(answer > randomNum){
                        randomFailMessage();
                        Console.WriteLine(lang.GetText("menu.guessing.error.bigger"));
            }
                }
                break;
            case "2":
                Console.Clear();
                if (!isScoreboardAvailable())
                {
                    Console.WriteLine(lang.GetText("error.startScreen.menu"));
                    break;
                }
                Console.Write(lang.GetText("menu.scoreboard.choice"));
                int scoreboardDifficulty;
                int.TryParse(Console.ReadLine(), out scoreboardDifficulty);
                Console.Clear();
                showScoreboard(scoreboardDifficulty);
                break;
            case "s":
            case "S":
                while(true){
                    Console.Clear();
                    Console.WriteLine("[DEV] Chosen settings");
                    Console.WriteLine(lang.GetText("menu.settings.language")+settings.language);
                    Console.WriteLine(lang.GetText("menu.settings.betmode")+settings.askBet);
                    Console.WriteLine(lang.GetText("menu.settings.resetHoF"));
                    Console.WriteLine(lang.GetText("menu.settings.exit"));
                    string option = Console.ReadLine();
                    switch (option)
                        {
                            case "1":
                                changeLanguage();
                                break;
                            case "2":
                                betModeQuestion();
                                break;
                            case "3":
                                resetScoreboard();
                                break;
                            case "q":
                            case "Q":
                                Console.Clear();
                                Main();
                                return;
                            default:
                                Console.WriteLine(lang.GetText("error.startScreen.menu"));
                                break;
                        }
                }
                break;
        }
    }
}

