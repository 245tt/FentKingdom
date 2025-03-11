using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI instance;
    public UIManager UIManager;
    public Player player;
    public ShopInventory shop;
    List<ItemSlot> itemSlots;
    public List<ItemSlot> sellItemSlots;
    public List<ItemSlot> buyItemSlots;
    public Button sellButton;
    public Button buyButton;
    public TMP_Text buyWorthText;
    public TMP_Text sellWorthText;
    public GameObject buyItemSlotsPanel;
    public GameObject sellItemSlotsPanel;
    public GameObject slotPrefab;
    public GameObject shopUIPanel;
    public GameObject shopInventoryItemsTransform;
    public bool IsOpen { get; private set; }

    public void ShowUI(ShopNPC shopNPC, Player player)
    {
        if (IsOpen)
        {
            HideUI();
        }
        shopUIPanel.SetActive(true);
        UIManager.ShowInventory();
        IsOpen = true;

        this.player = player;
        this.shop = shopNPC.shop;

        itemSlots = new List<ItemSlot>();
        buyItemSlots = new List<ItemSlot>();
        sellItemSlots = new List<ItemSlot>();
        for (int i = 0; i < shop.maxSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, shopInventoryItemsTransform.transform);
            ItemSlot slotComponent = slot.GetComponent<ItemSlot>();
            slotComponent.inventory = shop;
            slotComponent.draggable = false;
            itemSlots.Add(slotComponent);
            slotComponent.SetItem(shop.items[i]);
            slotComponent.button.onClick.AddListener(delegate { shop.AddToCart(slotComponent.itemStack); });
        }

        for (int i = 0; i < shop.buySlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, buyItemSlotsPanel.transform);
            ItemSlot slotComponent = slot.GetComponent<ItemSlot>();
            slotComponent.inventory = shop;
            slotComponent.draggable = false;
            buyItemSlots.Add(slotComponent);
            slotComponent.button.onClick.AddListener(delegate { shop.RemoveFromCart(slotComponent); });
        }

        for (int i = 0; i < shop.sellSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, sellItemSlotsPanel.transform);
            ItemSlot slotComponent = slot.GetComponent<ItemSlot>();
            slotComponent.inventory = shop;
            sellItemSlots.Add(slotComponent);
        }

        buyButton.onClick.AddListener(delegate { shop.BuyItems(player); });
        sellButton.onClick.AddListener(delegate { shop.SellItems(player); });
    }
    public void HideUI()
    {
        if (!IsOpen) return;
        shop.DropSellItems(player);

        shopUIPanel.SetActive(false);
        IsOpen = false;

        player = null;
        shop = null;

        foreach (ItemSlot slot in itemSlots) Destroy(slot.gameObject);
        foreach (ItemSlot slot in buyItemSlots) Destroy(slot.gameObject);
        foreach (ItemSlot slot in sellItemSlots) Destroy(slot.gameObject);

        buyButton.onClick.RemoveAllListeners();
        sellButton.onClick.RemoveAllListeners();
    }
    void Start()
    {
        instance = this;
        shopUIPanel.SetActive(false);
        IsOpen = false;
        UIManager = GetComponent<UIManager>();
    }

    private void Update()
    {
        if (!IsOpen) return;


        for (int i = 0; i < shop.maxSlots; i++)
        {
            itemSlots[i].SetItem(shop.items[i]);
        }
        for (int i = 0; i < shop.buySlots; i++)
        {
            buyItemSlots[i].SetItem(shop.items[i + shop.maxSlots]);
        }
        for (int i = 0; i < shop.sellSlots; i++)
        {
            sellItemSlots[i].SetItem(shop.items[i + shop.maxSlots + shop.buySlots]);
        }

        shop.CalculateItemWorth();
        buyWorthText.text = shop.buyWorth.ToString();
        sellWorthText.text = shop.sellWorth.ToString();

    }
}
