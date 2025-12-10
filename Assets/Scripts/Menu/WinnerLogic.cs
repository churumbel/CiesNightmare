using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class WinnerLogic : MonoBehaviour
{
    private SupabaseManager supabase;

    void Start()
    {
        if (SupabaseManager.Instance == null)
        {
            Debug.LogError("SupabaseManager no encontrado en la escena.");
            SceneManager.LoadScene("GameWinner"); 
            return;
        }

        StartCoroutine(CheckIfPlayerEntersRanking());
    }

    private IEnumerator CheckIfPlayerEntersRanking()
    {
        float finalTime = GameManager.Instance.GameTime;

        yield return SupabaseManager.Instance.GetTop10((json) =>
        {
            if (string.IsNullOrEmpty(json))
            {
                // No se pudo obtener ranking, lo mandamos a escena de ganador
                SceneManager.LoadScene("GameWinner");
                return;
            }

            List<ScoreEntry> scores = JsonUtilityWrapper.FromJsonList<ScoreEntry>(json);

            if (scores == null || scores.Count < 10)
            {
                SceneManager.LoadScene("AskName");
                return;
            }

            ScoreEntry worst = scores[scores.Count - 1];

            // ¿El jugador entra en el ranking?
            if (finalTime < worst.time)
            {
                SceneManager.LoadScene("AskName");
            }
            else
            {
                SceneManager.LoadScene("GameWinner");
            }
        });
    }

}

[System.Serializable]
public class ScoreEntry
{
    public string player_name;
    public float score;
    public float time;
}
