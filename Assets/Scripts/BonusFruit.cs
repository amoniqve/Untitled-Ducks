using UnityEngine;

public class BonusFruit : MonoBehaviour
{
    public int points = 100; // bonus score

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            ScoreManager.instance.AddScore(points); // give points yayyyy
            Destroy(gameObject); // remove fruit from scene
        }
    }
}
