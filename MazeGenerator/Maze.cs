using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MazeGenerator
{
    class Maze
    {
        private List<List<Cell>> cells { get; set; }

        public Maze(int width, int height)
        {
            // Init cells
            cells = new List<List<Cell>>();
            for (var colId = 0; colId < height; colId++)
            {
                var column = new List<Cell>();

                for (var rowId = 0; rowId < width; rowId++)
                {
                    column.Add(new Cell());
                }

                cells.Add(column);
            }
        }

        public void Print()
        {
            Console.WriteLine("Printing Maze");

            cells.ForEach(row => {
                row.ForEach(cell => Console.Write(cell.ConsoleString()));
                Console.WriteLine();
            });

            Console.WriteLine("Complete!");
        }
    }
}
