using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    [Header("Player inventory")]
    public Player player;
    public GameObject playerInventoryUI;
    public GameObject playerInventoryItemSlotsUI;
    public GameObject slotPrefab;
    public TMP_Text tebCoinDisplay;
    public TMP_Text fentCoinDisplay;
    public TMP_Text strengthDisplay;
    public TMP_Text speedDisplay;
    public TMP_Text defenseDisplay;
    public List<ItemSlot> playerInventorySlots;
    public List<ItemSlot> playerArmorSlots; // this list will have slots assigned manually no need to add them in Start() function
    void Start()
    {
        playerInventorySlots = new List<ItemSlot>();
        for (int i = 0; i < player.playerInventory.maxSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, playerInventoryItemSlotsUI.transform);
            ItemSlot slotComponent = slot.GetComponent<ItemSlot>();
            slotComponent.inventory = player.playerInventory;
            playerInventorySlots.Add(slotComponent);
        }
        UpdatePlayerInventory();
        playerInventoryUI.SetActive(false);
    }
    private void Update()
    {
        tebCoinDisplay.text = player.tebCoins.ToString();
        fentCoinDisplay.text = player.fentCoins.ToString();
        strengthDisplay.text = player.strength.ToString();
        speedDisplay.text = player.speed.ToString();
        defenseDisplay.text = player.defense.ToString();
    }
    public void UpdatePlayerInventory()
    {
        for (int i = 0; i < player.playerInventory.maxSlots; i++)
        {
            playerInventorySlots[i].SetItem(player.playerInventory.items[i]);
        }
        for (int i = 0; i < playerArmorSlots.Count; i++)
        {
            playerArmorSlots[i].SetItem(player.playerInventory.items[player.playerInventory.maxSlots + i]);
        }
        player.RecalculateStats();
    }
}
