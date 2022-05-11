using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obispo : MonoBehaviour
{
	private GameObject habilidadUI;
	public int color;

	public void Update()
	{
		habilidadUI = GameObject.FindWithTag("habilidad");

		if(habilidadUI != null)
		{
			for(int x = 0; x < habilidadUI.GetComponent<HabilidadPanel>()._mainGame._lector.cartasEnMesa.Count; x++)
			{
				if(habilidadUI.GetComponent<HabilidadPanel>()._mainGame._lector.cartasEnMesa[x].name == "Hospital")
				{
					if(habilidadUI.GetComponent<HabilidadPanel>()._mainGame._lector.cartasEnMesa[x].GetComponent<Hospital>().asesinado == true)
					{
						Destroy(this.gameObject);
						habilidadUI.GetComponent<HabilidadPanelGuerrero>()._mainGame.habilidad = false;
					}
				}

			}

			if (habilidadUI.GetComponent<HabilidadPanel>()._mainGame.habilidad == true)
			{
				habilidadUI.GetComponent<HabilidadPanel>()._mainGame._recaudarImpuestos.color = color;
				habilidadUI.GetComponent<HabilidadPanel>()._mainGame._recaudarImpuestos.boton.SetActive(true);
				habilidadUI.GetComponent<HabilidadPanel>()._mainGame.habilidad = false;
				habilidadUI.GetComponent<HabilidadPanel>()._mainGame.construir = true;
				habilidadUI.GetComponent<HabilidadPanel>()._mainGame.finalizarTurno = true;
				Destroy(this.gameObject);
			}

		}
	}
}
