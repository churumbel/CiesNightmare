using UnityEngine;

public class Rock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            Debug.Log("Roca recogida!");
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("Cloud"))
        {
            Debug.Log("Roca tomada por la nube!");
            gameObject.SetActive(false);
        }
    }
}
