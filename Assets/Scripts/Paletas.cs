using UnityEngine;
using UnityEngine.UI;

public class Paletas : MonoBehaviour
{
    public float velocidad = 5f;
    private float movimientoHorizontal = 0f;
    public float limiteIzquierdo = -10f; // Define el límite izquierdo
    public float limiteDerecho = 10f;    // Define el límite derecho

    // Agrega una referencia al botón en el Inspector
    public Button botonDerecha;
    public Button botonIzquierda;

    void Start()
    {
        // Agrega los listeners para los eventos de clic
        botonDerecha.onClick.AddListener(MoverDerecha);
        botonIzquierda.onClick.AddListener(MoverIzquierda);
    }

    void Update()
    {
        MoverPersonaje(movimientoHorizontal);
    }

    public void MoverDerecha()
    {
        movimientoHorizontal = 1f;
    }

    public void MoverIzquierda()
    {
        movimientoHorizontal = -1f;
    }

    public void DetenerMovimiento()
    {
        movimientoHorizontal = 0f;
    }

    private void MoverPersonaje(float movimientoHorizontal)
    {
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0f, 0f) * velocidad * Time.deltaTime;
        Vector3 nuevaPosicion = transform.position + movimiento;
        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, limiteIzquierdo, limiteDerecho); // Limita el movimiento al rango permitido
        transform.position = nuevaPosicion;
    }
}
