using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public float speed = 5f;
    public float detectionRadius = 15;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject target = GameObject.FindGameObjectsWithTag("PlayerEntity")[0];
        Vector2 moveDir = target.transform.position - rb.transform.position;
        if (moveDir.magnitude > detectionRadius) return;
        moveDir.Normalize();

        if (moveDir.x > 0) spriteRenderer.flipX = true;
        if (moveDir.x < 0) spriteRenderer.flipX = false;
        rb.linearVelocity = moveDir * speed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,detectionRadius);
    }
}
