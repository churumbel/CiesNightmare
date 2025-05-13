using UnityEngine;

public class Rock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("player"))
        {
            
            Destroy(this.gameObject);
            
        }
    }
}
