using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NotingBehaviour : MonoBehaviour
{
    private delegate void NotingToggledEvent(int number);

    [SerializeField] private SquareSelection selection;

    [SerializeField] private Transform numberHolder;

    private bool _noting = false;

    private void Start()
    {
        int i = 1;
        foreach (Transform t in numberHolder)
        {
            AddToggleEvent(selection.SetSelectedValue, i, t.GetComponent<Button>());
            i++;
        }
    }

    private static void NoteNumber(int number)
    {
        GameObject Note = SquareSelection.CurrentSquare.GetNoteByValue(number);
        Note.SetActive(!Note.activeSelf);
    }

    public void OnNotingToggled()
    {
        _noting = !_noting;

        int i = 1;
        if (!_noting) 
        {
            foreach (Transform t in numberHolder)
            {
                AddToggleEvent(selection.SetSelectedValue, i, t.GetComponent<Button>());
                i++;
            }

            return;
        }

        i = 1;
        foreach (Transform t in numberHolder)
        {
            AddToggleEvent(NoteNumber,i, t.GetComponent<Button>());
            i++;
        }
    }

    private void AddToggleEvent(NotingToggledEvent toggledEvent, int number, Button button)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { toggledEvent(number); });
    }
}