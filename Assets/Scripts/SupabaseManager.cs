using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.Rendering.STP;

public class SupabaseManager : MonoBehaviour
{
    
    // Singleton
    public static SupabaseManager Instance { get; private set; }

    public string supabaseUrl = "";
    public string apiKey = "";

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
