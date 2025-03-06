using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ItemEntity : MonoBehaviour
{
    public ItemStack itemStack;

    private void Start()
    {
        UpdateItemStack();
    }
    public void UpdateItemStack() 
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemStack.item.itemIcon;
    }

}
