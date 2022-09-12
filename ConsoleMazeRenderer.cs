using System;
using System.Linq;
using System.Collections.Generic;

namespace MazeGen
{
    public class ConsoleMazeRenderer: IMazeRenderer
    {
        private const string Offset = "  ";
        private const string Block = "██";
        private const string Empty = "  ";

        public void Render(Maze maze)
        {
            RenderFirstRow(maze.Cells.Where(c => c.Row == 0).ToList());

            for (int rowIndex = 0; rowIndex < maze.Height; rowIndex++)
            {
                RenderMazeRow(maze.Cells.Where(c => c.Row == rowIndex).ToList());
            }

            //RenderFirstRow(maze.Cells.Where(c => c.Row == 0).ToList());
        }

        private void RenderMazeRow(IList<Cell> cells)
        {
            void Render(Cell cell, bool bottomRun = false)
            {
                if(cell.HasWall(WallPosition.Left) || bottomRun)
                {
                    RenderBlock();
                }
                else
                {
                    RenderEmpty();
                }

                if (bottomRun && cell.HasWall(WallPosition.Bottom))
                {
                    RenderBlock();
                }
                else
                {
                    RenderEmpty();
                }

                if (cells.IndexOf(cell) == cells.Count -1)
                {
                    RenderBlock();
                }
            }

            RenderLeftOffset();

            // First row
            foreach (var cell in cells)
            {
                Render(cell);
            }

            RenderNewLine();
            RenderLeftOffset();

            // Second row render
            foreach (var cell in cells)
            {
                Render(cell, true);
            }

            RenderNewLine();
        }

        private void RenderFirstRow(IList<Cell> cells)
        {
            RenderLeftOffset();

            foreach (var cell in cells)
            {
                RenderBlock();

                if (cell.HasWall(WallPosition.Top))
                {
                    RenderBlock();
                }
                else
                {
                    RenderEmpty();
                }
            }

            // Render right outer wall
            RenderBlock();
            RenderNewLine();
        }

        private void RenderBlock()
        {
            Console.Write(Block);
        }

        private void RenderEmpty()
        {
            Console.Write(Empty);
        }

        private void RenderNewLine()
        {
            Console.Write(Environment.NewLine);
        }

        private void RenderLeftOffset()
        {
            Console.Write(Offset);
        }
    }
}