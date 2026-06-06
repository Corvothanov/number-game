using System;

class StandardGame : GameSession
{
    private UI ui;
    private GameSession gameSession;
    private Settings settings;
    public StandardGame(int diff, int maxAttempts, UI ui) : base(diff, ui)
    {
        settings = new Settings();
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
                        while (ui.readInt("menu.betmode.maximumNumer") < 1 || ui.readInt("menu.betmode.maximumNumer") > 10)
                        {
                            maxAttempts = ui.readInt("menu.betmode.maximumNumer");
                        }
                        break;
                    }
                case "n":
                case "N":
                    {
                        break;
                    }
            }
        }
        Console.WriteLine($"[DEBUG] Target number: {targetNumber}");
        ui.displayMessage("menu.guessing.selectedNumber");
        ui.displayMessage("menu.guessing.tries.mess1");
        Console.Write(currentAttempt+1);
        ui.displayMessage("menu.guessing.tries.mess2");
        while(currentAttempt < maxAttempts)
        {
            if (checkGuess(ui.readInt("menu.guessing.input")))
            {
                ui.displayMessage("menu.guessing.win");
                ui.displayMessage("menu.guessing.win.info.mess1");
                Console.Write(currentAttempt);
                // playerName = ui.readString("menu.guessing.win.info.mess2");
                break;
            }
        }
    }
    public override PlayerRecord Play()
    {
        return new PlayerRecord();
    }
}