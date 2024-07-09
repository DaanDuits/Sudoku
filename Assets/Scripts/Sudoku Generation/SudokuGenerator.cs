using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SudokuGenerator : MonoBehaviour
{

    [SerializeField] private Transform squareHolder;

    private static SudokuGenerator _singleton;
    public static Sudoku Sudoku;

    private void Start()
    {
        _singleton = this;
        GenerateSudoku(new Sudoku(40));
    }

    public void GenerateSudoku(int seed) => GenerateSudoku(new Sudoku(seed));
    public void GenerateSudoku(Sudoku sudoku)
    {
        Sudoku = sudoku;
        for (int x = 0; x < Sudoku.BoardSize; x++) 
        {
            for (int y = 0; y < Sudoku.BoardSize; y++)
            {
                Square square = GetSquareAt(x, y);
                square.Set(sudoku[x, y].value, sudoku[x, y].locked);
            }
        }
    }

    public static Square GetSquareAt(int x, int y)
    {
        Transform currentSquareObject = _singleton.squareHolder.GetChild(x * Sudoku.BoardSize + y);
        return currentSquareObject.GetComponent<Square>();
    }

    public static void SetValueAtSquare(Square square, int value)
    {
        int i = 0;
        foreach (Transform t in _singleton.squareHolder)
        {
            if (t.GetComponent<Square>() == square)
            {
                int x = i / Sudoku.BoardSize;
                int y = i % Sudoku.BoardSize;

                Sudoku[x, y] = (value, false);
            }
            i++;
        }
    }
}
