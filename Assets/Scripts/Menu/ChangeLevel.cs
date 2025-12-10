using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("GameSceneLevel2");
        }
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("GameSceneLevel2");
    }
}
