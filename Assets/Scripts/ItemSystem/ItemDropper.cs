using UnityEngine;
using UnityEngine.Events;

public class ItemDropper : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private int dropAmount;
    [Header("Events")]
    [SerializeField] private UnityEvent OnItemDrop;

    public void Drop()
    {
        PlayerInventory.Instance.AddItem(item, dropAmount);
        OnItemDrop?.Invoke();
    }
}
