using UnityEngine;

public class Poop : MonoBehaviour
{
    public float speed = 5f;

    //Me fijo si la caca está activa
    private bool isActive = false;
    public int valor = 1;
    
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    public void Activate()
    {
        isActive = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //No quiero que marque las colisiones si no esta activa
        if (!isActive) return; // Ignorar colisiones si no está activa

        if (collision.gameObject.CompareTag("Turist"))
        {
            Debug.Log("Poop hit the turist!");
            GameManager.Instance.RestarPuntos(valor);
            // Destruye el objeto de la caca al colisionar
            Destroy(gameObject); 
        }

        if (collision.gameObject.CompareTag("Politician"))
        {
            Debug.Log("Poop hit the politician!");
            GameManager.Instance.SumarPuntos(valor);
            // Destruye el objeto de la caca al colisionar
            Destroy(gameObject); 
        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("Poop hit the floor!");
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Factory"))
        {
            Debug.Log("Poop hit the factory!");
            GameManager.Instance.SumarPuntos(valor);
            Destroy(gameObject);
        }
    }
}
