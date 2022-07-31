using System;

namespace MazeGenerator
{
    class Cell
    {
        public Cell()
        {
            IsWall = true;
        }

        public bool IsWall { get; set; }

        /// <summary>
        /// Gets the string representation of this cell for printing to console.
        /// </summary>
        /// <returns>String representation of this cell.</returns>
        public string ConsoleString() => IsWall ? "#" : " ";
    }
}
