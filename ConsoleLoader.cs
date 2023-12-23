using System;

namespace NemLinha_Projeto
{
    public class ConsoleLoader
    {
        private const int ProgressBarLength = 72;
        private readonly char _progressBarChar = '\u2588';
        private readonly char _emptyProgressBarChar = '\u25a0';

        public void ShowLoader(string message, int totalItems,int itemsLoaded)
        {
            Console.Write(message);
            
            double percentage = (double)itemsLoaded / totalItems;
            int progressLength = (int)(percentage * ProgressBarLength);

            string progressBar = new string(_progressBarChar, progressLength) +
                                     new string(_emptyProgressBarChar, ProgressBarLength - progressLength);

            if (itemsLoaded == totalItems)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            Console.Write($"\r|{progressBar}| {Math.Round(percentage * 100)}%");
                
            Console.ResetColor();

            if (itemsLoaded == totalItems)
            {
                Console.WriteLine("\n");
            }
        }
    }
}