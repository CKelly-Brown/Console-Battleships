using System;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            char[,] playerOneBoard = new char[10, 10];
            char[,] playerTwoBoard = new char[10, 10];
            playerOneBoard = InitialisePlayer("Player one");
            ChangePlayer();
            playerTwoBoard = InitialisePlayer("Player two");

        }

        static void ChangePlayer()
        {
            Console.WriteLine("Press key to clear screen and change player");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("New player press key to resume");
            Console.ReadKey();
            Console.Clear();
        }

        static char[,] InitialisePlayer(string playerName)
        {

            Console.Clear();
            char[,] board = new char[10, 10];
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    board[x, y] = '·';
                }
            }
            DisplayBoard(board);
            Console.WriteLine("\n" + playerName + ": Enter first location of Carrier (5)");
            board = SetupGame(board, 5);
            Console.Clear();

            DisplayBoard(board);
            Console.WriteLine("\n" + playerName + ": Enter first location of Battleship (4)");
            board = SetupGame(board, 4);
            Console.Clear();

            DisplayBoard(board);
            Console.WriteLine("\n" + playerName + ": Enter first location of Destroyer (3)");
            board = SetupGame(board, 3);
            Console.Clear();

            DisplayBoard(board);
            Console.WriteLine("\n" + playerName + ": Enter first location of Submarine (3)");
            board = SetupGame(board, 3);
            Console.Clear();

            DisplayBoard(board);
            Console.WriteLine("\n" + playerName + ": Enter first location of Patrol Boat (2)");
            board = SetupGame(board, 2);
            Console.Clear();
            DisplayBoard(board);
            return board;
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
            Console.Write("\n");
            int xFact = 0;
            int yFact = 0;
            if (dir == 'n')
            {
                yFact = -1;
            }
            else if (dir == 's')
            {
                yFact = 1;
            }
            else if (dir == 'e')
            {
                xFact = 1;
            }
            else if (dir == 'w')
            {
                xFact = -1;
            }

            if (xFact != 0) // horizontal placement
            {
                for (int i = 0; Math.Abs(i) < shipLength; i += xFact)
                {
                    board[i + x, y] = '~';
                }
            }

            if (yFact != 0) // vertical placement
            {
                for (int n = 0; Math.Abs(n) < shipLength; n += yFact)
                {
                    board[x, n + y] = 'I';
                }
            }


            return board;
        }
    }
}
