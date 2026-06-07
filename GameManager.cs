using System;

class GameManager
{
    private Settings settings = new Settings();
    private HallOfFame hallOfFame = new HallOfFame();
    private UI ui = new UI();
    int diff, maxAttempts = 9999;
    private void showWelcomeScreen()
    {
        ui.displayMessage("menu.startscreen.startmess");
        ui.displayMessage("menu.startscreen.selectoption");
        ui.displayMessage("menu.startscreen.newgame");
        ui.displayMessage("menu.startscreen.newgamePlus");
        if (hallOfFame.hasAnyRecords())
        {
            ui.displayMessage("menu.startscreen.scoreboard");
        }
        // DEBUG
        else
        {
            Console.WriteLine("[DEBUG] No records in scoreboard");
        }
        ui.displayMessage("menu.startscreen.settings");

        // MENU RESULTS
        string choice = ui.readString("menu.difficultySelect.choice");
        switch (choice)
        {
            case "1":
                {
                    startNewGame();
                    GameSession gameSession = new StandardGame(diff, maxAttempts, ui);
                    gameSession.Play();
                    break;
                }
            case "2":
                {
                    startNewGame();
                    settings.askBet = false;
                    GameSession gameSession = new NewGamePlus(diff, ui);
                    gameSession.Play();
                    break;
                }
            case "3":
                {
                    if (hallOfFame.hasAnyRecords())
                    {
                        int diff = ui.readInt("menu.scoreboard.choice");
                        ui.showTop5(hallOfFame.getTop5(diff));
                    }
                    break;
                }
            case "s":
            case "S":
                {
                    showSettings();
                    break;
                }
        }
    }
    private void showSettings()
    {
        // Displaying current language setting
        ui.displayMessage("menu.settings.language");
        Console.WriteLine(settings.language);
        // Displaying current bet setting
        ui.displayMessage("menu.settings.betmode");
        Console.WriteLine(settings.askBet);

        ui.displayMessage("menu.settings.resetHoF");
        ui.displayMessage("menu.settings.exit");

        // Changing settings
        string choice = ui.readString("menu.difficultySelect.choice");
        switch (choice)
        {
            case "1":
                {
                    int langChoice = ui.readInt("menu.settings.language.change");
                    switch(langChoice)
                    {
                        case 1:
                            settings.language = "en";
                            settings.save();
                            settings.load();
                            break;
                        case 2:
                            settings.language = "pl";
                            settings.save();
                            settings.load();
                            break;
                        default:
                            ui.displayMessage("error.startScreen.menu");
                            return;
                    }
                    break;
                }
            case "2":
                {
                    int betChoice = ui.readInt("menu.settings.betmode.change");
                    switch (betChoice)
                    {
                        case 1:
                            settings.askBet = true;
                            settings.save();
                            settings.load();
                            break;
                        case 2:
                            settings.askBet = false;
                            settings.save();
                            settings.load();
                            break;
                        default:
                            ui.displayMessage("error.startScreen.menu");
                            return;
                    }
                    break;
                }
                case "3":
                {
                    string confirmClearance = ui.readString("menu.setting.resetScoreboard.confirm");
                    switch (confirmClearance)
                    {
                        case "y":
                        case "Y":
                            {
                                hallOfFame.clearRecords();
                                hallOfFame.load();
                                break;
                            }
                        default:
                            {
                                return;
                            }
                    }
                    break;
                }
                case "q":
                case "Q":
                {
                    return;
                }
        }
    }
    private void startNewGame()
    {
        // Displays difficulty selection menu
        ui.displayMessage("menu.difficultySelect.startmess");
        ui.displayMessage("menu.difficultySelect.available");
        ui.displayMessage("menu.difficultySelect.easy");
        ui.displayMessage("menu.difficultySelect.medium");
        ui.displayMessage("menu.difficultySelect.hard");
        ui.displayMessage("menu.difficultySelect.exit");
        string choice = ui.readString("menu.difficultySelect.choice");
        switch (choice)
        {
            case "1":
                {
                    diff = 1;
                    break;
                }
            case "2":
                {
                    diff = 2;
                    break;
                }
            case "3":
                {
                    diff = 3;
                    break;
                }
            case "q":
            case "Q":
                {
                    return;
                }
            default:
                {
                    ui.displayMessage("error.startScreen.menu");
                    return;
                }
        }
        if(settings.askBet)
        {
            switch (ui.readString("menu.betmode.question"))
            {
                default:
                    {
                        ui.displayMessage("error.startScreen.menu");
                        return;
                    }
                case "y":
                case "Y":
                    {
                        int input = 0;
                        while(input < 1 || input > 10){
                            input = ui.readInt("menu.betmode.maximumNumer");
                        }
                        maxAttempts = input;
                        break;
                    }
                case "n":
                case "N":
                    {
                        break;
                    }
            }
        }
    }
    public void run()
    {
        Console.WriteLine("[DEBUG] Running GameMager");
        settings.load();
        hallOfFame.load();
        showWelcomeScreen();
    }
    static void Main()
    {
        GameManager gameManager = new GameManager();
        gameManager.run();
    }
}