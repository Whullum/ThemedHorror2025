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
    [SerializeField] private CombinationItem[] items;
    [SerializeField] private Combination[] combination;
    [SerializeField] private UnityEvent onPuzzleCompleted;

    private Combination[] selectedCombination;

    private void Awake()
    {
        selectedCombination = new Combination[combination.Length];
    }

    public void ToggleUI()
    {
        ui.SetActive(!ui.activeSelf);
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
            onPuzzleCompleted?.Invoke();
        }
    }
}
