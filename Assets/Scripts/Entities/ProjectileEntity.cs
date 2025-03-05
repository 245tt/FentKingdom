using System.Collections;
using UnityEngine;

public class ProjectileEntity : MonoBehaviour
{
    public bool destroyOnCollision = true;
    public int despawnTimer = 15;
    void Start()
    {
        StartCoroutine(Timer());
    }
    IEnumerator Timer() 
    {
        yield return new WaitForSeconds(despawnTimer);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(destroyOnCollision) Destroy(gameObject);
    }
}
