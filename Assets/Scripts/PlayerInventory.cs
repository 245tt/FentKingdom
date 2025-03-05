using System.Collections.Generic;


public class PlayerInventory : Inventory
{
    public List<ItemStack> armorItems;
    public override void Start()
    {
        base.Start();
        armorItems = new List<ItemStack>();
        for (int i = 0; i < 6; i++)
        {
            ItemStack empty = new ItemStack(null, 0);
            items.Add(empty);
            armorItems.Add(empty);
        }
    }

    public List<ArmorBase> GetArmors()
    {
        List<ArmorBase> armors = new List<ArmorBase>();

        for (int i = maxSlots + 1; i < maxSlots + 6; i++)
        {
            armors.Add(((ArmorBase)items[i].item));
        }

        return armors;
    }
    public ItemStack GetHelmetSlot()
    {
        return items[maxSlots + 1];
    }
    public ItemStack GetArmorSlot()
    {
        return items[maxSlots + 2];
    }
    public ItemStack GetBootsSlot()
    {
        return items[maxSlots + 3];
    }
    public ItemStack GetRingSlot()
    {
        return items[maxSlots + 4];
    }
    public ItemStack GetNecklaceSlot()
    {
        return items[maxSlots + 5];
    }
    public ItemStack GetShieldSlot()
    {
        return items[maxSlots + 6];
    }
}
