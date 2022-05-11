using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class ElegirOro : MonoBehaviour
{
	public MainGame _mainGame;
	public GameObject segunda;


	public void OnClick()
	{
		if(!PhotonNetwork.IsMasterClient)
		{
			_mainGame.oro += 2;
			object[] datas5 = new object[] {PhotonNetwork.LocalPlayer.NickName+" +2 Oros"};
			PhotonNetwork.RaiseEvent(98, datas5, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			_mainGame.oroOcartas = true;
			//_mainGame.construir = true;
			_mainGame.habilidad = true;
			object[] datas = new object[] {PhotonNetwork.LocalPlayer.NickName, _mainGame.oro.ToString()};
			PhotonNetwork.RaiseEvent(81, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			_mainGame.personajesPanel.SetActive(false);
			Destroy(this.gameObject);
			Destroy(segunda);
			_mainGame.delay = 1;
		}
		else
		{
			_mainGame.oro += 2;
			object[] datas5 = new object[] {PhotonNetwork.LocalPlayer.NickName+" +2 Oros"};
			PhotonNetwork.RaiseEvent(98, datas5, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			_mainGame.oroOcartas = true;
			//_mainGame.construir = true;
			_mainGame.habilidad = true;
			object[] datas = new object[] {PhotonNetwork.LocalPlayer.NickName, _mainGame.oro.ToString()};
			PhotonNetwork.RaiseEvent(81, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			_mainGame.personajesPanel.SetActive(false);
			Destroy(this.gameObject);
			Destroy(segunda);
			_mainGame.delay = 1;
		}

	}

}

