namespace Word_Guessing_Game.classes
{
    public class Game
    {
        // - Fields -
        private List<string> words;
        private int winCounter;
        private List<string> mainMenu;

        // - Constructor -
        public Game()
        {
            this.words = new List<string>();
            this.mainMenu = new List<string>();
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
        }
    }
}