using System.Collections;
using UnityEngine;

public class ProjectileEntity : MonoBehaviour
{
    public bool destroyOnCollision = true;
    public int despawnTimer = 15;

    public float critChance;
    public float baseDamage;
    public AudioClip projectileHitSound;
    public PlayerAttack playerAttack;

    void Start()
    {
        StartCoroutine(Timer());
    }
    IEnumerator Timer() 
    {
        yield return new WaitForSeconds(despawnTimer);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LivingEntity livingEntity;
        if (collision.gameObject.TryGetComponent<LivingEntity>(out livingEntity))
        {
            livingEntity.OnKilledEvent += playerAttack.OnKilledEnemy;
            livingEntity.TakeDamage(baseDamage);
            if (livingEntity != null) livingEntity.OnKilledEvent -= playerAttack.OnKilledEnemy;
        }

        if (destroyOnCollision) Destroy(gameObject);
    }
}
