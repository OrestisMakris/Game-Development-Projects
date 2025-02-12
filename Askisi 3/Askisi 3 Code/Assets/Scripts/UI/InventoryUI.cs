using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class ItemIcon
{
    public string itemName;
    public Sprite icon;
}

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private List<ItemIcon> itemIcons;
    private Dictionary<string, Sprite> iconDictionary;

    void Awake()
    {
        // Initialize the dictionary
        iconDictionary = new Dictionary<string, Sprite>();
        foreach (var item in itemIcons)
        {
            iconDictionary[item.itemName] = item.icon;
        }
    }

    public Sprite GetItemIcon(string itemName)
    {
        if (iconDictionary.TryGetValue(itemName, out Sprite icon))
        {
            return icon;
        }
        return null;
    }
}