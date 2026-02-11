using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;                 
    public Transform spawnPoint; // Drag your PlayerSpawn here in Inspector

    private Rigidbody2D rb;

    private Vector2 currentDirection = Vector2.zero;   
    private Vector2 nextDirection = Vector2.zero;      

    // Horizontal wrap positions
    private float leftX = -18f;
    private float rightX = 18f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Move player to spawn immediately
        if (spawnPoint != null)
            transform.position = spawnPoint.position;

        // Make sure player is not moving at start
        rb.velocity = Vector2.zero;
    }

    void Update()
    {
        // Get input for next direction
        if (Input.GetKey(KeyCode.W)) nextDirection = Vector2.up;
        if (Input.GetKey(KeyCode.S)) nextDirection = Vector2.down;
        if (Input.GetKey(KeyCode.A)) nextDirection = Vector2.left;
        if (Input.GetKey(KeyCode.D)) nextDirection = Vector2.right;
    }

    void FixedUpdate()
    {
        // Update direction if possible
        if (CanMove(nextDirection))
            currentDirection = nextDirection;

        // Move if possible
        if (CanMove(currentDirection))
            rb.velocity = currentDirection * speed;
        else
            rb.velocity = Vector2.zero;

        CheckWrap();
    }

    bool CanMove(Vector2 dir)
    {
        if (dir == Vector2.zero) return false;
        RaycastHit2D hit = Physics2D.Raycast(rb.position, dir, 0.6f, LayerMask.GetMask("Walls"));
        return hit.collider == null;
    }

    void CheckWrap()
    {
        Vector3 pos = transform.position;

        if (pos.x < leftX)
            pos.x = rightX;
        else if (pos.x > rightX)
            pos.x = leftX;

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dot"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Enemy"))
        {
            PacManPowerUp pac = GetComponent<PacManPowerUp>();
            if (pac != null && pac.isSuper)
                return;

            Respawn();
            FindObjectOfType<GameOverManager>().ShowGameOver();
        }
    }

    public void Respawn()
    {
        if (spawnPoint != null)
            transform.position = spawnPoint.position;

        rb.velocity = Vector2.zero;
        currentDirection = Vector2.zero;
        nextDirection = Vector2.zero;
    }
}
