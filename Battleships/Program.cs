using System;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialize P1 board
            char[,] playerOnePlot = new char[10, 10];
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    playerOnePlot[x,y] = '~';
                }
            }

            DisplayBoard(playerOnePlot);
            Console.WriteLine("\nPlayer one Enter first location of destroyer (5)");
            SetupGame(playerOnePlot, 3);
            DisplayBoard(playerOnePlot);



        }
        static void DisplayBoard(char[,] board)
        {
            Console.WriteLine("   1 2 3 4 5 6 7 8 9 10");
            string labels = "ABCDEFGHIJ";
            for (int y = 0; y < 10; y++)
            {
                Console.Write(" " + labels[y] + " ");

                for (int x = 0; x < board.GetLength(1); x++)
                {
                    Console.Write(board[x, y]);
                    Console.Write(" ");
                }
                Console.Write("\n");
            }
        }

        static char[,] SetupGame(char[,] board, int shipLength)
        {
            string coord = Console.ReadLine(); // A1
            int x = (Int16.Parse(Convert.ToString(coord[1])) - 1);
            int y = char.ToUpper(coord[0]) - 64 - 1; // ABC to 012
            Console.WriteLine(x + "" + y);
            Console.Write("Which direction should the battleship point? (N/E/S/W) ");
            char dir = char.ToLower(Console.ReadKey().KeyChar);
            int dirInt;
            Console.Write("\n");
            int xFact = 0;
            int yFact = 0;
            if (dir == 'n')
            {
                yFact = -1;
            }
            if (dir == 's')
            {
                yFact = 1;
            }
            if (dir == 'e')
            {
                xFact = -1;
            }
            if (dir == 'w')
            {
                xFact = 1;
            }

            for (int i = 0; i < shipLength; i++)
            {
                board[x, y] = 'X';
            }

            return board;
        }
    }
}
