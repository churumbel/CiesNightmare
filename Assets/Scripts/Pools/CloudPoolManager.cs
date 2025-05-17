using System.Collections.Generic;
using UnityEngine;

public class CloudPoolManager : MonoBehaviour
{
    public static CloudPoolManager Instance;

    public GameObject cloudPrefab;
    public int cloudPoolSize = 5;

    public float startX = 12f;
    public float endX = -12f;

    private List<GameObject> cloudPool = new List<GameObject>();

    //Para manejar el tiempo entre nube y nube
    public float spawnInterval = 3f;
    private float timer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < cloudPoolSize; i++)
        {
            GameObject cloud = Instantiate(cloudPrefab);
            cloud.SetActive(false);
            cloudPool.Add(cloud);
        }

        timer = spawnInterval;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            bool spawned = TrySpawnCloud();
            if (spawned)
            {
                timer = spawnInterval;
            }
        }
    }

    private bool TrySpawnCloud()
    {
        GameObject cloud = GetInactiveCloud();
        if (cloud != null)
        {
            cloud.transform.position = new Vector3(startX, GetRandomY(), 0);
            cloud.SetActive(true);
            return true;
        }
        return false;
    }

    public void ReturnCloudToPool(GameObject cloud)
    {
        cloud.SetActive(false);
    }

    private GameObject GetInactiveCloud()
    {
        foreach (var cloud in cloudPool)
        {
            if (!cloud.activeInHierarchy)
                return cloud;
        }
        return null;
    }

    private float GetRandomY()
    {
        float[] positionsY = new float[] { -3f, -1f, 1f, 2.5f, 4f };
        return positionsY[Random.Range(0, positionsY.Length)];
    }



}
