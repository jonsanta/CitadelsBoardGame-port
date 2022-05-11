using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class ElegirCartas : MonoBehaviour
{
	public MainGame _mainGame;
	public GameObject segunda;


	public void OnClick()
	{
		if(!PhotonNetwork.IsMasterClient)
		{
			_mainGame.oroOcartas = true;
			_mainGame.elegirODescartar = true;

			if(_mainGame.observatorio == true)
			{
				object[] datas = new object[] {"yes"};
				RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient};
				PhotonNetwork.RaiseEvent(50, datas, raiseEventOptions, SendOptions.SendUnreliable);
			}
			else
			{
				object[] datas = new object[] {"no"};
				RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient};
				PhotonNetwork.RaiseEvent(50, datas, raiseEventOptions, SendOptions.SendUnreliable);

			}
			Destroy(this.gameObject);
			Destroy(segunda);
		}
		else
		{
			_mainGame.oroOcartas = true;
			_mainGame.elegirODescartar = true;
			_mainGame._masterGame.EnviarCartasDeTurno();
			Destroy(this.gameObject);
			Destroy(segunda);
		}

	}
}
