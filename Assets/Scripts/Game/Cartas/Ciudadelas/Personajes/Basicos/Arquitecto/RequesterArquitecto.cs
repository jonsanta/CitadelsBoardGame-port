using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class RequesterArquitecto : MonoBehaviour
{
	public Arquitecto _arquitecto;

    // Update is called once per frame
    void Update()
    {
		if(_arquitecto.pedirCartasCliente == true)
		{
			for(int x = 0; x < 2; x++)
			{
				object[] datas3 = new object[] {PhotonNetwork.LocalPlayer.NickName};
				RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient};
				PhotonNetwork.RaiseEvent(63, datas3, raiseEventOptions, SendOptions.SendUnreliable);
			} 
			_arquitecto.pedirCartasCliente = false;
			Destroy(this.gameObject);
		}

		if(_arquitecto == null)
		{
			Destroy(this.gameObject);
		}
    }
}
