using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoBall : MonoBehaviour
{
    public AudioSource Ball_Sound;


    private void OnCollisionEnter(Collision collision)
    {
        Ball_Sound.Play();
    }
}
