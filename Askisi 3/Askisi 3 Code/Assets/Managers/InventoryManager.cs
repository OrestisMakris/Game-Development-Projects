using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public string equippedItem { get; private set; }
    private Dictionary<string, int> items;

    public void Startup()
    {
        Debug.Log("Inventory manager starting...");
        items = new Dictionary<string, int>();
        status = ManagerStatus.Started;
    }

    private void DisplayItems()
    {
        string itemDisplay = "items: ";
        foreach (KeyValuePair<string, int> item in items)
        {
            itemDisplay += item.Key + "(" + item.Value + ")";
        }
        Debug.Log(itemDisplay);
    }

    public void AddItem(string name)
    {
        if (items.ContainsKey(name))
        {
            items[name] += 1;
        }
        else
        {
            items[name] = 1;
        }
        DisplayItems();
    }

    public List<string> GetItemList()
    {
        return new List<string>(items.Keys);
    }

    public int GetItemCount(string name)
    {
        return items.ContainsKey(name) ? items[name] : 0;
    }

    public bool EquipItem(string name)
    {
        if (items.ContainsKey(name) && equippedItem != name)
        {
            equippedItem = name;
            Debug.Log($"Equipped {name}");
            return true;
        }
        equippedItem = null;
        Debug.Log("Unequipped");
        return false;
    }

    public bool ConsumeItem(string name)
    {
        if (items.ContainsKey(name))
        {
            items[name]--;
            if (items[name] == 0)
            {
                items.Remove(name);
            }
            return true;
        }
        Debug.Log($"Cannot consume {name}");
        return false;
    }

    public void RemoveKeys()
    {
        List<string> keysToRemove = new List<string>();

        foreach (var pair in items)
        {
            if (pair.Key.ToLower().Contains("key"))
            {
                keysToRemove.Add(pair.Key);
            }
        }

        foreach (string key in keysToRemove)
        {
            items.Remove(key);
        }

        // Reset equipped item if it was a key
        if (equippedItem != null && equippedItem.ToLower().Contains("key"))
        {
            equippedItem = null;
        }
    }
    public void RemoveEnergyAndOres()
    {
        List<string> itemsToRemove = new List<string>();

        foreach (var pair in items)
        {
            if (pair.Key.ToLower().Contains("energy") || pair.Key.ToLower().Contains("ore"))
            {
                itemsToRemove.Add(pair.Key);
            }
        }

        foreach (string item in itemsToRemove)
        {
            items.Remove(item);
        }

        // Reset equipped item if it was energy or ore
        if (equippedItem != null && 
            (equippedItem.ToLower().Contains("energy") || equippedItem.ToLower().Contains("ore")))
        {
            equippedItem = null;
        }
    }

    public void ClearInventory()
    {
        items.Clear();
        equippedItem = null;
    }
}