using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Ball : MonoBehaviour
{
    [SerializeField] private GameObject MensajeGanador;
    [SerializeField] private GameObject Temporizador;
    [SerializeField] private GameObject pelotaReset;
    [SerializeField] private GameObject Inicio;
    [SerializeField] private GameObject volverAConfiguracion;
    [Header("Animacion")]
    //public Animator buttonAnimator;
    [Header("Audio")]
    public AudioSource countdownSound;
    
    [Header("Reiniciar_Ball")]
    //Reiniciar Pelota en el lugar inicial
       public Button reiniciarBoton;

    private bool pausaActiva = false;
    [Header("Particulas")]
    public ParticleSystem Confeti;
    public ParticleSystem Confeti_1;
    [Header("Conf_150Goles")]
    public ConfiguracionGoles configuracionGoles = new ConfiguracionGoles();
    private bool juegoTerminado = false;
    private int golesEquipoAzul = 0;
    private int golesEquipoRojo = 0;

    [Header("Text")]
    public Text mensajeGanadorText;
    
    [Header("Velocidad")]
    [SerializeField] private float velocidadInicial = 5f;
    [SerializeField] private float velocidadIncremento = 0.5f;

    public Text temporizadorText;
    private float tiempoRestante = 3.0f;
    private bool juegoComenzado = false;
    
    [Header("Puntaje")]
    public Text textoPuntajeRojo;
    public Text textoPuntajeAzul;
    private GameObject Pelota;
    private Rigidbody pelotaRb;
    private Vector3 direccion;
    private Vector3 posicionInicial;

    [Header("Temporizador")]
    private float tiempoFaltante = 3.0f;
    private bool temporizadorActivo = false;

    private void Awake()
    {
        Confeti.Pause();
        Confeti_1.Pause();
    }
    void Start()
    {
        reiniciarBoton.onClick.AddListener(ReiniciarPelota);
        pelotaRb = GetComponent<Rigidbody>();
        Time.timeScale = 0f;

        pelotaRb = GetComponent<Rigidbody>();
        direccion = GetRandomDirection();

        // Guardar la posición inicial de la pelota
        posicionInicial = transform.position;
        //Animator
        //buttonAnimator = GetComponent<Animator>();
    }
    void ReiniciarPelota()
    {
        // Detener la pelota si está en movimiento
        pelotaRb.velocity = Vector3.zero;
        pelotaRb.angularVelocity = Vector3.zero;

        // Volver a la posición inicial
        transform.position = posicionInicial;

        // Reiniciar la dirección y aplicar la velocidad inicial
        direccion = GetRandomDirection();
        pelotaRb.velocity = direccion * velocidadInicial;

        // Mostrar temporizador
        StartCoroutine(MostrarTemporizador());
    }

        IEnumerator MostrarTemporizador()
    {
        pausaActiva = true;  // Activar la pausa
        temporizadorText.enabled = true;

        for (int i = 3; i > 0; i--)
        {
            // Reproducir sonido cada vez que cambia de 3, 2, 1
            PlayCountdownSound();

            temporizadorText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }

        temporizadorText.enabled = false;
        pausaActiva = false;  // Desactivar la pausa

        // Reanudar la pelota después de que termine el temporizador
        pelotaRb.velocity = direccion * velocidadInicial;
    }

    void Update()
    {
        if (!juegoComenzado)
        {
            if (tiempoRestante > 0)
            {
                tiempoRestante -= Time.unscaledDeltaTime; //Time.unscaledDeltaTime para ignorar la escala de tiempo
                temporizadorText.text = Mathf.Ceil(tiempoRestante).ToString();
                
                if (tiempoRestante < 3 && tiempoRestante >= 2)
                {
                    PlayCountdownSound();
                }

                if (tiempoRestante <= 0)
                {
                    IniciarJuego();
                    tiempoRestante = 0;
                }
            }
        }
        if (pausaActiva)
        {
            // Pausar la pelota mientras el temporizador está activo
            pelotaRb.velocity = Vector3.zero;
            pelotaRb.angularVelocity = Vector3.zero;
        }
    }

      void PlayCountdownSound()
    {
        if (countdownSound != null)
        {
            countdownSound.Play();
        }
    }


void FixedUpdate()
{
    if (juegoComenzado)
    {
        if (!ColisionConPared())
        {
            pelotaRb.velocity = pelotaRb.velocity.normalized * velocidadInicial;
        }
    }
}

    //Dirección aleatoria
Vector3 GetRandomDirection()
{
    float randomX = Random.Range(0.5f, 1.5f);
    float randomY = Random.Range(0.5f, 1.5f);
    float randomZ = Random.Range(0.5f, 1.5f);

    Vector3 randomDirection = new Vector3(
        Random.Range(-1f, 1f) * randomX,
        Random.Range(-1f, 1f) * randomY,
        Random.Range(-1f, 1f) * randomZ
    ).normalized;

    return randomDirection;
}




private bool ColisionConPared()
{
    int paredesLayer = LayerMask.GetMask("Paredes");

    Ray rayo = new Ray(transform.position, direccion);
    RaycastHit hitInfo;

    if (Physics.Raycast(rayo, out hitInfo, 0.6f, paredesLayer))
    {
        // Actualizar la dirección con una nueva dirección aleatoria
        direccion = GetRandomDirection();
        return true;
    }
    return false;
}



Vector3 GetDiagonalDirection(Vector3 wallNormal)
{
    // Obtener una dirección perpendicular a la normal de la pared
    Vector3 perpendicular = Vector3.Cross(direccion, wallNormal).normalized;

    // Ajustar la dirección diagonal para ser diagonal independientemente de la dirección original
    Vector3 diagonalDirection = new Vector3(perpendicular.x, wallNormal.y, perpendicular.z);

    return diagonalDirection.normalized;
}

    // Función para detectar la colisión con una paleta y aumentar la velocidad
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paletas"))
        {
            // Aumentar la velocidad al colisionar con una paleta
            velocidadInicial += velocidadIncremento;

            Debug.Log("Nueva velocidad: " + velocidadInicial);

        }
    }

void OnTriggerEnter(Collider other)
{
    if (juegoTerminado)
        return;

    if (other.gameObject.CompareTag("ArcoAzul"))
    {
        golesEquipoRojo++;
    }
    else if (other.gameObject.CompareTag("ArcoRojo"))
    {
        golesEquipoAzul++;
    }

    ActualizarTextoPuntaje();
    Confeti.Play();
    Confeti_1.Play();

    // Mostrar temporizador después de anotar un gol
    StartCoroutine(ActivarTemporizador());

    volverAlaPosicionInicial(); // Volver a la posición inicial después de anotar un gol
    VerificarJuegoTerminado();
}


    IEnumerator ActivarTemporizador()
    {
        pausaActiva = true;  // Activar la pausa
        temporizadorText.enabled = true;

        for (int i = 3; i > 0; i--)
        {
            temporizadorText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }

        temporizadorText.enabled = false;
        pausaActiva = false;  // Desactivar la pausa

        // Reanudar la pelota después de que termine el temporizador
        pelotaRb.velocity = direccion * velocidadInicial;
    }


void ActualizarTextoPuntaje()
{
    textoPuntajeAzul.text = "" + golesEquipoAzul;
    textoPuntajeRojo.text = "" + golesEquipoRojo;
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

public void VerificarJuegoTerminado()
{
    if (golesEquipoAzul >= configuracionGoles.golesParaTerminar || golesEquipoRojo >= configuracionGoles.golesParaTerminar)
    {
        juegoTerminado = true;
        MensajeGanador.SetActive(true);
        //Aca va ir el boton de volver atras
        volverAConfiguracion.SetActive(true);
        Inicio.SetActive(false);
        pelotaReset.SetActive(false);
        Temporizador.SetActive(false);
        string mensajeGanador = "";

        if (golesEquipoAzul > golesEquipoRojo)
        {
            mensajeGanador = "¡Equipo Azul gana!";
        }
        else if (golesEquipoRojo > golesEquipoAzul)
        {
            mensajeGanador = "¡Equipo Rojo gana!";
        }
        else
        {
            mensajeGanador = "¡Empate!";
        }
        Time.timeScale = 0f;
        // Mostrar el mensaje de ganador en la consola
        Debug.Log(mensajeGanador);

        // Mostrar el mensaje de ganador en el objeto Text en la pantalla
        mensajeGanadorText.text = mensajeGanador;

        // Aquí puedes agregar cualquier lógica adicional que desees después de que se determine el ganador
    }
}


    public void ComenzarJuego()
    {
        juegoComenzado = false;
        tiempoRestante = 3.0f;
        temporizadorText.enabled = true;
        Time.timeScale = 0f;
        temporizadorText.text = tiempoRestante.ToString();
    }

void IniciarJuego()
{
    tiempoRestante = 0; // Detenemos el temporizador
    juegoComenzado = true; // Indicamos que el juego ha comenzado
    temporizadorText.text = "¡GO!";
    temporizadorText.enabled = false;
    Time.timeScale = 1f; // Reanudamos el tiempo para que el juego se desarrolle normalmente
        
    // Aplicar la velocidad inicial directamente sin impulso adicional
    pelotaRb.velocity = direccion * velocidadInicial;
}


        public void Game()
    {
        SceneManager.LoadScene(0);
    }
}
