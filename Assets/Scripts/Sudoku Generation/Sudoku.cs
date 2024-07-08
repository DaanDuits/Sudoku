using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sudoku
{
    public const int BoardSize = 9;
    public const int SubgridSize = 3;

    private int[,] _values;
    private System.Random random;

    public Sudoku()
    {
        Initialize(Random.Range(int.MinValue, int.MaxValue));
    }
    public Sudoku(int seed)
    {
        Initialize(seed);
    }

    private void Initialize(int seed)
    {
        _values = new int[BoardSize, BoardSize];
        random = new System.Random(seed);
        CreateSudoku();
    }

    private void CreateSudoku()
    {
        FillDiagonalSubgrids();
        FillRemaining();
    }

    private void FillDiagonalSubgrids()
    {
        for (int i = 0; i < BoardSize; i += SubgridSize) 
        {
            FillSubGrid(i, i);
        }
    }

    private void FillSubGrid(int row, int col)
    {
        int[] numbers = Enumerable.Range(1, BoardSize).OrderBy(i => random.Next()).ToArray();
        int index = 0;
        for (int x = 0; x < SubgridSize; x++) 
        {
            for (int y = 0;  y < SubgridSize; y++) 
            {
                _values[x + row, y + col] = numbers[index++];
            }
        }
    }

    private bool FillRemaining()
    {
        for (int row = 0; row < BoardSize; row++)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                if (_values[row, col] != 0)
                    continue;

                for (int num = 1; num <= BoardSize; num++)
                {
                    if (!IsSafeToPlace(row, col, num))
                        continue;

                    _values[row, col] = num;
                    if (FillRemaining())
                        return true;

                    _values[row, col] = 0;
                }
                return false;
            }
        }
        return true;
    }

    public bool IsSafeToPlace(int row, int col, int value)
    {
        return !ExistsInRow(row, value) && !ExistsInColumn(col, value) && !ExistsInSubGrid(row, col, value);
    }

    public bool ExistsInRow(int row, int value)
    {
        for (int col = 0; col < BoardSize; col++)
        {
            if (_values[row, col] == value)
                return true;
        }

        return false;
    }

    public bool ExistsInColumn(int col, int value)
    {
        for (int row = 0; row < BoardSize; row++)
        {
            if (_values[row, col] == value)
                return true;
        }

        return false;
    }

    public bool ExistsInSubGrid(int row, int col, int value)
    {
        int startRow = row - row % SubgridSize;
        int startCol = col - col % SubgridSize;

        for (int x = 0; x < SubgridSize; x++) 
        {
            for (int y = 0; y < SubgridSize; y++) 
            {
                if (_values[x + startRow, y + startCol] == value)
                    return true;
            }
        }

        return false;
    }
    
    public int this[int row, int col]
    {
        get { return _values[row, col]; }
        set { _values[row, col] = value;}
    }
}
