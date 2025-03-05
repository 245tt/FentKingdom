using NUnit.Framework.Internal.Execution;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bow", menuName = "Item/Weapons/Bow")]
public class Rangedbase : UsableItem
{
    public float baseDamage;
    public float baseCritChance;
    public float baseSpeed;
    public float projectileSpeed;

    public GameObject projectile;

    public override void Use(Player player)
    {
        GameObject bullet = Instantiate(projectile);
        bullet.transform.position = player.transform.position;
        bullet.transform.rotation = Quaternion.Euler( 0,0,Mathf.Atan2(player.playerAttack.shootDir.y, player.playerAttack.shootDir.x) * Mathf.Rad2Deg-90f);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = player.playerAttack.shootDir * projectileSpeed;

        ProjectileEntity projectileComp = bullet.GetComponent<ProjectileEntity>();
        projectileComp.baseDamage = baseDamage;
        projectileComp.critChance = baseCritChance;
        player.playerAttack.StartDelayAttackRanged(baseSpeed);
    }
}
