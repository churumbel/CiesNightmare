using System.Collections.Generic;
using UnityEngine;

public class FrisbeePool : MonoBehaviour
{
    public static FrisbeePool Instance;
    public GameObject frisbeePrefab;
    public int frisbeePoolSize = 3;
    public float startX = 12f;
    public float endX = -12f;
    private List<GameObject> frisbeePool = new List<GameObject>();

    //Para manejar el tiempo entre los frisbee
    public float spawnInterval = 12f;
    private float timer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < frisbeePoolSize; i++)
        {
            GameObject frisbee = Instantiate(frisbeePrefab);
            frisbee.SetActive(false);
            frisbeePool.Add(frisbee);
        }
        timer = spawnInterval;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f && CountActiveFrisbees() == 0)
        {
            bool spawned = TrySpawnCloud();
            if (spawned)
            {
                timer = spawnInterval;
            }
        }
    }
    //Nube de generación
    private bool TrySpawnCloud()
    {
        GameObject frisbee = GetInactiveFrisbee();
        if (frisbee != null)
        {
            frisbee.transform.position = new Vector3(startX, GetRandomY(), 0);
            frisbee.SetActive(true);
            frisbee.GetComponent<Frisbee>().Initialize();
            return true;
        }
        return false;
    }

    public void ReturnFrisbeeToPool(GameObject frisbee)
    {
        frisbee.SetActive(false);
    }

    private GameObject GetInactiveFrisbee()
    {
        foreach (var frisbee in frisbeePool)
        {
            if (!frisbee.activeInHierarchy)
                return frisbee;
        }
        return null;
    }

    private float GetRandomY()
    {
        float[] positionsY = new float[] { -3f, -1f, 1f, 2.5f, 4f };
        return positionsY[Random.Range(0, positionsY.Length)];
    }
    //Para no tener más de un frisbee en la pantalla
    private int CountActiveFrisbees()
    {
        int count = 0;
        foreach (var frisbee in frisbeePool)
        {
            if (frisbee.activeInHierarchy)
            {
                count++;
            }
        }
        return count;
    }


}
