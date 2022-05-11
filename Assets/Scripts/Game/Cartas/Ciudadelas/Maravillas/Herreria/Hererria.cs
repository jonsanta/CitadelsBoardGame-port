using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Hererria : MonoBehaviour
{

	public bool usado;

	public void Update()
	{
		GetComponent<Button>().onClick.AddListener(OnClick);

		if(GetComponent<CartaEnMesa>()._mainGame.esMiTurno == false)
		{
			usado = false;
		}

	}
		
	public void OnClick()
	{
		if(GetComponent<CartaEnMesa>()._mainGame.oro >= 3 && usado == false && GetComponent<CartaEnMesa>()._mainGame.finalizarTurno == true && GetComponent<CartaEnMesa>()._mainGame.delay == 0f)
		{
			GetComponent<CartaEnMesa>()._mainGame.oro -= 3;
			if(!PhotonNetwork.IsMasterClient)
			{
				gameObject.AddComponent<requesterHerreria>();
				usado = true;
			}
			else
			{
				for(int x = 0; x < 3; x++)
				{
					GameObject temp;
					temp = Instantiate(GetComponent<CartaEnMesa>()._mainGame.prefabCartaMano, GetComponent<CartaEnMesa>()._mainGame._content);
					temp.GetComponent<RawImage>().texture = GetComponent<CartaEnMesa>()._mainGame.mazoCartas[GetComponent<CartaEnMesa>()._mainGame._masterGame.cardID[GetComponent<CartaEnMesa>()._mainGame._masterGame.cartaRepartida]];
					temp.GetComponent<CartaMano>().id = GetComponent<CartaEnMesa>()._mainGame._masterGame.cardID[GetComponent<CartaEnMesa>()._mainGame._masterGame.cartaRepartida];
					temp.GetComponent<CartaMano>()._mainGame = GetComponent<CartaEnMesa>()._mainGame;
					GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Add(temp);
					GetComponent<CartaEnMesa>()._mainGame._masterGame.cartaRepartida += 1;
				}
				usado = true;
				object[] datas2 = new object[] {PhotonNetwork.LocalPlayer.NickName, GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count.ToString()};
				PhotonNetwork.RaiseEvent(82, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
				object[] datas3 = new object[] {"Usó", "67",""};
				PhotonNetwork.RaiseEvent(76, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
				GetComponent<CartaEnMesa>()._mainGame.delay = 1f;
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

		if(obj.Code == 52)
		{
			object[] datas = (object[])obj.CustomData;

			if((string)datas[0] == PhotonNetwork.LocalPlayer.NickName)
			{
				GameObject temp;
				temp = Instantiate(GetComponent<CartaEnMesa>()._mainGame.prefabCartaMano, GetComponent<CartaEnMesa>()._mainGame._content);
				temp.GetComponent<RawImage>().texture = GetComponent<CartaEnMesa>()._mainGame.mazoCartas[int.Parse((string)datas[1])];
				temp.GetComponent<CartaMano>().id = int.Parse((string)datas[1]);
				temp.GetComponent<CartaMano>()._mainGame = GetComponent<CartaEnMesa>()._mainGame;
				GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Add(temp);
				object[] datas2 = new object[] {PhotonNetwork.LocalPlayer.NickName, GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count.ToString()};
				PhotonNetwork.RaiseEvent(82, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			}

		}
	}
}
