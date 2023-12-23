using System;
namespace NemLinha_Projeto
{
    public class Menus
    {
        public bool ConfirmAction(string prompt)
        {
            Console.WriteLine(prompt);
            Console.Write(Program.LanguageManager.Translate("confirm_delete_text"));
            string userInput = Console.ReadLine()?.ToLower();

            //Anything else will be considered as error by the user
            return userInput == "s"||userInput=="y"||userInput=="j"||userInput=="o";
        }
        
        public int ShowMenu(string[] options, string menuTitle = "MENU", int style = 1, string specialText = null, bool spaceExit = true, int highlightedIndex = -1, string gameTitle=null)
        {
            bool looping = true;
            int selectedIndex = 0;

            while (looping)
            {
                Console.Clear();
                if (gameTitle != null)
                {
                    Console.WriteLine(gameTitle);
                }
                Console.WriteLine($"{MenuStyles(menuTitle, style)}\n");

                if (specialText != null)
                {
                    Console.WriteLine($"{specialText}\n");
                }

                for (int i = 0; i < options.Length - 1; i++)
                {
                    if (i == highlightedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                        
                    if (i == selectedIndex)
                    {
                        if (highlightedIndex != -1 && i == highlightedIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue; // Set the color for the highlighted item
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                    }

                    Console.WriteLine(options[i]);

                    Console.ResetColor();
                }

                if (spaceExit)
                {
                    // Add a larger space before the "Exit" option
                    Console.WriteLine("\n");
                }

                // Highlight the "Exit" option if it's selected
                if (options.Length - 1 == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(options[options.Length - 1]);

                Console.ResetColor();

                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % options.Length;
                        break;
                    case ConsoleKey.Enter:
                        looping = false;
                        return selectedIndex;
                }
            }

            return selectedIndex;
        }


        static string MenuStyles(string menuTitle, int styleType)
        {
            int borderLength = menuTitle.Length + 10; // Minimum border size

            string middleLine = $"║    {menuTitle.ToUpper()}    ║";
            string upperLine = $"╔{new string('═', borderLength - 2)}╗";
            string underLine = $"╚{new string('═', borderLength - 2)}╝";

            switch (styleType)
            {
                case 0:
                    return $"{upperLine}\n{middleLine}\n{underLine}";
                default:
                    return $"{upperLine}\n{middleLine}\n{underLine}";
            }
        }
    }
}