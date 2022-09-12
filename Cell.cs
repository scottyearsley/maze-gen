using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeGen
{
    public class Cell
    {
        private static Random _random = new Random();

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
            Walls = new List<Wall>
            {
                new Wall(WallPosition.Left),
                new Wall(WallPosition.Top),
                new Wall(WallPosition.Right),
                new Wall(WallPosition.Bottom)
            };
        }

        public int Row { get; }

        public int Column { get; }

        public int VisitIndex { get; set; }

        public bool HasAllWalls => Walls.All(w => w.Standing);

        public List<Wall> Walls { get; private set; }

        public Wall GetRandomWall()
        {
            return Walls[_random.Next(0, 3)];
        }

        public bool HasWall(WallPosition position)
        {
            return Walls.Any(w => w.Position == position && w.Standing);
        }

        public void KnockDownWall(Cell adjacentCell)
        {
            if (adjacentCell.Row < Row)
            {
                adjacentCell.KnockDownWallAtPosition(WallPosition.Bottom);
                KnockDownWallAtPosition(WallPosition.Top);
            }
            else if (adjacentCell.Row > Row)
            {
                adjacentCell.KnockDownWallAtPosition(WallPosition.Top);
                KnockDownWallAtPosition(WallPosition.Bottom);
            }
            else if (adjacentCell.Column < Column)
            {
                adjacentCell.KnockDownWallAtPosition(WallPosition.Right);
                KnockDownWallAtPosition(WallPosition.Left);
            }
            else if (adjacentCell.Column > Column)
            {
                adjacentCell.KnockDownWallAtPosition(WallPosition.Left);
                KnockDownWallAtPosition(WallPosition.Right);
            }
        }

        private void KnockDownWallAtPosition(WallPosition position)
        {
            var wall = Walls.Single(w => w.Position == position);
            wall.Standing = false;
        }

        public override string ToString()
        {
            return $"CELL: {Row}, {Column}";
        }
    }

    public class Wall
    {
        public Wall(WallPosition position)
        {
            Position = position;
        }

        public WallPosition Position { get; }

        public bool Standing { get; set; } = true;

        public override string ToString()
        {
            return $"WALL: {Position}, {Standing}";
        }
    }

    public enum WallPosition
    {
        Left,
        Top,
        Right,
        Bottom
    }
}