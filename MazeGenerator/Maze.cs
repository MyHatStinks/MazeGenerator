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

            //// Start position somewhere along the top
            var start = rand.Next(1, Width);
            Cells[0][start].IsWall = false;

            //// End position somewhere along the bottom
            var end = rand.Next(1, Width);
            Cells[Cells.Count - 1][end].IsWall = false;
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

                // Ensure we're not making a loop
                if (GetNeighbours(next.x, next.y).Any(c => c != thisCell && !c.IsWall))
                {
                    continue;
                }

                if (next.x < x) Cells[x - 1][y].IsWall = false;
                if (next.x > x) Cells[x + 1][y].IsWall = false;
                if (next.y < y) Cells[x][y - 1].IsWall = false;
                if (next.y > y) Cells[x][y + 1].IsWall = false;

                // Process the next cell
                CarveFromCell(next.x, next.y);
            }
        }

        private List<Cell> GetNeighbours(int x, int y)
        {
            if (x < 1 || x >= Width - 1) throw new ArgumentOutOfRangeException(nameof(x), "Must be within the maze.");
            if (y < 1 || y >= Height - 1) throw new ArgumentOutOfRangeException(nameof(y), "Must be within the maze.");

            var neighbours = new List<Cell>();

            if (x - 2 > 0)
            {
                neighbours.Add(Cells[x - 2][y]);
            }
            if (y - 2 > 0)
            {
                neighbours.Add(Cells[x][y - 2]);
            }

            if (x + 2 < Width)
            {
                neighbours.Add(Cells[x + 2][y]);
            }
            if (y + 2 > Height)
            {
                neighbours.Add(Cells[x][y + 2]);
            }

            return neighbours;
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
