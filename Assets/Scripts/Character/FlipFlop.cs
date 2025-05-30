using UnityEngine;

public class FlipFlop : MonoBehaviour
{
    public float speed = 5f;
    // Variable para controlar si la ojota est� activa o no
    public bool isActive = false; 

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //No quiero que marque las colisiones si no esta activa
        if (!isActive) return; // Ignorar colisiones si no est� activa

        if (collision.gameObject.CompareTag("Turist"))
        {
            Debug.Log("FlipFlop hit the turist!");
            // Destruye el objeto de la chancla al colisionar
            GameManager.Instance.RestarPuntos(1);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Politician"))
        {
            Debug.Log("FlipFlop hit the politician!");
            // Destruye el objeto de la chancla al colisionar
            GameManager.Instance.SumarPuntos(5);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("FlipFlop hit the floor!");
            Destroy(gameObject);
        }
    }
}
