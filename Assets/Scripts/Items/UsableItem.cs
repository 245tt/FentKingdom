using UnityEngine;

public class UsableItem : ItemBase
{
    public bool consumable = false;
    public AudioClip useSound;
    public virtual void Use(Player player) { }
}
