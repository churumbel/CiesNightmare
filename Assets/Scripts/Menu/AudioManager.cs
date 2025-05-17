using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audios;

    private AudioSource controlAudio;

    private void Awake()
    {
        controlAudio = GetComponent<AudioSource>();
        
    }

    public void SeleccionAudio(int indice, float volumen) 
    { 
    controlAudio.PlayOneShot(audios[indice], volumen);
    }
}
