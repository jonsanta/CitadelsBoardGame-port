using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Parque : MonoBehaviour
{
	public bool request;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(request == true)
		{
			if(GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count == 0)
			{
				if(!PhotonNetwork.IsMasterClient)
				{
					gameObject.AddComponent<requestParque>();
					request = false;
				}

				else
				{
					for(int x = 0; x < 2; x++)
					{
						Debug.Log("creando CARTA");
						GameObject temp;
						temp = Instantiate(GetComponent<CartaEnMesa>()._mainGame.prefabCartaMano, GetComponent<CartaEnMesa>()._mainGame._content);
						temp.GetComponent<RawImage>().texture = GetComponent<CartaEnMesa>()._mainGame.mazoCartas[GetComponent<CartaEnMesa>()._mainGame._masterGame.cardID[GetComponent<CartaEnMesa>()._mainGame._masterGame.cartaRepartida]];
						temp.GetComponent<CartaMano>().id = GetComponent<CartaEnMesa>()._mainGame._masterGame.cardID[GetComponent<CartaEnMesa>()._mainGame._masterGame.cartaRepartida];
						temp.GetComponent<CartaMano>()._mainGame = GetComponent<CartaEnMesa>()._mainGame;
						GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Add(temp);
						GetComponent<CartaEnMesa>()._mainGame._masterGame.cartaRepartida += 1;
					}
					object[] datas2 = new object[] {PhotonNetwork.LocalPlayer.NickName, GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count.ToString()};
					PhotonNetwork.RaiseEvent(82, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					object[] datas3 = new object[] {"+2 Cartas", "73",""};
					PhotonNetwork.RaiseEvent(76, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					request = false;
				}
			}
			request = false;
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

		if(obj.Code == 49)
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
