using System;

class GameManager
{
    private Settings settings = new Settings();
    private HallOfFame hallOfFame = new HallOfFame();
    private UI ui = new UI();

    private void showWelcomeScreen()
    {
        ui.displayMessage("menu.startscreen.startmess");
        ui.displayMessage("menu.startscreen.selectoption");
        ui.displayMessage("menu.startscreen.newgame");
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
                    break;
                }
            case "2":
                {
                    if (hallOfFame.hasAnyRecords())
                        ui.showTop5(hallOfFame.getTop5(1));
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
                ui.displayMessage("menu.difficultySelect.easy.confirm");
                {
                    GameSession gameSession = new StandardGame(1, 10, ui);
                    gameSession.Play();
                }
                break;
            case "2":
                ui.displayMessage("menu.difficultySelect.medium.confirm");
                {
                    GameSession gameSession = new StandardGame(2, 8, ui);
                    gameSession.Play();
                }
                break;
            case "3":
                ui.displayMessage("menu.difficultySelect.hard.confirm");
                {
                    GameSession gameSession = new StandardGame(3, 5, ui);
                    gameSession.Play();
                }
                break;
            case "q":
            case "Q":
                return;
            default:
                ui.displayMessage("menu.settings.exit");
                break;
        }
    }
    public void run()
    {
        Console.WriteLine("[DEBUG] Running GameMager");
        settings.load();
        showWelcomeScreen();
    }
    static void Main()
    {
        GameManager gameManager = new GameManager();
        gameManager.run();
    }
}