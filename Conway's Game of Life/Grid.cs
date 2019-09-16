using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Conway_s_Game_of_Life
{
    /// <summary>
    /// Contains the properties and methods for creating and drawing the game grid to the console
    /// </summary>
    class Grid
    {
        public const int columns = 20;
        public const int rows = 20;

        public Cell[,] grid = new Cell[columns, rows];

        public Grid(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath))
            {
                throw new InvalidOperationException("File not found, file is either missing or corrupt");
            }

            FileStream fileStream = File.OpenRead(Path.Combine(Environment.CurrentDirectory, filepath));
            StreamReader reader = new StreamReader(fileStream);

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    this.grid[x, y] = new Cell(x, y);

                    // Check to see if this cell should be alive...
                    char nextChar = (char)reader.Read();
                    // If the current char is the return char, skip the next two chars as the next char is the new line char
                    if (nextChar == '\r')
                    {
                        nextChar = (char)reader.Read();
                        nextChar = (char)reader.Read();
                    }

                    if (nextChar == 'X')
                    {
                        // Set the cell to the alive state 
                        grid[x, y].IsAlive = true;
                        grid[x, y].IsDeadNextTurn = false;
                        grid[x, y].VOString = Cell.aliveString;
                    }
                }
            }

            // Close the file stream and the stream reader
            fileStream.Close();
            reader.Close();
        }

        /// <summary>
        /// Draws the grid to the console
        /// </summary>
        public void DrawGrid()
        {
            Console.Clear();

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (grid[x, y].IsAlive)
                    {
                        // Set the cell font to green if it is alive
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.Write(this.grid[x, y].VOString);

                    // Set the cell font to dark grey if it is dead
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }

                Console.WriteLine();
            }

            Console.ResetColor();
            Console.WriteLine("\nPress and hold 'Enter' to iterate through the cells evolution" );
            Console.WriteLine("      To exit press 'Q' followed by enter");
        }
    }
}
