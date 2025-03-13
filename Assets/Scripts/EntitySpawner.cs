using System.Collections;
using UnityEngine;
using static UnityEngine.UI.Image;

public class EntitySpawner : MonoBehaviour
{
    public GameObject entityPrefab;
    public float spawnRadius;
    public float spawnDelay;
    public float spawnGroup;

    void Start()
    {
        StartCoroutine(SpawnTick());   
    }

    void Update()
    {

    }
    IEnumerator SpawnTick() 
    {
        while (true)
        {
            for (int i = 0; i < spawnGroup; i++)
            {
                Vector3 randomDir = Random.insideUnitCircle;
                float randomDistance = Random.Range(0, spawnRadius);
                Vector3 point = transform.position + randomDir * randomDistance;

                GameObject enemy = Instantiate(entityPrefab);
                enemy.transform.position = point;
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
