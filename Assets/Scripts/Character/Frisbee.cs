using UnityEngine;

public class Frisbee : MonoBehaviour
{
    public float speed = 4f;
    public float direction = 1f;
    public float velocity = 2f;

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
            pos = new Vector3(20f, Random.Range(lowerBound, upperBound), 0);
            direction = Random.value > 0.5f ? 1f : -1f;
        }

        transform.position = pos;

    }
}
