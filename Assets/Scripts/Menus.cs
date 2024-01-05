using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menus : MonoBehaviour
{
    public GameObject boton_movimiento_Blue;
    public GameObject boton_movimiento_Red;
    [Header("Jugar")]
    [SerializeField] private GameObject pelotaReset;
    [SerializeField] private GameObject volverAConfiguracion;
    [SerializeField] private GameObject canvasPuntaje;
    [SerializeField] private GameObject OpcionesMenu;
    [SerializeField] private GameObject PanelMenu;
    [SerializeField] private GameObject Desarrollador;
    [SerializeField] private int NumeroEscena;
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
    void Awake()
    {
        canvasPuntaje.SetActive(false);
        pelotaReset.SetActive(false);
        volverAConfiguracion.SetActive(false);
    }
    public void Game()
    {
        SceneManager.LoadScene(0);
    }

    public void Jugar()
    {
        PanelMenu.SetActive(false);
        canvasPuntaje.SetActive(false);
        pelotaReset.SetActive(false);
        volverAConfiguracion.SetActive(false);
        OpcionesMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GolesConfig()
    {
        boton_movimiento_Blue.SetActive(true);
        boton_movimiento_Red.SetActive(true);
        canvasPuntaje.SetActive(true);
        pelotaReset.SetActive(true);
        volverAConfiguracion.SetActive(true);
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
    public void atras()
    {
        OpcionesMenu.SetActive(false);
        PanelMenu.SetActive(true);
    }
    public void salir()
    {
        Application.Quit();
        Debug.Log("Saliendo...");
    }


}
