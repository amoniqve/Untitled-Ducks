using UnityEngine;

public class PacManPowerUp : MonoBehaviour
{
    public Vector3 startPosition;  // spawn point
    public bool isSuper = false;

    void Start()
    {
        transform.position = startPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerPellet"))
        {
            Destroy(collision.gameObject);
            ActivateSuper(5f); // 5 seconds supersayian 
        }
    }

    public void ActivateSuper(float duration = 5f)
    {
        isSuper = true;
        CancelInvoke("DeactivateSuper");
        Invoke("DeactivateSuper", duration);
    }

    void DeactivateSuper()
    {
        isSuper = false;
    }

    public void Respawn()
    {
        transform.position = startPosition;
        isSuper = false; // reset super when respawning for now
    }
}
