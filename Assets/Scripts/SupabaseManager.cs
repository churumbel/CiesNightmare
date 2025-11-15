using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.Rendering.STP;

public class SupabaseManager : MonoBehaviour
{
    
    // Singleton
    public static SupabaseManager Instance { get; private set; }

    public string supabaseUrl = "https://pffqzpedhqzaytwiajmh.supabase.co/rest/v1/scores";
    public string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InBmZnF6cGVkaHF6YXl0d2lham1oIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NjMxODA5ODQsImV4cCI6MjA3ODc1Njk4NH0.4-U0XSff5hITiHCb0rVyxdNgb5E9lWPkkmhU_YYWF5A";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public IEnumerator InsertScore(string playerName, float score, float time)
    {
        string jsonBody = $"{{\"player_name\":\"{playerName}\",\"score\":{score.ToString(CultureInfo.InvariantCulture)},\"time\":{time.ToString(CultureInfo.InvariantCulture)}}}";
        Debug.Log("JSON enviado a Supabase: " + jsonBody);

        UnityWebRequest request = new UnityWebRequest(supabaseUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("apikey", apiKey);
        request.SetRequestHeader("Authorization", $"Bearer {apiKey}");
        request.SetRequestHeader("Prefer", "return=minimal");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Score insertado correctamente");
        }
        else
        {
            Debug.LogError("Error al insertar score: " + request.error);
            Debug.LogError("Respuesta Supabase: " + request.downloadHandler.text);
        }
    }

    public IEnumerator GetTop10(System.Action<string> callback)
    {
        string url = supabaseUrl + "?order=time.asc&limit=10";

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("apikey", apiKey);
        request.SetRequestHeader("Authorization", $"Bearer {apiKey}");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            callback?.Invoke(request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
            callback?.Invoke(null);
        }
    }


}
