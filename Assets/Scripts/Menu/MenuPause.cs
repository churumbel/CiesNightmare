using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public GameObject menuPause;
    public bool juegoPausado = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(juegoPausado)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;
    }

    public void PauseGame()
    {
        menuPause.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
