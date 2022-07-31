using System;

namespace MazeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var width = 0;
            var height = 0;

            Console.WriteLine("**********************************************");
            Console.WriteLine("*************** Maze Generator ***************");
            Console.WriteLine("**********************************************\n");

            Console.Write("Enter a width for your maze: (45) ");
            var inputWidth = Console.ReadLine();
            while (!string.IsNullOrWhiteSpace(inputWidth) && (!int.TryParse(inputWidth, out width) || width < 5))
            {
                Console.Write("Invalid input, please enter a number greater than 5: ");
                inputWidth = Console.ReadLine();
            }
            if (string.IsNullOrWhiteSpace(inputWidth))
            {
                width = 45;
            }

            Console.Write("Enter a height for your maze: (15) ");
            var inputHeight = Console.ReadLine();
            while (!string.IsNullOrWhiteSpace(inputWidth) && (!int.TryParse(inputHeight, out height) || height < 5))
            {
                Console.Write("Invalid input, please enter a number greater than 5: ");
                inputHeight = Console.ReadLine();
            }
            if (string.IsNullOrWhiteSpace(inputHeight))
            {
                height = 15;
            }

            Console.Clear();

            Console.WriteLine($"{width} x {height} Maze\n");
            var maze = new Maze(width, height);
            maze.Print();
        }
    }
}
