using System;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{

    public float MaxHealth;
    public float Health;
    public int killExp;
    public List<ItemStack> lootItems;
    public GameObject itemEntity;

    public AudioClip idleSound;
    public AudioClip hurtSound;

    public event Action<LivingEntity> OnKilledEvent;

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        OnKilledEvent?.Invoke(this);

        foreach (var item in lootItems)
        {
            GameObject entity = Instantiate(itemEntity);
            ItemEntity entityItem = entity.GetComponent<ItemEntity>();
            entityItem.itemStack = item;
            entityItem.UpdateItemStack();
            entity.transform.position = transform.position;
        }

        Destroy(gameObject);
    }
    public virtual void Start()
    {
        Health = MaxHealth;
    }
}
