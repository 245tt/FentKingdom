using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject itemEntity;


    private static World instance;

    public static World GetInstance() { return instance; }
    private void Start()
    {
        instance = this;
    }
    public GameObject SpawnItemEntity(Vector3 position, ItemStack itemStack)
    {
        GameObject entity = Instantiate(itemEntity);
        ItemEntity entityItem = entity.GetComponent<ItemEntity>();
        entityItem.itemStack = itemStack;
        entityItem.UpdateItemStack();
        entity.transform.position = position;
        Debug.Log($"spawned item entity at {entity.transform.position}");
        return entity;
    }

}
