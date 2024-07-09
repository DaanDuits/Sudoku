using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SquareSelection : MonoBehaviour, IPointerClickHandler
{
    private static Square _square;
    private static Image _current;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_current == null)
        {
            _current = GetComponent<Image>();
            _square = _current.GetComponent<Square>();
        }

        Vector2Int[] coords = SudokuGenerator.Sudoku.GetValueCoordinates(_square.Value);
        for (int i = 0; i < coords.Length; i++)
        {
            Square square = SudokuGenerator.GetSquareAt(coords[i].x, coords[i].y);
            Image image = square.GetComponent<Image>();

            image.color *= new Color(1, 1, 1, 0f);
        }

        _current = GetComponent<Image>();
        _square = _current.GetComponent<Square>();

        _current.color += new Color(0, 0, 0, 0.6f);

        if (_square.Value == 0)
            return;

        coords = SudokuGenerator.Sudoku.GetValueCoordinates(_square.Value);
        for (int i = 0; i < coords.Length; i ++)
        {
            Square square = SudokuGenerator.GetSquareAt(coords[i].x, coords[i].y);
            Image image = square.GetComponent<Image>();

            if (image == _current)
                continue;

            image.color += new Color(0, 0, 0, 0.3f);
        }
    }

    public void SetSelectedValue(int value)
    {
        SudokuGenerator.SetValueAtSquare(_square, value);
        _square.Value = value;
    }
}
