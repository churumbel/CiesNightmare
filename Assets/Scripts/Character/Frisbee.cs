using UnityEngine;

public class Frisbee : MonoBehaviour
{
    //speed es en el eje y
    public float speed = 3f;
    public float direction = 1f;
    //uso velocity en el eje x
    public float velocity = 1f;
    private float upperBound = 4f;
    private float lowerBound = -3.5f;


    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += -1 * Time.deltaTime * velocity;
        pos.y += speed * direction * Time.deltaTime;

        if (pos.y >= upperBound)
        {
            pos.y = upperBound;
            direction = -1f;
        }
        else if (pos.y <= lowerBound)
        {
            pos.y = lowerBound;
            direction = 1f;
        }

        if (pos.x < -11f)
        {
            FrisbeePool.Instance.ReturnFrisbeeToPool(gameObject);
            return;
        }
        transform.position = pos;
    }
    public void Initialize()
    {
        direction = Random.value > 0.5f ? 1f : -1f;
    }
}
