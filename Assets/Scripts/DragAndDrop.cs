using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
    public EventSystem eventSystem;

    [Header("Item infomation components")]
    private Coroutine hoverCoroutine;
    static float hoverThreshold = 0.5f;
    public ItemInformation itemInformation;
    public GameObject itemInformationPrefab;
    public GameObject popupCanvas;
    public GameObject draggedItem;
    public Image draggedItemImage;
    public ItemSlot startingItemSlot;
    ItemSlot hoveredSlot;
    private void Start()
    {
        draggedItemImage = draggedItem.GetComponent<Image>();
    }
    void Update()
    {
        //drag and drop
        if (Input.GetMouseButtonDown(0))
        {
            startingItemSlot = GetSlotUnderCursor();
        }
        if (Input.GetMouseButtonUp(0))
        {
            ItemSlot slot = GetSlotUnderCursor();
            if (slot != null && startingItemSlot != null)
            {
                SwapSlotsMK3(startingItemSlot, slot);
            }
            startingItemSlot = null;
        }
        if (Input.GetMouseButton(0))
        {
            if (startingItemSlot != null)
            {

                if (startingItemSlot.draggable && startingItemSlot.itemStack.item != null)
                {
                    draggedItem.transform.position = Input.mousePosition;
                    draggedItemImage.enabled = true;
                    draggedItemImage.sprite = startingItemSlot.itemStack.item.itemIcon;
                }
            }
            else draggedItemImage.enabled = false;
        }
        else draggedItemImage.enabled = false;


        // item information
        ItemSlot slotinfo = GetSlotUnderCursor();
        if (slotinfo != null)
        {
            if (hoveredSlot == null)
            {
                hoveredSlot = slotinfo;
                hoverCoroutine = StartCoroutine(HoverTimer(slotinfo));
            }
        }
        else
        {
            hoveredSlot = null;
            if (hoverCoroutine != null)
                StopCoroutine(hoverCoroutine);
            if (itemInformation != null)
            {
                Destroy(itemInformation.gameObject);
                itemInformation = null;
            }
        }
    }

    private ItemSlot GetSlotUnderCursor()
    {
        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            ItemSlot slot = result.gameObject.GetComponent<ItemSlot>();
            if (slot != null)
            {
                return slot;
            }
        }
        return null;
    }
    public static void SwapSlotsMK3(ItemSlot slot1, ItemSlot slot2)
    {
        if (slot1.draggable && slot2.draggable)
        {
            if (slot1.itemStack.item == null) return;
            if (slot2.itemStack.item != null)
                if (!slot1.CanAllowItem(slot2.itemStack)) return;
            if (!slot2.CanAllowItem(slot1.itemStack)) return;

            int indexA = slot1.inventory.items.IndexOf(slot1.itemStack);
            int indexB = slot2.inventory.items.IndexOf(slot2.itemStack);


            slot1.inventory.SwapItemsBetweenInventories(indexA, indexB, slot2.inventory);
        }
    }

    private IEnumerator HoverTimer(ItemSlot itemslot)
    {
        ItemStack itemStack = itemslot.itemStack;
        yield return new WaitForSeconds(hoverThreshold);
        if (itemStack.item != null)
        {
            itemInformation = Instantiate(itemInformationPrefab, popupCanvas.transform).GetComponent<ItemInformation>();
            itemInformation.transform.SetAsLastSibling();
            itemInformation.transform.position = itemslot.transform.position;
            itemInformation.itemName.text = itemStack.item.itemName;
            itemInformation.itemDescription.text = itemStack.item.itemDescription;
            itemInformation.itemStats.text = "item stats placeholder";
            itemInformation.itemModifiers.text = "item modifier placeholder";
            itemInformation.itemRarity.text = itemStack.item.itemRarity.ToString();
        }
    }
}
