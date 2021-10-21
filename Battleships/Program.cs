using System;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Enter Player 1 Name: ");
            string playerOneName = Console.ReadLine();

            Console.WriteLine("Enter Player 2 Name: ");
            string playerTwoName = Console.ReadLine();

            char[,] playerOneBoard = new char[10, 10];
            char[,] playerTwoBoard = new char[10, 10];

            playerOneBoard = InitialisePlayer(playerOneName);
            ChangePlayer();
            Console.Clear();
            playerTwoBoard = InitialisePlayer(playerTwoName);

            // create guess maps

            char[,] playerOneGuessMap = new char[10, 10];
            playerOneGuessMap = NewBoard();

            char[,] playerTwoGuessMap = new char[10, 10];
            playerTwoGuessMap = NewBoard();

            bool gameEnded = false;
            int round = 1;

            while (gameEnded == false)
            {
                ChangePlayer();
                if (round % 2 == 1)   // player 1's go
                {
                    DisplayBoard(playerOneGuessMap);
                    Console.WriteLine();
                    DisplayBoard(playerOneBoard);
                    Console.WriteLine(playerOneName + ": Enter guess coordinates (A1/a1): ");
                    var guessIndexCoords = EnterCoordToIndex();
                    if (playerTwoBoard[guessIndexCoords.Item1, guessIndexCoords.Item2] != '·')
                    {
                        playerTwoBoard[guessIndexCoords.Item1, guessIndexCoords.Item2] = 'X';
                        playerOneGuessMap[guessIndexCoords.Item1, guessIndexCoords.Item2] = 'X';
                        Console.Clear();
                        DisplayBoard(playerOneGuessMap);
                        DisplayBoard(playerOneBoard);
                        Console.WriteLine("\nHIT!");
                    }
                    else
                    {
                        playerTwoBoard[guessIndexCoords.Item1, guessIndexCoords.Item2] = 'O';
                        playerOneGuessMap[guessIndexCoords.Item1, guessIndexCoords.Item2] = 'O';
                        Console.Clear();
                        DisplayBoard(playerOneGuessMap);
                        DisplayBoard(playerOneBoard);
                        Console.WriteLine("\nMISS!");
                    }
                }
                else    // player 2's go
                {
                    DisplayBoard(playerTwoGuessMap);
                    Console.WriteLine();
                    DisplayBoard(playerTwoBoard);
                    Console.WriteLine(playerTwoName + ": Enter guess coordinates: ");
                    var guessIndexCoords = EnterCoordToIndex();
                    if (playerOneBoard[guessIndexCoords.Item1, guessIndexCoords.Item2] != '·')
                    {
                        playerOneBoard[guessIndexCoords.Item1, guessIndexCoords.Item2] = 'X';
                        playerTwoGuessMap[guessIndexCoords.Item1, guessIndexCoords.Item2] = 'X';
                        Console.Clear();
                        DisplayBoard(playerTwoGuessMap);
                        DisplayBoard(playerTwoBoard);
                        Console.WriteLine("\nHIT!");
                    }
                    else
                    {
                        playerOneBoard[guessIndexCoords.Item1, guessIndexCoords.Item2] = 'O';
                        playerTwoGuessMap[guessIndexCoords.Item1, guessIndexCoords.Item2] = 'O';
                        Console.Clear();
                        DisplayBoard(playerTwoGuessMap);
                        DisplayBoard(playerTwoBoard);
                        Console.WriteLine("\nMISS!");
                    }
                }
                round++;
            }

        }

        static Tuple<int, int> EnterCoordToIndex()
        {
            while (true)
            {
                try
                {
                    string coord = Console.ReadLine();
                    int x = Int32.Parse(coord.Remove(0, 1)) - 1;
                    int y = char.ToUpper(coord[0]) - 64 - 1; // ABC to 012

                    var indexCoords = new Tuple<int, int>(x, y);
                    return indexCoords;
                }
                catch (Exception ex)
                {

                    Console.Write("Error info:" + ex.Message);
                    Console.WriteLine("Try again");
                }
            }
        }

        static char[,] NewBoard()
        {
            char[,] board = new char[10, 10];
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    board[x, y] = '·';
                }
            }
            return board;
        }

        static void ChangePlayer()
        {
            Console.WriteLine("Press key to clear screen and change player");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Change player and press key to resume");
            Console.ReadKey();
            Console.Clear();
        }

        static char[,] InitialisePlayer(string playerName)
        {

            Console.Clear();
            char[,] board = NewBoard();

            DisplayBoard(board);
            Console.WriteLine("\n" + playerName + ": Enter first location of Carrier (5)");
            board = PlaceFleet(board, 5);
            Console.Clear();

            DisplayBoard(board);
            Console.WriteLine("\n" + playerName + ": Enter first location of Battleship (4)");
            board = PlaceFleet(board, 4);
            Console.Clear();

            DisplayBoard(board);
            Console.WriteLine("\n" + playerName + ": Enter first location of Destroyer (3)");
            board = PlaceFleet(board, 3);
            Console.Clear();

            DisplayBoard(board);
            Console.WriteLine("\n" + playerName + ": Enter first location of Submarine (3)");
            board = PlaceFleet(board, 3);
            Console.Clear();

            DisplayBoard(board);
            Console.WriteLine("\n" + playerName + ": Enter first location of Patrol Boat (2)");
            board = PlaceFleet(board, 2);
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

                for (int x = 0; x < 10; x++)
                {
                    Console.Write(board[x, y]);
                    Console.Write(" ");
                }
                Console.Write("\n");
            }
        }

        static char[,] PlaceFleet(char[,] board, int shipLength)
        {
            var indexCoords = EnterCoordToIndex();
            int x = indexCoords.Item1;
            int y = indexCoords.Item2;
            Console.Write("Which direction should the battleship point? (arrow key) ");
            var dir = Console.ReadKey();
            Console.Write("\n");
            int xFact = 0;
            int yFact = 0;
            if (dir.Key == ConsoleKey.UpArrow)
            {
                yFact = -1;
            }
            else if (dir.Key == ConsoleKey.DownArrow)
            {
                yFact = 1;
            }
            else if (dir.Key == ConsoleKey.RightArrow)
            {
                xFact = 1;
            }
            else if (dir.Key == ConsoleKey.LeftArrow)
            {
                xFact = -1;
            }

            if (xFact != 0) // horizontal placement
            {
                for (int i = 0; Math.Abs(i) < shipLength; i += xFact)
                {
                    board[i + x, y] = '=';
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
