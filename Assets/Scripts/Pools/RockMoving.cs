using UnityEngine;

public class RockMoving : MonoBehaviour
{
    public float speed = 5f;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //No quiero que marque las colisiones si no esta activa
        if (!isActive) return; // Ignorar colisiones si no está activa

        if (collision.gameObject.CompareTag("Factory"))
        {
            Debug.Log("Rock hit the factory!");
            // Destruye el objeto de la piedra al colisionar
            GameManager.Instance.SumarPuntos(5);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Politician"))
        {
            Debug.Log("Rock hit the politician!");
            GameManager.Instance.SumarPuntos(5);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("Rock hit the floor!");
            Destroy(gameObject);
        }
    }
}
