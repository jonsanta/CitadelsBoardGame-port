using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviourPunCallbacks
{

	public GameObject masterPrefab;
	private MasterEngine _masterEngine;

	[SerializeField]
	private Text _roomName;

	private AllCanvas _allCanvas;

	public void FirstInitialize(AllCanvas canvases)
	{
		_allCanvas = canvases;
	}


	public void OnClick_CreateRoom()
	{
		if(!PhotonNetwork.IsConnected)
		{
			return;
		}
		RoomOptions options = new RoomOptions();
		options.MaxPlayers = 8;
		PhotonNetwork.JoinOrCreateRoom(_roomName.text,options, TypedLobby.Default);
	}
		
	public override void OnCreatedRoom()
	{
		Debug.Log("Sala creada correctamente");
		_allCanvas.insideRoomCanvas.Show();
		GameObject _masterPrefab = Instantiate(masterPrefab);
		_masterEngine = _masterPrefab.GetComponent<MasterEngine>();
		_masterEngine.playerList.Add(PhotonNetwork.LocalPlayer.NickName);
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		Debug.Log("No se ha podido crear la sala"+ message);
	}
}
