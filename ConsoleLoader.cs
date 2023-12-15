using System;
using System.Threading;

namespace NemLinha_Projeto
{
    public class ConsoleLoader
    {
        private const int ProgressBarLength = 72;
        private readonly char _progressBarChar = '\u2588';
        private readonly char _emptyProgressBarChar = '\u25a0';

        public void ShowLoader(string message, int totalItems)
        {
            Random random = new Random();
            Console.Write(message);

            for (int i = 0; i <= totalItems; i++)
            {
                double percentage = (double)i / totalItems;
                int progressLength = (int)(percentage * ProgressBarLength);

                string progressBar = new string(_progressBarChar, progressLength) +
                                     new string(_emptyProgressBarChar, ProgressBarLength - progressLength);

                Console.Write($"\r[{progressBar}] {Math.Round(percentage * 100)}%");
                int randomTime = random.Next(100, 1000);
                Thread.Sleep(randomTime);

                if (i == totalItems)
                {
                    Console.WriteLine("\n");
                }
            }
        }
    }
}