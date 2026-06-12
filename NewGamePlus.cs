using System;

class NewGamePlus : GameSession
{
    private int shotsToReshuffle = 6;
    private int shotsTakenSinceReshuffle = 0;
    private Settings settings;
    private PlayerRecord playerRecord;
    private HallOfFame hallOfFame;

    public NewGamePlus(int diff, UI ui) : base(diff, ui)
    {
        settings = new Settings();
        playerRecord = new PlayerRecord();
        hallOfFame = new HallOfFame();
        hallOfFame.load();
        ui.displayMessage("menu.guessing.selectedNumber");
        ui.displayMessage($"menu.guessing.tries.mess1");
        Console.Write(currentAttempt+1);
        ui.displayMessage("menu.guessing.tries.mess2");
        while(true)
        {
            ui.displayMessage("menu.guessing.newgameplus.shotstoreshuffle.mess1");
            Console.Write(shotsToReshuffle - shotsTakenSinceReshuffle);
            ui.displayMessage("menu.guessing.newgameplus.shotstoreshuffle.mess2");

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
                playerRecord.isNewGamePlus = true;
                hallOfFame.addRecord(playerRecord);
                break;
            }
            shotsTakenSinceReshuffle++;
            if(shotsTakenSinceReshuffle >= shotsToReshuffle)
            {
                shotsTakenSinceReshuffle = 0;
                reshuffleNumber();
            }
        }
    }

    public override PlayerRecord Play()
    {
        return new PlayerRecord();
    }

    private void reshuffleNumber()
    {
        ui.displayMessage("menu.guessing.newgameplus.reshuffle");
        GenerateNumber();
    }
}