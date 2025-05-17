using UnityEngine;

public class MusicFondo : MonoBehaviour
{
    private static MusicFondo instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Elimina duplicados
        }
    }

}
