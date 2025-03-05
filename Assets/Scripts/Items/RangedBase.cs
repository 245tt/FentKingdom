using UnityEngine;

[CreateAssetMenu(fileName = "New Bow", menuName = "Item/Weapons/Bow")]
public class Rangedbase : ItemBase
{
    public float baseDamage;
    public float baseCritChance;
    public float baseSpeed;
    public GameObject projectile;
    public float projectileSpeed;

    public virtual void Shoot(PlayerAttack player)
    {
        GameObject bullet = Instantiate(projectile);
        bullet.transform.position = player.transform.position;
        bullet.GetComponent<Rigidbody2D>().linearVelocity = player.shootDir * projectileSpeed;
    }
}
