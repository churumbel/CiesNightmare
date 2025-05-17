using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WaveSpawner : MonoBehaviour

{
    public static WaveSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    [System.Serializable]
    public class PoolItem
    {
        public string name;
        public GameObject prefab;
    }
    [System.Serializable]
    public class NivelData
    {
        public string nombreNivel;
        public List<GameObject> ordenDeAparicion;
    }

    [SerializeField] private List<NivelData> nivelesData;
    private List<GameObject> pool = new List<GameObject>();
    private List<GameObject> ordenActual = new List<GameObject>();

    

    public Transform spawnPoint; 
    public float spawnInterval = 2f;

    private int nextIndex = 0;
    private float timer;

    private void Start()
    {
        
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnNextObject();
            timer = spawnInterval;
        }
    }

    private void SpawnNextObject()
    {
        if (ordenActual.Count == 0 || pool.Count == 0)
            return;

        GameObject obj = pool[nextIndex];
        obj.transform.position = spawnPoint.position;
        obj.SetActive(true);

        nextIndex++;
        if (nextIndex >= pool.Count)
            nextIndex = 0;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void SetNivel(string nombreNivel)
    {
        NivelData data = nivelesData.Find(n => n.nombreNivel == nombreNivel);
        if (data == null)
        {
            Debug.LogError("No se encontró data para el nivel: " + nombreNivel);
            return;
        }

        ordenActual = data.ordenDeAparicion;
        CrearPool();
    }

    private void CrearPool()
    {
        pool.Clear();
        foreach (var prefab in ordenActual)
        {
            
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
            
        }

        nextIndex = 0;
        timer = 0f;
    }


}
