using System;

namespace NemLinha_Projeto
{
    //┌ ┐ │ ├ ┤ └ ┘ ┼ ┬ ┴
    //⏺ ◍ ⚪ ⚫
    public class Grid
    {
        private string _grid;
        private string _topLine="";
        private string _midGridDivision="";
        private string _botLine="";
        public void DisplayGrid(int rows, int columns,int[,] gameGrid)
        {
            // Generate and display the grid
            if (_grid is null)
            {
                GenerateGridBoundries(rows,3);
                this._grid = GenerateGrid(rows, columns,gameGrid,3);
            }
            else
            {
                this._grid = GenerateGrid(rows, columns,gameGrid,3);
            }
            Console.WriteLine(_grid);
        }

        private void GenerateGridBoundries(int columns, int cellWidth = 5)
        {
            string horizontalLine = new string('─', cellWidth);
            
            //Top Line of Grid
            _topLine += "\u250c";
            for (int c = 0; c < columns; c++)
            {
                _topLine += horizontalLine;
                if (c + 1 == columns)
                {
                    _topLine += $"\u2510";
                }
                else
                {
                    _topLine += $"\u252c";
                }
            }
            _topLine += "\n";

            //Mid Line of Grid
            _midGridDivision += $"\u251c";
            for (int c = 0; c < columns; c++)
            {
                _midGridDivision += horizontalLine;
                if (c + 1 == columns)
                {
                    _midGridDivision += $"\u2524";
                }
                else
                {
                    _midGridDivision += $"\u253c";
                }
            }
            _midGridDivision += "\n";
            
            //Bot Line of Grid
            _botLine += $"\u2514";
            for (int c = 0; c < columns; c++)
            {
                _botLine += horizontalLine;
                if (c + 1 == columns)
                {
                    _botLine += $"\u2518";
                }
                else
                {
                    _botLine += $"\u2534";
                }
            }
        }

        public string GenerateGrid(int rows, int columns, int[,] gameGrid,int cellHeight=5)
        {
            string grid = "";
            
            string verticalLine = new string(' ', cellHeight);
            
            grid+=this.Lines(verticalLine, rows, columns,gameGrid);
            
            return grid;
        }

        private string Lines(string verticalLine,int rows,int columns,int[,] gameGrid=null)
        {
            string lineRows="";
            lineRows += _topLine;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    if (gameGrid != null)
                        switch (gameGrid[r, c])
                        {
                            case 1:
                                lineRows += "│ O ";
                                break;
                            case 2:
                                lineRows += "│ X ";
                                break;
                            default:
                                lineRows += "│" + verticalLine;
                                break;
                        }
                }
                lineRows += "│\n";
                
                if (r < rows - 1)
                {
                    lineRows+=_midGridDivision;
                }
                else
                {
                    lineRows += _botLine;
                }
                
            }

            return lineRows;
        }
    } 
}