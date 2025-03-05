using UnityEngine;

public enum ItemRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

public enum ItemType
{
    None,
    Helmet,
    Armor,
    Boots,
    Ring,
    Necklace,
    Shield,

}
public class ItemBase : ScriptableObject
{
    public string itemID;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public ItemRarity itemRarity;
    public ItemType itemType = ItemType.None;
    public int maxStack = 1;

    public int value = 0;
    public bool isStackable()
    {
        if (maxStack == 1) return false;
        return true;
    }
    public bool isSellable()
    {
        if (value == 0) return false;
        return true;
    }
}
