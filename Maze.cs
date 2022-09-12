using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeGen
{
    public class Maze
    {
        // Based upon https://www.c-sharpcorner.com/article/generating-maze-using-C-Sharp-and-net/

        private int _totalCells;
        private int _visitedCells;
        private Cell _currentCell;
        private static Random _random = new Random();
        private Stack<Cell> _cellStack = new Stack<Cell>();
        
        public List<Cell> Cells { get; private set; } = new List<Cell>();
        public int Width { get; }
        public int Height { get; }

        public Maze(int width = 4, int height = 4)
        {
            Width = width;
            Height = height;
            _totalCells = Width * Height;

            InitilizeCells();
        }

        public void Generate() 
        {
            while(_visitedCells < _totalCells)
            {
                var neighbours = GetNeighboursWithWalls();
                if (neighbours.Count > 0)
                {
                    var randomIndex = _random.Next(0, neighbours.Count);
                    var selectedCell = neighbours[randomIndex];
                    _currentCell.KnockDownWall(selectedCell);
                    
                    _cellStack.Push(_currentCell);
                    _currentCell = selectedCell;

                    _visitedCells++;

                    //Understand path
                    _currentCell.VisitIndex = _visitedCells;
                }
                else
                {
                    if (_cellStack.Count > 0)
                    {
                        _currentCell = _cellStack.Pop();
                    }
                    else
                    {
                        break;
                    } 
                }
            }

            var entryCell = GetCellAt(0,0);
            entryCell.Walls.Single(w => w.Position == WallPosition.Top).Standing = false;

            var exitCell = GetCellAt(Height - 1, Width - 1);
            exitCell.Walls.Single(w => w.Position == WallPosition.Bottom).Standing = false;

        }

        private void InitilizeCells()
        {
            for (int rowIndex = 0; rowIndex < Width; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < Height; columnIndex++)
                {
                    Cells.Add(new Cell(rowIndex, columnIndex));
                }
            }

            // Start at top-left
            _currentCell = Cells[0];
        }

        private List<Cell> GetNeighboursWithWalls()
        {
            var cellLeft = GetCellAt(_currentCell.Row, _currentCell.Column -1);
            var cellRight = GetCellAt(_currentCell.Row, _currentCell.Column +1);
            var cellTop = GetCellAt(_currentCell.Row - 1, _currentCell.Column);
            var cellBottom = GetCellAt(_currentCell.Row + 1, _currentCell.Column);

            var cells = new List<Cell> { cellLeft, cellTop, cellRight, cellBottom }
                .Where(c => c != null && c.HasAllWalls)
                .ToList();

            return cells;
        }

        public Cell GetCellAt(int row, int column)
        {
            return Cells.SingleOrDefault(c => c.Row == row && c.Column == column);
        }
    }
}