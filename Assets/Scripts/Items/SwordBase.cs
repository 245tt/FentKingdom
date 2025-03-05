using UnityEngine;

[CreateAssetMenu(fileName = "New Sword", menuName = "Item/Weapons/Sword")]
public class Swordbase : UsableItem
{
    public float baseDamage;
    public float baseCritChance;
    public float baseSpeed;
    public GameObject swordObject;
    public override void Use(Player player)
    {
        ItemStack currentWeapon = player.playerAttack.currentWeapon;
        PlayerWeapon playerWeapon = player.playerAttack.playerWeapon;

        Swordbase swordItem = (Swordbase)currentWeapon.item;

        GameObject swordGO = Instantiate(swordItem.swordObject, player.playerAttack.itemHolderTransform);
        playerWeapon = swordGO.GetComponent<PlayerWeapon>();
        playerWeapon.playerAttack = player.playerAttack;

        player.playerAttack.attackBlocked = true;
        swordGO.GetComponent<Animator>().SetTrigger("Attack");
        swordGO.GetComponent<Animator>().speed = swordItem.baseSpeed;
        player.playerAttack.StartDelayAttack(swordItem.baseSpeed, swordGO);

    }
}
