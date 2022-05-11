using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SendInterfaceInfo : MonoBehaviour
{
	public GameObject panelCementerio;
	public GameObject cementerioPrefab;
	public Transform cementerioContent;
	public List<GameObject> cementerioLista = new List<GameObject>();


	public bool startSending = false;
	public int x = 0;
	public int once = 0;
	public MainGame _mainGame;

	public List<string> playerNames = new List<string>();

	public List<GameObject> mesas = new List<GameObject>();

	public void Start()
	{
		if(_mainGame != null)
		{
			_mainGame._sendInterfaceInfo = this;
		}

		once = 0;
		x = 0;
		startSending = false;
	}

	void Update()
	{
		if(once == 0 && startSending == true)
		{
			EnviarInfoALaMesa();
		}
	}

	private void EnviarInfoALaMesa()
	{
		if(x == playerNames.Count)
		{
			once = 1;
		}

		playerNames.Remove(PhotonNetwork.LocalPlayer.NickName);

		if(x < playerNames.Count)
		{
			mesas[x].GetComponent<PlayersInterface>().playerName = playerNames[x];
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
		if(obj.Code == 80)
		{

			object[] datas = (object[])obj.CustomData;
			playerNames.Add((string)datas[0]);
			startSending = true;
		}
	}
}
