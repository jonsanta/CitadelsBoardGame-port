using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class CartasElegibles : MonoBehaviour
{
	public MainGame _mainGame;
	public int id;
	public int idDescartado;
	public int idDescartado2;
	public GameObject segunda;
	public GameObject tercera;

	public void OnClick()
	{
		if(!PhotonNetwork.IsMasterClient)
		{
			_mainGame.elegirODescartar = false;
			GameObject temp;
			if(_mainGame.observatorio == false)
			{
				object[] datas = new object[] {idDescartado, idDescartado2, 1};
				RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient};
				PhotonNetwork.RaiseEvent(70, datas, raiseEventOptions, SendOptions.SendUnreliable);
			}
			else
			{
				object[] datas = new object[] {idDescartado, idDescartado2, 2};
				RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient};
				PhotonNetwork.RaiseEvent(70, datas, raiseEventOptions, SendOptions.SendUnreliable);
			}
			temp = Instantiate(_mainGame.prefabCartaMano, _mainGame._content);
			temp.GetComponent<RawImage>().texture = _mainGame.mazoCartas[id];
			temp.GetComponent<CartaMano>().id = id;
			temp.GetComponent<CartaMano>()._mainGame = _mainGame;
			_mainGame.cartasEnLaMano.Add(temp);
			object[] datas2 = new object[] {PhotonNetwork.LocalPlayer.NickName, _mainGame.cartasEnLaMano.Count.ToString()};
			PhotonNetwork.RaiseEvent(82, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			object[] datas5 = new object[] {PhotonNetwork.LocalPlayer.NickName+" +1 Carta"};
			PhotonNetwork.RaiseEvent(98, datas5, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			_mainGame.habilidad = true;
			_mainGame.personajesPanel.SetActive(false);
			Destroy(this.gameObject);
			Destroy(segunda);
			Destroy(tercera);
			_mainGame.delay = 1f;
		}
		else
		{
			_mainGame.elegirODescartar = false;
			if(_mainGame.observatorio == true)
			{
				_mainGame._masterGame.cardID.Add(idDescartado);
				_mainGame._masterGame.cardID.Add(idDescartado2);
			}
			else
			{
			_mainGame._masterGame.cardID.Add(idDescartado);
			}
			GameObject temp;
			temp = Instantiate(_mainGame.prefabCartaMano, _mainGame._content);
			temp.GetComponent<RawImage>().texture = _mainGame.mazoCartas[id];
			temp.GetComponent<CartaMano>().id = id;
			temp.GetComponent<CartaMano>()._mainGame = _mainGame;
			_mainGame.cartasEnLaMano.Add(temp);
			object[] datas = new object[] {PhotonNetwork.LocalPlayer.NickName, _mainGame.cartasEnLaMano.Count.ToString()};
			PhotonNetwork.RaiseEvent(82, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			object[] datas5 = new object[] {PhotonNetwork.LocalPlayer.NickName+" +1 Carta"};
			PhotonNetwork.RaiseEvent(98, datas5, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			_mainGame.habilidad = true;
			_mainGame.personajesPanel.SetActive(false);
			Destroy(this.gameObject);
			Destroy(segunda);
			Destroy(tercera);
			_mainGame.delay = 1f;
		}


	}
 
}
