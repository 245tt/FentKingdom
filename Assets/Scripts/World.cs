using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject itemEntity;


    private static World instance;

    public static World GetInstance() { return instance; }
    public GameObject SpawnItemEntity(Vector3 position, ItemStack itemStack)
    {
        GameObject entity = Instantiate(itemEntity);
        ItemEntity entityItem = entity.GetComponent<ItemEntity>();
        entityItem.itemStack = itemStack;
        entityItem.UpdateItemStack();
        entity.transform.position = transform.position;
        return entity;
    }

}
