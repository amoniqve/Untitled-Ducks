using UnityEngine;
using UnityEngine.SceneManagement;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruitPrefab;        // Assign BonusFruit prefab
    public Transform[] level1SpawnPoints; // Assign empty objects for Level 1
    public Transform[] level2SpawnPoints; // Assign empty objects for Level 2
    public float spawnInterval = 20f;     // Time between fruit spawns

    private GameObject currentFruit;      // Track currently spawned fruit
    private Transform[] activeSpawnPoints;

    private void Start()
    {
        // Choose spawn points based on current level
        string levelName = SceneManager.GetActiveScene().name;
        if (levelName == "Level1")
            activeSpawnPoints = level1SpawnPoints;
        else if (levelName == "Level2")
            activeSpawnPoints = level2SpawnPoints;
        else
            activeSpawnPoints = new Transform[0]; // no spawn points if not Level 1 or 2

        // Start spawning fruit repeatedly
        InvokeRepeating(nameof(SpawnFruit), spawnInterval, spawnInterval);
    }

    void SpawnFruit()
    {
        if (fruitPrefab == null || activeSpawnPoints.Length == 0) return;

        // Only spawn if there isn't already a fruit in the scene
        if (currentFruit != null) return;

        int index = Random.Range(0, activeSpawnPoints.Length);
        currentFruit = Instantiate(fruitPrefab, activeSpawnPoints[index].position, Quaternion.identity);

        // Ensure correct size
        currentFruit.transform.localScale = Vector3.one;
    }
}
