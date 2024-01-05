using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pelotita : MonoBehaviour
{
    [Header("Velocidad")]
    [SerializeField] private float velocidadInicial = 5f;
    [SerializeField] private float velocidadIncremento = 0.5f;

    [Header("Time")]
    public int temporizador = 3;
    public Text temporizadorText;
    [Header("Puntaje")]
    public int puntajeEquipoAzul;
    public int puntajeEquipoRojo;
    public Text textoPuntajeRojo;
    public Text textoPuntajeAzul;
    private GameObject Pelota;
    private Rigidbody pelotaRb;
    private Vector3 direccion;
    private Vector3 posicionInicial;

    void Start()
    {
        Time.timeScale = 0f;
        temporizador = 3;

        pelotaRb = GetComponent<Rigidbody>();
        direccion = GetRandomDirection();
        pelotaRb.velocity = direccion * velocidadInicial;

        // Guardar la posición inicial de la pelota
        posicionInicial = transform.position;

        // Obtener el componente Text del temporizador
        temporizadorText = GetComponent<Text>();

        // Iniciar la rutina para el temporizador
        StartCoroutine(ContarRegresivo());
    }

    // Función para el conteo regresivo y activación del juego
    IEnumerator ContarRegresivo()
    {
        // Mostrar el temporizador en pantalla
        while (temporizador > 0)
        {
            temporizadorText.text = temporizador.ToString();
            yield return new WaitForSeconds(1f);
            temporizador--;
            Time.timeScale = 0f;
        }

        temporizadorText.text = "¡GO!";
        yield return new WaitForSeconds(1f);
        temporizadorText.enabled = false;
        Time.timeScale = 1f; 
    }

    Vector3 GetRandomDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        Vector3 randomDirection = new Vector3(randomX, randomY, randomZ).normalized;
        return randomDirection;
    }

     // Función para detectar la colisión con una paleta
    bool ColisionConPared()
    {
        // Obtener la capa de las paredes (asegúrate de asignar las paredes a una capa específica en Unity)
        int paredesLayer = LayerMask.GetMask("Paredes");

        // Lanzar un rayo desde la posición actual de la pelota en la dirección de su movimiento
        Ray rayo = new Ray(transform.position, direccion);
        RaycastHit hitInfo;

        // Si el rayo golpea una pared, devuelve verdadero; de lo contrario, devuelve falso
        if (Physics.Raycast(rayo, out hitInfo, 0.6f, paredesLayer))
        {
            // Realizar un rebote simple cuando la pelota colisiona con una pared
            direccion = Vector3.Reflect(direccion, hitInfo.normal);
            return true;
        }

        return false;
    }

    // Función para detectar la colisión con una paleta y aumentar la velocidad
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paletas"))
        {
             velocidadInicial += velocidadIncremento;


            Debug.Log("Nueva velocidad: " + velocidadInicial);
        }
    }

void OnTriggerEnter(Collider other)
{
    if (other.gameObject.CompareTag("ArcoAzul"))
    {
        puntajeEquipoRojo++;
        ActualizarTextoPuntaje();
        volverAlaPosicionInicial();
    }
    else if (other.gameObject.CompareTag("ArcoRojo"))
    {
        puntajeEquipoAzul++;
        ActualizarTextoPuntaje();
        volverAlaPosicionInicial();
    }
}


    void ActualizarTextoPuntaje()
{
    textoPuntajeAzul.text = "" + puntajeEquipoAzul;
    textoPuntajeRojo.text = "" + puntajeEquipoRojo;
}


    public void volverAlaPosicionInicial()
    {
        // Establecer una nueva dirección aleatoria
        direccion = GetRandomDirection();
        // Colocar la pelota en la posición inicial
        transform.position = posicionInicial;
        // Reiniciar la velocidad y la velocidad angular de la pelota
        pelotaRb.velocity = Vector3.zero;
        pelotaRb.angularVelocity = Vector3.zero;
        // Aplicar la velocidad inicial con la nueva dirección
        pelotaRb.velocity = direccion * velocidadInicial;
    }
}