using System;
using System.Threading;

namespace NemLinha_Projeto
{
    class Program
    {
        static void Main()
        {
            int[,] gameGrid = { 
                {1,2,3,4,1}, 
                {6,7,2,1,0}, 
                {1,2,1,2,5}, 
                {6,1,8,9,0}, 
                {1,2,3,4,5} };
            Grid uIgrid = new Grid();
            Console.Clear();
            uIgrid.DisplayGrid(5,5,gameGrid);
            Thread.Sleep(5000);
            Console.Clear();
            int[,] GameGrid2={ 
                {1,2,3,4,1}, 
                {6,7,2,1,0}, 
                {1,2,1,1,5}, 
                {6,1,8,1,0}, 
                {1,2,3,1,5} };
            uIgrid.DisplayGrid(5, 5, GameGrid2);
        }
    }
}