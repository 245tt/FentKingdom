using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public UIManager UIManager;

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
    public AudioClip walkingSound;
    public AudioClip runningSound;


    void Start()
    {
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
        //armor calculation to do
        health -= damage;
        if (health <= 0) Die();
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
                Debug.Log(armorItem.itemName);
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
            EntityItem item = collision.GetComponent<EntityItem>();
            if (item != null)
            {
                playerInventory.AddItem(item.itemStack);
                UIManager.UpdateUIs();
                Destroy(item.gameObject);
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
            foreach (PotionEffect potion in potionEffects)
            {
                potion.duration--;
                if (potion.potionType == PotionType.Health) 
                {
                    health += potion.power;
                    health = Mathf.Min(health,maxHealth);
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
            potionEffects.Add(new PotionEffect {potionType = newPotion.potionType, power=newPotion.power,duration=newPotion.duration });
        }
    }
}
