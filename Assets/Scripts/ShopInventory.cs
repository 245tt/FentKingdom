public class ShopInventory : Inventory
{
    public int buySlots;
    public int sellSlots;

    public int buyWorth;
    public int sellWorth;

    public override void Start()
    {
        while (items.Count != maxSlots) items.Add(new ItemStack(null, 0));

        for (int i = 0; i < sellSlots; i++)
        {
            items.Add(new ItemStack(null, 0));
        }
        for (int i = 0; i < buySlots; i++)
        {
            items.Add(new ItemStack(null, 0));
        }
    }

    public void AddToCart(ItemStack itemStack)
    {
        for (int i = maxSlots; i < maxSlots + buySlots; i++)
        {
            if (items[i].item == itemStack.item)
            {
                items[i].amount++;
                needsUpdate = true;
                break;
            }
            else if (items[i].item == null)
            {
                items[i].item = itemStack.item;
                items[i].amount = 1;
                needsUpdate = true;
                break;
            }
        }
        CalculateItemWorth();
    }
    public void RemoveFromCart(ItemSlot itemSlot)
    {
        int indexA = items.IndexOf(itemSlot.itemStack);
        items[indexA].amount--;
        if (items[indexA].amount <= 0) items[indexA].item = null;
        CalculateItemWorth();
    }

    public void BuyItems(Player player)
    {
        CalculateItemWorth();
        if (buyWorth > player.tebCoins) return;
        for (int i = maxSlots; i < maxSlots + buySlots; i++)
        {
            if (items[i].item != null)
            {
                if (!player.playerInventory.AddItem(items[i]))
                {
                    World.GetInstance().SpawnItemEntity(player.transform.position, items[i]);
                }
            }
        }
        player.tebCoins -= buyWorth;

        for (int i = maxSlots; i < maxSlots + buySlots; i++)
        {
            items[i].item = null;
            items[i].amount = 0;
        }
    }
    public void SellItems(Player player)
    {
        CalculateItemWorth();
        for (int i = maxSlots + buySlots; i < maxSlots + buySlots + sellSlots; i++)
        {
            if (items[i].item != null)
            {
                if (items[i].item.value > 0)
                {
                    player.tebCoins += (items[i].item.value * items[i].amount) / 2;
                    items[i].item = null;
                    items[i].amount = 0;
                }
            }
        }
    }
    public void DropSellItems(Player player)
    {
        for (int i = maxSlots + buySlots; i < maxSlots + buySlots + sellSlots; i++)
        {
            if (items[i].item != null)
            {
                if (!player.playerInventory.AddItem(items[i]))
                {
                    World.GetInstance().SpawnItemEntity(player.transform.position, items[i]);
                }
            }
        }
    }
    public void CalculateItemWorth()
    {
        buyWorth = 0;
        sellWorth = 0;
        for (int i = maxSlots; i < maxSlots + buySlots; i++)
        {
            if (items[i].item != null)
            {
                buyWorth += items[i].item.value * items[i].amount;
            }
        }
        for (int i = maxSlots + buySlots; i < maxSlots + buySlots + sellSlots; i++)
        {
            if (items[i].item != null)
            {
                sellWorth += (items[i].item.value * items[i].amount) / 2;
            }
        }
    }
}
