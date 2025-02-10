using UnityEngine;
using UnityEngine.Events;

public class ItemConsumer : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] private Item item;
    [SerializeField] private int amount;
    [Header("Events")]
    [SerializeField] private UnityEvent OnItemConsumed;
    [SerializeField] private UnityEvent OnItemFailConsumed;

    public void ConsumeItem()
    {
        if (PlayerInventory.Instance.HasItem(item, amount))
        {
            PlayerInventory.Instance.RemoveItem(item, amount);
            OnItemConsumed?.Invoke();
        }
        else
        {
            OnItemFailConsumed?.Invoke();
        }
    }
}
