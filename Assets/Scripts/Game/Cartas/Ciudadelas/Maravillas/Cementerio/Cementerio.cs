using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Cementerio : MonoBehaviour
{
	public int id;
	public string name;
	public SendInterfaceInfo _sendInterfaceInfo;
	float contador = 0f;

	void Start()
	{
		contador = 0f;

	}

	void Update()
	{
		contador += Time.deltaTime;

		if(contador > 20f)
		{
			for(int x = 0; x < _sendInterfaceInfo.cementerioLista.Count; x++)
			{
				Destroy(_sendInterfaceInfo.cementerioLista[x]);
			}
			_sendInterfaceInfo.cementerioLista.Clear();
			_sendInterfaceInfo.panelCementerio.SetActive(false);
			Destroy(this.gameObject);

		}

	}


	public void OnClick()
	{
		if(name == "Cancelar")
		{
			for(int x = 0; x < _sendInterfaceInfo.cementerioLista.Count; x++)
			{
				Destroy(_sendInterfaceInfo.cementerioLista[x]);
			}
			_sendInterfaceInfo.cementerioLista.Clear();
			_sendInterfaceInfo.panelCementerio.SetActive(false);
			Destroy(this.gameObject);

		}
		else
		{
			_sendInterfaceInfo._mainGame.oro -= 1;
			GameObject temp;
			temp = Instantiate(_sendInterfaceInfo._mainGame.prefabCartaMano, _sendInterfaceInfo._mainGame._content);
			temp.GetComponent<RawImage>().texture = _sendInterfaceInfo._mainGame.mazoCartas[id];
			temp.GetComponent<CartaMano>().id = id;
			temp.GetComponent<CartaMano>()._mainGame = _sendInterfaceInfo._mainGame;
			_sendInterfaceInfo._mainGame.cartasEnLaMano.Add(temp);
			object[] datas2 = new object[] {PhotonNetwork.LocalPlayer.NickName, _sendInterfaceInfo._mainGame.cartasEnLaMano.Count.ToString()};
			PhotonNetwork.RaiseEvent(82, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);

			for(int x = 0; x < _sendInterfaceInfo.cementerioLista.Count; x++)
			{
				Destroy(_sendInterfaceInfo.cementerioLista[x]);
			}
			_sendInterfaceInfo.cementerioLista.Clear();
			_sendInterfaceInfo.panelCementerio.SetActive(false);
			Destroy(this.gameObject);

		}

	}
}
