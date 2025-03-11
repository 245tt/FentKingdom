using System.Collections.Generic;
using UnityEngine;

public class UsableItem : ItemBase
{
    public bool consumable = false;
    public List<AudioClip> useSounds;
    public virtual void Use(Player player) { }
}
