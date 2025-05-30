using UnityEngine;

public class Poop : MonoBehaviour
{
    public float speed = 5f;
    //Me fijo si la caca est� activa
    public int valor = 1;

    private void OnEnable()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Turist"))
        {
            Debug.Log("Poop hit the turist!");
            GameManager.Instance.RestarPuntos(valor);
            // Desactiva el objeto de la caca al colisionar
            gameObject.SetActive(false);  
        }

        if (collision.gameObject.CompareTag("Politician"))
        {
            Debug.Log("Poop hit the politician!");
            GameManager.Instance.SumarPuntos(valor);
            gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("Poop hit the floor!");
            gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Factory"))
        {
            Debug.Log("Poop hit the factory!");
            GameManager.Instance.SumarPuntos(valor);
            gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Frisbee"))
        {
            Debug.Log("Poop hit the frisbee!");
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Cloud"))
        {
            Debug.Log("Poop hit the cloud!");
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("food"))
        {
            Debug.Log("Poop hit the cloud!");
            gameObject.SetActive(false);
        }
    }
}
