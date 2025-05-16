using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float speed = 7f;
    public float direction = 1f;
    public float velocity = 2f;

    public float startX = 12f;
    public float endX = -12f;

    // Las 3 posiciones fijas de Y donde la nube puede aparecer
    private float[] positionsY = new float[] { -3f,-1f,1f,2.5f, 4f };

    void Start()
    {
        ResetPosition();
    }

    void Update()
    {
        // Mover solo en X (línea recta)
        transform.position += Vector3.left * velocity * Time.deltaTime;

        // Si salió por la izquierda, la reseteamos a la derecha y con nueva Y
        if (transform.position.x < endX)
        {
            ResetPosition();
        }
    }

    void ResetPosition()
    {
        // Aleatoriamente uno de los tres valores de Y
        float newY = positionsY[Random.Range(0, positionsY.Length)];

        transform.position = new Vector3(startX, newY, 0);
    }
}
