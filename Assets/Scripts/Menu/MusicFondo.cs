using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class MusicFondo : MonoBehaviour
{
    private static MusicFondo instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); 
            }
        }
        else
        {
            Destroy(gameObject); 
        }
    }

}
