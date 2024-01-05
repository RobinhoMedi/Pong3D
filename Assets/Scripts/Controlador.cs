using UnityEngine;

public class Controlador : MonoBehaviour
{
    public Paletas paleta1;
    public Paletas paleta2;

    // Llamar estos métodos para mover las paletas
    public void MoverPaleta1Derecha()
    {
        paleta1.MoverDerecha();
    }

    public void MoverPaleta1Izquierda()
    {
        paleta1.MoverIzquierda();
    }

    public void MoverPaleta2Derecha()
    {
        paleta2.MoverDerecha();
    }

    public void MoverPaleta2Izquierda()
    {
        paleta2.MoverIzquierda();
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput < 0)
        {
            MoverPaleta1Izquierda();
        }
        else if (horizontalInput > 0)
        {
            MoverPaleta1Derecha();
        }

        float horizontalInput2 = Input.GetAxis("Horizontal2");

        if (horizontalInput2 < 0)
        {
            MoverPaleta2Izquierda();
        }
        else if (horizontalInput2 > 0)
        {
            MoverPaleta2Derecha();
        }
    }
}
