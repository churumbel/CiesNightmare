using UnityEngine;

public class Food : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player")) 
        {
            GameManager.Instance.AddPoop(3);
            gameObject.SetActive(false);

        }
    }
}
