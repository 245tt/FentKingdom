using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerInventoryUI inventoryUI;
    public Player player;
    public bool playerInventoryVisible;
    [Header("Player HUD")]
    public Slider healthSlider;
    public GameObject hud;
    public List<ItemSlot> hudItemSlots;
    public GameObject slotPointer;
    public Slider expSlider;
    public TMP_Text lvlText;

    void Start()
    {
        inventoryUI = GetComponent<PlayerInventoryUI>();
        healthSlider.value = player.health / player.maxHealth;
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            playerInventoryVisible = !playerInventoryVisible;
            inventoryUI.playerInventoryUI.SetActive(playerInventoryVisible);
            hud.SetActive(!playerInventoryVisible);
            slotPointer.SetActive(!playerInventoryVisible);
            UpdateUIs();
        }
        if (player.playerInventory.needsUpdate)
        {
            UpdateUIs();
        }

        healthSlider.value = player.health;
        healthSlider.maxValue = player.maxHealth;

        expSlider.value = player.experience;
        expSlider.maxValue = player.experienceToNextLevel;
        lvlText.text = player.level.ToString();

        slotPointer.transform.position = hudItemSlots[player.playerAttack.currentSlot].transform.position;
    }

    public void UpdateUIs() 
    {
        inventoryUI.UpdatePlayerInventory();
        for (int i = 0; i < hudItemSlots.Count; i++)
        {
            hudItemSlots[i].SetItem(player.playerInventory.items[i]);
        }
    }
}
