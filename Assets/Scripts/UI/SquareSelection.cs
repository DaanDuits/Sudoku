using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SquareSelection : MonoBehaviour
{
    private delegate void Operation(Image image, Color color);

    public static Color SelectedColor = new Color(0, 0, 0, 0.6f), HighlightedColor = new Color(0, 0, 0, 0.3f), Transparent = new Color(1, 1, 1, 0f);

    private static Square _currentSquare;
    public static Square CurrentSquare
    {
        set
        {
            if (_currentSquare == null) 
                _currentSquare = value;

            SetColors(Transparent, false, Multiply);

            _currentSquare = value;

            _currentImage.color += SelectedColor;

            if (_currentSquare.Value == 0)
                return;

            SetColors(HighlightedColor, true, Add);
        }

        get { return _currentSquare; }
    }

    private static void Multiply(Image image, Color color)
    {
        image.color *= color; 
    }

    private static void Add(Image image, Color color)
    {
        image.color += color;
    }

    private static void SetColors(Color color, bool skipSelected, Operation operation)
    {
        Vector2Int[] coords = SudokuGenerator.Sudoku.GetValueCoordinates(_currentSquare.Value);
        for (int i = 0; i < coords.Length; i++)
        {
            Square square = SudokuGenerator.GetSquareAt(coords[i].x, coords[i].y);
            Image image = square.GetComponent<Image>();

            if (image == _currentImage && skipSelected)
                continue;

            operation(image, color);
        }
    }

    private static Image _currentImage
    {
        get { return _currentSquare.GetComponent<Image>(); }
    }

    public void SetSelectedValue(int value)
    {
        if (_currentSquare == null) return;
        if (_currentSquare.Locked) return;
        if (_currentSquare.Value == value) return;

        SetColors(Transparent, false, Multiply);
        SudokuGenerator.SetValueAtSquare(_currentSquare, value);
        CurrentSquare = _currentSquare;
    }
}