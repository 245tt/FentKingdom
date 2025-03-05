using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemSlot : MonoBehaviour
{
    [Header("Item slot components")]
    public Image image;
    public TMP_Text label;
    public Button button;
    public ItemStack itemStack = new ItemStack(null, 0);
    public ItemType itemType = ItemType.None;
    public bool draggable = true;

    [Header("Inventory reference")]
    public Inventory inventory;
    void Awake()
    {
        button = GetComponent<Button>();
        image = transform.GetChild(0).GetComponent<Image>();
        label = transform.GetChild(1).GetComponent<TMP_Text>();
        image.preserveAspect = true;
    }
    public void UpdateUI()
    {
        if (itemStack != null)
        {
            if (itemStack.item != null)
            {
                image.enabled = true;
                image.sprite = itemStack.item.itemIcon;
                if (itemStack.amount > 1) label.text =  itemStack.amount.ToString();
                else label.text = "";

            }
            else
            {
                image.sprite = null;
                image.enabled = false;
                label.text = "";
            }
        }
    }
    public void SetItem(ItemStack item)
    {
        itemStack = item;
        UpdateUI();
    }
    public bool CanAllowItem(ItemStack newItem)
    {
        if (itemType == ItemType.None) return true;
        if (newItem.item.itemType == itemType) return true;

        return false;
    }
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
