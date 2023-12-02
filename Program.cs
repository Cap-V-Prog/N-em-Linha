using System;
using System.Threading;

namespace NemLinha_Projeto
{
    class Program
    {
        static void Main()
        {
            Menu menu = new Menu();
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
            int selectedIndex = menu.ShowMenu(menuOptions,"Menu principal",0);

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

        static void CloseProgram()
        {
            Console.Clear();
            Console.WriteLine("Closing the program. Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }
        
    }
}