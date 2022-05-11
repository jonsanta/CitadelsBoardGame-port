using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SenderMago : MonoBehaviour
{

	public Mago _mago;

	void Update()
    {
		if(_mago.pedirCartaEnCliente ==  true)
		{
			for(int x = 0; x < _mago.habilidadUI.GetComponent<HabilidadPanel>().cartasEnManoID.Count; x++)
			{
				object[] datas = new object[] {_mago.habilidadUI.GetComponent<HabilidadPanel>().cartasEnManoID[x], 1000, 1};
				RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient};
				PhotonNetwork.RaiseEvent(70, datas, raiseEventOptions, SendOptions.SendUnreliable);
			}
			Destroy(this.gameObject);
		}

		if(_mago == null)
		{
			Destroy(this.gameObject);
		}
    }
}
