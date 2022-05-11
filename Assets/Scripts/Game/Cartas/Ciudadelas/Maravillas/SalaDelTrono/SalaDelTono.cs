using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SalaDelTono : MonoBehaviour
{

	public string name1;
	public string name2;
	int once = 0;
    // Start is called before the first frame update
    void Start()
    {
		once = 0;   
    }

    // Update is called once per frame
    void Update()
    {
		name2 = GetComponent<CartaEnMesa>()._mainGame.corona;
		if(once == 0)
		{
			name1 = GetComponent<CartaEnMesa>()._mainGame.corona;
			once = 1;
		}

		if(name1 != name2)
		{
			GetComponent<CartaEnMesa>()._mainGame.oro +=1;
			object[] datas = new object[] {PhotonNetwork.LocalPlayer.NickName, GetComponent<CartaEnMesa>()._mainGame.oro.ToString()};
			PhotonNetwork.RaiseEvent(81, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			object[] datas3 = new object[] {PhotonNetwork.LocalPlayer.NickName, "78",""};
			PhotonNetwork.RaiseEvent(76, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			once = 0;

		}

        
    }
}
