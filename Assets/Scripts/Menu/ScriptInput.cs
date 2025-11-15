using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class ScriptInput : MonoBehaviour
{
    public TMP_InputField nameField;

    public void SaveName()
    {
        string playerName = nameField.text;
        float time = GameManager.Instance.GameTime;
        int score = GameManager.Instance.PuntosTotales;

        if (SupabaseManager.Instance == null)
        {
            Debug.LogError("SupabaseManager no encontrado");
            return;
        }

        StartCoroutine(SaveAndLoadLeaderboard(playerName, score, time));
    }

    private IEnumerator SaveAndLoadLeaderboard(string playerName, int score, float time)
    {
        yield return SupabaseManager.Instance.InsertScore(playerName, score, time);
        SceneManager.LoadScene("LeaderboardScene");
    }

}