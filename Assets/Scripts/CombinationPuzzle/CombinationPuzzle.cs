using UnityEngine;
using UnityEngine.Events;

public class CombinationPuzzle : MonoBehaviour
{
    public enum Combination
    {
        Circle,
        Square,
        Hexagon,
        Hourglass
    }

    [SerializeField] private GameObject ui;
    [SerializeField] private ChangeImage[] items;
    [SerializeField] private Combination[] combination;
    [SerializeField] private UnityEvent onPuzzleCompleted;

    private Combination[] selectedCombination;
    private bool completed;
    private float elapsed;

    private void Awake()
    {
        selectedCombination = new Combination[combination.Length];

        for (int i = 0; i < items.Length; i++)
        {
            items[i].puzzleManager = this;
        }
    }

    public void ToggleUI()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            GameManager.Instance.PauseGame();
        }
        else
        {
            GameManager.Instance.ResumeGame();
        }
    }

    public void CheckCombination()
    {
        bool correct = true;

        for (int i = 0; i < combination.Length; i++)
        {
            if (items[i].GetCurrentKey() != combination[i])
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            GameManager.Instance.ResumeGame();
            completed = true;

            for (int i = 0; i < items.Length; i++)
            {
                items[i].enabled = false;
            }
        }
    }

    private void Update()
    {
        if (completed && elapsed < 1)
        {
            elapsed += Time.deltaTime;

            if (elapsed > 1)
            {
                onPuzzleCompleted?.Invoke();
            }
        }
    }
}
