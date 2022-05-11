using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OnClickDestroyMe : MonoBehaviour
{

	public void Start()
	{

		if(!PhotonNetwork.IsMasterClient)
		{
			Destroy(this.gameObject);
		}
	}


	public void OnClick_Destroy()
	{
		Destroy(this.gameObject);
	}

}
