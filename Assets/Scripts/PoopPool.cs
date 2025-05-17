using System.Collections.Generic;
using UnityEngine;

public class PoopPool : MonoBehaviour
{
    [SerializeField] private GameObject poop;
    [SerializeField] private List<GameObject> listaPoop;
    private int poolSize = 6;

    public static PoopPool Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        AddObjectToPool(5);
    }

    private void AddObjectToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(poop);
            obj.SetActive(false);
            listaPoop.Add(obj);
            obj.transform.parent = transform; 
        }

    }

    public GameObject RequestObject()
    {
        for (int i = 0; i < listaPoop.Count; i++)
        {
            if (!listaPoop[i].activeSelf)
            {
                listaPoop[i].SetActive(true);
                return listaPoop[i];
            }
        }
        return null;
    }
}
