using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class requestParque : MonoBehaviour
{
	public void Start()
	{
		for(int x = 0; x < 2; x++)
		{
			object[] datas3 = new object[] {PhotonNetwork.LocalPlayer.NickName};
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient};
			PhotonNetwork.RaiseEvent(47, datas3, raiseEventOptions, SendOptions.SendUnreliable);
		}
		Destroy(GetComponent<requesterHerreria>());
	}
}
