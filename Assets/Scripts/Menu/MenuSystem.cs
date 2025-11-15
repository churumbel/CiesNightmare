using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public void Play()
    {
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }
        SceneManager.LoadScene("GameScene");
    }

    public void ReadInstructions()
    {
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }
        SceneManager.LoadScene("Instructions1");  
    }

    public void ReadRealInstructions()
    {
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }
        SceneManager.LoadScene("Instructions2");
    }

    public void ReadCreditos()
    {
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }
        SceneManager.LoadScene("Creditos");
    }

    public void ReadLeaderboardScene()
    {
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }
        SceneManager.LoadScene("LeaderboardScene");
    }

}
