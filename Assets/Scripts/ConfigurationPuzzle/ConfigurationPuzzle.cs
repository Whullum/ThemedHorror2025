using UnityEngine;
using UnityEngine.Events;

public class ConfigurationPuzzle : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private ConfigurationData[] puzzleData;
    [Header("Events")]
    [SerializeField] private UnityEvent OnPuzzleCompleted;
    [SerializeField] private UnityEvent OnPuzzleNotCompleted;

    private void Awake()
    {
        InitializePuzzle();
    }

    private void OnDestroy()
    {
        CleanPuzzle();
    }

    private void InitializePuzzle()
    {
        for (int i = 0; i < puzzleData.Length; i++)
        {
            puzzleData[i].Item.OnItemActivated += OnItemActivated;
            puzzleData[i].Item.OnItemDeactivated += OnItemDeactivated;
        }
    }

    private void CleanPuzzle()
    {
        for (int i = 0; i < puzzleData.Length; i++)
        {
            puzzleData[i].Item.OnItemActivated -= OnItemActivated;
            puzzleData[i].Item.OnItemDeactivated -= OnItemDeactivated;
        }
    }

    public void CheckPuzzle()
    {
        bool completed = true;

        for (int i = 0; i < puzzleData.Length; i++)
        {
            if (puzzleData[i].Item.Activated != puzzleData[i].Active)
            {
                completed = false;
            }
        }

        if (completed)
        {
            OnPuzzleCompleted?.Invoke();
        }
        else
        {
            OnPuzzleNotCompleted?.Invoke();
        }
    }

    private void OnItemActivated(ConfigurationItem item)
    {

    }

    private void OnItemDeactivated(ConfigurationItem item)
    {
    }
}
