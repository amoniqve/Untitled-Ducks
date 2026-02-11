using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    
    public void PlayAgain()
    {
        SceneManager.LoadScene("Level1"); 
    }

    
    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
