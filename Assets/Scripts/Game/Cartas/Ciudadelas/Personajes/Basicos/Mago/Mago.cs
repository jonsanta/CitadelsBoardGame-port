using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Mago : MonoBehaviour
{
	public GameObject sender;
	private GameObject senderClone;
	public GameObject requester;
	private GameObject requesterClone;

	public bool pedirCartaEnCliente = false;
	private int once2 = 0;
	private int once = 0;
	private int cantidadDeCartasEnLaMano;
	public GameObject habilidadUI;
	public int id;
	private int cartasRecibidas = 0;
	private int cartasRecibidas2 = 0;
	private int cartasRecibidas3 = 0;

	private void Start()
	{
		pedirCartaEnCliente = false;
		cartasRecibidas = 0;
		cartasRecibidas2 = 0;
		cartasRecibidas3 = 0;
		once = 0;
		once2 = 0;
		cantidadDeCartasEnLaMano = 0;

		senderClone = Instantiate(sender);
		senderClone.GetComponent<SenderMago>()._mago = this;

		requesterClone = Instantiate(requester);
		requesterClone.GetComponent<RequesterMago>()._mago = this;
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

			if(habilidadUI.GetComponent<HabilidadPanel>().saveCards == true)
			{
				for(int x = 0; x < habilidadUI.GetComponent<HabilidadPanel>()._mainGame.cartasEnLaMano.Count; x++)
				{
					habilidadUI.GetComponent<HabilidadPanel>().cartasEnMano.Add(habilidadUI.GetComponent<HabilidadPanel>()._mainGame.cartasEnLaMano[x]);
				}
				habilidadUI.GetComponent<HabilidadPanel>()._mainGame.cartasEnLaMano.Clear();

			}

			if (habilidadUI.GetComponent<HabilidadPanel>()._mainGame.habilidad == true)
			{
				habilidadUI.GetComponent<HabilidadPanel>().reference = this.gameObject;
				habilidadUI.GetComponent<HabilidadPanel>().habilidadBoton.SetActive(true);
				habilidadUI.GetComponent<HabilidadPanel>()._mainGame.construir = true;
				habilidadUI.GetComponent<HabilidadPanel>()._mainGame.finalizarTurno = true;
			}

		}

		if(habilidadUI.GetComponent<HabilidadPanel>().click == true)
		{
			if(once == 0)
			{
				cantidadDeCartasEnLaMano = habilidadUI.GetComponent<HabilidadPanel>()._mainGame.cartasEnLaMano.Count;
				once = 1;
			}

			habilidadUI.GetComponent<HabilidadPanel>().usoDeHabilidad2();

			if(habilidadUI.GetComponent<HabilidadPanel>().sent == true)
			{
				name = habilidadUI.GetComponent<HabilidadPanel>().name;

				if(name == "Mazo")
				{
					if(once2 == 0)
					{
						object[] datas2 = new object[] {"Cambia cartas con [Mazo]"};
						PhotonNetwork.RaiseEvent(98, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
						once2 =1;
					}

					if(PhotonNetwork.IsMasterClient)
					{
						for(int x = 0; x < habilidadUI.GetComponent<HabilidadPanel>().cartasEnManoID.Count; x++)
						{
							habilidadUI.GetComponent<HabilidadPanel>()._mainGame._masterGame.cardID.Add(habilidadUI.GetComponent<HabilidadPanel>().cartasEnManoID[x]);
						}

						for(int x = 0; x < habilidadUI.GetComponent<HabilidadPanel>().cartasEnManoID.Count; x++)
						{
							GameObject temp2;
							temp2 = Instantiate(habilidadUI.GetComponent<HabilidadPanel>()._mainGame.prefabCartaMano, habilidadUI.GetComponent<HabilidadPanel>()._mainGame._content);
							temp2.GetComponent<RawImage>().texture = habilidadUI.GetComponent<HabilidadPanel>()._mainGame.mazoCartas[habilidadUI.GetComponent<HabilidadPanel>()._mainGame._masterGame.cardID[habilidadUI.GetComponent<HabilidadPanel>()._mainGame._masterGame.cartaRepartida]];
							temp2.GetComponent<CartaMano>().id = habilidadUI.GetComponent<HabilidadPanel>()._mainGame._masterGame.cardID[habilidadUI.GetComponent<HabilidadPanel>()._mainGame._masterGame.cartaRepartida];
							temp2.GetComponent<CartaMano>()._mainGame = habilidadUI.GetComponent<HabilidadPanel>()._mainGame;
							habilidadUI.GetComponent<HabilidadPanel>()._mainGame.cartasEnLaMano.Add(temp2);
							habilidadUI.GetComponent<HabilidadPanel>()._mainGame._masterGame.cartaRepartida +=1;
						}

						habilidadUI.GetComponent<HabilidadPanel>().name = "";
						habilidadUI.GetComponent<HabilidadPanel>()._mainGame.habilidad = false;
						habilidadUI.GetComponent<HabilidadPanel>().sent = false;
						habilidadUI.GetComponent<HabilidadPanel>().once = 0;
						habilidadUI.GetComponent<HabilidadPanel>().habilidadPanel2.SetActive(false);
						habilidadUI.GetComponent<HabilidadPanel>().habilidadBoton.SetActive(false);
						habilidadUI.GetComponent<HabilidadPanel>().click = false;
						habilidadUI.GetComponent<HabilidadPanel>().cartasEnMano.Clear();
						habilidadUI.GetComponent<HabilidadPanel>().cartasEnManoID.Clear();
						habilidadUI.GetComponent<HabilidadPanel>().botonDescarteCancelar.SetActive(false);
						habilidadUI.GetComponent<HabilidadPanel>().botonDescarte.SetActive(false);
						habilidadUI.GetComponent<HabilidadPanel>().text2.text = "";
						Destroy(senderClone);
						Destroy(requesterClone);
						Destroy(this.gameObject);
					}
					else
					{
						pedirCartaEnCliente = true;
						habilidadUI.GetComponent<HabilidadPanel>()._mainGame.habilidad = false;
					}
				}
				else
				{

					if(cantidadDeCartasEnLaMano == 0)
					{
						object[] datas3 = new object[] {PhotonNetwork.LocalPlayer.NickName, name};
						PhotonNetwork.RaiseEvent(7, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					}

					if(cantidadDeCartasEnLaMano > 0)
					{
						for(int x = 0; x < habilidadUI.GetComponent<HabilidadPanel>()._mainGame.cartasEnLaMano.Count; x++)
						{
							object[] datas3 = new object[] {PhotonNetwork.LocalPlayer.NickName, habilidadUI.GetComponent<HabilidadPanel>()._mainGame.cartasEnLaMano[x].GetComponent<CartaMano>().id.ToString(), habilidadUI.GetComponent<HabilidadPanel>()._mainGame.cartasEnLaMano.Count.ToString(), name};
							PhotonNetwork.RaiseEvent(88, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
							Destroy(habilidadUI.GetComponent<HabilidadPanel>()._mainGame.cartasEnLaMano[x]);
						}
					}
					habilidadUI.GetComponent<HabilidadPanel>()._mainGame.cartasEnLaMano.Clear();
					if(once2 == 0)
					{
						object[] datas5 = new object[] {"Cambia cartas con ["+name+"]"};
						PhotonNetwork.RaiseEvent(98, datas5, RaiseEventOptions.Default, SendOptions.SendUnreliable);
						once2 = 1;
					}
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
				cartasRecibidas2 +=1;
			}

			if(habilidadUI.GetComponent<HabilidadPanel>().cartasEnManoID.Count == cartasRecibidas2)
			{
				habilidadUI.GetComponent<HabilidadPanel>().name = "";
				habilidadUI.GetComponent<HabilidadPanel>()._mainGame.habilidad = false;
				habilidadUI.GetComponent<HabilidadPanel>().sent = false;
				habilidadUI.GetComponent<HabilidadPanel>().once = 0;
				habilidadUI.GetComponent<HabilidadPanel>().habilidadPanel2.SetActive(false);
				habilidadUI.GetComponent<HabilidadPanel>().habilidadBoton.SetActive(false);
				habilidadUI.GetComponent<HabilidadPanel>().click = false;
				habilidadUI.GetComponent<HabilidadPanel>().cartasEnMano.Clear();
				habilidadUI.GetComponent<HabilidadPanel>().cartasEnManoID.Clear();
				habilidadUI.GetComponent<HabilidadPanel>().botonDescarteCancelar.SetActive(false);
				habilidadUI.GetComponent<HabilidadPanel>().botonDescarte.SetActive(false);
				habilidadUI.GetComponent<HabilidadPanel>().text2.text = "";
				Destroy(this.gameObject);
				Destroy(senderClone);
				Destroy(requesterClone);
			}

		}

		if(obj.Code == 42)
		{
			object[] datas = (object[])obj.CustomData;

			if((string)datas[0] == PhotonNetwork.LocalPlayer.NickName)
			{
				cartasRecibidas3 = int.Parse((string)datas[2]);
				GameObject temp2;
				temp2 = Instantiate(habilidadUI.GetComponent<HabilidadPanel>()._mainGame.prefabCartaMano, habilidadUI.GetComponent<HabilidadPanel>()._mainGame._content);
				temp2.GetComponent<RawImage>().texture = habilidadUI.GetComponent<HabilidadPanel>()._mainGame.mazoCartas[int.Parse((string)datas[1])];
				temp2.GetComponent<CartaMano>().id = int.Parse((string)datas[1]);
				temp2.GetComponent<CartaMano>()._mainGame = habilidadUI.GetComponent<HabilidadPanel>()._mainGame;
				habilidadUI.GetComponent<HabilidadPanel>()._mainGame.cartasEnLaMano.Add(temp2);
				cartasRecibidas2 +=1;
			}

			if( cartasRecibidas3 == cartasRecibidas2)
			{
				object[] datas10 = new object[] {PhotonNetwork.LocalPlayer.NickName, habilidadUI.GetComponent<HabilidadPanel>()._mainGame.cartasEnLaMano.Count.ToString()};
				PhotonNetwork.RaiseEvent(82, datas10, RaiseEventOptions.Default, SendOptions.SendUnreliable);
				habilidadUI.GetComponent<HabilidadPanel>().name = "";
				habilidadUI.GetComponent<HabilidadPanel>()._mainGame.habilidad = false;
				habilidadUI.GetComponent<HabilidadPanel>().sent = false;
				habilidadUI.GetComponent<HabilidadPanel>().once = 0;
				habilidadUI.GetComponent<HabilidadPanel>().habilidadPanel2.SetActive(false);
				habilidadUI.GetComponent<HabilidadPanel>().habilidadBoton.SetActive(false);
				habilidadUI.GetComponent<HabilidadPanel>().click = false;
				Destroy(this.gameObject);
				Destroy(senderClone);
				Destroy(requesterClone);
			}
		}
	}
}
