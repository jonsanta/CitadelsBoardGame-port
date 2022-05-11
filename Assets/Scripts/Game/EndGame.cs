using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class EndGame : MonoBehaviour
{
	public GameObject putancionesDisplay;
	public GameObject puntacionesPrefab;
	public Transform puntuacionesContent;
	public bool start;
	public int size;


	public bool fin;
	public bool display;
	public int puntos;
	int campanario = 0;
	int encontrado;
	int encontradoTemp;
	MasterEngine _masterEngine;
	public bool first = false;
	public bool firstComp = false;
	int once = 0;
	int once1 = 0;
	int justonce = 0;
	public bool onceupn;

	public GameObject ciudadEmbrujada;
	public bool ciudadEmbrujadaEncontrada;
	public GameObject displayBotones;
	public GameObject finalDisplay;
	int playerList;

	public int distritosFin;

	public List<string> names = new List<string>();
	public List<int> puntuaciones = new List<int>();

	public List<int> colores = new List<int>();

	public bool nomore =false;

    // Start is called before the first frame update
    void Start()
    {
		fin = false;
		start = false;
		campanario = 0;
		encontradoTemp = 0;
		justonce = 0;
		first = false;
		firstComp = false;
		once = 0;
		ciudadEmbrujadaEncontrada = false;
		onceupn = false;
		distritosFin = 20;
		once1 = 0;
		nomore = false;

    }

    // Update is called once per frame
    void Update()
    {
		if(PhotonNetwork.IsMasterClient)
		{
			_masterEngine =	GameObject.FindWithTag("GameEngine").GetComponent<MasterEngine>();
			_masterEngine.noChanges = true;
		}

		if(display == true)
		{
			putancionesDisplay.SetActive(true);



			if(!PhotonNetwork.IsMasterClient)
			{
				if(justonce == 0)
				{
					object[] datas = new object[] {PhotonNetwork.LocalPlayer.NickName, puntos.ToString()};
					RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient};
					PhotonNetwork.RaiseEvent(22, datas, raiseEventOptions, SendOptions.SendUnreliable);
					justonce = 1;
				}
			}

			if(names.Count == playerList)
			{
				if(nomore == false)
				{
					for(int x = 0; x < names.Count; x++)
					{
						GameObject temp;
						temp = Instantiate(puntacionesPrefab, puntuacionesContent);
						temp.GetComponent<Text>().text = names[x]+": "+puntuaciones[x]+" Puntos";
						if(PhotonNetwork.IsMasterClient)
						{
							object[] datas = new object[] {names[x], puntuaciones[x].ToString(), size.ToString()};
							PhotonNetwork.RaiseEvent(22, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
						}
					}
					playerList = 0;
					nomore = true;
				}
			}


		}

		if(fin == true)
		{
			finalDisplay.SetActive(true);

			if(GameObject.Find("Ciudad Embrujada") && onceupn == false)
			{
				ciudadEmbrujada = GameObject.Find("Ciudad Embrujada");
				displayBotones.SetActive(true);
				ciudadEmbrujadaEncontrada = true;
				onceupn = true;
			}

			if(ciudadEmbrujadaEncontrada == false)
			{
				for(int x = 0; x < GetComponent<Lector>().cartasEnMesa.Count; x++)
				{
					puntos += GetComponent<Lector>().cartasEnMesa[x].GetComponent<CartaEnMesa>().puntos;
					if(colores.Contains(GetComponent<Lector>().cartasEnMesa[x].GetComponent<CartaEnMesa>().color))
					{
					}
					else
					{
					colores.Add(GetComponent<Lector>().cartasEnMesa[x].GetComponent<CartaEnMesa>().color);
					}
				}

				if(colores.Count == 5)
				{
					puntos += 3;
				}

				fin = false;
				display = true;
				if(PhotonNetwork.IsMasterClient)
				{
					names.Add(PhotonNetwork.LocalPlayer.NickName);
					puntuaciones.Add(puntos);
				}
			}
		}

		for(int x = 0; x < GetComponent<MainGame>()._sendInterfaceInfo.mesas.Count; x++)
		{
			if(GetComponent<MainGame>()._sendInterfaceInfo.mesas[x].GetComponent<PlayersInterface>().cartasEnMesa.Count  == distritosFin-campanario)
			{
				if( first == false)
				{
					first = true;
				}
			}

		}


		if(GetComponent<Lector>().cartasEnMesa.Count  == distritosFin-campanario)
		{
			if( first == false)
			{
				first = true;
				firstComp = true;
				puntos += 4;
			}
			if(once == 0 && firstComp == false)
			{
				puntos += 2;
				once = 1;
			}
		}

		if(encontrado != encontradoTemp)
		{
			campanario = 1;
		}
		else
		{
			campanario = 0;
		}

		if(PhotonNetwork.IsMasterClient)
		{
			playerList = _masterEngine.playerList.Count;

			if(once1 == 0)
			{
				distritosFin =_masterEngine.distritosFin;
				object[] datas = new object[] {distritosFin};
				PhotonNetwork.RaiseEvent(28, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
				once1 = 1;
			}

			if(display == false)
			{
				encontradoTemp = encontrado;
				for(int x = 0; x < GetComponent<MainGame>()._sendInterfaceInfo.mesas.Count; x++)
				{
					for(int z = 0; z < GetComponent<MainGame>()._sendInterfaceInfo.mesas[x].GetComponent<PlayersInterface>().cartasEnMesa.Count; z++)
					{
						if(GetComponent<MainGame>()._sendInterfaceInfo.mesas[x].GetComponent<PlayersInterface>().cartasEnMesa[z].name == "58")
						{
							encontrado += 1;

						}
					}
				}

				for(int x = 0; x < GetComponent<Lector>().cartasEnMesa.Count; x++)
				{
					if(GetComponent<Lector>().cartasEnMesa[x].name == "Campanario")
					{
						encontrado += 1;
					}
				}

				for(int x = 0; x < GetComponent<MainGame>()._sendInterfaceInfo.mesas.Count; x++)
				{
					if(GetComponent<MainGame>()._sendInterfaceInfo.mesas[x].GetComponent<PlayersInterface>().cartasEnMesa.Count == _masterEngine.distritosFin-campanario && GetComponent<MainGame>()._masterGame.nj == 0)
					{
						fin = true;
						object[] datas = new object[] {"yes", _masterEngine.playerList.Count.ToString()};
						PhotonNetwork.RaiseEvent(45, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					}

				}

				if(GetComponent<MainGame>()._lector.cartasEnMesa.Count == _masterEngine.distritosFin-campanario && GetComponent<MainGame>()._masterGame.nj == 0)
				{
					fin = true;
					object[] datas = new object[] {"yes", _masterEngine.playerList.Count.ToString()};
					PhotonNetwork.RaiseEvent(45, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
				}

			}

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
		if(obj.Code == 45)
		{
			object[] datas = (object[])obj.CustomData;
			playerList = int.Parse((string)datas[1]);
			fin = true;
		}

		if(obj.Code == 28)
		{
			object[] datas = (object[])obj.CustomData;
			distritosFin = (int)datas[0];
		}

		if(obj.Code == 22)
		{
			object[] datas = (object[])obj.CustomData;

			if(PhotonNetwork.IsMasterClient)
			{
				names.Add((string)datas[0]);
				puntuaciones.Add(int.Parse((string)datas[1]));
				size = _masterEngine.playerList.Count;
			}else
			{
				names.Add((string)datas[0]);
				puntuaciones.Add(int.Parse((string)datas[1]));
				size = int.Parse((string)datas[2]);
			}
		}
	}
}
