using System;
using System.Collections.Generic;

namespace MazeGenerator
{
    class Cell
    {
        public Cell()
        {
            IsWall = true;
            PathsTo = new HashSet<Cell>();
        }

        public bool IsWall { get; set; }

        /// <summary>
        /// Gets the string representation of this cell for printing to console.
        /// </summary>
        /// <returns>String representation of this cell.</returns>
        public string ConsoleString() => IsWall ? "#" : " ";

        public HashSet<Cell> PathsTo { get; set; }
    }
}
