using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SeleccionaPersonaje : MonoBehaviour
{
	public int id;
	public MainGame _mainGame;

	void Start()
	{
		object[] datas2 = new object[] {PhotonNetwork.LocalPlayer.NickName, "yes"};
		PhotonNetwork.RaiseEvent(29, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);

	}

	public void OnClick_Send()
	{
		if(!PhotonNetwork.IsMasterClient)
		{
			object[] datas = new object[] {id};
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient};
			PhotonNetwork.RaiseEvent(5, datas, raiseEventOptions, SendOptions.SendUnreliable);
			_mainGame.GetComponent<MainGame>().RemoveAll();
			_mainGame.GetComponent<MainGame>().personajeLocal = id;
			_mainGame.GetComponent<MainGame>().pesonajesEnLaMesa[0].GetComponent<Image>().sprite = _mainGame.mazoPersonajes[id];
			GameObject temp;
			temp = Instantiate(_mainGame.GetComponent<MainGame>().habilidadesPrefab[id],_mainGame.GetComponent<MainGame>().pesonajesEnLaMesa[0].transform);
			_mainGame.GetComponent<MainGame>().personajeEscogidoHabilidad = temp;
			_mainGame.personajesPanel.SetActive(false);
			object[] datas2 = new object[] {PhotonNetwork.LocalPlayer.NickName, "no"};
			PhotonNetwork.RaiseEvent(29, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			_mainGame._log.RestartAllUI();
		}else
		{
			_mainGame.GetComponent<MainGame>().personajeLocal = id;
			_mainGame.GetComponent<MainGame>().pesonajesEnLaMesa[0].GetComponent<Image>().sprite = _mainGame.mazoPersonajes[id];
			GameObject temp;
			temp = Instantiate(_mainGame.GetComponent<MainGame>().habilidadesPrefab[id],_mainGame.GetComponent<MainGame>().pesonajesEnLaMesa[0].transform);
			_mainGame.GetComponent<MainGame>().personajeEscogidoHabilidad = temp;
			_mainGame.GetComponent<MainGame>()._masterGame.personajeRecibidotemp.Add(id);
			_mainGame.GetComponent<MainGame>()._masterGame.yaHeEscogidoPersonaje = false;
			_mainGame.GetComponent<MainGame>()._masterGame.nj += 1;
			_mainGame.GetComponent<MainGame>().RemoveAll();
			_mainGame.personajesPanel.SetActive(false);
			object[] datas2 = new object[] {PhotonNetwork.LocalPlayer.NickName, "no"};
			PhotonNetwork.RaiseEvent(29, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			_mainGame._log.RestartAllUI();
		}

	}


}
