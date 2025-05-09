using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

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

    public GameObject frisbee;
    private GameObject frisbeeInstance;
    private float frisbeeSpeed = 4f;
    private float frisbeeDirection = 1f;

    //Los límites de movimiento del frisbee
    private float upperBound = 4;
    private float lowerBound = -3.5f;

    private int lifes = 3;

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
        for (int i = 0; i < 21; i++)
        {
            //instancio el piso
            flat.Add(Instantiate(floor, new Vector2(-10+i,-4), Quaternion.identity));
        }

        //creo el turista
        obstaculos.Add(Instantiate(turist, new Vector2(14, -3), Quaternion.identity));

        //creo al político
        obstaculos.Add(Instantiate(politician, new Vector2(20, -3), Quaternion.identity));

        //creo el frisbee
        frisbeeInstance = Instantiate(frisbee, new Vector2(10f, Random.Range(lowerBound, upperBound)), Quaternion.identity);
        Debug.Log("Frisbee creado");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //acá muevo el fondo de la playa
        backgroud.material.mainTextureOffset = backgroud.material.mainTextureOffset + new Vector2(0.015f, 0) * Time.deltaTime;

        //muevo el piso
        for (int i = 0; i < flat.Count; i++)
        {
            if(flat[i].transform.position.x <= -10)
            {
                flat[i].transform.position = new Vector3(10, -4,0);
            }
            flat[i].transform.position = flat[i].transform.position + new Vector3(-1,0,0) * Time.deltaTime*velocitity;
        }

        //mover obstaculos
        for (int i = 0; i < obstaculos.Count; i++)
        {
            if (obstaculos[i].transform.position.x <= -10)
            {
                float randomObstaculos = Random.Range(11, 18);
                obstaculos[i].transform.position = new Vector3(randomObstaculos, -3, 0);
            }
            obstaculos[i].transform.position = obstaculos[i].transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * velocitity;
        }

        //mover frisbee
        if (frisbeeInstance != null)
        {
            // Movimiento vertical (rebota entre límites)
            Vector3 position = frisbeeInstance.transform.position;

            //Movmiento en forma de zigzag
            position.x += -1 * Time.deltaTime * velocitity;
            position.y += frisbeeSpeed * frisbeeDirection * Time.deltaTime;

            // Rebotar en los límites superior e inferior
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

            // Movimiento horizontal como los demás
            position.x += -1 * Time.deltaTime * velocitity;

            // Resetear si sale de la pantalla
            if (position.x < -11f)
            {
                position = new Vector3(20f, Random.Range(lowerBound, upperBound), 0);
                // Randomizar la dirección inicial
                frisbeeDirection = Random.value > 0.5f ? 1f : -1f; 
            }

            frisbeeInstance.transform.position = position;
        }



    }

    public void LoseLife()
    {
        lifes--;
        if (lifes < 0)
        {
            Debug.Log("Game over =(");
        }
    }
 }
