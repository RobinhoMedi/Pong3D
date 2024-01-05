using UnityEngine;
using UnityEngine.UI;

public class ConfiguracionGoles : MonoBehaviour
{
    public GameObject boton_movimiento_Blue;
    public GameObject boton_movimiento_Red;

    [SerializeField] private GameObject MenuConfig;
    [SerializeField] private GameObject BotonReiniciarBall;
    [SerializeField] private GameObject Puntaje;    
    [Header("Game")]
    [SerializeField] private GameObject Ball;
    [SerializeField] private GameObject PlayerRed;
    [SerializeField] private GameObject PlayerBlue;
    [SerializeField] private GameObject Plane;
    [SerializeField] private GameObject Pared1;
    [SerializeField] private GameObject Pared2;
    [SerializeField] private GameObject Pared3;
    [SerializeField] private GameObject Pared4;
    [SerializeField] private GameObject Default;
    [SerializeField] private GameObject Default1;
    [SerializeField] private GameObject Fondo;

    public Ball pelotaScript; // Cambiado de Pelotita a Ball
    public int golesParaTerminar = 3;

    public void ConfigurarGoles(int goles)
    {
        boton_movimiento_Blue.SetActive(true);
        boton_movimiento_Red.SetActive(true);

        golesParaTerminar = goles;
        pelotaScript.ComenzarJuego();
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        MenuConfig.SetActive(true);
        BotonReiniciarBall.SetActive(true);
        Puntaje.SetActive(true);
        //Game
        Ball.SetActive(true);
        PlayerRed.SetActive(true);
        PlayerBlue.SetActive(true);
        Plane.SetActive(true);
        Pared1.SetActive(true);
        Pared2.SetActive(true);
        Pared3.SetActive(true);
        Pared4.SetActive(true);
        Default.SetActive(true);
        Default1.SetActive(true);
        Fondo.SetActive(true);
    }
}
