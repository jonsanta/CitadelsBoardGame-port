using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Ladron : MonoBehaviour
{
	private int number = 2;
	private GameObject habilidadUI;
	public int id;
	private void Start()
	{
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

			habilidadUI.GetComponent<HabilidadPanel>().reference = this.gameObject;

			if (habilidadUI.GetComponent<HabilidadPanel>()._mainGame.habilidad == true && habilidadUI.GetComponent<HabilidadPanel>()._mainGame.delay == 0)
			{
				habilidadUI.GetComponent<HabilidadPanel>().number = number;
				habilidadUI.GetComponent<HabilidadPanel>().usoDeHabilidad();
				habilidadUI.GetComponent<HabilidadPanel>().text.text = "Elige quien quieres Robar";
				if(habilidadUI.GetComponent<HabilidadPanel>().sent == true)
				{
					id = habilidadUI.GetComponent<HabilidadPanel>().tempID;
					object[] datas = new object[] {id.ToString(), PhotonNetwork.LocalPlayer.NickName};
					PhotonNetwork.RaiseEvent(87, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					object[] datas2 = new object[] {"Robado :",id.ToString()};
					PhotonNetwork.RaiseEvent(75, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					habilidadUI.GetComponent<HabilidadPanel>()._mainGame.habilidad = false;
					habilidadUI.GetComponent<HabilidadPanel>()._mainGame.construir = true;
					habilidadUI.GetComponent<HabilidadPanel>()._mainGame.finalizarTurno = true;
					habilidadUI.GetComponent<HabilidadPanel>().sent = false;
					habilidadUI.GetComponent<HabilidadPanel>().tempID = 0;
					habilidadUI.GetComponent<HabilidadPanel>().once = 0;
					habilidadUI.GetComponent<HabilidadPanel>().text.text = "";
					habilidadUI.GetComponent<HabilidadPanel>().habilidadPanel.SetActive(false);
					Destroy(this.gameObject);
					habilidadUI.GetComponent<HabilidadPanel>()._mainGame.delay = 1f;
				}

			}
		}

	}


}
