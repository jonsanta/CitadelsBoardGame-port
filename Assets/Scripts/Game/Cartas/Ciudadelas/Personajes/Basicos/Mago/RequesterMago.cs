using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class RequesterMago : MonoBehaviour
{
    // Start is called before the first frame update

	public Mago _mago;

    void Update()
    {
		if(_mago.pedirCartaEnCliente ==  true)
		{
			for(int x = 0; x < _mago.habilidadUI.GetComponent<HabilidadPanel>().cartasEnManoID.Count; x++)
			{
				object[] datas3 = new object[] {PhotonNetwork.LocalPlayer.NickName};
				RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient};
				PhotonNetwork.RaiseEvent(63, datas3, raiseEventOptions, SendOptions.SendUnreliable);

			}
			Destroy(this.gameObject);
		}

		if(_mago == null)
		{
			Destroy(this.gameObject);
		}
    }
}
