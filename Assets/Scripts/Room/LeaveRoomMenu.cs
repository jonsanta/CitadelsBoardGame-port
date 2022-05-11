using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class LeaveRoomMenu : MonoBehaviour
{
	byte removePlayerOnList = 2;
	private AllCanvas _allCanvas;

	public void FirstInitialize(AllCanvas canvases)
	{
		_allCanvas = canvases;

	}
  
	public void OnClick_LeaveRoom()
	{
		if(PhotonNetwork.InRoom == true && !PhotonNetwork.IsMasterClient)
		{
				object[] datas = new object[] {PhotonNetwork.LocalPlayer.NickName};
				RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient};
				PhotonNetwork.RaiseEvent(removePlayerOnList, datas, raiseEventOptions, SendOptions.SendUnreliable);
		}

		PhotonNetwork.LeaveRoom(true);
		_allCanvas.insideRoomCanvas.Hide();
		GameObject test;
		test = GameObject.FindGameObjectWithTag("Connection");
		test.gameObject.GetComponent<TestConnection>().sent = 0;
	}


}
