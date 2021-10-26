using System;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            string playerOneName;
            string playerTwoName;

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Player 1 Name: ");
                    playerOneName = Console.ReadLine();
                    if (playerOneName.Length < 1)
                    {
                        throw new Exception();
                    }
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid format, Try again\n");
                }
            }

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Player 2 Name: ");
                    playerTwoName = Console.ReadLine();
                    if (playerTwoName.Length < 1)
                    {
                        throw new Exception();
                    }
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid format, Try again\n");
                }
            }

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
                    var guessIndexCoords = new Tuple<int, int>(11,11); // values should be replaced but if not, 11 will ensure error
                    while (true)
                    {
                        try
                        {
                            DisplayBoard(playerOneGuessMap);
                            Console.WriteLine();
                            DisplayBoard(playerOneBoard);
                            Console.WriteLine(playerOneName + ": Enter guess coordinates: ");
                            guessIndexCoords = AskInputCoordToIndex();
                            if (playerTwoBoard[guessIndexCoords.Item1, guessIndexCoords.Item2] == '·')
                            {
                                playerTwoBoard[guessIndexCoords.Item1, guessIndexCoords.Item2] = 'O';
                                playerOneGuessMap[guessIndexCoords.Item1, guessIndexCoords.Item2] = 'O';
                                Console.Clear();
                                DisplayBoard(playerOneGuessMap);
                                DisplayBoard(playerOneBoard);
                                Console.WriteLine("\nMISS!");
                                break;
                            }
                            else if (playerTwoBoard[guessIndexCoords.Item1, guessIndexCoords.Item2] == 'I' || playerTwoBoard[guessIndexCoords.Item1, guessIndexCoords.Item2] == '=')
                            {
                                playerTwoBoard[guessIndexCoords.Item1, guessIndexCoords.Item2] = 'X';
                                playerOneGuessMap[guessIndexCoords.Item1, guessIndexCoords.Item2] = 'X';
                                Console.Clear();
                                DisplayBoard(playerOneGuessMap);
                                DisplayBoard(playerOneBoard);
                                Console.WriteLine("\nHIT!");
                                if (WinCheck(playerOneBoard))
                                {
                                    Console.WriteLine("\n" + playerOneName + " wins! Press key to close.");
                                    Console.ReadKey();
                                    gameEnded = true;
                                }
                                break;
                            }
                            else
                            {
                                throw new Exception("You've already guessed there, try again.\n");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }

                else    // player 2's go
                {
                    var guessIndexCoords = new Tuple<int, int>(11, 11); // values should be replaced but if not, 11 will ensure error
                    while (true)
                    {
                        try
                        {
                            DisplayBoard(playerTwoGuessMap);
                            Console.WriteLine();
                            DisplayBoard(playerTwoBoard);
                            Console.WriteLine(playerTwoName + ": Enter guess coordinates: ");
                            guessIndexCoords = AskInputCoordToIndex();
                            if (playerOneBoard[guessIndexCoords.Item1, guessIndexCoords.Item2] == '·')
                            {
                                playerOneBoard[guessIndexCoords.Item1, guessIndexCoords.Item2] = 'O';
                                playerTwoGuessMap[guessIndexCoords.Item1, guessIndexCoords.Item2] = 'O';
                                Console.Clear();
                                DisplayBoard(playerTwoGuessMap);
                                DisplayBoard(playerTwoBoard);
                                Console.WriteLine("\nMISS!");
                                break;
                            }
                            else if (playerOneBoard[guessIndexCoords.Item1, guessIndexCoords.Item2] == 'I' || playerOneBoard[guessIndexCoords.Item1, guessIndexCoords.Item2] == '=')
                            {
                                playerOneBoard[guessIndexCoords.Item1, guessIndexCoords.Item2] = 'X';
                                playerTwoGuessMap[guessIndexCoords.Item1, guessIndexCoords.Item2] = 'X';
                                Console.Clear();
                                DisplayBoard(playerTwoGuessMap);
                                DisplayBoard(playerTwoBoard);
                                Console.WriteLine("\nHIT!");
                                if (WinCheck(playerOneBoard))
                                {
                                    Console.WriteLine("\n" + playerTwoName.ToUpper() + " WINS! Press key to close.");
                                    Console.ReadKey();
                                    gameEnded = true;
                                }
                                break;
                            }
                            else
                            {
                                throw new Exception("You've already guessed there, try again.\n");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                round++;
            }

        }

        static Tuple<int, int> AskInputCoordToIndex()
        {
            while (true)
            {
                try
                {
                    string coord = Console.ReadLine();
                    int x = Int32.Parse(coord.Remove(0, 1)) - 1;
                    int y = char.ToUpper(coord[0]) - 64 - 1; // ABC to 012
                    if (x > 9 || y > 9 || x < 0 || y < 0)
                    {
                        throw new Exception("Coords invalid");
                    }

                    var indexCoords = new Tuple<int, int>(x, y);
                    return indexCoords;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input, try again!\n");
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
            board = PlaceFleet(board, 5, "Carrier", playerName);
            Console.Clear();

            DisplayBoard(board);
            board = PlaceFleet(board, 4, "Battleship", playerName);
            Console.Clear();

            DisplayBoard(board);
            board = PlaceFleet(board, 3, "Destroyer", playerName);
            Console.Clear();

            DisplayBoard(board);
            board = PlaceFleet(board, 3, "Submarine", playerName);
            Console.Clear();

            DisplayBoard(board);
            board = PlaceFleet(board, 2, "Patrol Boat", playerName);
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

        static char[,] PlaceFleet(char[,] board, int shipLength, string ship, string playerName)
        {
            char[,] boardCopy = board.Clone() as char[,];
            while (true)
            {
                try
                {
                    Console.WriteLine("\n" + playerName + ": Enter first location of " + ship + " (" + shipLength + ")");
                    var indexCoords = AskInputCoordToIndex();
                    ConsoleKeyInfo dir;
                    int x = indexCoords.Item1;
                    int y = indexCoords.Item2;
                    int xFact = 0;
                    int yFact = 0;
                    while (true)
                    {
                        try
                        {
                            Console.Write("Which direction should the battleship point? (arrow key) ");
                            dir = Console.ReadKey();
                            Console.Write("\n");
                            if (dir.Key == ConsoleKey.UpArrow)
                            {
                                yFact = -1;
                                break;
                            }
                            else if (dir.Key == ConsoleKey.DownArrow)
                            {
                                yFact = 1;
                                break;
                            }
                            else if (dir.Key == ConsoleKey.RightArrow)
                            {
                                xFact = 1;
                                break;
                            }
                            else if (dir.Key == ConsoleKey.LeftArrow)
                            {
                                xFact = -1;
                                break;
                            }
                            else
                            {
                                throw new Exception(); // Input is not arrow key
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Direction input must be an arrow key");
                        }
                    }

                    if (xFact != 0) // horizontal placement
                    {
                        for (int i = 0; Math.Abs(i) < shipLength; i += xFact)
                        {
                            if (board[i + x, y] == '·')
                            {
                                board[i + x, y] = '=';
                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                    }

                    if (yFact != 0) // vertical placement
                    {
                        for (int n = 0; Math.Abs(n) < shipLength; n += yFact)
                        {
                            if (board[x, n + y] == '·')
                            {
                                board[x, n + y] = 'I';
                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                    }


                    return board;
                }
                catch (Exception)
                {
                    board = boardCopy.Clone() as char[,];
                    Console.WriteLine("\nShip could not be placed on that location, try again");
                }
            }
        }

        static bool WinCheck(char[,] board)
        {
            bool haveWon = true;
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (board[x, y] == '=' || board[x, y] == 'I')
                    {
                        haveWon = false;
                    }
                }
            }
            return haveWon;
        }
    }
}