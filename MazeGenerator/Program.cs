using System;

namespace MazeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var maze = new Maze(15, 15);
            maze.Print();
        }
    }
}
