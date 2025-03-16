using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Player player;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public UIManager UIManager;
    public Vector2 shootDir;

    public bool flipped = false;
    public bool flipLock = false;
    public void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (input.x > 0) flipped = true;
        if (input.x < 0) flipped = false;

        if(!flipLock) spriteRenderer.flipX = flipped;

        if (input.x == 0 && input.y == 0) animator.SetTrigger("Idle");
        else if (input.x == 0 && input.y > 0)
        {
            animator.SetTrigger("WalkUp");
        }
        else
            animator.SetTrigger("WalkSideways");

        rb.linearVelocity = input * player.speed;
    }

}
