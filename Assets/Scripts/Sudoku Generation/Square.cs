using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Square : MonoBehaviour
{
    [SerializeField] private TMP_Text valueDisplay;

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
            valueDisplay.text = value > 0 ? value.ToString() : " ";
            _value = value;
        }
    }

    public void Set(int value, bool locked) 
    { 
        Value = value;
        Locked = locked;
    }
}
