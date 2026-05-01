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
            Console.WriteLine("Before you start select your difficulty level" + Environment.NewLine + "Available difficulties:");
            Console.WriteLine(" 1) Easy - numbers from 1 to 50 ");
            Console.WriteLine(" 2) Medium - numbers from 1 to 100 ");
            Console.WriteLine(" 3) Hard - numbers from 1 to 250 ");
            Console.WriteLine(" Or Q to quit the menu");
            Console.Write("Your choice: ");
            string diffSelect = Console.ReadLine();
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
                    Console.WriteLine("Chosen exit");
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
    static void Main()
    {
        // Console.WriteLine("Hello, World!");
        int maxNum = 0, answer = 0, tries = 0;
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
                Random rand = new Random();
                int randomNum = rand.Next(1, maxNum + 1);
                Console.WriteLine("Random number: " + randomNum);
                while (answer != randomNum)
                {
                    Console.WriteLine("Successfully selected random number, try to guess it!");
                    Console.Write("It's your ");
                    Console.Write(tries + 1);
                    Console.Write(" try" + Environment.NewLine);
                    Console.Write("Your guess: ");
                    int.TryParse(Console.ReadLine(), out answer);
                    tries++;

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
                if (!isScoreboardAvailable())
                {
                    Console.WriteLine("Selected incorrect option");
                    break;
                }
                Console.Write("Select difficulty to see top scores:" + Environment.NewLine + "1) Easy" + Environment.NewLine + "2) Medium" + Environment.NewLine + "3) Hard" + Environment.NewLine + "Your choice: ");
                int scoreboardDifficulty;
                int.TryParse(Console.ReadLine(), out scoreboardDifficulty);
                showScoreboard(scoreboardDifficulty);
                break;
        }
    }
}

