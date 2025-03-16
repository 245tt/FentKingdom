using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public GameObject entityPrefab;
    public float spawnRadius;
    public float spawnDelay;
    public float spawnGroup;
    public int maxentities;

    public List<LivingEntity> entites;
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
            if (entites.Count <= maxentities)
            {
                for (int i = 0; i < spawnGroup; i++)
                {
                    Vector3 randomDir = Random.insideUnitCircle;
                    float randomDistance = Random.Range(0, spawnRadius);
                    Vector3 point = transform.position + randomDir * randomDistance;

                    GameObject enemy = Instantiate(entityPrefab);
                    enemy.transform.position = point;
                    if (enemy.TryGetComponent<LivingEntity>(out LivingEntity livingEntity))
                    {
                        entites.Add(livingEntity);
                        livingEntity.OnKilledEvent += LivingEntity_OnKilledEvent;
                    }
                }
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void LivingEntity_OnKilledEvent(LivingEntity obj)
    {
        entites.Remove(obj);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
