using System;

class Sudoku
{
    static int[,] board = new int[9, 9]
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

    static void Main()
    {
        Console.WriteLine("=== Initial Sudoku Board ===");
        PrintBoard();

        if (Solve())
        {
            Console.WriteLine("\n=== Solved Sudoku Board ===");
            PrintBoard();
        }
        else
        {
            Console.WriteLine("No solution exists.");
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

        return true; // solved
    }

    static bool IsValid(int row, int col, int num)
    {
        // Row check
        for (int x = 0; x < 9; x++)
            if (board[row, x] == num) return false;

        // Column check
        for (int x = 0; x < 9; x++)
            if (board[x, col] == num) return false;

        // 3x3 box check
        int startRow = row / 3 * 3;
        int startCol = col / 3 * 3;
        for (int r = startRow; r < startRow + 3; r++)
            for (int c = startCol; c < startCol + 3; c++)
                if (board[r, c] == num) return false;

        return true;
    }

    static void PrintBoard()
    {
        for (int r = 0; r < 9; r++)
        {
            if (r % 3 == 0 && r != 0) Console.WriteLine("---------------------");

            for (int c = 0; c < 9; c++)
            {
                if (c % 3 == 0 && c != 0) Console.Write("| ");
                Console.Write(board[r, c] == 0 ? ". " : board[r, c] + " ");
            }

            Console.WriteLine();
        }
    }
}
