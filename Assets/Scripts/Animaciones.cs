using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animaciones : MonoBehaviour
{
      
    private Animator buttonAnimator;

    private void Start()
    {
        buttonAnimator = GetComponent<Animator>();
    }
}