using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public UIManager UIManager;
    public AudioSource audioSource;
    public bool actionsBlocked;
    [Header("Player components")]
    public PlayerMovement playerMovement;
    public PlayerInventory playerInventory;
    public PlayerAttack playerAttack;
    [Header("Player stats")]
    public float maxHealth;
    public float health;
    public float strength;
    public float speed;
    public int defense;

    public int level = 1;
    public int experience;
    public int experienceToNextLevel;

    public int tebCoins;
    public int fentCoins;

    public List<PotionEffect> potionEffects;

    [Header("Sounds")]
    public List<AudioClip> hurtSounds;
    public List<AudioClip> walkingSounds;
    public List<AudioClip> runningSounds;

    const float armorFactor = 50;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInventory = GetComponent<PlayerInventory>();
        playerAttack = GetComponent<PlayerAttack>();

        potionEffects = new List<PotionEffect>();
        StartCoroutine(PotionEffectCounter());

        health = maxHealth;
        experienceToNextLevel = CalculateExpToNextLevel(level);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact(this);
                }
            }
        }
    }
    public void TakeDamage(float damage)
    {
        //armor factor is point where armor reduction is 50%
        float armorReduction = 1f - (defense / (defense + armorFactor));
        health -= damage * armorReduction;
        PlayRandomSound(hurtSounds);
        if (health <= 0) Die();
    }

    public void PlayRandomSound(List<AudioClip> sounds)
    {
        if (sounds.Count < 1) return;
        audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Count - 1)]);
    }

    public void RecalculateStats()
    {
        strength = 0;
        speed = 5;
        defense = 0;

        foreach (ArmorBase armorItem in playerInventory.GetArmors())
        {
            if (armorItem != null)
            {
                defense += armorItem.defense;
                speed += armorItem.speed;
                strength += armorItem.strength;
            }
        }

        foreach (PotionEffect effect in potionEffects)
        {
            if (effect.potionType == PotionType.Strength) strength += effect.power;
            if (effect.potionType == PotionType.Speed) speed += effect.power;
        }

    }
    private void Die()
    {
        // L    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            ItemEntity item = collision.GetComponent<ItemEntity>();
            if (item != null)
            {
                if (playerInventory.AddItem(item.itemStack)) 
                {
                    UIManager.UpdateUIs();
                    Destroy(item.gameObject);
                }
            }
        }
    }

    public void AddExp(int exp)
    {
        experience += exp;
        if (experience >= experienceToNextLevel)
        {
            level++;
            experienceToNextLevel = CalculateExpToNextLevel(level);
        }
    }
    public int CalculateExpToNextLevel(int lvl)
    {
        return (int)(Mathf.Pow(level, 1.5f) * 100);
    }
    IEnumerator PotionEffectCounter()
    {
        while (true)
        {

            for (int i = potionEffects.Count-1; i >= 0; i--)
            {
                
                PotionEffect potion = potionEffects[i];
            
                potion.duration--;
                if (potion.potionType == PotionType.Health)
                {
                    health += potion.power;
                    health = Mathf.Min(health, maxHealth);
                }
                if (potion.duration == 0)
                {
                    potionEffects.Remove(potion);
                    RecalculateStats();
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
    public void AddPotionEffect(PotionEffect newPotion)
    {
        bool foundPotType = false;
        foreach (PotionEffect pot in potionEffects)
        {
            if (pot.potionType == newPotion.potionType)
            {
                pot.duration = newPotion.duration;
                foundPotType = true;
                break;
            }
        }
        if (!foundPotType)
        {
            potionEffects.Add(new PotionEffect { potionType = newPotion.potionType, power = newPotion.power, duration = newPotion.duration });
        }
    }
}
