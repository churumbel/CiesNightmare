using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    
    public void Play()

    {
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject); 
        }

        SceneManager.LoadScene("GameScene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
