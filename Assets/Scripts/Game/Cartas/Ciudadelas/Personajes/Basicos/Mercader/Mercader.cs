using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Mercader : MonoBehaviour
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
				if(habilidadUI.GetComponent<HabilidadPanel>()._mainGame.delay == 0f)
				{
					habilidadUI.GetComponent<HabilidadPanel>()._mainGame.oro +=1;
					object[] datas5 = new object[] {PhotonNetwork.LocalPlayer.NickName+" +1 ORO"};
					PhotonNetwork.RaiseEvent(98, datas5, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					habilidadUI.GetComponent<HabilidadPanel>()._mainGame.delay = 1f;
					object[] datas = new object[] {PhotonNetwork.LocalPlayer.NickName, habilidadUI.GetComponent<HabilidadPanel>()._mainGame.oro.ToString()};
					PhotonNetwork.RaiseEvent(81, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
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

}
