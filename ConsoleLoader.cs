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

        public void ShowLoader(string message, int totalItems,int itemsLoaded)
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
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.Clear();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(Program.GameTitle);
                Console.ResetColor();
                await Program.ExecuteTask($"", tasks[i]);
                ShowLoader("",tasks.Count,i+1);
                Console.WriteLine($"Task Progress: {i + 1}/{tasks.Count}\n");
                //ensure all is done
                Thread.Sleep(100);
            }
        }
    }
}