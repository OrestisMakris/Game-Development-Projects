using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryPopup : MonoBehaviour
{
    [SerializeField] Image[] itemIcons;
    [SerializeField] TMP_Text[] itemLabels;
    [SerializeField] TMP_Text curItemLabel;
    [SerializeField] Button equipButton;
    [SerializeField] Button useButton;
    private string curItem;

    public void Refresh()
    {
        List<string> itemList = Managers.Inventory.GetItemList();
        int len = itemIcons.Length;
        for (int i = 0; i < len; i++)
        {
            if (i < itemList.Count)
            {
                itemIcons[i].gameObject.SetActive(true);
                itemLabels[i].gameObject.SetActive(true);
                string item = itemList[i];

                // Try to load the exact icon name first
                Sprite sprite = Resources.Load<Sprite>($"Icons/{item}");
                
                // If no specific icon found and item contains "key", use default key icon
                if (sprite == null && item.ToLower().Contains("key"))
                {
                    sprite = Resources.Load<Sprite>("Icons/key");
                }

                if (sprite != null)
                {
                    itemIcons[i].sprite = sprite;
                    itemIcons[i].SetNativeSize();
                }

                int count = Managers.Inventory.GetItemCount(item);
                string message = $"x{count}";
                if (item == Managers.Inventory.equippedItem)
                {
                    message = "Equipped\n" + message;
                }
                itemLabels[i].text = message;

                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                string itemCopy = item; // Create a copy for the lambda
                entry.callback.AddListener((BaseEventData data) =>
                {
                    OnItem(itemCopy);
                });
                EventTrigger trigger = itemIcons[i].GetComponent<EventTrigger>();
                if (trigger == null)
                {
                    trigger = itemIcons[i].gameObject.AddComponent<EventTrigger>();
                }
                trigger.triggers.Clear();
                trigger.triggers.Add(entry);
            }
            else
            {
                itemIcons[i].gameObject.SetActive(false);
                itemLabels[i].gameObject.SetActive(false);
            }
        }

        if (!itemList.Contains(curItem))
        {
            curItem = null;
        }

        if (curItem == null)
        {
            curItemLabel.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(false);
            useButton.gameObject.SetActive(false);
        }
        else
        {
            curItemLabel.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(true);
            if (curItem == "health")
            {
                useButton.gameObject.SetActive(true);
            }
            else
            {
                useButton.gameObject.SetActive(false);
            }
            curItemLabel.text = $"{curItem}:";
        }
    }

    public void OnItem(string item)
    {
        curItem = item;
        Refresh();
    }

    public void OnEquip()
    {
        Managers.Inventory.EquipItem(curItem);
        Refresh();
    }

    public void OnUse()
    {
        Managers.Inventory.ConsumeItem(curItem);
        if (curItem == "health")
        {
            Managers.Player.ChangeHealth(25);
        }
        Refresh();
    }
}