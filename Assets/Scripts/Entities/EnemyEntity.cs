using System.Collections;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    public float damage;
    public float damageDelay;
    public bool damageCooldown;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        DealDamage(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        DealDamage(collision);
    }
    private void DealDamage(Collider2D collision)
    {
        if (!damageCooldown)
        {
            if (collision != null) 
            {
                Player player = collision.gameObject.GetComponent<Player>();
                if (player != null) 
                {
                    player.TakeDamage(damage);
                    StartCoroutine(DamageCooldown());
                }
            }

        }
    }
    IEnumerator DamageCooldown()
    {
        damageCooldown = true;
        yield return new WaitForSeconds(damageDelay);
        damageCooldown = false;
    }
}
