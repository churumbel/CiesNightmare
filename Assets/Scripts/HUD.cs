using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI puntos;
    public GameObject[] vidas;
    public GameObject flipflop;
    public GameObject rock;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Desactiva la ojota y la piedra al inicio
        flipflop.SetActive(false);
        rock.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        puntos.text = "Puntos: " + GameManager.Instance.PuntosTotales.ToString();

    }

    public void UpdatePuntos(int puntosTotales)
    {
        puntos.text = "Puntos: " + puntosTotales.ToString();
    }

    public void DesactivateLife(int index)
    {
        vidas[index].SetActive(false);
    }
    public void ActivateLife(int index)
    {
        vidas[index].SetActive(true);
    }

    public void DesactivateFlipFlop()
    {
        flipflop.SetActive(false);
    }

    public void ActivateFlipFlop()
    {
        flipflop.SetActive(true);
    }

    public void DesactivateRock()
    {
        rock.SetActive(false);
    }

    public void ActivateRock()
    {
        rock.SetActive(true);
    }


}