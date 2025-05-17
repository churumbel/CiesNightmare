using UnityEngine;

public class MovimientoObstaculo : MonoBehaviour
{
    public float speed = 2f;
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < -10)
        {
            WaveSpawner.Instance.ReturnToPool(gameObject);
        }
    }
}
