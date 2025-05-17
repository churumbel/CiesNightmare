using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //para que el fondo de la playa se mueva
    public Renderer backgroud;
    //para generar el piso
    public GameObject floor;
    public List <GameObject> flat;

    //public GameObject turist;
    //public GameObject politician;
    public List<GameObject> obstaculos;
    
    public float velocitity = 2f;

    //public GameObject frisbee;
    //private GameObject frisbeeInstance;

    //Para controlar la caca
    private int cantidadCacas = 0;
    

    //para el nivel 2
    public GameObject nube; 
    private GameObject enemigoActual;
    public GameObject piedra;
    public GameObject fabrica;


    private int puntosTotales=0;

    //Los límites de movimiento del frisbee
    private float upperBound = 4;
    private float lowerBound = -3.5f;

    private int lifes = 3;

    //Comida
    public List<GameObject> comidas;
    public GameObject snack;
    public GameObject sardina;

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
        //acá muevo el fondo de la playa
        
        if (backgroud != null)
        {
            backgroud.material.mainTextureOffset += new Vector2(0.015f, 0) * Time.deltaTime;
        }

        if (puntosTotales>3) {
            string nivelActual = SceneManager.GetActiveScene().name;
            if(nivelActual == "GameScene")
            {
                Debug.Log("Pasaste al siguiente nivel");
                // Cambia a la escena del siguiente nivel
                SceneManager.LoadScene("ChangeLevel");
                //NextLevel();
            }
            
        }
        if (puntosTotales > 10)
        {
            string nivelActual = SceneManager.GetActiveScene().name;
            if (nivelActual == "GameSceneLevel2")
            {
                Debug.Log("Ganaste");
                // Cambia a la escena del siguiente nivel
                SceneManager.LoadScene("GameWinner");
                //NextLevel();
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
            //enemigoActual = Instantiate(frisbee, new Vector2(10f, Random.Range(lowerBound, upperBound)), Quaternion.identity);
            lifes = 3;
        }
        else if (nivelActual == "GameSceneLevel2")
        {
            float yPos = Random.value > 0.5f ? upperBound : lowerBound;
            enemigoActual = Instantiate(nube, new Vector2(10f, yPos), Quaternion.identity);
        }
    }


    public void NextLevel()
    {
        if (enemigoActual != null)
        {
            Destroy(enemigoActual);
        }

        SceneManager.LoadScene("ChangeLevel");
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
            //enemigoActual = Instantiate(frisbee, new Vector2(10f, Random.Range(lowerBound, upperBound)), Quaternion.identity);
            WaveSpawner.Instance.SetNivel(nivelActual);
        }
        else if (nivelActual == "GameSceneLevel2")
        {
            hud = FindAnyObjectByType<HUD>();
            hud.SetLifes(lifes);
            float yPos = Random.value > 0.5f ? upperBound : lowerBound;
            enemigoActual = Instantiate(nube, new Vector2(10f, yPos), Quaternion.identity);
            WaveSpawner.Instance.SetNivel(nivelActual);


        }
        


        // Asegurate de volver a encontrar el fondo si cambia entre escenas
        backgroud = GameObject.FindWithTag("Background")?.GetComponent<Renderer>();

        

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
