using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Item/Armor/Armor Piece")]
public class ArmorBase : ItemBase
{
    public int defense;
    public int speed;
    public int strength;

    public virtual void OnEquip(Player player) { }
}
