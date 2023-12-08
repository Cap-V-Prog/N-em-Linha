﻿using System;
namespace NemLinha_Projeto
{
    public class Menu
    {
        public bool ConfirmAction(string prompt)
        {
            Console.WriteLine(prompt);
            Console.Write("Digite 's' para confirmar ou 'n' para cancelar: ");
            string userInput = Console.ReadLine().ToLower();

            return userInput == "s";
        }
        
        public int ShowMenu(string[] options,string menuTitle = "MENU",int style=1,string specialText=null,bool spaceExit=true )
        {
            
            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{MenuStyles(menuTitle,style)}\n");

                if (specialText != null)
                {
                    Console.WriteLine(specialText);
                }
                
                for (int i = 0; i < options.Length - 1; i++)
                {
                    if (i == selectedIndex)
                    {
                        if (specialText != null && selectedIndex == options.Length-2)
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
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
                
                if(spaceExit)
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
    }
}