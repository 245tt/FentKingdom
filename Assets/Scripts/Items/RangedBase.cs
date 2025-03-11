using NUnit.Framework.Internal.Execution;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bow", menuName = "Item/Weapons/Bow")]
public class Rangedbase : UsableItem
{
    public float baseDamage;
    public float baseCritChance;
    public float baseSpeed;
    public float projectileSpeed;
    public GameObject rangedObject;
    public float rangedScale;
    public GameObject projectile;

    public override void Use(Player player)
    {

        GameObject ranged = Instantiate(rangedObject, player.playerAttack.itemHolderTransform);
        float angle = Mathf.Atan2(player.playerAttack.shootDir.y, player.playerAttack.shootDir.x) * Mathf.Rad2Deg;
        ranged.transform.rotation = Quaternion.Euler(0, 0, angle);
        ranged.transform.localScale = new Vector3(rangedScale, rangedScale, rangedScale);
        float distance = 1.4f;
        ranged.transform.localPosition = new Vector3(player.playerAttack.shootDir.x* distance, player.playerAttack.shootDir.y * distance, 0);
        ranged.transform.SetAsLastSibling();
        PlayerWeapon rangedWeapon = ranged.GetComponent<PlayerWeapon>();
        rangedWeapon.playerAttack = player.playerAttack;
        rangedWeapon.weaponSprite.sprite = itemIcon;

        GameObject bullet = Instantiate(projectile);
        bullet.transform.position = player.transform.position;
        bullet.transform.rotation = Quaternion.Euler( 0,0,Mathf.Atan2(player.playerAttack.shootDir.y, player.playerAttack.shootDir.x) * Mathf.Rad2Deg-90f);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = player.playerAttack.shootDir * projectileSpeed;

        ProjectileEntity projectileComp = bullet.GetComponent<ProjectileEntity>();
        projectileComp.playerAttack = player.playerAttack;
        projectileComp.baseDamage = baseDamage;
        projectileComp.critChance = baseCritChance;

        player.playerAttack.StartDelayAttack(baseSpeed,ranged);
    }
}
