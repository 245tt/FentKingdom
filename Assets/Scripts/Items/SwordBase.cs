using UnityEngine;

[CreateAssetMenu(fileName = "New Sword", menuName = "Item/Weapons/Sword")]
public class Swordbase : UsableItem
{
    public float baseDamage;
    public float baseCritChance;
    public float baseSpeed;
    public GameObject swordObject;
    public float weaponScale = 1;
    public override void Use(Player player)
    {
        PlayerWeapon playerWeapon = player.playerAttack.playerWeapon;


        GameObject swordGO = Instantiate(swordObject, player.playerAttack.itemHolderTransform);
        swordGO.transform.localScale.Set(weaponScale, weaponScale, weaponScale);
        swordGO.transform.SetAsLastSibling();
        playerWeapon = swordGO.GetComponent<PlayerWeapon>();
        playerWeapon.playerAttack = player.playerAttack;
        playerWeapon.weaponSprite.sprite = itemIcon;

        swordGO.GetComponent<Animator>().SetTrigger("Attack");
        swordGO.GetComponent<Animator>().speed = baseSpeed;
        player.playerAttack.StartDelayAttack(baseSpeed, swordGO);

    }
}
