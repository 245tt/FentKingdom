using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Item/Potion")]
public class PotionItem : UsableItem
{
    public PotionEffect effect;
    public override void Use(Player player) 
    {
        player.AddPotionEffect(effect);
    }
}
