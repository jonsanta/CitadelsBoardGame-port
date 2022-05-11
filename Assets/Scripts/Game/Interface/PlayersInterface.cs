using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PlayersInterface : MonoBehaviour
{
	public GameObject puntos;
	private int cartasRecibidas;
	private int cartasRecibidasTempVar;
	private List<int> cartasRecibidasTemp = new List<int>();
	public List<GameObject> cartasEnMesa = new List<GameObject>();
	private string nameIntercambio;

	public GameObject content;
	public GameObject cartaPrefab;
	private int once;
	public SendInterfaceInfo _sendInterfaceInfo;
	public bool turno;
	public GameObject turnoImage;

	public bool personajeSelect;
	public GameObject turnoPersonaje;

	public int number;

	public Text oroText;
	public Text cartasText;
	public Text jugadorText;
	public Image personaje;
	public GameObject corona;

	public string playerName;

	public void Update()
	{
		if (turno == true)
		{
			turnoImage.SetActive(true);
		}
		else
		{
			turnoImage.SetActive(false);
		}

		if(_sendInterfaceInfo.playerNames.Count <= number &&_sendInterfaceInfo.once == 1)
		{
			_sendInterfaceInfo.mesas.Remove(this.gameObject);
			Destroy(this.gameObject);
			Destroy(personaje.gameObject);
			Destroy(corona.gameObject);
			Destroy(puntos.gameObject);
		}

		if(playerName != "" && once == 0)
		{
			jugadorText.text = playerName;
			_sendInterfaceInfo.x += 1;
			once = 1;
		}

		if(_sendInterfaceInfo._mainGame.corona == playerName)
		{
			corona.SetActive(true);
		}
		else
		{
			corona.SetActive(false);
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

		if(obj.Code == 7)
		{
			object[] datas = (object[])obj.CustomData;

			if((string)datas[1] == PhotonNetwork.LocalPlayer.NickName)
			{
				for(int x = 0; x < _sendInterfaceInfo._mainGame.cartasEnLaMano.Count; x++)
				{
					object[] datas2 = new object[] {(string)datas[0] ,_sendInterfaceInfo._mainGame.cartasEnLaMano[x].GetComponent<CartaMano>().id.ToString(),_sendInterfaceInfo._mainGame.cartasEnLaMano.Count.ToString()};
					PhotonNetwork.RaiseEvent(42, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					Destroy(_sendInterfaceInfo._mainGame.cartasEnLaMano[x]);
				}
				_sendInterfaceInfo._mainGame.cartasEnLaMano.Clear();
				object[] datas10 = new object[] {PhotonNetwork.LocalPlayer.NickName, _sendInterfaceInfo._mainGame.cartasEnLaMano.Count.ToString()};
				PhotonNetwork.RaiseEvent(82, datas10, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			}


		}

		if(obj.Code == 32)
		{
			object[] datas = (object[])obj.CustomData;

			if((string)datas[0] == playerName)
			{
				puntos.GetComponent<Text>().text = (string)datas[1];
			}


		}

		if(obj.Code == 29)
		{
			object[] datas = (object[])obj.CustomData;

			if((string)datas[0] == playerName)
			{
				if((string)datas[1] == "yes")
				{
					turnoPersonaje.SetActive(true);
				}
				else if((string)datas[1] == "no")
				{
					turnoPersonaje.SetActive(false);
				}
			}


		}

		if(obj.Code == 11)
		{
			object[] datas = (object[])obj.CustomData;

			if((string)datas[0] == playerName)
			{
				string temp;
				temp = (string)datas[1];
				GameObject temp2;
				for(int x = 0; x < cartasEnMesa.Count; x++)
				{
					if(cartasEnMesa[x].name == temp)
					{
						temp2 = cartasEnMesa[x];
						cartasEnMesa.Remove(temp2);
						Destroy(temp2);
					}

				}
				if(_sendInterfaceInfo._mainGame.oro >= 1)
				{
					for(int x = 0; x < _sendInterfaceInfo._mainGame._lector.cartasEnMesa.Count; x++)
					{
					
						if(_sendInterfaceInfo._mainGame._lector.cartasEnMesa[x].name == "Cementerio" && (string)datas[2] == "yes")
						{
							_sendInterfaceInfo.panelCementerio.SetActive(true);
							GameObject temp4;
							temp4 = Instantiate(_sendInterfaceInfo.cementerioPrefab, _sendInterfaceInfo.cementerioContent);
							_sendInterfaceInfo.cementerioLista.Add(temp4);
							temp4.GetComponent<Cementerio>()._sendInterfaceInfo = _sendInterfaceInfo;
							temp4.GetComponent<Cementerio>().id = int.Parse(temp);
							temp4.GetComponent<RawImage>().texture = _sendInterfaceInfo._mainGame.mazoCartas[int.Parse(temp)];
							temp4.GetComponent<Cementerio>().name = temp;
							GameObject temp3;
							temp3 = Instantiate(_sendInterfaceInfo.cementerioPrefab, _sendInterfaceInfo.cementerioContent);
							_sendInterfaceInfo.cementerioLista.Add(temp3);
							temp3.GetComponent<Cementerio>()._sendInterfaceInfo = _sendInterfaceInfo;
							temp3.GetComponent<Cementerio>().name = "Cancelar";
							temp3.GetComponentInChildren<Text>().text = "Cancelar";
						}
					}
				}
			}
		}

		if(obj.Code == 81)
		{

			object[] datas = (object[])obj.CustomData;

			if((string)datas[0] == playerName)
			{
				int temp;
				int temp2;
				temp = int.Parse((string)datas[1]);
				oroText.text = temp.ToString();

			}
		}

		if(obj.Code == 82)
		{

			object[] datas = (object[])obj.CustomData;

			if((string)datas[0] == playerName)
			{
				int temp;
				temp = int.Parse((string)datas[1]);
				cartasText.text = temp.ToString();
			}
		}

		if(obj.Code == 84)
		{
			object[] datas = (object[])obj.CustomData;

			if((string)datas[0] == playerName)
			{
				GameObject temp;
				int temp2;
				temp2 = int.Parse((string)datas[1]);
				temp = Instantiate(cartaPrefab, content.transform);
				temp.GetComponent<RawImage>().texture = _sendInterfaceInfo._mainGame.mazoCartas[temp2];
				temp.name = temp2.ToString();
				cartasEnMesa.Add(temp);
			}
		}

		if(obj.Code == 85)
		{
			object[] datas = (object[])obj.CustomData;

			if((string)datas[0] == playerName)
			{
				int temp;
				temp = int.Parse((string)datas[1]);
				personaje.GetComponent<Image>().sprite = _sendInterfaceInfo._mainGame.mazoPersonajes[temp];
			}
		}

		if(obj.Code == 86)
		{
			object[] datas = (object[])obj.CustomData;
			int temp;
			temp = int.Parse((string)datas[0]);

			if(_sendInterfaceInfo._mainGame.personajeLocal == temp)
			{
				_sendInterfaceInfo._mainGame.asesinado = true;

			}
		}

		if(obj.Code == 12)
		{
			object[] datas = (object[])obj.CustomData;

			string temp;
			temp = (string)datas[0];

			if(temp == playerName)
			{
				turno = true;
			}
			else
			{
				turno = false;
			}
		}

		if(obj.Code == 13)
		{
			object[] datas = (object[])obj.CustomData;

			string temp;
			temp = (string)datas[0];

			if(temp == playerName)
			{
				turno = false;
			}
		}

		if(obj.Code == 87)
		{
			object[] datas = (object[])obj.CustomData;
			int temp;
			temp = int.Parse((string)datas[0]);

			if(_sendInterfaceInfo._mainGame.personajeLocal == temp)
			{
				if(_sendInterfaceInfo._mainGame.asesinado == true)
				{

				}
				else
				{
					int temp2;
					temp2 = _sendInterfaceInfo._mainGame.oro;
					object[] datas2 = new object[] {(string)datas[1], temp2.ToString()};
					PhotonNetwork.RaiseEvent(95, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					_sendInterfaceInfo._mainGame.oro -= temp2;
					object[] datas3 = new object[] {PhotonNetwork.LocalPlayer.NickName, _sendInterfaceInfo._mainGame.oro.ToString()};
					PhotonNetwork.RaiseEvent(81, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
				}

			}
		}

		if(obj.Code == 88)
		{
			object[] datas = (object[])obj.CustomData;
			if((string)datas[3] == PhotonNetwork.LocalPlayer.NickName)
			{
				nameIntercambio = (string)datas[0];
				cartasRecibidasTemp.Add(int.Parse((string)datas[1]));
				cartasRecibidas = int.Parse((string)datas[2]);
				cartasRecibidasTempVar +=1;

				if(cartasRecibidasTempVar == cartasRecibidas)
				{
					for(int x = 0; x < _sendInterfaceInfo._mainGame.cartasEnLaMano.Count; x++)
					{
						object[] datas2 = new object[] {nameIntercambio ,_sendInterfaceInfo._mainGame.cartasEnLaMano[x].GetComponent<CartaMano>().id.ToString(),_sendInterfaceInfo._mainGame.cartasEnLaMano.Count.ToString()};
						PhotonNetwork.RaiseEvent(42, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
						Destroy(_sendInterfaceInfo._mainGame.cartasEnLaMano[x]);
					}
					_sendInterfaceInfo._mainGame.cartasEnLaMano.Clear();

					for(int x = 0; x < cartasRecibidasTempVar ; x++)
					{
						GameObject temp2;
						temp2 = Instantiate(_sendInterfaceInfo._mainGame.prefabCartaMano, _sendInterfaceInfo._mainGame._content);
						temp2.GetComponent<RawImage>().texture = _sendInterfaceInfo._mainGame.mazoCartas[cartasRecibidasTemp[x]];
						temp2.GetComponent<CartaMano>().id = cartasRecibidasTemp[x];
						temp2.GetComponent<CartaMano>()._mainGame = _sendInterfaceInfo._mainGame;
						_sendInterfaceInfo._mainGame.cartasEnLaMano.Add(temp2);

					}

					object[] datas10 = new object[] {PhotonNetwork.LocalPlayer.NickName, _sendInterfaceInfo._mainGame.cartasEnLaMano.Count.ToString()};
					PhotonNetwork.RaiseEvent(82, datas10, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					nameIntercambio = "";
					cartasRecibidasTemp.Clear();
					cartasRecibidas = 0;
					cartasRecibidasTempVar = 0;
				}

			}
		}
	}

}
