using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public SpriteRenderer weaponSprite;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LivingEntity livingEntity;
        if (collision.TryGetComponent<LivingEntity>(out livingEntity))
        {
            livingEntity.OnKilledEvent += playerAttack.OnKilledEnemy;
            playerAttack.DamageLivingEntity(livingEntity);
            if (livingEntity != null) livingEntity.OnKilledEvent -= playerAttack.OnKilledEnemy;
            //unsubscribe to prevent event doubling
        }
    }
}
