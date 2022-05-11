using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class MasterEngine : MonoBehaviour
{
	byte addPlayerOnList = 1;
	byte removePlayerOnList = 2;

	public int distritosFin = 5;
	int distritosFinTemp = 0;
	Text text;
	public bool noChanges = false;


	[SerializeField]
	public List<string> playerList = new List<string>();
	public int playerListSize;

	void Awake()
	{
		DontDestroyOnLoad (this);
	}
	void Update()
	{
		if(noChanges == false)
		{
			if(GameObject.FindWithTag("Distritos"))
			{
				text = GameObject.FindWithTag("Distritos").GetComponent<Text>();
			}

			distritosFinTemp = int.Parse(text.text);

			if(distritosFinTemp != 0)
			{
				if(distritosFinTemp >= 3)
				{
				distritosFin = distritosFinTemp;
				}
			}
		}


		if(GetComponent<MasterGame>().playerList.Count == 0)
		{
			GetComponent<MasterGame>().playerList = playerList;
		}


		if(!PhotonNetwork.IsMasterClient)
		{
			Destroy(this.gameObject);
		}
		playerListSize = playerList.Count;
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
		if(obj.Code == addPlayerOnList)
		{
			object[] datas = (object[])obj.CustomData;
			playerList.Add((string)datas[0]);
		}

		if(obj.Code == removePlayerOnList)
		{
			object[] datas = (object[])obj.CustomData;
			playerList.Remove((string)datas[0]);
		}
	}
}