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

    public GameObject turist;
    public GameObject politician;
    public List<GameObject> obstaculos;
    
    public float velocitity = 2;

    private string enemigoTipo;
    public GameObject frisbee;
    private GameObject frisbeeInstance;
    private float frisbeeSpeed = 4f;
    private float frisbeeDirection = 1f;
    private float nubeDirection = 1f;


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

    public HUD hud;
    public int PuntosTotales {get { return puntosTotales; } }
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //acá muevo el fondo de la playa
        //backgroud.material.mainTextureOffset = backgroud.material.mainTextureOffset + new Vector2(0.015f, 0) * Time.deltaTime;
        if (backgroud != null)
        {
            backgroud.material.mainTextureOffset += new Vector2(0.015f, 0) * Time.deltaTime;
        }

        //muevo el piso
        /*
        for (int i = 0; i < flat.Count; i++)
        {
            if(flat[i].transform.position.x <= -10)
            {
                flat[i].transform.position = new Vector3(10, -4,0);
            }
            flat[i].transform.position = flat[i].transform.position + new Vector3(-1,0,0) * Time.deltaTime*velocitity;
        }*/

        //mover obstaculos
        for (int i = 0; i < obstaculos.Count; i++)
        {
            // Para evitar el acceso a objetos destruidos
            if (obstaculos[i] == null) continue; 
            if (obstaculos[i].transform.position.x <= -10)
            {
                float randomObstaculos = Random.Range(11, 18);
                obstaculos[i].transform.position = new Vector3(randomObstaculos, -3, 0);
            }
            obstaculos[i].transform.position = obstaculos[i].transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * velocitity;
        }

        //mover frisbee
        if (enemigoActual != null)
        {
            Vector3 position = enemigoActual.transform.position;

            if (enemigoTipo == "frisbee")
            {
                // Movimiento zigzag vertical
                position.x += -1 * Time.deltaTime * velocitity;
                position.y += frisbeeSpeed * frisbeeDirection * Time.deltaTime;

                if (position.y >= upperBound)
                {
                    position.y = upperBound;
                    frisbeeDirection = -1f;
                }
                else if (position.y <= lowerBound)
                {
                    position.y = lowerBound;
                    frisbeeDirection = 1f;
                }

                if (position.x < -11f)
                {
                    position = new Vector3(20f, Random.Range(lowerBound, upperBound), 0);
                    frisbeeDirection = Random.value > 0.5f ? 1f : -1f;
                }
            }
            else if (enemigoTipo == "nube")
            {
                float nubeSpeed = frisbeeSpeed * 1.5f;
                position.x += -1 * Time.deltaTime * velocitity;
                position.y += nubeSpeed * nubeDirection * Time.deltaTime;

                if (position.y >= upperBound)
                {
                    position.y = upperBound;
                    nubeDirection = -1f;
                }
                else if (position.y <= lowerBound)
                {
                    position.y = lowerBound;
                    nubeDirection = 1f;
                }

                if (position.x < -11f)
                {
                    float yPos = Random.Range(lowerBound, upperBound);  // más variado
                    position = new Vector3(20f, yPos, 0);
                    nubeDirection = Random.value > 0.5f ? 1f : -1f;
                }
            }


            enemigoActual.transform.position = position;
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
        hud.DesactivateLife(lifes);
        if (lifes < 1)
        {
            Debug.Log("Game over =(");
            SceneManager.LoadScene("GameOver");
            RestartGame();
        }
    }
    public void RestartGame()
    {
        lifes = 3;

        if (enemigoActual != null)
        {
            Destroy(enemigoActual);
        }

        string nivelActual = SceneManager.GetActiveScene().name;

        if (nivelActual == "GameScene")
        {
            enemigoActual = Instantiate(frisbee, new Vector2(10f, Random.Range(lowerBound, upperBound)), Quaternion.identity);
            enemigoTipo = "frisbee";
        }
        else if (nivelActual == "GameSceneLevel2")
        {
            float yPos = Random.value > 0.5f ? upperBound : lowerBound;
            enemigoActual = Instantiate(nube, new Vector2(10f, yPos), Quaternion.identity);
            enemigoTipo = "nube";
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
            enemigoActual = Instantiate(frisbee, new Vector2(10f, Random.Range(lowerBound, upperBound)), Quaternion.identity);
            enemigoTipo = "frisbee";
            Debug.Log("Frisbee creado");

            obstaculos.Clear();
            obstaculos.Add(Instantiate(turist, new Vector2(14, -3), Quaternion.identity));
            obstaculos.Add(Instantiate(politician, new Vector2(20, -3), Quaternion.identity));
        }
        else if (nivelActual == "GameSceneLevel2")
        {
            float yPos = Random.value > 0.5f ? upperBound : lowerBound;
            enemigoActual = Instantiate(nube, new Vector2(10f, yPos), Quaternion.identity);
            enemigoTipo = "nube";
            nubeDirection = Random.value > 0.5f ? 1f : -1f;


            obstaculos.Clear();
            obstaculos.Add(Instantiate(piedra, new Vector2(14, -3), Quaternion.identity));
            obstaculos.Add(Instantiate(fabrica, new Vector2(20, -3), Quaternion.identity));
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

}
