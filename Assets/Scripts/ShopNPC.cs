using UnityEngine;


public class ShopNPC : MonoBehaviour, IInteractable
{
    public ShopInventory shop;

    private void Start()
    {
        shop = GetComponent<ShopInventory>();
    }
    public void Interact(Player player)
    {
        if (!ShopUI.instance.IsOpen)
            ShopUI.instance.ShowUI(this, player);
        else
            ShopUI.instance.HideUI();
    }
}