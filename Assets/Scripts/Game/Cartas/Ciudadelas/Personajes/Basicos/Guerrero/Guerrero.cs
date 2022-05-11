using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guerrero : MonoBehaviour
{
	private GameObject habilidadUI;
	public int color;
	int once = 0;

	void Start()
	{
		once = 0;
	}

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

			if (habilidadUI.GetComponent<HabilidadPanelGuerrero>()._mainGame.habilidad == true)
			{
				habilidadUI.GetComponent<HabilidadPanelGuerrero>()._mainGame._recaudarImpuestos.color = color;
				if(once == 0)
				{
					habilidadUI.GetComponent<HabilidadPanelGuerrero>()._mainGame._recaudarImpuestos.boton.SetActive(true);
					once = 1;
				}
				habilidadUI.GetComponent<HabilidadPanelGuerrero>()._mainGame.construir = true;
				habilidadUI.GetComponent<HabilidadPanelGuerrero>()._mainGame.finalizarTurno = true;
				habilidadUI.GetComponent<HabilidadPanelGuerrero>().habilidadBoton.SetActive(true);

				if(habilidadUI.GetComponent<HabilidadPanelGuerrero>().click == true)
				{
					habilidadUI.GetComponent<HabilidadPanelGuerrero>().usoDeHabilidad();
				}
				if(habilidadUI.GetComponent<HabilidadPanelGuerrero>().terminarHabilidad == true)
				{
					habilidadUI.GetComponent<HabilidadPanelGuerrero>().jugadores.Clear();
					habilidadUI.GetComponent<HabilidadPanelGuerrero>().cardsToRemove.Clear();
					habilidadUI.GetComponent<HabilidadPanelGuerrero>().once = 0;
					habilidadUI.GetComponent<HabilidadPanelGuerrero>().click = false;
					habilidadUI.GetComponent<HabilidadPanelGuerrero>().habilidadPanel.SetActive(false);
					habilidadUI.GetComponent<HabilidadPanelGuerrero>().terminarHabilidad = false;
					habilidadUI.GetComponent<HabilidadPanelGuerrero>().habilidadBoton.SetActive(false);
					Destroy(this.gameObject);

				}
			}

		}
	}
}
