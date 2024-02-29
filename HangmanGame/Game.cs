using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HangmanGame
{
    internal class Game
    {
        private readonly ILogger _logger;
        private bool isGameFinished = false;
        string tryAgain = string.Empty;
        public Game(ILogger logger)
        {
            _logger = logger;
        }
        public void GuessTheWord()
        {                     
            int score = 0;
            List<string> words = new List<string>
            {
                "apple", "banana", "orange", "grape", "kiwi",
                "strawberry", "pineapple", "blueberry", "peach", "watermelon"
            };
            while (true)
            {
                Console.WriteLine("Enter your name");
                string name = Console.ReadLine()!;
                string wordToGuess = words[new Random().Next(9)];
                Console.WriteLine($"Word has {wordToGuess.Length} letters");
                char[] letters = new char[wordToGuess.Length];
                for (int i = 0; i < wordToGuess.Length; i++)
                {
                    letters[i] = '-';
                }
                string guessedWord;
                int count = 0;
                for (int i = 0; i < 6; i++)
                {
                    Console.WriteLine("Guess the letter");
                    char guessedLetter = default;
                    bool isValid = false;
                    while (!isValid)
                    {
                        isValid = char.TryParse(Console.ReadLine()!, out guessedLetter) && char.IsLetter(guessedLetter);
                        if (!isValid)
                            Console.WriteLine("Enter valid letter");
                    }
                    bool correct = false;
                    for (int j = 0; j < wordToGuess.Length; j++)
                    {
                        if (char.ToLower(guessedLetter) == wordToGuess[j])
                        {
                            count++;
                            Console.WriteLine($"Letter {guessedLetter} is at the position {j + 1}");
                            letters[j] = guessedLetter;                            
                            score += 10;
                            correct = true;
                            if (count == wordToGuess.Length)
                            {
                                foreach (char letter in letters)
                                {
                                    Console.Write(letter);
                                }
                                Console.WriteLine();
                                Console.WriteLine($"Congrats {name}, you guessed all the letters, you won!");
                                score += 50;
                                _logger.LogGameHistory(name, score, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), wordToGuess, "Won");
                                Console.WriteLine("Do you want to play again? (y/n)");
                                tryAgain = Console.ReadLine()!;
                                while (tryAgain != "n" && tryAgain != "y")
                                {
                                    Console.WriteLine("y/n");
                                    tryAgain = Console.ReadLine()!;
                                }
                                if (tryAgain == "n")
                                {
                                    isGameFinished = true;
                                    return;
                                }
                                else if (tryAgain == "y")
                                {
                                    GuessTheWord();
                                    if (isGameFinished) return;
                                }
                            }
                        }
                    }
                    foreach (char letter in letters)
                    {
                        Console.Write(letter);
                    }
                    Console.WriteLine();
                    if (!correct) { Console.WriteLine("Wrong letter"); }
                    Console.WriteLine($"You can guess {6 - i - 1} more letters");
                }
                if (count == 0)
                {
                    Console.WriteLine("You lost..");
                    _logger.LogGameHistory(name, score, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), wordToGuess, "Lost");
                    Console.WriteLine("Do you want to play again? (y/n)");
                    tryAgain = Console.ReadLine()!;
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
                Console.WriteLine("Guess the word");
                guessedWord = Console.ReadLine()!.ToLower();
                if (guessedWord == wordToGuess)
                {
                    Console.WriteLine($"Congrats {name}, {guessedWord} is right");
                    score += 50;
                    _logger.LogGameHistory(name, score, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), wordToGuess, "Won");
                }
                else
                {
                    Console.WriteLine("Wrong, you lost..");
                    _logger.LogGameHistory(name, score, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), wordToGuess, "Lost");
                }
                Console.WriteLine("Do you want to play again? (y/n)");
                tryAgain = Console.ReadLine()!;
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
