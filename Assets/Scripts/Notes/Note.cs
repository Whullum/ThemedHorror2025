using TMPro;
using UnityEngine;

public class Note : MonoBehaviour
{
    [TextArea(10, 40)]
    [SerializeField] private string text;
    [SerializeField] private TextMeshProUGUI uiText;
    [SerializeField] private GameObject noteUI;

    private void Awake()
    {
        uiText.text = text;
    }

    public void ToggleNote()
    {
        noteUI.SetActive(!noteUI.activeSelf);

        if (noteUI.activeSelf)
        {
            GameManager.Instance.PauseGame();
        }
        else
        {
            GameManager.Instance.ResumeGame();
        }
    }
}
