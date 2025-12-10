using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardUI : MonoBehaviour
{
    public TextMeshProUGUI leaderboardText;

    void Start()
    {

        if (SupabaseManager.Instance == null)
        {
            Debug.LogError("SupabaseManager no encontrado en la escena.");
            leaderboardText.text = "No se pudo cargar el ranking.";
            return;
        }

        StartCoroutine(LoadLeaderboard());
    }

    IEnumerator LoadLeaderboard()
    {
        yield return SupabaseManager.Instance.GetTop10((json) =>
        {
            if (string.IsNullOrEmpty(json))
            {
                leaderboardText.text = "No se pudo cargar el ranking.";
                return;
            }

            List<ScoreEntry> scores = JsonUtilityWrapper.FromJsonList<ScoreEntry>(json);

            if (scores == null || scores.Count == 0)
            {
                leaderboardText.text = "Todavía nadie ayudó a la gaviota";
                return;
            }

            leaderboardText.text = "";

            for (int i = 0; i < scores.Count; i++)
            {
                ScoreEntry s = scores[i];

                string formattedTime = FormatTime(s.time);

                leaderboardText.text += $"{i + 1}. {s.player_name} — {formattedTime}\n";
            }
        });
    }

    private string FormatTime(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60f);
        int secs = Mathf.FloorToInt(seconds % 60f);
        return $"{minutes:00}:{secs:00}";
    }
}
