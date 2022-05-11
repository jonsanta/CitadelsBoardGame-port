using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscueladeMagia : MonoBehaviour
{

    void Update()
    {
		if(GetComponent<CartaEnMesa>()._mainGame.esMiTurno == true)
		{
			GetComponent<CartaEnMesa>().color = GetComponent<CartaEnMesa>()._mainGame._recaudarImpuestos.color;
		}
		else
		{
			GetComponent<CartaEnMesa>().color = 0;
		}
    }
}
