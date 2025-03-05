using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public List<ItemStack> items;
    public int maxSlots;

    public bool needsUpdate = false;
    public virtual void Start()
    {
        items = new List<ItemStack>();
        for (int i = 0; i < maxSlots; i++)
        {
            items.Add(new ItemStack(null,0));
        }
    }

    public virtual bool AddItem(ItemBase item, int count = 1)
    {
        foreach (ItemStack itemStack in items)
        {
            if (itemStack.isEmpty || (itemStack.item == item && itemStack.amount < item.maxStack))
            {
                itemStack.AddItem(item, count);
                return true;
            }
        }
        return false;  // No space found
    }
    public virtual bool AddItem(ItemStack item)
    {
        int itemAmountLeft = item.amount;
        int maxStack = item.item.maxStack;
        for (int i = 0; i < items.Count; i++) 
        {

            if (items[i] == null) 
            {
                items[i].item = item.item;
                int itemsMoved = math.min(itemAmountLeft,maxStack);
                items[i].amount = itemsMoved;
                itemAmountLeft -= itemsMoved;
                Debug.Log($"Added Item at {i}");
            }
            else if (items[i].item == null) 
            {
                items[i].item = item.item;
                int itemsMoved = math.min(itemAmountLeft, maxStack);
                items[i].amount = itemsMoved;
                itemAmountLeft -= itemsMoved;
                Debug.Log($"Added Item at {i}");
            }
            if (itemAmountLeft == 0) { needsUpdate = true; return true; }
        }
            
        return false;  // No space found
    }

    // Remove item from inventory
    public virtual bool RemoveItem(ItemBase item, int count = 1)
    {
        foreach (ItemStack itemStack in items)
        {
            if (itemStack.item == item && itemStack.amount >= count)
            {
                itemStack.amount -= count;
                if (itemStack.amount <= 0)
                {
                    itemStack.RemoveItem();
                }
                return true;
            }
        }
        return false;
    }
    public void SwapItemsInInventory(int indexA, int indexB)
    {
        if (indexA < 0 || indexA >= items.Count || indexB < 0 || indexB >= items.Count) return;

        // Swap references in inventory
        ItemStack temp = items[indexA];
        items[indexA] = items[indexB];
        items[indexB] = temp;
        
        needsUpdate = true;
    }

    public void SwapItemsBetweenInventories(int indexA, int indexB,Inventory inventory2)
    {
        if (indexA < 0 || indexA >= items.Count || indexB < 0 || indexB >= items.Count) return;

        // Swap references in inventory
        ItemStack temp = items[indexA];
        items[indexA] = inventory2.items[indexB];
        inventory2.items[indexB] = temp;

        needsUpdate = true;
        inventory2.needsUpdate = true;

    }

    public void SwapItemsBetweenInventories(ItemSlot slot1,ItemSlot slot2) 
    {

        int indexA = slot1.inventory.items.IndexOf(slot1.itemStack);
        int indexB = slot2.inventory.items.IndexOf(slot2.itemStack);

        slot1.inventory.SwapItemsBetweenInventories(indexA, indexB, slot2.inventory);
    }


}