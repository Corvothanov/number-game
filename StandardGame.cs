using System;

class StandardGame : GameSession
{
    // private UI ui;
    private Settings settings;
    private PlayerRecord playerRecord;
    private HallOfFame hallOfFame;
    public StandardGame(int diff, int maxAttempts, UI ui) : base(diff, ui)
    {
        settings = new Settings();
        playerRecord = new PlayerRecord();
        hallOfFame = new HallOfFame();
        hallOfFame.load();
        Console.WriteLine($"[DEBUG] Target number: {targetNumber}");
        ui.displayMessage("menu.guessing.selectedNumber");
        ui.displayMessage($"menu.guessing.tries.mess1");
        Console.Write(currentAttempt+1);
        ui.displayMessage("menu.guessing.tries.mess2");
        while(currentAttempt < maxAttempts)
        {
            if (checkGuess(ui.readInt("menu.guessing.input")))
            {
                TimeSpan gameTime = DateTime.Now - startTime;
                ui.displayMessage("menu.guessing.win");
                ui.displayMessage("menu.guessing.win.info.mess1");
                Console.Write(currentAttempt);
                playerRecord.playerName = ui.readString("menu.guessing.win.info.mess2");
                playerRecord.attempts = currentAttempt;
                playerRecord.difficultyLevel = difficulty;
                playerRecord.timeInSeconds = (int)Math.Round(gameTime.TotalSeconds);
                playerRecord.isNewGamePlus = false;
                hallOfFame.addRecord(playerRecord);
                break;
            }
        }
    }
    public override PlayerRecord Play()
    {
        return new PlayerRecord();
    }
}