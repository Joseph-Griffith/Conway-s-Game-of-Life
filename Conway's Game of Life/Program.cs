using System;
using System.Threading;

/*
 * Conway's game of life is a zero player game created by British mathematician John Conway in 1970
 * The game simulates life by cells becoming alive or dying based on certain conditions
 * The conditions are as follows:
 *      A cell can come alive if there are exactly three neighbours that are alive
 *      A cell dies if
 *          It has more than three neighbours (Overcrowding)
 *          It has 0 - 1 neighbours (Loneliness)
 */

namespace Conway_s_Game_of_Life
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new game instance
            Game game = new Game();
        }
    }
}
