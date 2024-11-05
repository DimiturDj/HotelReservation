using System;

namespace Matrica
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a positive number ");
            string input = Console.ReadLine();
            int n = 0;
            while (!int.TryParse(input, out n) || n < 0 || n > 100)
            {
                Console.WriteLine("You haven't entered a correct positive number");
                input = Console.ReadLine();
            }

            int matrixSize = 3;
            int[,] matrix = new int[matrixSize, matrixSize];

            int Value = 1, row = 0, col = 0, rowStep = 1, colStep = 1;


            while (true)
            {
                matrix[row, col] = Value;

                if (!Check(matrix, row, col)) break;

                while (row + rowStep >= matrixSize || row + rowStep < 0 || col + colStep >= matrixSize || col + colStep < 0 || matrix[row + rowStep, col + colStep] != 0)
                {
                    Change(ref rowStep, ref colStep);
                }

                row += rowStep;

                col += colStep;

                Value++;
            }


            FindCell(matrix, out row, out col);

            if (row != 0 && col != 0)
            {
                rowStep = 1; colStep = 1;

                while (true)
                {
                    matrix[row, col] = Value;
                    if (!Check(matrix, row, col)) break;

                    while (row + rowStep >= matrixSize || row + rowStep < 0 || col + colStep >= matrixSize || col + colStep < 0 || matrix[row + rowStep, col + colStep] != 0)
                    {
                        Change(ref rowStep, ref colStep);
                    }

                    row += rowStep;

                    col += colStep;

                    Value++;
                }
            }

        }
        static void Change(ref int deltaRow, ref int deltaCol)
        {
            int[] rowDirections = { 1, 1, 1, 0, -1, -1, -1, 0 };

            int[] colDirections = { 1, 0, -1, -1, -1, 0, 1, 1 };

            int currentDirectionIndex = 0;

            for (int count = 0; count < 8; count++)
            {
                if (rowDirections[count] == deltaRow && colDirections[count] == deltaCol)
                {
                    currentDirectionIndex = count;
                    break;
                }
            }
            if (currentDirectionIndex == 7)
            {
                deltaRow = rowDirections[0];

                deltaCol = colDirections[0];
            }
            else
            {
                deltaRow = rowDirections[currentDirectionIndex + 1];

                deltaCol = colDirections[currentDirectionIndex + 1];
            }
        }

        static bool Check(int[,] matrix, int row, int col)
        {
            int[] rowDirections = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] colDirections = { 1, 0, -1, -1, -1, 0, 1, 1 };

            for (int i = 0; i < 8; i++)
            {
                if (row + rowDirections[i] >= matrix.GetLength(0) || row + rowDirections[i] < 0) rowDirections[i] = 0;

                if (col + colDirections[i] >= matrix.GetLength(1) || col + colDirections[i] < 0) colDirections[i] = 0;
            }

            for (int i = 0; i < 8; i++)
                if (matrix[row + rowDirections[i], col + colDirections[i]] == 0) return true;

            return false;
        }
        static void FindCell(int[,] matrix, out int row, out int col)
        {
            row = 0;
            col = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        row = i;

                        col = j;

                        return;
                    }
                }
            }
        }
    }
}