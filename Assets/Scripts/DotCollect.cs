using UnityEngine;

public class DotCollect : MonoBehaviour
{
    public int points = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Add points safely
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(points);
            }

            // Destroy the dot
            Destroy(gameObject);
        }
    }
}
