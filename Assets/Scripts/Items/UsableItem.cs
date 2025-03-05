public class UsableItem : ItemBase
{
    public bool consumable = false;
    public virtual void Use(Player player) { }
}
