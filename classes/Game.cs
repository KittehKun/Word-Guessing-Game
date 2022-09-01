namespace Word_Guessing_Game.classes
{
    public class Game
    {
        // - Fields -
        private List<string> words;
        private int winCounter;
        private List<string> mainMenu;
        private Random random;

        // - Constructor -
        public Game()
        {
            this.words = new List<string>();
            this.mainMenu = new List<string>();
            this.random = new Random();
            this.mainMenu.Add("1. New Game");
            this.mainMenu.Add("2. Display Win Counter");
            this.mainMenu.Add("\n0. Exit");
            this.winCounter = 0;
            LoadFile();
            MainMenu();
        }

        // - Methods -

        /// <summary>
        /// Loads words from the files folder.
        /// </summary>
        private void LoadFile()
        {
            int counter = 0;
            try
            {
                foreach(string word in System.IO.File.ReadLines(@"files/Words.txt"))
                {
                    words.Add(word);
                    counter++;
                }
                Console.WriteLine($"{counter} words have been added to the game.\n");
            } catch (Exception error)
            {
                Console.WriteLine($"ERROR: {error.Message}");
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Displays the main menu to the console.
        /// </summary>
        private void MainMenu()
        {
            foreach(string menuOption in this.mainMenu)
            {
                Console.WriteLine(menuOption);
            }
            bool isValidChoice = int.TryParse(Console.ReadLine(), out int menuChoice);
            if(isValidChoice)
            {
                switch(menuChoice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Generating a new game...\n");
                        StartNewGame();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine($"You have won {this.winCounter} games!");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine("\n-= Thank you for playing! =-\n");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Something went wrong. Exiting program...");
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private void StartNewGame()
        {
            string chosenWord = words[random.Next(words.Count+1)].ToLower();
            Console.WriteLine($"{chosenWord} was chosen.");
            int wordLength = chosenWord.Length;
            Console.WriteLine($"The chosen word has a length of {wordLength} characters!");
            char[] charChosenWord = chosenWord.ToCharArray();
            for(int i=0; i < wordLength; i++)
            {
                Console.Write("_ ");
            }
            Console.WriteLine(); //Empty line needed
            BeginGuessPhase(charChosenWord, chosenWord);
        }

        private void BeginGuessPhase(char[] chosenWord, string answer)
        {
            int guesses = 0;
            int maxGuesses = 10;
            string input;
            while(guesses < maxGuesses)
            {
                Console.WriteLine($"You have {maxGuesses - guesses} guesses remaining.\n");
                Console.WriteLine("Please type a letter or word to guess:");
                input = Console.ReadLine()!.ToLower();
                if(input.Length == 1) //Guessed a letter
                {
                    char letterGuess = Convert.ToChar(input);
                    Console.WriteLine($"\nYou guessed {letterGuess}.");
                    
                    for(int i = 0; i < chosenWord.Length; i++)
                    {
                        if(letterGuess == chosenWord[i]) //Correct Guess
                        {
                            Console.WriteLine($"That letter appears at position {i+1} of the word!");
                        }
                    }
                    guesses++; //Increases guesses
                } else //Guessed a word
                {
                    if(input == answer) //Correct guess
                    {
                        this.winCounter++;
                        guesses = maxGuesses+1;
                        Console.WriteLine("\n -= You win! =-\n");
                        MainMenu();
                        return;
                    } else //Incorrect guess
                    {
                        Console.WriteLine($"\nThat word is incorrect.\n");
                        guesses++;
                    }
                }
            }
        }
    }
}