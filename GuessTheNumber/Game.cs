using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheNumber
{
    internal class Game
    {
        private readonly ILogger _logger;
        private bool isGameFinished = false;
        public Game(ILogger logger)
        {
            _logger = logger;
        }
        public void GuessTheNumber()
        {
            while (true)
            {                
                Console.WriteLine("1 - Play, 2 - Check Top 10 scores");
                string choise = Console.ReadLine()!;
                while (choise != "1" && choise != "2")
                {
                    Console.WriteLine("Invalid input, try again..");
                    choise = Console.ReadLine()!;
                }
                if(choise == "2")
                {
                    _logger.GetTop10();
                    continue;
                }
                Console.WriteLine("Enter your name");
                string user = Console.ReadLine()!;
                Console.WriteLine("Choose difficulty (Easy/Medium/Hard)");
                string difficulty = Console.ReadLine()!.ToLower();
                while (difficulty != "easy" && difficulty != "medium" && difficulty != "hard")
                {
                    Console.WriteLine("Choose difficulty (Easy/Medium/Hard");
                    difficulty = Console.ReadLine()!.ToLower();
                }
                int max = 0;
                double multiplier = 0;
                switch (difficulty)
                {
                    case "easy":
                        max = 15;
                        multiplier = 1;
                        break;
                    case "medium":
                        max = 25;
                        multiplier = 1.5;
                        break;
                    case "hard":
                        max = 50;
                        multiplier = 2;
                        break;
                }
                int numberToGuess = new Random().Next(1, max);
                Console.WriteLine("Guess the Number");
                int counter = 1;
                int number = 0;
                while (counter < 11)
                {
                    bool isValid = false;
                    while (!isValid)
                    {
                        isValid = int.TryParse(Console.ReadLine()!, out number) && number > 0 && number <= max;
                        if (!isValid)
                            Console.WriteLine("Enter valid number between 1 and " + max);
                    }
                    if (number == numberToGuess)
                    {
                        double score = 0;
                        switch (counter)
                        {
                            case 1: score = 100 * multiplier;
                                break;
                            case 2: score = 90 * multiplier;
                                break;
                            case 3: score = 80 * multiplier;
                                break;
                            case 4: score = 70 * multiplier;
                                break;
                            case 5: score = 60 * multiplier;
                                break;
                            case 6: score = 50 * multiplier;
                                break;
                            case 7: score = 40 * multiplier;
                                break;
                            case 8: score = 30 * multiplier;
                                break;
                            case 9: score = 20 * multiplier;
                                break;
                            case 10: score = 10 * multiplier;
                                break;
                        }
                        Console.WriteLine($"Congrats {user}, You won in {counter} attempts, your score is {score}");
                        _logger.LogGameHistory(difficulty, user, score, DateTime.Now);
                        Console.WriteLine("Do you want to play again? (y/n)");
                        string again = Console.ReadLine()!;
                        while (again != "n" && again != "y")
                        {
                            Console.WriteLine("y/n");
                            again = Console.ReadLine()!;
                        }
                        if (again == "n")
                        {
                            isGameFinished = true;
                            return;
                        }
                        else if (again == "y")
                        {
                            GuessTheNumber();
                            if (isGameFinished) return;
                        }
                    }
                    else if (number < numberToGuess)
                    {
                        if (counter == 10) { break; }
                        Console.WriteLine("More");
                    }
                    else if (number > numberToGuess)
                    {
                        if (counter == 10) { break; }
                        Console.WriteLine("Less");
                    }
                    counter++;
                }
                Console.WriteLine("You Lose!");
                Console.WriteLine("Do you want to play again? (y/n)");
                string tryAgain = Console.ReadLine()!;
                while (tryAgain != "n" && tryAgain != "y")
                {
                    Console.WriteLine("y/n");
                    tryAgain = Console.ReadLine()!;
                }
                if (tryAgain == "n")
                {
                    break;
                }
            }
        }
    }
}
