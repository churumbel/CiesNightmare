using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float velocity = 8f;
    public float endX = -12f;

    void Update()
    {
        transform.position += Vector3.left * velocity * Time.deltaTime;

        if (transform.position.x < endX)
        {
            CloudPoolManager.Instance.ReturnCloudToPool(gameObject);
            
        }
    }
}
