using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float speed = 1.5f;
    public float chaseSpeed = 1f;

    private Vector2 direction;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector3 startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        startPos = transform.position;
        ChooseDirection();
    }

    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    void Update()
    {
        PacManPowerUp pac = FindObjectOfType<PacManPowerUp>();

        // Change color if player is super
        if (pac != null && pac.isSuper)
            sr.color = Color.blue;
        else
            sr.color = Color.red;

        // Random movement
        if (Random.value < 0.01f)
            ChooseDirection();

        // Chase logic
        if (pac != null && !pac.isSuper)
        {
            float distance = Vector2.Distance(transform.position, pac.transform.position);
            float chaseRange = 5f;

            if (distance < chaseRange)
            {
                Vector2 dir = (pac.transform.position - transform.position).normalized;
                direction = new Vector2(Mathf.Round(dir.x), Mathf.Round(dir.y));
                speed = chaseSpeed;
            }
            else
            {
                speed = 1.5f;
            }
        }
    }
    void ChooseDirection()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0: direction = Vector2.up; break;
            case 1: direction = Vector2.down; break;
            case 2: direction = Vector2.left; break;
            case 3: direction = Vector2.right; break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            ChooseDirection();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            PacManPowerUp pac = collision.gameObject.GetComponent<PacManPowerUp>();

            if (pac != null)
            {
                if (pac.isSuper)
                {
                    ScoreManager.instance.AddScore(200);
                    RespawnGhost();
                }
                else
                {
                    GameManager gm = FindObjectOfType<GameManager>();
                    if (gm != null)
                        gm.PlayerDied();
                }
            }
        }
    }

    public void RespawnGhost()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPos;
        ChooseDirection();
    }
}