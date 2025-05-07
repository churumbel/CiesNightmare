using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    //[SerializeField] private float bound = 4.5f;
    private float upperBound=4;
    private float lowerBound= -3.5f;

    // Posici�n inicial de la gaviota
    private Vector2 startPos;

    // Prefab de la caca y la posici�n de lanzamiento
    public GameObject poop; 
    public Transform poopPoint; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        // Lanza la caca al presionar la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject poopInstance = Instantiate(poop, poopPoint.position, Quaternion.identity);
            poopInstance.GetComponent<Poop>().Activate(); // Activar colisiones
        }
    }
    void PlayerMovement()
    {
        // Para que la gaviota se mueva en el eje vertical
        float moveInput = Input.GetAxisRaw("Vertical"); 

        Vector2 playerPosition = transform.position;
        // Limitamos el movimiento en el eje Y
        playerPosition.y = Mathf.Clamp(playerPosition.y + moveInput * speed * Time.deltaTime, lowerBound, upperBound);
        transform.position = playerPosition;
    }

    public void ResetPlayer()
    {
        // Posici�n inicial de la gaviota
        transform.position = startPos; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Turist")) // Si colisionamos con un turista
        {
            Debug.Log("Colisi�n con turista"); 
            //GameManager.Instance.GanarOjota();
        }

        if (collision.CompareTag("Politician")) // Si colisionamos con un turista
        {
            Debug.Log("Colisi�n con pol�tico");
            //Si toca al politico deber�a perder una vida, si toca suelo, deber�a perder una vida
            //si la chancla toca al pol�tico, deber�a destruir al proyecto de ley
            //Destroy(collision.gameObject); // Lo destruimos
            //GameManager.Instance.PerderLife();
        }

        if (collision.CompareTag("Frisbee")) // Si colisionamos con un turista
        {
            Debug.Log("Colisi�n con Frisbee");
            //GameManager.Instance.GanarOjota();
        }

    }



}
