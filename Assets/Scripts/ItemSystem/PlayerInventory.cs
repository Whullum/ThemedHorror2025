using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get { return instance; } }

    private Dictionary<Item, int> inventory = new Dictionary<Item, int>();
    private static PlayerInventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    public void AddItem(Item item, int amount)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] += amount;
        }
        else
        {
            inventory.Add(item, amount);
        }
    }

    public void RemoveItem(Item item, int amount)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] -= amount;

            if (inventory[item] <= 0)
            {
                inventory.Remove(item);
            }
        }
    }

    public bool HasItem(Item item, int amount)
    {
        if (inventory.ContainsKey(item))
        {
            if (inventory[item] >= amount)
            {
                return true;
            }
        }

        return false;
    }
}
