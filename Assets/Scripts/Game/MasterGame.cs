using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class MasterGame : MonoBehaviour
{
	public int once = 0;
	public int once2 = 0;
	public string corona;

	public int sentTimes;
	public bool yaHeEscogidoPersonaje = false; 
	public List<int> personajeRecibidotemp = new List<int>();
	public bool personajesYaSeleccionados = false;
	private int j;
	public int nj;
	public int turnoDe;
	public int cartaRepartida = 0;
	public MainGame _mainGame;
	private bool barajarUnaVez = false;
	private bool empezarElJuego = false;

	public List<string> playerList = new List<string>();

	public List<int> cardID = new List<int>();
	public int[] personajeID;

	private void Start()
	{
		turnoDe = 0;
		sentTimes = 0;
		once = 0;
	}

	private void Update()
	{
		if(GameObject.Find("Game"))
		{
			_mainGame = GameObject.Find("Game").GetComponent<MainGame>();
			_mainGame._masterGame = this;
			RondaInicial();
			if(barajarUnaVez == false)
			{
				for(int x = 0; x < playerList.Count; x++)
				{
					object[] datas5 = new object[] {playerList[x]};
					PhotonNetwork.RaiseEvent(80, datas5, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					_mainGame._sendInterfaceInfo.playerNames.Add(playerList[x]);
				}
				_mainGame._sendInterfaceInfo.startSending = true;
				ShuffleMazo();
				ShuffleJugadores();
				barajarUnaVez = true;
			}
		}
			
		if(empezarElJuego == true)
		{
			if(turnoDe > playerList.Count-1)
			{
				//turno de 0

				nj = 0;
				once = 0;
				personajesYaSeleccionados = false;
				turnoDe = 0;
				personajeRecibidotemp.Clear();
				_mainGame.RestartRound();
				object[] datas5 = new object[] {"Restart"};
				PhotonNetwork.RaiseEvent(38, datas5, RaiseEventOptions.Default, SendOptions.SendUnreliable);
				ShufflePersonajes();
				ShuffleJugadores2();

			}

			MandarPersonajes();

			if(personajesYaSeleccionados == true)
			{
				Turno();
			}

		}
	}


	public void Turno()
	{	
		personajeRecibidotemp.Sort();

		if(personajeRecibidotemp[turnoDe] == _mainGame.personajeLocal)
		{
			_mainGame.esMiTurno = true;
			if(once2 == 0)
			{
				_mainGame.delay = 1f;
				once2 = 1;
			}

		}
		else if(sentTimes == 0)
		{
			object[] datas = new object[] {personajeRecibidotemp[turnoDe],turnoDe};
			PhotonNetwork.RaiseEvent(10, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			sentTimes = 1;
		}
	}


	public void MandarPersonajes()
	{
		if(nj >= playerList.Count)
		{
			personajesYaSeleccionados = true;
		}

		if(personajesYaSeleccionados == false)
		{
			List<int> tempList = new List<int>();

			if(playerList.Count < 8)
			{
				for(int x = 0; x < playerList.Count+1; x++)
				{
					tempList.Add(personajeID[x]);
				}
				PersonajesDescartados();
			}

			if(playerList.Count == 8)
			{
				for(int x = 0; x < playerList.Count; x++)
				{
					tempList.Add(personajeID[x]);
				}
			}

			for(int f = 0; f < nj; f++)
			{
				tempList.Remove(personajeRecibidotemp[f]);
			}

			if(playerList[nj] == PhotonNetwork.LocalPlayer.NickName && yaHeEscogidoPersonaje == false)
			{
				for(int i = 0; i < tempList.Count; i++)
				{
					Debug.Log("Estoy aqui");
					_mainGame.SeleccionandoPersonajes(tempList[i]);
				}
				yaHeEscogidoPersonaje = true;
				once2 = 0;
			}
			else if(yaHeEscogidoPersonaje == false)
			{
				for(int z = 0; z < tempList.Count; z++)
				{
					Debug.Log("Estoy Enviando");
					string temp;
					temp = tempList[z].ToString();
					object[] datas5 = new object[] {playerList[nj], temp};
					PhotonNetwork.RaiseEvent(3, datas5, RaiseEventOptions.Default, SendOptions.SendUnreliable);
				}
				yaHeEscogidoPersonaje = true;
				once2 = 0;
			}
		}
	}
		

	public void RondaInicial()
	{
		if(_mainGame.rondaInicial == true)
		{
			for( j = 0; j < playerList.Count; j++)
			{
				if(playerList[j] == PhotonNetwork.LocalPlayer.NickName)
				{
					for(int i = 0; i < 4; i++)
					{
						_mainGame.cartasRecibidas.Add(cardID[cartaRepartida]);
						_mainGame.CartasALaMano(1);
						cartaRepartida += 1;
						
					}
				}
				else
				{
					EnviadoCarta();
					cartaRepartida += 4;
				}

				if(j == playerList.Count -1)
				{
					_mainGame.rondaInicial = false;
					ShufflePersonajes();
					corona = playerList[0];
					_mainGame.corona = playerList[0];
					object[] datas = new object[] {playerList[0]};
					PhotonNetwork.RaiseEvent(4, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					empezarElJuego = true;
				}
						
			}
		}

	}

	public void ShuffleJugadores(){
		for(int i = 0; i < playerList.Count; i++)
		{
			string tmp = playerList[i];
			int rnd = Random.Range(i, playerList.Count);
			playerList[i] = playerList[rnd];
			playerList[rnd] = tmp;

		}
	}

	public void ShuffleJugadores2(){
		for(int i = 0; i < playerList.Count; i++)
		{
			string tmp = playerList[i];
			int rnd = Random.Range(i, playerList.Count);
			playerList[i] = playerList[rnd];
			playerList[rnd] = tmp;

		}
		string temp;
		temp = playerList[0];
		if(temp == corona)
		{

		}
		else
		{
			playerList.Remove(corona);
			playerList[0] = corona;
			playerList.Add(temp);
		}
	}

	public void ShuffleMazo(){
		for(int i = 0; i < cardID.Count; i++)
		{
			int tmp = cardID[i];
			int rnd = Random.Range(i, cardID.Count);
			cardID[i] = cardID[rnd];
			cardID[rnd] = tmp;

		}
	}

	public void ShufflePersonajes(){
		for(int i = 0; i < personajeID.Length; i++)
		{
			int tmp = personajeID[i];
			int rnd = Random.Range(i, personajeID.Length);
			personajeID[i] = personajeID[rnd];
			personajeID[rnd] = tmp;

		}
	}
		
	public void EnviadoCarta()
	{
		object[] datas = new object[] {playerList[j], cardID[cartaRepartida].ToString(), cardID[cartaRepartida+1].ToString(),cardID[cartaRepartida+2].ToString(),cardID[cartaRepartida+3].ToString() };
		PhotonNetwork.RaiseEvent(1, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
	}

	public void EnviarCartasDeTurno()
	{
		_mainGame.cartasRecibidasEnTurno.Add(cardID[cartaRepartida]);
		_mainGame.cartasRecibidasEnTurno.Add(cardID[cartaRepartida+1]);
		if(_mainGame.observatorio == true)
		{
			_mainGame.cartasRecibidasEnTurno.Add(cardID[cartaRepartida+2]);
			cartaRepartida += 3;
		}else
		{
		cartaRepartida += 2;
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

		if(obj.Code == 5)
		{
			object[] datas = (object[])obj.CustomData;
			personajeRecibidotemp.Add((int)datas[0]); 
			nj +=1;
			yaHeEscogidoPersonaje = false;

		}

		if(obj.Code == 6)
		{

			object[] datas = (object[])obj.CustomData;
			corona = (string)datas[0];

		}

		if(obj.Code == 20)
		{
			object[] datas = (object[])obj.CustomData;
			if((int)datas[0] == 1)
			{
				turnoDe += 1;
				sentTimes = 0;
			}

		}

		if(obj.Code == 50)
		{
			object[] datas = (object[])obj.CustomData;

			if((string)datas[0] == "yes")
			{
				object[] datas2 = new object[] {personajeRecibidotemp[turnoDe],cardID[cartaRepartida], cardID[cartaRepartida+1], cardID[cartaRepartida+2]};
				PhotonNetwork.RaiseEvent(60, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
				cartaRepartida += 3;
			}
			if((string)datas[0] == "no")
			{
				object[] datas3 = new object[] {personajeRecibidotemp[turnoDe],cardID[cartaRepartida], cardID[cartaRepartida+1]};
				PhotonNetwork.RaiseEvent(60, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
				cartaRepartida += 2;
			}

		}

		if(obj.Code == 63)
		{
			object[] datas = (object[])obj.CustomData;
			string temp;
			temp = (string)datas[0];
			object[] datas2 = new object[] {temp , cardID[cartaRepartida].ToString()};
			PhotonNetwork.RaiseEvent(39, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			cartaRepartida += 1;
			Debug.Log("cartasEnviadas");

		}

		if(obj.Code == 59)
		{
			object[] datas = (object[])obj.CustomData;
			string temp;
			temp = (string)datas[0];
			object[] datas2 = new object[] {temp , cardID[cartaRepartida].ToString()};
			PhotonNetwork.RaiseEvent(52, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			cartaRepartida += 1;
			Debug.Log("cartasEnviadas");

		}

		if(obj.Code == 47)
		{
			object[] datas = (object[])obj.CustomData;
			string temp;
			temp = (string)datas[0];
			object[] datas2 = new object[] {temp , cardID[cartaRepartida].ToString()};
			PhotonNetwork.RaiseEvent(49, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			cartaRepartida += 1;
			Debug.Log("cartasEnviadas");

		}

		if(obj.Code == 70)
		{
			object[] datas = (object[])obj.CustomData;
			if((int)datas[2] == 1)
			{
			cardID.Add((int)datas[0]);
			Debug.Log("CartasAñadidas");
			}
			else if((int)datas[2] == 2)
			{
				cardID.Add((int)datas[0]);
				cardID.Add((int)datas[1]);
				Debug.Log("CartasAñadidas");
			}
		}

		if(obj.Code == 14)
		{	
			object[] datas = (object[])obj.CustomData;

			for(int x = cartaRepartida; x < cardID.Count; x++)
			{
				object[] datas2 = new object[] {(string)datas[0] , cardID[x].ToString(), cardID.Count.ToString(), cartaRepartida.ToString()};
				PhotonNetwork.RaiseEvent(64, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
				Debug.Log("Mandando Cartas");
			}
			cartaRepartida += 1;
		}
	}

	public void PersonajesDescartados()
	{
		if(once == 0)
		{
			for(int x = playerList.Count+1; x < 8; x++)
			{
				_mainGame.personajesDescartados.Add(_mainGame.mazoPersonajes[personajeID[x]]);
				object[] datas = new object[] {personajeID[x],8 -(playerList.Count+1)};
				PhotonNetwork.RaiseEvent(17, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			}
			_mainGame.personajesDescartadosInstantiate();
			once = 1;
		}

	}

}
