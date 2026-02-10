using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruitPrefab; // assign BonusFruit prefab
    public Transform[] spawnPoints; // assign empty objects for spawn positions
    public float spawnInterval = 20f; // time between fruit spawns

    private void Start()
    {
        // Start spawning fruit repeatedly
        InvokeRepeating("SpawnFruit", spawnInterval, spawnInterval);
    }

    void SpawnFruit()
    {
        if (fruitPrefab == null || spawnPoints.Length == 0) return;

        int index = Random.Range(0, spawnPoints.Length);
        Instantiate(fruitPrefab, spawnPoints[index].position, Quaternion.identity);
    }
}
