#include <iostream>
#include <random>
#include <fstream>
#include <vector>
#include <sstream>
#include <algorithm>
#include <cstdlib>

// Scoreboard structure
struct Score{
    std::string playerName;
    int tries;
    int difficulty;
};

void pause(){
    std::cout << "\nPress ENTER to continue...";
    std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
    std::cin.get();
}
// Main menu screen
int startScreen(){
    int menuSelect;
    std::cout << "Welcome to the 'Guess The Number' game!\nPlease select available options\n 1) Start the game\n 2) Check scoreboard\n";
    std::cin >> menuSelect;

    return menuSelect;
}
int main() {

    while(true){
        // Main menu screen
        int menuSelect = startScreen();
        int difficulty, randomNum, maxNum, tries = 0, answer;
        std::fstream score("scoreboard.txt");
        std::string playerName;
        std::string line;

        // Difficulty screen
        if(menuSelect == 1){
            system("cls");
            std::cout << "Before you start select your difficulty level \n Available difficulties:\n 1) Easy - numbers from 1 to 50)\n 2) Medium - numbers from 1 to 100\n 3) Hard - numbers from 1 to 250\n Selected diffuculty: ";
            std::cin >> difficulty;
            std::cout << "------\n";
            system("cls");
        }
        // Show scoreboard by difficulty
        else if(menuSelect == 2){
            system("cls");
            int diffSelect;
            std::cout << "Select difficulty to see top scores:\n1) Easy\n2) Medium\n3) Hard\nYour choice: ";
            std::cin >> diffSelect;
            system("cls");

            if(diffSelect < 1 || diffSelect >3){
                std::cout << "Difficulty not available";
                system("cls");
                continue;
            }
            if(!score.is_open()){
                std::cout << "Scoreboard is not available";
                system("cls");
                continue;
            }
            // Start of code responsible for working scoreboard
            std::vector<Score> scores;
            int scoreboardTries, scoreboardDifficulty;

            while(score >> playerName >> scoreboardTries >> scoreboardDifficulty){
                if(scoreboardDifficulty == diffSelect){
                    scores.push_back({playerName, scoreboardTries, scoreboardDifficulty});
                }
            }
            score.close();

            // Sorting from lowerst score to highest
            std::sort(scores.begin(), scores.end(), [](const Score &a, const Score &b){
                return a.tries < b.tries;
            });

            // Disyplaying top 5 scores
            std::cout << "Top 5 scores for selected difficulty:\n";
            for(int i = 0; i < scores.size() && i < 5; i++){
                std::cout << i+1 << ". " << scores[i].playerName << " - " << scores[i].tries << " tries\n";
            }

            pause();
            system("cls");
            continue;
        }
        else{
            std::cout << "You selected wrong menu option! Try again!";
            system("cls");
            continue;
        }
        if(difficulty == 1){
            maxNum = 50;
        }
        else if(difficulty == 2){
            maxNum = 100;
        }
        else if(difficulty == 3){
            maxNum = 250;
        }
        else{
            std::cout << "You selected wrong difficulty!";
            system("cls");
            continue;
        }

        // Random number generator
        std::random_device rd;
        std::mt19937 gen(rd());
        std::uniform_int_distribution<> dist(1, maxNum);

        randomNum = dist(gen);

        std::cout << "Random number is: " << randomNum << "\n";

        // Main gameplay core
        while(answer != randomNum){
            std::cout << "What is the random number?\n Your guess: ";
            std::cin >> answer;
            tries++;

            // Right guess
            if(answer == randomNum){
                std::cout << "That's right! Congratulations!\n";
                std::cout << "You needed " << tries << " tries to guess the right number!\n Enter your name to save your score: ";

                // Writing score to file
                std::cin >> playerName;
                
                std::ofstream score("scoreboard.txt", std::ios::app);
                score << playerName << " " << tries << " " << difficulty << std::endl;
                score.close();

                // startScreen();
                break;
            }
            // Number too small
            else if(answer < randomNum){
                std::cout << "Opps, looks like your answer is smaller that a hidden number. Try again!\n";
                std::cout << "It's your " << tries+1 << " try\n";
            }
            // Number too big
            else if(answer > randomNum){
                std::cout << "Opps, looks like your answer is bigger that a hidden number. Try again!\n";
                std::cout << "It's your " << tries+1 << " try\n";
            }
        }
        system("cls");
        continue;
        }
}
