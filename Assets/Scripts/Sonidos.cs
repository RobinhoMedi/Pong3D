using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Sonidos : MonoBehaviour
{
    [Header("Variables")]
    public AudioSource Boton;

    public void Click()
    {
        Boton.Play();
    }
}
