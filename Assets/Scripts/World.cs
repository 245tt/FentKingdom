using UnityEngine;
using static UnityEditor.Progress;

public class World : MonoBehaviour
{
    public GameObject itemEntity;

    private static World instance;

    public static World GetInstance() { return instance; }
    public  void SpawnItemEntity(Vector3 position,ItemStack itemStack) 
    {
        GameObject entity = Instantiate(itemEntity);
        EntityItem entityItem = entity.GetComponent<EntityItem>();
        entityItem.itemStack = itemStack;
        entityItem.UpdateItemStack();
        entity.transform.position = transform.position;
    }
}
