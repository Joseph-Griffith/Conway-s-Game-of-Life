using System;
using System.Collections.Generic;
using System.Text;

namespace Conway_s_Game_of_Life
{
    /// <summary>
    /// Represents a cell with properties such as X and Y position, if the cell is alive and it's visual output <br></br>
    /// It also contains methods that return how many neighbours it has e.t.c
    /// </summary>
    class Cell
    {
        public const string aliveString = "X  ";
        public const string deadString = ".  ";

        public int X { get; set; }
        public int Y { get; set; }
        public string VOString { get; set; }
        public bool IsDeadNextTurn { get; set; } = true;
        public bool IsAlive { get; set; } = false;

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            VOString = deadString;
        }

        /// <summary>
        /// Checks and returns how many of a cells top, right, bottom and left neighbours are alive
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public int HowManyNeighbours(Grid grid)
        {
            int numOfNeighbours = 0;

            // Top
            if (Y != 0)
            {
                if (grid.grid[this.X, this.Y - 1].IsAlive)
                    numOfNeighbours++;
            }
            // Top-Right
            if (X != Grid.rows - 1 && Y != 0)
            {
                if (grid.grid[this.X + 1, this.Y - 1].IsAlive)
                    numOfNeighbours++;
            }
            // Right
            if (X != Grid.rows - 1)
            {
                if (grid.grid[this.X + 1, this.Y].IsAlive)
                    numOfNeighbours++;
            }
            // Bottom-Right
            if (X != Grid.rows - 1 && Y != Grid.columns - 1)
            {
                if (grid.grid[this.X + 1, this.Y + 1].IsAlive)
                    numOfNeighbours++;
            }
            // Bottom
            if (Y != Grid.columns - 1)
            {
                if (grid.grid[this.X, this.Y + 1].IsAlive)
                    numOfNeighbours++;
            }
            // Bottom-Left
            if (X != 0 && Y != Grid.columns - 1)
            {
                if (grid.grid[this.X - 1, this.Y + 1].IsAlive)
                    numOfNeighbours++;
            }
            // Left
            if (X != 0)
            {
                if (grid.grid[this.X - 1, this.Y].IsAlive)
                    numOfNeighbours++;
            }
            // Top-Right
            if (X != 0 && Y != 0)
            {
                if (grid.grid[this.X - 1, this.Y - 1].IsAlive)
                    numOfNeighbours++;
            }

            return numOfNeighbours;
        }
    }
}
