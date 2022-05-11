using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalaDeMapas : MonoBehaviour
{
	int size1;
	int size2;
	int puntos;

	int once = 0;
    // Start is called before the first frame update
    void Start()
    {
		puntos = GetComponent<CartaEnMesa>().puntos;
		once = 0;
		size1 = 1;
		size2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
		if(size1 != size2)
		{
			GetComponent<CartaEnMesa>().puntos = puntos + GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count;
			once = 0;
		}

		size2 = GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count;

		if(once == 0)
		{
			size1 = GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count;
			once = 1;
		}
    }
}
