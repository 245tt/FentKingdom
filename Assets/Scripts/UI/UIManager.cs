using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerInventoryUI inventoryUI;
    public DialogueUI dialogueUI;
    public ShopUI shopUI;
    public Player player;
    public bool playerInventoryVisible;
    public bool hudVisible;
    public bool dialogueVisible;
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
        shopUI = GetComponent<ShopUI>();
        dialogueUI = GetComponent<DialogueUI>();
        healthSlider.value = player.health / player.maxHealth;
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (playerInventoryVisible)
            {
                HideInventory();
                shopUI.HideUI();
                dialogueUI.HideUI();
            }
            else
            {
                ShowInventory();
            }
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


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideInventory();
            shopUI.HideUI();
            dialogueUI.HideUI();
        }

    }

    public void UpdateUIs()
    {
        inventoryUI.UpdatePlayerInventory();
        for (int i = 0; i < hudItemSlots.Count; i++)
        {
            hudItemSlots[i].SetItem(player.playerInventory.items[i]);
        }
        player.playerAttack.UpdateCurrentWeapon();
    }
    public void ShowInventory()
    {
        playerInventoryVisible = true;
        inventoryUI.playerInventoryUI.SetActive(true);
        HideHUD();
        UpdateUIs();
        player.actionsBlocked = true;
    }
    public void HideInventory()
    {
        playerInventoryVisible = false;
        inventoryUI.playerInventoryUI.SetActive(false);
        ShowHUD();
        player.actionsBlocked = false;
    }
    public void ShowHUD() 
    {
        hudVisible = true;
        hud.SetActive(true);
        slotPointer.SetActive(true);
    }
    public void HideHUD()
    {
        hudVisible = false;
        hud.SetActive(false);
        slotPointer.SetActive(false);
    }
}
