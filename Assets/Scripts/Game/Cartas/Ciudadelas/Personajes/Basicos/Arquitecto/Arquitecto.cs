using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Arquitecto : MonoBehaviour
{
	public int once;
	public GameObject requester;
	private GameObject requesterClone;

	public bool pedirCartasCliente = false;
	private int cartasRecibidas;
	private GameObject habilidadUI;

	public void Start()
	{
		once = 0;
		pedirCartasCliente = false;
		requesterClone = Instantiate(requester);
		requesterClone.GetComponent<RequesterArquitecto>()._arquitecto = this;

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


			if (habilidadUI.GetComponent<HabilidadPanel>()._mainGame.habilidad == true)
			{
				habilidadUI.GetComponent<HabilidadPanel>()._mainGame._lector.construccionesPermitidas = 3;
				if(PhotonNetwork.IsMasterClient)
				{
					for(int x = 0; x < 2; x++)
					{
						GameObject temp2;
						temp2 = Instantiate(habilidadUI.GetComponent<HabilidadPanel>()._mainGame.prefabCartaMano, habilidadUI.GetComponent<HabilidadPanel>()._mainGame._content);
						temp2.GetComponent<RawImage>().texture = habilidadUI.GetComponent<HabilidadPanel>()._mainGame.mazoCartas[habilidadUI.GetComponent<HabilidadPanel>()._mainGame._masterGame.cardID[habilidadUI.GetComponent<HabilidadPanel>()._mainGame._masterGame.cartaRepartida]];
						temp2.GetComponent<CartaMano>().id = habilidadUI.GetComponent<HabilidadPanel>()._mainGame._masterGame.cardID[habilidadUI.GetComponent<HabilidadPanel>()._mainGame._masterGame.cartaRepartida];
						temp2.GetComponent<CartaMano>()._mainGame = habilidadUI.GetComponent<HabilidadPanel>()._mainGame;
						habilidadUI.GetComponent<HabilidadPanel>()._mainGame.cartasEnLaMano.Add(temp2);
						habilidadUI.GetComponent<HabilidadPanel>()._mainGame._masterGame.cartaRepartida +=1;
					}
					object[] datas = new object[] {PhotonNetwork.LocalPlayer.NickName, habilidadUI.GetComponent<HabilidadPanel>()._mainGame.cartasEnLaMano.Count.ToString()};
					PhotonNetwork.RaiseEvent(82, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					if(once == 0)
					{
						object[] datas5 = new object[] {PhotonNetwork.LocalPlayer.NickName+" +2 CARTAS"};
						PhotonNetwork.RaiseEvent(98, datas5, RaiseEventOptions.Default, SendOptions.SendUnreliable);
						once = 1;
						habilidadUI.GetComponent<HabilidadPanel>()._mainGame.delay = 1f;
					}
					habilidadUI.GetComponent<HabilidadPanel>()._mainGame.habilidad = false;
					habilidadUI.GetComponent<HabilidadPanel>()._mainGame.construir = true;
					habilidadUI.GetComponent<HabilidadPanel>()._mainGame.finalizarTurno = true;
					Destroy(requesterClone);
					Destroy(this.gameObject);
				}
				else if(!PhotonNetwork.IsMasterClient)
				{
					pedirCartasCliente = true;
					habilidadUI.GetComponent<HabilidadPanel>()._mainGame.habilidad = false;
					if(once == 0)
					{
						object[] datas5 = new object[] {PhotonNetwork.LocalPlayer.NickName+" +2 CARTAS"};
						PhotonNetwork.RaiseEvent(98, datas5, RaiseEventOptions.Default, SendOptions.SendUnreliable);
						once = 1;
						habilidadUI.GetComponent<HabilidadPanel>()._mainGame.delay = 1f;
					}
					habilidadUI.GetComponent<HabilidadPanel>()._mainGame.construir = true;
					habilidadUI.GetComponent<HabilidadPanel>()._mainGame.finalizarTurno = true;
				}
			}

		}
	}

	private void OnEnable()
	{
		PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
	}

	private void OnDisable()
	{
		PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
	}


	private void NetworkingClient_EventReceived(EventData obj)
	{
		if(obj.Code == 39)
		{
			object[] datas = (object[])obj.CustomData;

			if((string)datas[0] == PhotonNetwork.LocalPlayer.NickName)
			{
				GameObject temp2;
				temp2 = Instantiate(habilidadUI.GetComponent<HabilidadPanel>()._mainGame.prefabCartaMano, habilidadUI.GetComponent<HabilidadPanel>()._mainGame._content);
				temp2.GetComponent<RawImage>().texture = habilidadUI.GetComponent<HabilidadPanel>()._mainGame.mazoCartas[int.Parse((string)datas[1])];
				temp2.GetComponent<CartaMano>().id = int.Parse((string)datas[1]);
				temp2.GetComponent<CartaMano>()._mainGame = habilidadUI.GetComponent<HabilidadPanel>()._mainGame;
				habilidadUI.GetComponent<HabilidadPanel>()._mainGame.cartasEnLaMano.Add(temp2);
				cartasRecibidas +=1;
			}

			if(cartasRecibidas == 2)
			{
				object[] datas3 = new object[] {PhotonNetwork.LocalPlayer.NickName, habilidadUI.GetComponent<HabilidadPanel>()._mainGame.cartasEnLaMano.Count.ToString()};
				PhotonNetwork.RaiseEvent(82, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
				habilidadUI.GetComponent<HabilidadPanel>()._mainGame.habilidad = false;
				habilidadUI.GetComponent<HabilidadPanel>()._mainGame.construir = true;
				habilidadUI.GetComponent<HabilidadPanel>()._mainGame.finalizarTurno = true;
				Destroy(requesterClone);
				Destroy(this.gameObject);
			}
		}

	}

}
