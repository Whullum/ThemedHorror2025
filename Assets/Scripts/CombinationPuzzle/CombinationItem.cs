using UnityEngine;

public class CombinationItem : MonoBehaviour
{
    [SerializeField] private CombinationPuzzle.Combination correctKey;
    [SerializeField] private CombinationPuzzle.Combination[] keyOrder;

    private int currentKey;

    public CombinationPuzzle.Combination GetCurrentKey()
    {
        return keyOrder[currentKey];
    }

    public void NextKey()
    {
        currentKey++;

        if (currentKey >= keyOrder.Length)
        {
            currentKey = 0;
        }
    }

    public void PreviousKey()
    {
        currentKey--;

        if (currentKey < 0)
        {
            currentKey = keyOrder.Length - 1;
        }
    }
}
