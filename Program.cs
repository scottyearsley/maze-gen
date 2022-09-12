using System;

namespace MazeGen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Random Maze Generator");
            Console.WriteLine("=====================");
            Console.WriteLine();

            var maze = new Maze(20,20);
            maze.Generate();

            new HtmlMazeRenderer().Render(maze);
            new ConsoleMazeRenderer().Render(maze);

            Console.Read();
        }
    }
}
