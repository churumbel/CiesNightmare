using UnityEngine;
using System.Collections.Generic;   

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject sardina;
    [SerializeField] private List<GameObject> listaObjetos;
    private int poolSize = 4;

    public static ObjectPool Instance;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        AddObjectToPool(5);

    }

    private void AddObjectToPool(int amount) {
        for(int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(sardina);
            obj.SetActive(false);
            listaObjetos.Add(obj);
        }

    }

    public GameObject RequestObject() { 
        for(int i = 0; i < listaObjetos.Count; i++)
        {
            if (!listaObjetos[i].activeSelf)
            {
                listaObjetos[i].SetActive(true);
                return listaObjetos[i];
            }
        }
        return null;
    }
}
