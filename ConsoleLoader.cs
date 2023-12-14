using System;
using System.Threading;

namespace NemLinha_Projeto
{
    public class ConsoleLoader
    {
        private const int ProgressBarLength = 50;
        private readonly char ProgressBarChar = '\u2588';
        private readonly char EmptyProgressBarChar = '\u25a0';

        public void ShowLoader(string message, int totalItems)
        {
            Random random = new Random();
            Console.Write(message);

            for (int i = 0; i <= totalItems; i++)
            {
                double percentage = (double)i / totalItems;
                int progressLength = (int)(percentage * ProgressBarLength);

                string progressBar = new string(ProgressBarChar, progressLength) +
                                     new string(EmptyProgressBarChar, ProgressBarLength - progressLength);

                Console.Write($"\r[{progressBar}] {Math.Round(percentage * 100)}%");
                int randomTime = random.Next(100, 1000);
                Thread.Sleep(randomTime); // Simulate work being done

                if (i == totalItems)
                {
                    Console.WriteLine("\n"); // Move to the next line when loading is complete
                }
            }
        }
    }
}