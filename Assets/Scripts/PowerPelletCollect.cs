using UnityEngine;

public class PowerPelletCollect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PacManPowerUp pac = collision.GetComponent<PacManPowerUp>();
            if (pac != null)
                pac.ActivateSuper();

            Destroy(gameObject);
        }
    }
}
