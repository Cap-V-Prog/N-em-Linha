using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace NemLinha_Projeto
{
    public class ConsoleLoader
    {
        private readonly int _progressBarLength = 72;
        private readonly char _progressBarChar = '\u2588';
        private readonly char _emptyProgressBarChar = '\u25a0';

        public void ShowLoader(string message, int totalItems, int itemsLoaded)
        {
            Console.Write(message);

            double percentage = (double)itemsLoaded / totalItems;
            int progressLength = (int)(percentage * _progressBarLength);

            string progressBar = new string(_progressBarChar, progressLength) +
                                 new string(_emptyProgressBarChar, _progressBarLength - progressLength);

            if (itemsLoaded == totalItems)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            Console.Write($"\r|{progressBar}| {Math.Round(percentage * 100)}%\n");

            Console.ResetColor();
        }

        public async Task ExecuteTasks(List<Func<Task>> tasks)
        {
            for (int countdown = 3; countdown > 0; countdown--)
            {
                Console.Clear();
                Console.WriteLine($"Starting program in {countdown} seconds. Press ESC to enable DEBUG.");
                Thread.Sleep(1000);

                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    Program.DebugMode = true;
                    Console.Title = "Connect 4 DEBUG";
                    break;
                }
            }

            // Loop through each task
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.Clear();

                // Display the game title
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(Program.GameTitle);
                Console.ResetColor();

                // Display DEBUG above the title if debug mode is enabled
                if (Program.DebugMode)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("DEBUG");
                    Console.ResetColor();
                }

                // Execute the current task and display loading progress
                await Program.ExecuteTask("", tasks[i]);
                ShowLoader("", tasks.Count, i + 1);

                // Display the task progress
                Console.WriteLine($"Task Progress: {i + 1}/{tasks.Count}\n");

                if (Program.DebugMode && tasks.Count!=i+1)
                {
                    Console.WriteLine("Press any key to proceed ...");
                    Console.ReadKey();
                }
                else
                {
                    Thread.Sleep(50);
                }
            }
        }
    }
}
