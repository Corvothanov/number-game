using System;

abstract class GameSession
{
    protected int difficulty { get; set; }
    protected int maxAttempts { get; set; }
    protected int currentAttempt { get; set; }
    protected int targetNumber { get; set; }
    protected DateTime startTime { get; set; }
    protected int guess { get; set; }
    protected UI ui { get; set; }
    protected bool isBetMode { get; set; }

    public GameSession(int difficulty, UI ui)
    {
        this.difficulty = difficulty;
        this.ui = ui;
        currentAttempt = 0;
        startTime = DateTime.Now;
        GenerateNumber();
    }

    protected void GenerateNumber()
    {
        Random rand = new Random();
        switch(difficulty)
        {
            case 1:
                {
                    targetNumber = rand.Next(1, 51);
                    break;
                }
            case 2:
                {
                    targetNumber = rand.Next(1, 101);
                    break;
                }
            case 3:
                {
                    targetNumber = rand.Next(1, 251);
                    break;
                }
        }
    }

    protected bool checkGuess(int guess)
    {
        currentAttempt++;
        if (guess == targetNumber)
        {
            return true;
        }
        if (guess < targetNumber)
        {
            ui.displayMessage("menu.guessing.error.smaller");
            ui.displayMessage("menu.guessing.tries.mess1");
            Console.Write(currentAttempt+1);
            ui.displayMessage("menu.guessing.tries.mess2");
            return false;
        }
        if (guess > targetNumber)
        {
            ui.displayMessage("menu.guessing.error.bigger");
            ui.displayMessage("menu.guessing.tries.mess1");
            Console.Write(currentAttempt+1);
            ui.displayMessage("menu.guessing.tries.mess2");
            return false;
        }
        return false;
    }

    public abstract PlayerRecord Play();
}