using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class LivingEntity : MonoBehaviour
{
    private AudioSource audioSource;
    public float MaxHealth;
    public float Health;
    public int killExp;
    public List<ItemStack> lootItems;

    public List<AudioClip> idleSounds;
    public List<AudioClip> hurtSounds;
    public AudioClip dieSound;

    public event Action<LivingEntity> OnKilledEvent;

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
        else PlayRandomSound(hurtSounds);
        
    }
    public virtual void Die()
    {
        OnKilledEvent?.Invoke(this);
        World world = World.GetInstance();
        foreach (var item in lootItems)
        {
            world.SpawnItemEntity(gameObject.transform.position,item);
        }
        audioSource.PlayOneShot(dieSound);
        Destroy(gameObject);
    }
    public virtual void Start()
    {
        Health = MaxHealth;
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayRandomSound(List<AudioClip> sounds)
    {
        if (sounds.Count < 1) return;
        audioSource.PlayOneShot(sounds[UnityEngine.Random.Range(0, sounds.Count - 1)]);
    }
}
