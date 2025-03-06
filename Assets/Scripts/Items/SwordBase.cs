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
        float angle = Mathf.Atan2(player.playerAttack.shootDir.y, player.playerAttack.shootDir.x) * Mathf.Rad2Deg;
        //swordGO.transform.parent.rotation = Quaternion.Euler(0, 0, angle);
        swordGO.transform.localScale = new Vector3(weaponScale, weaponScale,weaponScale);
        swordGO.transform.SetAsLastSibling();
        playerWeapon = swordGO.GetComponent<PlayerWeapon>();
        playerWeapon.playerAttack = player.playerAttack;
        playerWeapon.weaponSprite.sprite = itemIcon;

        Animator swordAnimator = swordGO.GetComponent<Animator>();
        swordAnimator.SetTrigger("Attack");

        if (angle > -90 && angle < 90)
        {
            swordAnimator.SetBool("Flipped",false);
        }
        else 
        {
            swordAnimator.SetBool("Flipped", true);
        }

        swordAnimator.speed = baseSpeed ;

        player.playerAttack.StartDelayAttack(baseSpeed, swordGO);

    }
}
