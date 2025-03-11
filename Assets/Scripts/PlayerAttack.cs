using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerInventory inventory;
    Player player;
    [Header("Item holder variables")]
    public Transform itemHolderTransform;
    public bool attackBlocked = false;
    public PlayerWeapon playerWeapon;
    public ItemStack currentWeapon = new ItemStack(null, 0);
    public int currentSlot;
    public Vector2 shootDir;

    void Start()
    {
        player = GetComponent<Player>();
        inventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {

        Vector3 p = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(p);
        worldMousePos.z = 0;

        Vector2 lookDir = worldMousePos - transform.position;
        shootDir = lookDir.normalized;

        ChangeHotbarItem();

        if (currentWeapon.item != null)
        {
            if (Input.GetButton("Fire1") && !attackBlocked && !player.actionsBlocked)
            {
                if (currentWeapon.item is UsableItem)
                {
                    UsableItem usable = (UsableItem)currentWeapon.item;
                    usable.Use(player);
                    player.PlayRandomSound(usable.useSounds);
                    if (usable.consumable)
                    {
                        currentWeapon.amount--;
                        if (currentWeapon.amount == 0)
                        {
                            currentWeapon.item = null;
                            inventory.needsUpdate = true;
                        }
                    }
                }
            }
        }

    }
    private void ChangeHotbarItem()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            currentSlot++;
            currentSlot %= 5;
            UpdateCurrentWeapon();
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            currentSlot--;
            if (currentSlot == -1) currentSlot = 4;
            UpdateCurrentWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) currentSlot = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentSlot = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentSlot = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) currentSlot = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5)) currentSlot = 4;
    }
    public void UpdateCurrentWeapon() 
    {
        currentWeapon = inventory.items[currentSlot];
    }
    public void StartDelayAttack(float speed, GameObject attackObject)
    {
        StartCoroutine(DelayAttack(speed, attackObject));
    }

    IEnumerator DelayAttack(float speed, GameObject attackObject)
    {
        attackBlocked = true;
        yield return new WaitForSeconds(1f / speed);
        attackBlocked = false;
        Destroy(attackObject);
    }
    public void DamageLivingEntity(LivingEntity livingEntity)
    {
        livingEntity.TakeDamage(GetCurrentItemDamage());
    }

    public void OnKilledEnemy(LivingEntity livingEntity)
    {
        Debug.Log("Killed an enemy");
        player.AddExp(livingEntity.killExp);
        livingEntity.OnKilledEvent -= OnKilledEnemy;
    }
    private float GetCurrentItemDamage()
    {
        // to do ranged items and modifiers
        return ((Swordbase)currentWeapon.item).baseDamage + player.strength;
    }

}
