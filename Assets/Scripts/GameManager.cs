using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI winText;    
    public TextMeshProUGUI livesText;  

    [Header("Game Settings")]
    public int lives = 3;               // Knight lives
    public float nextLevelDelay = 2f;   // delay before loading next level
    public float fadeDuration = 1f;     

    private PacManPowerUp pacMan;
    private GhostMovement[] ghosts;

    void Start()
    {
        pacMan = FindObjectOfType<PacManPowerUp>();
        ghosts = FindObjectsOfType<GhostMovement>();

        if (winText != null)
        {
            winText.gameObject.SetActive(true);
            winText.alpha = 0f;
        }

        UpdateLivesUI(); 

        // Adjust ghost speeds based on level :)
        string levelName = SceneManager.GetActiveScene().name;
        foreach (var ghost in ghosts)
        {
            if (levelName == "Level1")
            {
                ghost.speed = 1.5f;
                ghost.chaseSpeed = 1f;
            }
            else if (levelName == "Level2")
            {
                ghost.speed = 3f;
                ghost.chaseSpeed = 2f;
            }
        }
    }

    void Update()
    {
        // Win condition: all dots collected yay <3
        if (GameObject.FindGameObjectsWithTag("Dot").Length == 0)
        {
            if (SceneManager.GetActiveScene().name == "Level2")
                StartCoroutine(ShowTextAndEnd("You Win!"));
            else
                StartCoroutine(ShowTextAndNextLevel("Next Level"));
        }
    }

    public void PlayerDied()
    {
        lives--;
        UpdateLivesUI();

        if (lives > 0)
        {
            // Respawn Knighty at start btw change the start
            pacMan.Respawn();

            // Reset all ghosts to start positions, only one for now
            foreach (var ghost in ghosts)
                ghost.RespawnGhost();
        }
        else
        {
            // Game over womp womp :(
            StartCoroutine(ShowTextAndEnd("You Lose!"));
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = "Lives: " + lives;
    }

    IEnumerator ShowTextAndNextLevel(string message)
    {
        if (winText == null) yield break;

        winText.text = message;
        yield return StartCoroutine(FadeText());
        yield return new WaitForSeconds(nextLevelDelay);
        LoadNextLevel();
    }

    IEnumerator ShowTextAndEnd(string message)
    {
        if (winText == null) yield break;

        winText.text = message;
        yield return StartCoroutine(FadeText());
        Time.timeScale = 0f; // pause game at end maybe??
    }

    IEnumerator FadeText()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime; // unscaled so works even if game is paused
            if (winText != null)
                winText.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }
    }

    void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(currentIndex + 1);
        else
            Debug.Log("No more levels!");
    }
}
