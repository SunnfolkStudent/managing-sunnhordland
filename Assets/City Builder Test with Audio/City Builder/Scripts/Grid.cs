﻿using System;
using System.Collections.Generic;

namespace City_Builder_Test_with_Audio.City_Builder.Scripts
{
    /// <summary>
    /// Source https://github.com/lordjesus/Packt-Introduction-to-graph-algorithms-for-game-developers
    /// </summary>
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is Point point)
            {
                return this.X == point.X && this.Y == point.Y;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 6949;
                hash = hash * 7907 + X.GetHashCode();
                hash = hash * 7907 + Y.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return "P(" + this.X + ", " + this.Y + ")";
        }
    }

    public enum CellType
    {
        Empty,
        Road,
        Structure,
        SpecialStructure,
        None
    }

    public class Grid
    {
        private CellType[,] _grid;
        
        // We have private ints _width and _height, which other scrips can retrieve though the public ints Width and Height.
        private int _width;
        public int Width => _width;
        
        private int _height;
        public int Height => _height;

        private List<Point> _roadList = new List<Point>();
        private List<Point> _specialStructure = new List<Point>();

        // This method below asks for a grid, and returns one.
        public Grid(int width, int height)
        {
            _width = width;
            _height = height;
            _grid = new CellType[width, height];
        }

        // Adding index operator to our Grid class so that we can use grid[][] to access specific cell from our grid. 
        public CellType this[int i, int j]
        {
            get => _grid[i, j];
            set
            {
                if (value == CellType.Road)
                {
                    _roadList.Add(new Point(i, j));
                }
                else
                {
                    _roadList.Remove(new Point(i, j));
                }
                if (value == CellType.SpecialStructure)
                {
                    _specialStructure.Add(new Point(i, j));
                }
                else
                {
                    _specialStructure.Remove(new Point(i, j));
                }
                _grid[i, j] = value;
            }
        }

        public static bool IsCellWalkable(CellType cellType, bool aiAgent = false)
        {
            if (aiAgent)
            {
                return cellType == CellType.Road;
            }
            return cellType == CellType.Empty || cellType == CellType.Road;
        }

        public Point GetRandomRoadPoint()
        {
            Random rand = new Random();
            return _roadList[rand.Next(0, _roadList.Count - 1)];
        }

        public Point GetRandomSpecialStructurePoint()
        {
            Random rand = new Random();
            return _roadList[rand.Next(0, _roadList.Count - 1)];
        }

        public List<Point> GetAdjacentCells(Point cell, bool isAgent)
        {
            return GetWakableAdjacentCells((int)cell.X, (int)cell.Y, isAgent);
        }

        public float GetCostOfEnteringCell(Point cell)
        {
            return 1;
        }

        public List<Point> GetAllAdjacentCells(int x, int y)
        {
            List<Point> adjacentCells = new List<Point>();
            if (x > 0)
            {
                adjacentCells.Add(new Point(x - 1, y));
            }
            if (x < _width - 1)
            {
                adjacentCells.Add(new Point(x + 1, y));
            }
            if (y > 0)
            {
                adjacentCells.Add(new Point(x, y - 1));
            }
            if (y < _height - 1)
            {
                adjacentCells.Add(new Point(x, y + 1));
            }
            return adjacentCells;
        }

        public List<Point> GetWakableAdjacentCells(int x, int y, bool isAgent)
        {
            List<Point> adjacentCells = GetAllAdjacentCells(x, y);
            for (int i = adjacentCells.Count - 1; i >= 0; i--)
            {
                if(IsCellWalkable(_grid[adjacentCells[i].X, adjacentCells[i].Y], isAgent)==false)
                {
                    adjacentCells.RemoveAt(i);
                }
            }
            return adjacentCells;
        }

        public List<Point> GetAdjacentCellsOfType(int x, int y, CellType type)
        {
            List<Point> adjacentCells = GetAllAdjacentCells(x, y);
            for (int i = adjacentCells.Count - 1; i >= 0; i--)
            {
                if (_grid[adjacentCells[i].X, adjacentCells[i].Y] != type)
                {
                    adjacentCells.RemoveAt(i);
                }
            }
            return adjacentCells;
        }

        /// <summary>
        /// Returns array [Left neighbour, Top neighbour, Right neighbour, Down neighbour]
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public CellType[] GetAllAdjacentCellTypes(int x, int y)
        {
            CellType[] neighbours = { CellType.None, CellType.None, CellType.None, CellType.None };
            if (x > 0)
            {
                neighbours[0] = _grid[x - 1, y];
            }
            if (x < _width - 1)
            {
                neighbours[2] = _grid[x + 1, y];
            }
            if (y > 0)
            {
                neighbours[3] = _grid[x, y - 1];
            }
            if (y < _height - 1)
            {
                neighbours[1] = _grid[x, y + 1];
            }
            return neighbours;
        }
    }
}