using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MazeGenerator
{
    class Maze
    {
        private List<List<Cell>> Cells { get; set; }

        private int Width => Cells.Count;
        private int Height => Cells.Count == 0 ? 0 : Cells[0].Count;

        public Maze(int width, int height)
        {
            // Init cells
            Cells = new List<List<Cell>>();
            for (var colId = 0; colId < height; colId++)
            {
                var column = new List<Cell>();

                for (var rowId = 0; rowId < width; rowId++)
                {
                    column.Add(new Cell());
                }

                Cells.Add(column);
            }

            // Begin carving
            CarveMaze();
        }

        private void CarveMaze()
        {
            var rand = new Random();

            // Carve inner walls
            CarveFromCell(
                rand.Next(0, (Width - 1) / 2) * 2 + 1,
                rand.Next(0, (Height - 1) / 2) * 2 + 1);

            // Start position somewhere along the top
            var start = rand.Next(1, Width);
            var row = 0;
            Cells[row][start].IsWall = false;

            if (Cells[row + 1][start].IsWall)
            {
                Cells[row + 1][start].IsWall = false;
            }

            // End position somewhere along the bottom
            var end = rand.Next(1, Width);
            row = Cells.Count - 1;
            Cells[row][end].IsWall = false;

            if (Cells[row - 1][end].IsWall)
            {
                Cells[row - 1][end].IsWall = false;
            }
        }

        private void CarveFromCell(int x, int y)
        {
            var rand = new Random();

            var thisCell = Cells[x][y];

            if (!thisCell.IsWall)
            {
                return;
            }

            thisCell.IsWall = false;

            // Find next directions
            (int x,int y)[] nextCells = new[]
            {
                (x - 2, y),
                (x + 2, y),
                (x, y - 2),
                (x, y + 2),
            }.OrderBy(_ => rand.Next()).ToArray();

            foreach (var next in nextCells)
            {
                // Make sure we're not on the edge
                if (next.x < 1 || next.x >= Width - 1)
                {
                    continue;
                }

                if (next.y < 1 || next.y >= Height - 1)
                {
                    continue;
                }

                var nextCell = GetCell(next.x, next.y);

                // Ensure we're not making a loop
                if (nextCell.PathsTo.Any(c => c != thisCell))
                {
                    continue;
                }

                // Setting up paths
                if (next.x < x) Cells[x - 1][y].IsWall = false;
                if (next.x > x) Cells[x + 1][y].IsWall = false;
                if (next.y < y) Cells[x][y - 1].IsWall = false;
                if (next.y > y) Cells[x][y + 1].IsWall = false;

                thisCell.PathsTo.Add(nextCell);
                nextCell.PathsTo.Add(thisCell);

                // Process the next cell
                CarveFromCell(next.x, next.y);
            }
        }

        public Cell GetCell (int x, int y)
        {
            if (x < 1 || x >= Width - 1) throw new ArgumentOutOfRangeException(nameof(x), "Must be within the maze.");
            if (y < 1 || y >= Height - 1) throw new ArgumentOutOfRangeException(nameof(y), "Must be within the maze.");

            return Cells[x][y];
        }

        public void Print()
        {
            Console.WriteLine("Printing Maze");

            Cells.ForEach(row => {
                row.ForEach(cell => Console.Write(cell.ConsoleString()));
                Console.WriteLine();
            });

            Console.WriteLine("Complete!");
        }
    }
}
