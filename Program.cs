using System;

class SudokuGame
{
    static int[,] board = new int[9, 9];

    static void Main()
    {
        Console.Title = "🧩 Sudoku Solver - Console Edition";
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== Welcome to the Sudoku Solver ===\n");
        Console.ResetColor();

        Console.Write("Do you want to (1) use default puzzle or (2) input your own? Enter 1 or 2: ");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            LoadDefaultPuzzle();
        }
        else
        {
            LoadUserPuzzle();
        }

        Console.Clear();
        Console.WriteLine("Initial Sudoku Board:\n");
        PrintBoard();

        Console.WriteLine("\nSolving the puzzle...");

        if (Solve())
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Puzzle Solved:\n");
            Console.ResetColor();
            PrintBoard();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ No solution exists.");
            Console.ResetColor();
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    static void LoadDefaultPuzzle()
    {
        board = new int[9, 9]
        {
            {5, 3, 0, 0, 7, 0, 0, 0, 0},
            {6, 0, 0, 1, 9, 5, 0, 0, 0},
            {0, 9, 8, 0, 0, 0, 0, 6, 0},
            {8, 0, 0, 0, 6, 0, 0, 0, 3},
            {4, 0, 0, 8, 0, 3, 0, 0, 1},
            {7, 0, 0, 0, 2, 0, 0, 0, 6},
            {0, 6, 0, 0, 0, 0, 2, 8, 0},
            {0, 0, 0, 4, 1, 9, 0, 0, 5},
            {0, 0, 0, 0, 8, 0, 0, 7, 9}
        };
    }

    static void LoadUserPuzzle()
    {
        Console.WriteLine("Enter your puzzle row-by-row, use 0 for empty cells.");
        for (int row = 0; row < 9; row++)
        {
            while (true)
            {
                Console.Write($"Row {row + 1}: ");
                string input = Console.ReadLine();
                string[] values = input.Split(' ');

                if (values.Length != 9)
                {
                    Console.WriteLine("⚠️  Enter exactly 9 numbers separated by spaces.");
                    continue;
                }

                try
                {
                    for (int col = 0; col < 9; col++)
                    {
                        board[row, col] = int.Parse(values[col]);
                        if (board[row, col] < 0 || board[row, col] > 9)
                            throw new Exception();
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("⚠️  Invalid input. Only numbers 0–9 are allowed.");
                }
            }
        }
    }

    static void PrintBoard()
    {
        for (int r = 0; r < 9; r++)
        {
            if (r % 3 == 0 && r != 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("------+-------+------");
                Console.ResetColor();
            }

            for (int c = 0; c < 9; c++)
            {
                if (c % 3 == 0 && c != 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("| ");
                    Console.ResetColor();
                }

                if (board[r, c] == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(". ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(board[r, c] + " ");
                }
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }

    static bool Solve()
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (board[row, col] == 0)
                {
                    for (int num = 1; num <= 9; num++)
                    {
                        if (IsValid(row, col, num))
                        {
                            board[row, col] = num;

                            if (Solve())
                                return true;

                            board[row, col] = 0;
                        }
                    }

                    return false;
                }
            }
        }

        return true;
    }

    static bool IsValid(int row, int col, int num)
    {
        for (int x = 0; x < 9; x++)
        {
            if (board[row, x] == num || board[x, col] == num)
                return false;
        }

        int startRow = row / 3 * 3;
        int startCol = col / 3 * 3;

        for (int r = startRow; r < startRow + 3; r++)
            for (int c = startCol; c < startCol + 3; c++)
                if (board[r, c] == num)
                    return false;

        return true;
    }
}
