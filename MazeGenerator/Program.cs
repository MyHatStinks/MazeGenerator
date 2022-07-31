using System;

namespace MazeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var maze = new Maze(45, 15);
            maze.Print();
        }
    }
}
