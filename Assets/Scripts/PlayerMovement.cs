using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Player player;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public UIManager UIManager;
    public Vector2 shootDir;

    public bool flipped = false;
    public bool flipLock = false;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (input.x > 0) flipped = false;
        if (input.x < 0) flipped = true;

        if(!flipLock) spriteRenderer.flipX = flipped;

        rb.linearVelocity = input * player.speed;
    }

}
