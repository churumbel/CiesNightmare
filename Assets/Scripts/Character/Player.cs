using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    private float upperBound=4;
    private float lowerBound= -3.5f;

    // Posición inicial de la gaviota
    private Vector2 startPos;

    // Prefab de la caca y la posición de lanzamiento
    public GameObject ObjectPool; 
    public Transform poopPoint;

    private Animator animator;

    // Prefab de la chancla y la posición de lanzamiento
    public GameObject flipflop;
    public Transform flipflopPoint;
    private bool hasFlipFlop = false;

    // Prefab de la roca y la posición de lanzamiento
    public GameObject rock;
    public Transform rockPoint;
    private bool hasRock = false;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("No se encontró el AudioManager en la escena.");
        }
    }
    void Start()
    {
        startPos = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameManager.Instance.UsePoop())
            {
                GameObject poopInstance = PoopPool.Instance.RequestObject();
                poopInstance.transform.position = poopPoint.position;
                audioManager.SeleccionAudio(0, 0.5f);
                Debug.Log("uso caca del pool");
            }
        }


        if (Input.GetKeyDown(KeyCode.A) && hasFlipFlop==true)
        {
            GameObject flipflopInstance = Instantiate(flipflop, flipflopPoint.position, Quaternion.identity);
            flipflopInstance.GetComponent<FlipFlop>().Activate();
            audioManager.SeleccionAudio(1, 0.5f);
            // Desactivo la chancla después de lanzarla
            hasFlipFlop = false; 
            GameManager.Instance.DesactivateFlipFlop(); 
            animator.SetBool("hasFlipFlop", false);
        }

        if (Input.GetKeyDown(KeyCode.A) && hasRock == true)
        {
            GameObject rockInstance = Instantiate(rock, rockPoint.position, Quaternion.identity);
            rockInstance.GetComponent<RockMoving>().Activate();
            audioManager.SeleccionAudio(1, 0.5f);
            // Desactivo la roca después de lanzarla
            hasRock = false; 
            GameManager.Instance.DesactivateRock(); 
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
        // Posición inicial de la gaviota
        transform.position = startPos; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Turist")) 
        {
            Debug.Log("Colisión con turista");
            //cambia la animación de la gaviota
            animator.SetBool("hasFlipFlop", true);
            GameManager.Instance.hadFlipFlop(); 
            animator.SetBool("wasHurt", false);
            //cambia la posesión de la ojota sobre la gaviota
            hasFlipFlop = true;

        }

        if (collision.CompareTag("Politician")) 
        {
            Debug.Log("Colisión con político");
        }

        if (collision.CompareTag("Frisbee")) 
        {
            Debug.Log("Colisión con Frisbee");
            BeHurt();
        }
        if (collision.CompareTag("Cloud"))
        {
            Debug.Log("Colisión con Cloud");
            BeHurt();
        }

        if (collision.CompareTag("Factory")) 
        {
            Debug.Log("Colisión con fabrica");
            BeHurt();
        }

        if (collision.CompareTag("rock")) 
        {
            Debug.Log("Colisión con piedra fija");
            animator.SetBool("wasHurt", false);
            //cambia la posesión de la ROCA sobre la gaviota
            hasRock = true;
            GameManager.Instance.hadRock(); 
        }

    }

    public void BeHurt() 
    {
        audioManager.SeleccionAudio(2, 0.5f);
        animator.SetBool("hasFlipFlop", false);
        animator.SetBool("wasHurt", true);
        GameManager.Instance.LoseLife();
        StartCoroutine(MiCorrutina());
        
    }


    // Corrutina que espera 2 segundos para la animacion del daño
    private IEnumerator MiCorrutina()
    {
        Debug.Log("Esperando 2 segundos...");
        yield return new WaitForSeconds(2);
        animator.SetBool("wasHurt", false);
    }
}
