using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI puntos;
    public TextMeshProUGUI cacas;
    public GameObject[] vidas;
    public GameObject flipflop;
    public GameObject rock;

    void Start()
    {
        // Desactiva la ojota y la piedra al inicio
        flipflop.SetActive(false);
        rock.SetActive(false);
        UpdatePoop(GameManager.Instance.CacasTotales);
        SetLifes(GameManager.Instance.VidasTotales);

    }

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

    public void UpdatePoop(int cantidad)
    {
        Debug.Log("Actualizando HUD con cantidad de caca: " + cantidad);
        cacas.text = cantidad.ToString();
    }

    public void SetLifes(int lifesParam)
    {
        for (int i = 0; i < lifesParam; i++)
        {
            vidas[i].SetActive(true);
        }

        for (int i = lifesParam; i < GameManager.Instance.MAX_LIFES; i++)
        {
            vidas[i].SetActive(false);
        }

    }


}