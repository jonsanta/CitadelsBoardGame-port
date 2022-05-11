using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : MonoBehaviour
{

	public bool asesinado = false;
    // Start is called before the first frame update
    void Start()
    {
		asesinado = false;

    }

    // Update is called once per frame
    void Update()
    {
		if(GetComponent<CartaEnMesa>()._mainGame.asesinado == true && GetComponent<CartaEnMesa>()._mainGame.habilidad == false)
		{
			asesinado = true;
			GetComponent<CartaEnMesa>()._mainGame.asesinado = false;
		}

		if(GetComponent<CartaEnMesa>()._mainGame.habilidad == true)
		{
			if(asesinado == true)
			{
				GetComponent<CartaEnMesa>()._mainGame.asesinado = true;
			}
		}
    }
}
