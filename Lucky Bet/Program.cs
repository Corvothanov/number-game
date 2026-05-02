using System;
using System.IO;
using System.Linq;
class Program
{
    // Checks if scoreboard.txt exists
static bool isScoreboardAvailable()
{
    return File.Exists("scoreboard.txt");
}
static void pause()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
static int startScreen() // method (Static) works only when called
{
    Console.Clear();
    Console.WriteLine("Welcome to the 'Lucky Bet' game!");
    Console.WriteLine("Please select option from the menu");
    Console.WriteLine("1) Start new game");
    if (isScoreboardAvailable())
    {
        Console.WriteLine("2) Check scoreboard");
    }
    int menuSelect;
    Int32.TryParse(Console.ReadLine(), out menuSelect); //After succesfully converting user input value is assigned to menuSelect
    return menuSelect;
}
static string difficultySelect()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Before you start select your difficulty level" + Environment.NewLine + "Available difficulties:");
            Console.WriteLine(" 1) Easy - numbers from 1 to 50 ");
            Console.WriteLine(" 2) Medium - numbers from 1 to 100 ");
            Console.WriteLine(" 3) Hard - numbers from 1 to 250 ");
            Console.WriteLine(" Or Q to quit the menu");
            Console.Write("Your choice: ");
            string diffSelect = Console.ReadLine();
            Console.Clear();
            switch (diffSelect)
            {
                default:
                    Console.Clear();
                    Console.WriteLine("Difficulty not available");
                    continue;
                case "1":
                    Console.WriteLine("Chosen easy difficulty");
                    return diffSelect;
                case "2":
                    Console.WriteLine("Chosen mid difficulty");
                    return diffSelect;
                case "3":
                    Console.WriteLine("Chosen hard difficulty");
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
                Console.WriteLine("No results");
                pause();
                Main();
            }
    Console.WriteLine($"=== TOP 5 ===");
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
            "Oops!",
            "Almost!",
            "Wrong!",
            "Yikes!",
            "Nah-uhh!",
            "Snap!",
            "Ah man!"
        };
        Random rand = new Random();
        int randomFailMessage = rand.Next(0, randomFailMessageList.Count);
        Console.WriteLine(randomFailMessageList[randomFailMessage]);
    }
static bool isBetModeOn()
    {
        Console.WriteLine("Would you like to turn on a bet mode? If you chose YES you have to chose maximum number of available tries, if you pass that number you will fail (Y/N)");
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
        int maxNum = 0, answer = 0, tries = 0, bet = 0;
        int menuSelect = startScreen();
        // Console.WriteLine("You have chosen option: " + menuSelect);
        switch (menuSelect)
        {
            default:
                Console.WriteLine("Selected incorrect option");
                break;
            case 1:
                string diffSelect = difficultySelect();
                if(diffSelect == null)
                {
                    Console.WriteLine("Exiting game");
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
                bool betMode = isBetModeOn();
                if (betMode)
                {
                    Console.WriteLine("[DEV] Bet mode is on");
                    Console.WriteLine("Choose your maximum number of tries (from 1 to 10)");
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
                        Console.WriteLine("You failed the bet");
                        Main();
                    }
                    Console.WriteLine("Successfully selected random number, try to guess it!");
                    if(betMode)
                        Console.WriteLine($"It's your {tries + 1} try and you have {bet} bets left before fail");
                    else
                        Console.WriteLine($"It's your {tries + 1} try");
                    Console.Write("Your guess: ");
                    int.TryParse(Console.ReadLine(), out answer);
                    tries++;
                    bet--;

                    if(answer == randomNum)
                    {
                        Console.WriteLine("Congratulations! You guessed right");
                        Console.Write("You needed " + tries + " tries to guess the right number!" + Environment.NewLine + "Enter your name to save your score: ");
                        string playerName = Console.ReadLine();
                        File.AppendAllText(Path.Combine(".", "scoreboard.txt"), playerName + ";" + tries + ";" + diffSelect + Environment.NewLine);
                    }
                    else if(answer < randomNum)
                    {
                        randomFailMessage();
                        Console.WriteLine("Looks like your answer is smaller than a hidden number. Try again!");
                    }
                    else if(answer > randomNum){
                        randomFailMessage();
                        Console.WriteLine("Looks like your answer is bigger than a hidden number. Try again!");
            }
                }
                break;
            case 2:
                Console.Clear();
                if (!isScoreboardAvailable())
                {
                    Console.WriteLine("Selected incorrect option");
                    break;
                }
                Console.Write("Select difficulty to see top scores:" + Environment.NewLine + "1) Easy" + Environment.NewLine + "2) Medium" + Environment.NewLine + "3) Hard" + Environment.NewLine + "Your choice: ");
                int scoreboardDifficulty;
                int.TryParse(Console.ReadLine(), out scoreboardDifficulty);
                Console.Clear();
                showScoreboard(scoreboardDifficulty);
                break;
        }
    }
}

