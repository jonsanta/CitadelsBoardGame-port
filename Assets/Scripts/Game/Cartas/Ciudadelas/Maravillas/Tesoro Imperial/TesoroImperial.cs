using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesoroImperial : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
		GetComponent<CartaEnMesa>().puntos = GetComponent<CartaEnMesa>()._mainGame.oro;
    }
}
