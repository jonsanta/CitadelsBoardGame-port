using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Faro : MonoBehaviour
{
	public HabilidadPanel _habilidad;
	public List<int> cartas = new List<int>();
	public bool display;
	public List<GameObject> cartasPrefab = new List<GameObject>();
	public int id;
	public bool selected;

    // Start is called before the first frame update
    void Start()
    {
		for(int p = 0; p < GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count; p++)
		{
			GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano[p].GetComponent<Button>().enabled = false;
		}

		id = 0;
		selected = false;

		if(!PhotonNetwork.IsMasterClient)
		{
		object[] datas = new object[] {PhotonNetwork.LocalPlayer.NickName};
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient};
		PhotonNetwork.RaiseEvent(14, datas, raiseEventOptions, SendOptions.SendUnreliable);
		}
		else
		{
			for(int x = GetComponent<CartaEnMesa>()._mainGame._masterGame.cartaRepartida; x < GetComponent<CartaEnMesa>()._mainGame._masterGame.cardID.Count; x++)
			{
				cartas.Add(GetComponent<CartaEnMesa>()._mainGame._masterGame.cardID[x]);
			}
			GetComponent<CartaEnMesa>()._mainGame._masterGame.cartaRepartida += 1;
			display = true;
		}
    }

	void Update()
	{
		_habilidad = GameObject.FindWithTag("habilidad").GetComponent<HabilidadPanel>();
		_habilidad.disabler.SetActive(true);

		if(display == true)
		{
			_habilidad.faro.SetActive(true);
			cartas.Sort();
			for(int x = 0; x < cartas.Count; x++)
			{
				GameObject temp;
				temp = Instantiate(_habilidad.faroPrefab, _habilidad.faroContent);
				temp.GetComponent<RawImage>().texture = _habilidad._mainGame.mazoCartas[cartas[x]];
				temp.GetComponent<CartaFaro>().id = cartas[x];
				temp.GetComponent<CartaFaro>()._faro = this;
				cartasPrefab.Add(temp);
			}
			display = false;
		}

		if(selected == true)
		{
			GameObject temp2;
			temp2 = Instantiate(_habilidad._mainGame.prefabCartaMano, _habilidad._mainGame._content);
			temp2.GetComponent<RawImage>().texture = _habilidad._mainGame.mazoCartas[id];
			temp2.GetComponent<CartaMano>().id = id;
			temp2.GetComponent<CartaMano>()._mainGame = _habilidad._mainGame;
			_habilidad._mainGame.cartasEnLaMano.Add(temp2);
			object[] datas2 = new object[] {PhotonNetwork.LocalPlayer.NickName, _habilidad._mainGame.cartasEnLaMano.Count.ToString()};
			PhotonNetwork.RaiseEvent(82, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);

			_habilidad.disabler.SetActive(false);
			for(int p = 0; p < GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count; p++)
			{
				GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano[p].GetComponent<Button>().enabled = true;
			}

			Destroy(GetComponent<Faro>());
		}

	}

	private void OnEnable()
	{
		PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
	}

	private void OnDisable()
	{
		PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
	}


	private void NetworkingClient_EventReceived(EventData obj)
	{

		if(obj.Code == 64)
		{
			object[] datas = (object[])obj.CustomData;

			if((string)datas[0] == PhotonNetwork.LocalPlayer.NickName)
			{
				cartas.Add(int.Parse((string)datas[1]));
			}

			int temp = int.Parse((string)datas[3]);
			int temp2 = int.Parse((string)datas[2]);

			if(cartas.Count + temp >= temp2)
			{
				display = true;
			}

		}
	}
}
