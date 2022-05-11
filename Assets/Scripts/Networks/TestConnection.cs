using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class TestConnection : MonoBehaviourPunCallbacks
{
	byte addPlayerOnList = 1;
	public int sent = 0;

	public GameObject avanzarASala;
    // Start is called before the first frame update
    void Start()
    {
		Debug.Log("Conectando Con el servidor");
		PhotonNetwork.AutomaticallySyncScene = true;
		PhotonNetwork.NickName = MasterManager.gameSettings.nickName;

		PhotonNetwork.GameVersion = MasterManager.gameSettings.gameVersion;
		PhotonNetwork.ConnectUsingSettings();
    }

	void Update()
	{
		if(PhotonNetwork.InRoom == true && !PhotonNetwork.IsMasterClient)
		{
			if(sent == 0)
			{
				object[] datas = new object[] {PhotonNetwork.LocalPlayer.NickName};
				RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient};
				PhotonNetwork.RaiseEvent(addPlayerOnList, datas, raiseEventOptions, SendOptions.SendUnreliable);
				sent = 1;			
			}
		}

	}

	public override void OnConnectedToMaster()
	{
		Debug.Log("Conectado");
		Debug.Log("Nick: "+PhotonNetwork.LocalPlayer.NickName);
		if(!PhotonNetwork.InLobby)
		{
			PhotonNetwork.JoinLobby();
		}
	}

	public override void OnDisconnected(DisconnectCause cause)
	{

		Debug.Log("Desconectado del servidor - error"+ cause.ToString());
	}

	public override void OnJoinedLobby()
	{
		Debug.Log("Conectado a Lobby");
		GameObject acceso = Instantiate(avanzarASala);
	}
		
}

