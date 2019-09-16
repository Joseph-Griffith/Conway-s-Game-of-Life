using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Conway_s_Game_of_Life
{
    /// <summary>
    /// Contains all the logic for executing and displaying the game to the console
    /// </summary>
    class Game
    {
        private string[] m_MainMenuText;
        private string[] m_DescriptionText;
        private string[] m_GameOptionsText;
        private string userSelection;
        private bool hasQuit = false;
        private Grid grid;

        public Game()
        {
            // Generate all menu text from the the menu files
            m_MainMenuText = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, @"Assets\MenuText\MainMenu.txt"));
            m_DescriptionText = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, @"Assets\MenuText\Description.txt"));
            m_GameOptionsText = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, @"Assets\MenuText\GameOptions.txt"));

            // Display the main menu
            MainMenu();
        }

        /// <summary>
        /// Displays the main menu on the console and executes the users selection
        /// </summary>
        private void MainMenu()
        {
            do
            {
                Console.Clear();
                DisplayText(m_MainMenuText); // Doing this twice to fix a formatting error. TODO: Fix this
                Console.Clear();
                DisplayText(m_MainMenuText);
                userSelection = Console.ReadKey().Key.ToString();

                // Execute the users selection
                switch (userSelection)
                {
                    case "S":
                        {
                            StartNewGame();
                            userSelection = null;
                            break;
                        }
                    case "D":
                        {
                            DisplayDescription();
                            userSelection = null;
                            break;
                        }
                    case "Q":
                        {
                            QuitGame();
                            userSelection = null;
                            break;
                        }
                    default:
                        break;
                }
            } while (!hasQuit);
        }

        /// <summary>
        /// Writes each line of text to the console
        /// </summary>
        /// <param name="text"></param>
        private void DisplayText(string[] text)
        {
            foreach (string line in text)
            {
                Console.WriteLine(line);
            }
        }
        
        /// <summary>
        /// Displays the games description on the console
        /// </summary>
        private void DisplayDescription()
        {
            do
            {
                Console.Clear();
                DisplayText(m_DescriptionText);
                userSelection = Console.ReadKey().Key.ToString();
            } while (userSelection != "Escape");
        }

        /// <summary>
        /// Shows the new game menu which displays the possible game modes and executes the users selection
        /// </summary>
        private void StartNewGame()
        {
            // Display Game options
            do
            {
                Console.Clear();
                DisplayText(m_GameOptionsText);
                userSelection = Console.ReadKey().Key.ToString();

                // Execute the users selection
                switch (userSelection)
                {
                    case "D1":
                    case "NumPad1":
                        {
                            grid = new Grid(@"Assets\GridLayouts\Blinker.txt");
                            Turn(grid);
                            break;
                        }
                    case "D2":
                    case "NumPad2":
                        {
                            grid = new Grid(@"Assets\GridLayouts\Glider.txt");
                            Turn(grid);
                            break;
                        }
                    case "D3":
                    case "NumPad3":
                        {
                            grid = new Grid(@"Assets\GridLayouts\Diehard.txt");
                            Turn(grid);
                            break;
                        }
                    case "D4":
                    case "NumPad4":
                        {
                            grid = new Grid(@"Assets\GridLayouts\Pulsar.txt");
                            Turn(grid);
                            break;
                        }
                    default:
                        break;
                }
            } while (userSelection != "Q");
        }

        /// <summary>
        /// Executes the turn logic and displays the current grid state to the console
        /// </summary>
        /// <param name="grid"></param>
        private void Turn(Grid grid)
        {
            do
            {
                userSelection = "";
                grid.DrawGrid();

                foreach (Cell cell in grid.grid)
                {
                    // Check against the copy of the grid here so the cells neighbour checks are not affected by the cells changing whether they are alive or not this turn
                    int numOfNeighbours = cell.HowManyNeighbours(grid);

                    // Check to see if the sell is overcrowded/lonely or not and set the cell to alive or dead accordingly
                    if (numOfNeighbours <= 1 || numOfNeighbours > 3)
                    {
                        //cell.IsAlive = false;
                        //cell.VOString = cell.deadString;
                        cell.IsDeadNextTurn = true;
                    }
                    else if (numOfNeighbours == 3)
                    {
                        cell.IsDeadNextTurn = false;
                    }
                }

                // Sets the cell to the correct state (Alive or Dead)
                SetCellState(grid);

                // Pause for half a second so the user has can observe the changes this turn 
                Thread.Sleep(200);

                userSelection = Console.ReadLine().ToString().ToUpper();
            } while (userSelection != "Q");
        }

        /// <summary>
        /// Sets the cell to dead or alive 
        /// </summary>
        /// <param name="grid"></param>
        private void SetCellState(Grid grid)
        {
            foreach (Cell cell in grid.grid)
            {
                if (cell.IsDeadNextTurn)
                {
                    cell.IsAlive = false;
                    cell.VOString = Cell.deadString;
                }
                else
                {
                    cell.IsAlive = true;
                    cell.VOString = Cell.aliveString;
                }
            }
        }

        /// <summary>
        /// Checks that the user really wants to quit the game
        /// </summary>
        private void QuitGame()
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want to quit?");
            Console.WriteLine("If yes press 'Q', otherwise press any other key");
            userSelection = Console.ReadKey().Key.ToString();

            if (userSelection == "Q")
            {
                hasQuit = true;
            }
        }
    }
}
