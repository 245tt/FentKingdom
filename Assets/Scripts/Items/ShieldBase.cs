using UnityEngine;

[CreateAssetMenu(fileName = "New Shield", menuName = "Item/Armor/Shield")]
public class Shield : ArmorBase
{
    public float blockChance;
    public virtual void Block(Player player) 
    {

    }
}
