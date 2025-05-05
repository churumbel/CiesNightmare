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
        obstaculos.Add(Instantiate(politician, new Vector2(18, -3), Quaternion.identity));
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


    }
}
