using System;
using System.Collections.Generic;

[System.Serializable]
public class ItemStack
{
    public ItemBase item;
    public int amount = 1;
    public bool isEmpty => item == null;
    public Dictionary<string, int> metadata = new Dictionary<string, int>();

    public ItemStack(ItemBase item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
    public void AddItem(ItemBase _item, int count = 1)
    {
        item = _item;
        amount = count;
    }

    // Remove an item from the slot
    public void RemoveItem()
    {
        item = null;
        amount = 0;
    }

}
