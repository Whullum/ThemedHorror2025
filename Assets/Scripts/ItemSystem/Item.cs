using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "PlayerInventory/Item")]
public class Item : ScriptableObject
{
    public string ItemName { get { return itemName; } }

    [SerializeField] private string itemName;
}
