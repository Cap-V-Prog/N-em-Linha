using System;
using System.Threading;

namespace NemLinha_Projeto
{
    class Program
    {
        static void Main()
        {
            /*
            int[,] gameGrid = { 
                {1,2,3,4,1}, 
                {6,7,2,1,0}, 
                {1,2,1,2,5}, 
                {6,1,8,9,0}, 
                {1,2,3,4,5} };
            Grid uIgrid = new Grid();
            Console.Clear();
            uIgrid.DisplayGrid(5,5,gameGrid);
            Thread.Sleep(2500);*/
            
            Console.CursorVisible = false;
            
            string[] menuOptions = { "Novo jogo", "Continuar","Jogadores", "Opções", "Sair" };
            int selectedIndex = ShowMenu(menuOptions,"Menu principal",0);

            if (selectedIndex == menuOptions.Length - 1)
            {
                CloseProgram();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"You selected: {menuOptions[selectedIndex]}");
                Console.ReadKey();
            }
        }

        static int ShowMenu(string[] options,string menuTitle = "MENU",int style=1 )
        {
            
            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{MenuStyles(menuTitle,style)}\n");
                
                for (int i = 0; i < options.Length - 1; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(options[i]);

                    Console.ResetColor();
                }

                // Add a larger space before the "Exit" option
                Console.WriteLine("\n");

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
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % options.Length;
                        break;
                    case ConsoleKey.Enter:
                        return selectedIndex;
                }
            }
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

        static void CloseProgram()
        {
            Console.Clear();
            Console.WriteLine("Closing the program. Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }
        
    }
}