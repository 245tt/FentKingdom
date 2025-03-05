using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Player player;
    Rigidbody2D rb;
    public UIManager UIManager;
    public Vector2 shootDir;


    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.linearVelocity = input * player.speed;
    }

}
