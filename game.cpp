#include <iostream>
#include <random>
#include <fstream>
#include <vector>
#include <sstream>
#include <algorithm>
#include <cstdlib>
#include <windows.h>

// Scoreboard structure
struct Score{
    std::string playerName;
    int tries;
    int difficulty;
};

// Function for setting the color of text in conosle
void setColor(int color){
    SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), color);
};

// Random message pool
std::vector<std::string> randMessPool = {
    "Oops!",
    "Almost!",
    "Wrong!",
    "Yikes!",
    "Nah-uhh!",
    "Snap!",
    "Ah man!"
};

// Checks if scoreboard.txt if existing and isn't empty
bool isScoreboardAvailable(){
    std::fstream score("scoreboard.txt");
    if(!score.is_open())
        return false;
    return score.peek() != std::fstream::traits_type::eof();
}

void pause(){
    std::cout << "\nPress ";
    setColor(6);
    std::cout << "ENTER ";
    setColor(15);
    std::cout << "to continue...";
    std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
    std::cin.get();
}
// Main menu screen
int startScreen(){
    int menuSelect;
    std::cout << "----------\nWelcome to the ";
    setColor(6);
    std::cout << "'Lucky Bet'" ;
    setColor(15);
    std::cout << " game!\nPlease select menu from available options\n 1) Start the game";
    if(isScoreboardAvailable()) std::cout << "\n 2) Check scoreboard";
    std::cout << "\n----------\n";
    std::cin >> menuSelect;

    return menuSelect;
}
int main() {

    while(true){
        // Main menu screen
        int menuSelect = startScreen();
        int difficulty, randomNum, maxNum, tries = 0, answer = 0, randMess;
        std::fstream score("scoreboard.txt");
        std::string playerName;
        std::string line;

        // Difficulty screen
        if(menuSelect == 1){
            system("cls");
            std::cout << "----------\nBefore you start select your difficulty level\nAvailable difficulties:\n ";
            setColor(2);
            std::cout << "1) Easy - numbers from 1 to 50\n ";
            setColor(6);
            std::cout <<"2) Medium - numbers from 1 to 100\n ";
            setColor(12);
            std::cout << "3) Hard - numbers from 1 to 250\n";
            setColor(15);
            std::cout << "Selected difficulty: ";
            std::cin >> difficulty;
            system("cls");
        }
        // Show scoreboard by difficulty
        else if(menuSelect == 2 && isScoreboardAvailable()){
            system("cls");
            int diffSelect;
            std::cout << "Select difficulty to see top scores:\n ";
            setColor(2);
            std::cout << "1) Easy\n ";
            setColor(6);
            std::cout << "2) Medium\n ";
            setColor(12);
            std::cout << "3) Hard\n";
            setColor(15);
            std::cout <<"Your choice: ";
            std::cin >> diffSelect;
            system("cls");

            if(diffSelect < 1 || diffSelect >3){
                std::cout << "Difficulty not available";
                system("cls");
                continue;
            }
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
                std::cout << i+1 << ". ";
                setColor(6);
                std::cout << scores[i].playerName;
                setColor(15);
                std::cout << " - ";
                setColor(3);
                std::cout << scores[i].tries;
                setColor(15);
                std::cout << " tries\n";
                setColor(15);
            }

            pause();
            system("cls");
            continue;
        }
        else{
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

        // Main gameplay core
        while(answer != randomNum){
            std::cout << "Random number is: " << randomNum << "\n";
            std::cout << "Successfully selected random number, try to guess it!\n";
            std::cout << "It's your ";
            setColor(3);
            std::cout << tries+1;
            setColor(15);
            std::cout << " try\n";
            std::cout << "\nYour guess: ";
            std::cin >> answer;
            tries++;

            // Right guess
            if(answer == randomNum){
                setColor(6);
                std::cout << "Congratulations! You guessed right\n";
                setColor(15);
                std::cout << "You needed ";
                setColor(3);
                std::cout << tries;
                setColor(15);
                std::cout << " tries to guess the right number!\n\nEnter your name to save your score: ";
                setColor(6);

                // Writing score to file
                std::cin >> playerName;
                setColor(15);
                
                std::ofstream score("scoreboard.txt", std::ios::app);
                score << playerName << " " << tries << " " << difficulty << std::endl;
                score.close();

                // startScreen();
                break;
            }
            // Number too small
            else if(answer < randomNum){
                randMess = rand() % randMessPool.size();
                std::cout << randMessPool[randMess] << " ";
                std::cout << "Looks like your answer is smaller than a hidden number. Try again!\n";
            }
            // Number too big
            else if(answer > randomNum){
                randMess = rand() % randMessPool.size();
                std::cout << randMessPool[randMess] << " ";
                std::cout << "Looks like your answer is bigger than a hidden number. Try again!\n";
            }
        }
        system("cls");
        continue;
        }
}
