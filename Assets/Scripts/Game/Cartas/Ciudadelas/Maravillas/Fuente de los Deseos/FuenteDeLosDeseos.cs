using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuenteDeLosDeseos : MonoBehaviour
{
	int puntos;

	void Start()
	{
		puntos = GetComponent<CartaEnMesa>().puntos;
		GetComponent<CartaEnMesa>().violeta = 0;

	}

    void Update()
    {
		int cuenta = 0;
		for(int x = 0; x < GetComponent<CartaEnMesa>()._mainGame._lector.cartasEnMesa.Count; x++)
		{

			if(GetComponent<CartaEnMesa>()._mainGame._lector.cartasEnMesa[x].GetComponent<CartaEnMesa>().violeta == 1)
			{
				cuenta += 1;

			}

		}
		GetComponent<CartaEnMesa>().puntos = puntos+cuenta;
    }
}
