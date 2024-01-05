using UnityEngine;
using UnityEngine.UI;
using System;
public class PanelConfiguracionGoles : MonoBehaviour
{
    public Ball ball; // Referencia al script "Pelotita"
    public void SeleccionarCantidadGoles(int cantidadGoles)
{
    // Configurar la cantidad de goles a través de la instancia de ConfiguracionGoles en Pelotita
    ball.configuracionGoles.ConfigurarGoles(cantidadGoles);

    // Cerrar el panel de configuración
    gameObject.SetActive(false);
}
}