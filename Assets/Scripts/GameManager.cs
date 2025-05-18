using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject floor;
    public List <GameObject> flat;
    public List<GameObject> obstaculos;
    public float velocitity = 2f;

    //Para controlar la caca
    private int cantidadCacas = 0;
    
    //para el nivel 2
    private GameObject enemigoActual;
    private int puntosTotales=0;
    private int lifes = 3;
    public int MAX_LIFES = 3;

    public HUD hud;
    public int VidasTotales { get { return lifes; } }
    public int PuntosTotales {get { return puntosTotales; } }
    public int CacasTotales { get { return cantidadCacas; } }
    public void SumarPuntos(int puntosASumar) 
    {
        puntosTotales += puntosASumar;
        hud.UpdatePuntos(puntosTotales);
        Debug.Log("Puntos totales: " + puntosTotales);
    }
    public void RestarPuntos(int puntosARestar)
    {
        if (puntosTotales > 0)
        {
            puntosTotales -= puntosARestar;
            Debug.Log("Puntos totales: " + puntosTotales);
        }
    }

    // Singleton para acceder a la instancia de GameManager
    public static GameManager Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        //Patrón Singleton
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

    void FixedUpdate()
    {
        if (puntosTotales>14) {
            string nivelActual = SceneManager.GetActiveScene().name;
            if(nivelActual == "GameScene")
            {
                Debug.Log("Pasaste al siguiente nivel");
                // Cambia a la escena del siguiente nivel
                SceneManager.LoadScene("ChangeLevel");
                
            }
            
        }
        if (puntosTotales > 29)
        {
            string nivelActual = SceneManager.GetActiveScene().name;
            if (nivelActual == "GameSceneLevel2")
            {
                Debug.Log("Ganaste");
                // Cambia a la escena del siguiente nivel
                SceneManager.LoadScene("GameWinner");
                
            }
        }
    }

    public void LoseLife()
    {
        lifes--;
        if (hud != null)
            hud.DesactivateLife(lifes);
        else
            Debug.LogWarning("HUD no está asignado en GameManager");

        if (lifes < 1)
        {
            Debug.Log("Game over =(");
            SceneManager.LoadScene("GameOver");
            RestartGame();
        }
    }
    public void RestartGame()
    {
        if (enemigoActual != null)
        {
            Destroy(enemigoActual);
        }

        string nivelActual = SceneManager.GetActiveScene().name;

        if (nivelActual == "GameScene")
        {
            lifes = 3;
        }
        else if (nivelActual == "GameSceneLevel2")
        {

        }
    }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InicializarEscena(scene.name);
    }

    private void InicializarEscena(string nivelActual)
    {
        if (nivelActual == "GameScene")
        {
            WaveSpawner.Instance.SetNivel(nivelActual);
        }
        else if (nivelActual == "GameSceneLevel2")
        {
            hud = FindAnyObjectByType<HUD>();
            hud.SetLifes(lifes);
            WaveSpawner.Instance.SetNivel(nivelActual);
        }
    }

    public void hadFlipFlop() { 
        hud.ActivateFlipFlop();
    }

    public void DesactivateFlipFlop()
    {
        hud.DesactivateFlipFlop();
    }

    public void AddPoop(int cantidad)
    {
        cantidadCacas += cantidad;
        Debug.Log("Cantidad de cacas: " + cantidadCacas);
        hud.UpdatePoop(cantidadCacas); 
    }

    public bool UsePoop()
    {
        if (cantidadCacas > 0)
        {
            cantidadCacas--;
            hud.UpdatePoop(cantidadCacas);
            return true;
        }
        return false;
    }

    public void hadRock()
    {
        hud.ActivateRock();
    }

    public void DesactivateRock()
    {
        hud.DesactivateRock();
    }
}
