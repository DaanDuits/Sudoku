using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Square : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text valueDisplay;

    public static Color LockedColor = new Color(1,1,1), OpenColor = new Color(0.65f, 0.65f, 0.65f);

    private void Start()
    {
        foreach (Transform t in transform.GetChild(0))
        {
            t.gameObject.SetActive(false);
        }
    }

    private void ToggleNotes(bool toggle)
    {
        GameObject noteHolder = transform.GetChild(0).gameObject;
        noteHolder.SetActive(toggle);
    }

    public GameObject GetNoteByValue(int  value)
    {
        int i = 0;
        foreach (Transform t in transform.GetChild(0))
        {
            if (i == value - 1)
            {
                return t.gameObject;
            }
            i++;
        }

        return null;
    }

    public bool Locked
    {
        get;
        set;
    }

    private int _value;

    public int Value
    {
        get { return _value; }
        set 
        {
            ToggleNotes(value == 0 ? true : false);

            valueDisplay.text = value > 0 ? value.ToString() : " ";
            _value = value;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SquareSelection.CurrentSquare = this;
    }

    public void Set(int value, bool locked) 
    { 
        Value = value;
        Locked = locked;
        valueDisplay.color = locked ? LockedColor : OpenColor;
    }
}
