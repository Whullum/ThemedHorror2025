using UnityEngine;

public class PuzzleNote : MonoBehaviour
{
    public GameObject ui;

    public void ToggleUI()
    {
        ui.SetActive(!ui.activeSelf);
    }
}
