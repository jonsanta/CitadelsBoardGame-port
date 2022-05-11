using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class MainGame : MonoBehaviour
{
	public Log _log;
	public GameObject puntos;
	public GameObject habilidadPanel;

	public List<Sprite> personajesDescartados = new List<Sprite>();
	public List<GameObject> personajesDescartadosLista = new List<GameObject>();
	public GameObject personajeDescartadoPanel;
	public GameObject personajeDescartadoPrebab;
	public Transform persoanjeDescartadoContent;
	public int once;

	public bool observatorio = false;
	int observatorio1;
	int observatorio2;

	public GameObject recaudarBoton;
	public GameObject FinalizarBoton;
	public MaravillasResources _maravillasResources;
	public bool bypass = false;
	public RecaudarImpuestos _recaudarImpuestos;
	public GameObject personajeEscogidoHabilidad;

	public string corona;
	public GameObject coronaImage;

	public int personajeAsesinado;
	public bool asesinado;

	//Inteface
	public SendInterfaceInfo _sendInterfaceInfo;
	public Lector _lector;
	public List<GameObject> pesonajesEnLaMesa = new List<GameObject>();
	public Sprite dorso;

	//TURNO oro o cartas
	public bool oroOcartas = false;
	private int turnoDe;
	public int oro;
	GameObject tempSelect = null;
	public GameObject oroPrefab;
	GameObject tempSelect2 = null;
	public GameObject pedirCartasPrefab;

	//TURNO Elegir y Descartar Cartas
	public bool elegirODescartar = false;
	public GameObject tempSelectCartaPrefab;
	GameObject tempSelectCarta = null;
	GameObject tempSelectCarta2 = null;
	GameObject tempSelectCarta3 = null;
	public List<int> cartasRecibidasEnTurno = new List<int>();
	//FINALIZAR TURNO 
	public bool finalizarTurno;
	public bool construir;
	public bool habilidad;
	public List<GameObject> habilidadesPrefab = new List<GameObject>();



	public Text displayOro;
	public int personajeLocal;
	public bool esMiTurno = false;

	private List<GameObject> listaPersonajesPrefab = new List<GameObject>();
	public List<GameObject> cartasEnLaMano = new List<GameObject>();
	public int cartasActivadas = 0;

	public MasterGame _masterGame;

	public GameObject personajesPrefab;
	public GameObject personajesPanel;
	public Transform personajes_content;

	public bool rondaInicial = false;
	private bool cartasALaMano = false;

	public GameObject prefabCartaMano;
	public Transform _content;
	[SerializeField]
	public Sprite[] mazoPersonajes;

	[SerializeField]
	public Texture[] mazoCartas;

	public List<int> cartasRecibidas = new List<int>();

	public float delay;
	public GameObject delayObject;

	public void Start()
	{
		delay = 0f;
		oro = 2;
		construir = false;
		habilidad = false;
		finalizarTurno = false;
		asesinado = false;
		observatorio = false;
		cartasActivadas = 0;
	}

	public void Update()
	{
		if(delay >= 1)
		{
			delay += Time.deltaTime;
			delayObject.SetActive(true);
			recaudarBoton.GetComponent<Image>().enabled = false;
			recaudarBoton.GetComponentInChildren<Text>().enabled = false;
			habilidadPanel.GetComponent<HabilidadPanel>().habilidadBoton.GetComponent<Image>().enabled = false;
			habilidadPanel.GetComponent<HabilidadPanel>().habilidadBoton.GetComponentInChildren<Text>().enabled = false;
			habilidadPanel.GetComponent<HabilidadPanelGuerrero>().habilidadBoton.GetComponent<Image>().enabled = false;
			habilidadPanel.GetComponent<HabilidadPanelGuerrero>().habilidadBoton.GetComponentInChildren<Text>().enabled = false;
			if(delay >2.7f)
			{
				delay = 0f;
				recaudarBoton.GetComponent<Image>().enabled = true;
				recaudarBoton.GetComponentInChildren<Text>().enabled = true;
				if(habilidad == true)
				{
					habilidadPanel.GetComponent<HabilidadPanel>().habilidadBoton.GetComponent<Image>().enabled = true;
					habilidadPanel.GetComponent<HabilidadPanel>().habilidadBoton.GetComponentInChildren<Text>().enabled = true;
					habilidadPanel.GetComponent<HabilidadPanelGuerrero>().habilidadBoton.GetComponent<Image>().enabled = true;
					habilidadPanel.GetComponent<HabilidadPanelGuerrero>().habilidadBoton.GetComponentInChildren<Text>().enabled = true;
				}
			}

		}
		else
		{
			delayObject.SetActive(false);
		}

		displayOro.text = oro.ToString();

		if(observatorio1 != observatorio2)
		{
			observatorio = true;
			observatorio2 = observatorio1;
		}
		else
		{
			observatorio = false;
		}

		for(int x = 0; x < _lector.cartasEnMesa.Count; x++)
		{
			if(_lector.cartasEnMesa[x].name == "Observatorio")
			{
				observatorio1 +=1;
			}

		}

		if(habilidad == false)
		{
			habilidadPanel.GetComponent<HabilidadPanel>().habilidadPanel.SetActive(false);
			habilidadPanel.GetComponent<HabilidadPanel>().habilidadPanel2.SetActive(false);
			habilidadPanel.GetComponent<HabilidadPanel>().habilidadBoton.SetActive(false);
			habilidadPanel.GetComponent<HabilidadPanelGuerrero>().habilidadBoton.SetActive(false);
			habilidadPanel.GetComponent<HabilidadPanelGuerrero>().habilidadPanel.SetActive(false);
		}

		if(esMiTurno == true)
		{
			Asesinado();
			TurnoOroOCartas();
			ElegirDescartarCartas();
			ActivarBotonesDeCartas();
		}else
		{
			Destroy(tempSelect);
			tempSelect = null;
			Destroy(tempSelect2);
			tempSelect2 = null;
			cartasActivadas = 0;
			habilidadPanel.GetComponent<HabilidadPanel>().faro.SetActive(false);
			habilidadPanel.GetComponent<HabilidadPanel>().disabler.SetActive(false);
			habilidadPanel.GetComponent<HabilidadPanel>().habilidadPanel.SetActive(false);
			habilidadPanel.GetComponent<HabilidadPanel>().habilidadPanel2.SetActive(false);
			habilidadPanel.GetComponent<HabilidadPanel>().habilidadBoton.SetActive(false);
			habilidadPanel.GetComponent<HabilidadPanelGuerrero>().habilidadBoton.SetActive(false);
			habilidadPanel.GetComponent<HabilidadPanelGuerrero>().habilidadPanel.SetActive(false);
		}

		if(corona == PhotonNetwork.LocalPlayer.NickName)
		{
			coronaImage.SetActive(true);
		}
		else
		{
			coronaImage.SetActive(false);
		}
	}

	public void ElegirDescartarCartas()
	{
		int encontrado;
		encontrado = 0;
		for(int x = 0; x < _lector.cartasEnMesa.Count; x++)
		{
			if(_lector.cartasEnMesa[x].name == "Biblioteca")
			{
				encontrado = 1;

			}
		}

		if(encontrado == 1)
		{
			bypass = true;
		}
		else
		{
			bypass = false;
		}

		if(elegirODescartar == true && oroOcartas == true && cartasRecibidasEnTurno.Count > 1 && bypass == false)
		{
			if(tempSelectCarta == null)
			{
				tempSelectCarta = Instantiate(tempSelectCartaPrefab, personajes_content);
			}
			tempSelectCarta.GetComponent<RawImage>().texture = mazoCartas[cartasRecibidasEnTurno[0]];
			tempSelectCarta.GetComponent<CartasElegibles>().id = cartasRecibidasEnTurno[0];
			tempSelectCarta.GetComponent<CartasElegibles>().idDescartado = cartasRecibidasEnTurno[1];
			if(observatorio == true)
			{
				tempSelectCarta.GetComponent<CartasElegibles>().idDescartado2 = cartasRecibidasEnTurno[2];
				tempSelectCarta.GetComponent<CartasElegibles>().tercera = tempSelectCarta3;
			}
			tempSelectCarta.GetComponent<CartasElegibles>().segunda = tempSelectCarta2;
			tempSelectCarta.GetComponent<CartasElegibles>()._mainGame = this;

			if(tempSelectCarta2 == null)
			{
				tempSelectCarta2 = Instantiate(tempSelectCartaPrefab, personajes_content);
			}
			tempSelectCarta2.GetComponent<RawImage>().texture = mazoCartas[cartasRecibidasEnTurno[1]];
			tempSelectCarta2.GetComponent<CartasElegibles>().id = cartasRecibidasEnTurno[1];
			tempSelectCarta2.GetComponent<CartasElegibles>().idDescartado = cartasRecibidasEnTurno[0];
			if(observatorio == true)
			{
				tempSelectCarta2.GetComponent<CartasElegibles>().idDescartado2 = cartasRecibidasEnTurno[2];
				tempSelectCarta2.GetComponent<CartasElegibles>().tercera = tempSelectCarta3;
			}
			tempSelectCarta2.GetComponent<CartasElegibles>().segunda = tempSelectCarta;
			tempSelectCarta2.GetComponent<CartasElegibles>()._mainGame = this;

			if(observatorio == true)
			{
				if(tempSelectCarta3 == null)
				{
					tempSelectCarta3 = Instantiate(tempSelectCartaPrefab, personajes_content);
				}
				tempSelectCarta3.GetComponent<RawImage>().texture = mazoCartas[cartasRecibidasEnTurno[2]];
				tempSelectCarta3.GetComponent<CartasElegibles>().id = cartasRecibidasEnTurno[2];
				tempSelectCarta3.GetComponent<CartasElegibles>().idDescartado = cartasRecibidasEnTurno[0];
				tempSelectCarta3.GetComponent<CartasElegibles>().idDescartado2 = cartasRecibidasEnTurno[1];
				tempSelectCarta3.GetComponent<CartasElegibles>().segunda = tempSelectCarta;
				tempSelectCarta3.GetComponent<CartasElegibles>().tercera = tempSelectCarta2;
				tempSelectCarta3.GetComponent<CartasElegibles>()._mainGame = this;

			}
		}
		else if(elegirODescartar == false && oroOcartas == true && esMiTurno == true && bypass == false)
		{
			Destroy(tempSelectCarta);
			tempSelectCarta = null;
			Destroy(tempSelectCarta2);
			tempSelectCarta2 = null;
			Destroy(tempSelectCarta3);
			tempSelectCarta3 = null;

			//FINALIZAR EL TURNO
			habilidad = true;
		}
		else
		{
			Bypass();
		}

	}

	public void TurnoOroOCartas()
	{
		if(delay == 0)
		{
			if(oroOcartas == false && asesinado == false)
			{
				if(tempSelect == null)
				{
					personajesPanel.SetActive(true);
					object[] datas = new object[] {PhotonNetwork.LocalPlayer.NickName,personajeLocal.ToString()};
					PhotonNetwork.RaiseEvent(85, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					tempSelect = Instantiate(oroPrefab, personajes_content);
				}

				if(tempSelect2 == null)
				{
					tempSelect2 = Instantiate(pedirCartasPrefab, personajes_content);
				}
		
				tempSelect.GetComponent<ElegirOro>().segunda = tempSelect2;
				tempSelect.GetComponent<ElegirOro>()._mainGame = this;
				tempSelect2.GetComponent<ElegirCartas>().segunda = tempSelect;
				tempSelect2.GetComponent<ElegirCartas>()._mainGame = this;
			}else
			{
				Destroy(tempSelect);
				tempSelect = null;
				Destroy(tempSelect2);
				tempSelect2 = null;
			}
		}
	}

	public void SeleccionandoPersonajes(int container)
	{
		personajesPanel.SetActive(true);
		personajeAsesinado = 10;
		GameObject temp;
		temp = Instantiate(personajesPrefab, personajes_content);
		temp.GetComponent<Image>().sprite = mazoPersonajes[container];
		temp.GetComponent<SeleccionaPersonaje>().id = container;
		temp.GetComponent<SeleccionaPersonaje>()._mainGame = this;
		listaPersonajesPrefab.Add(temp);
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
		if(obj.Code == 1)
		{
			string playerNameTemp = "";

			object[] datas = (object[])obj.CustomData;
			playerNameTemp = (string)datas[0];
			if(playerNameTemp == PhotonNetwork.LocalPlayer.NickName)
			{
				cartasRecibidas.Add(int.Parse((string)datas[1]));
				CartasALaMano(1);
				cartasRecibidas.Add(int.Parse((string)datas[2]));
				CartasALaMano(1);
				cartasRecibidas.Add(int.Parse((string)datas[3]));
				CartasALaMano(1);
				cartasRecibidas.Add(int.Parse((string)datas[4]));
				CartasALaMano(1);

			}
		}

		if(obj.Code == 4)
		{
			object[] datas = (object[])obj.CustomData;
			corona = (string)datas[0];

		}

		if(obj.Code == 10)
		{
			int temp;
			object[] datas = (object[])obj.CustomData;
			temp = (int)datas[0];

			if(temp == personajeLocal)
			{
				esMiTurno = true;
				delay = 1f;
			}
			else if(!PhotonNetwork.IsMasterClient)
			{
				esMiTurno = false;
			}
		}

		if(obj.Code == 60)
		{
			int temp;
			object[] datas = (object[])obj.CustomData;
			temp = (int)datas[0];
			if(temp == personajeLocal)
			{
				if(observatorio == false)
				{
					cartasRecibidasEnTurno.Add((int)datas[1]);
					cartasRecibidasEnTurno.Add((int)datas[2]);
				}
				else
				{
					cartasRecibidasEnTurno.Add((int)datas[1]);
					cartasRecibidasEnTurno.Add((int)datas[2]);
					cartasRecibidasEnTurno.Add((int)datas[3]);
				}
			}
		}

		if(obj.Code == 17)
		{	
			object[] datas = (object[])obj.CustomData;
			personajesDescartados.Add(mazoPersonajes[(int)datas[0]]);
			if(personajesDescartados.Count >= (int)datas[1])
			{
				personajesDescartadosInstantiate();
			}

		}

		if(obj.Code == 3)
		{
			string playerNameTemp = "";

			object[] datas = (object[])obj.CustomData;
			playerNameTemp = (string)datas[0];
			if(playerNameTemp == PhotonNetwork.LocalPlayer.NickName)
			{
				for(int i = 1; i < datas.Length; i++)
				{
					SeleccionandoPersonajes(int.Parse((string)datas[i]));
					Debug.Log("He recibido Los personajes");
				}
			}
		}

		if(obj.Code == 38)
		{

			object[] datas = (object[])obj.CustomData;
			if((string)datas[0] == "Restart")
			{
				RestartRound();
			}
		}

		if(obj.Code == 12)
		{
			
			object[] datas = (object[])obj.CustomData;

			if((string)datas[0] == PhotonNetwork.LocalPlayer.NickName)
			{
				string temp;
				temp = mazoCartas[int.Parse((string)datas[1])].name;
				GameObject temp2;
				for(int x = 0; x < _lector.cartasEnMesa.Count; x++)
				{
					if(_lector.cartasEnMesa[x].name == temp)
					{
						temp2 = _lector.cartasEnMesa[x];
						_lector.cartasEnMesa.Remove(temp2);
						Destroy(temp2);
					}

				}
				if(oro >= 1)
				{
					for(int x = 0; x < _lector.cartasEnMesa.Count; x++)
					{

						if(_lector.cartasEnMesa[x].name == "Cementerio" && (string)datas[2] == "yes" )
						{
							_sendInterfaceInfo.panelCementerio.SetActive(true);
							GameObject temp4;
							temp4 = Instantiate(_sendInterfaceInfo.cementerioPrefab, _sendInterfaceInfo.cementerioContent);
							_sendInterfaceInfo.cementerioLista.Add(temp4);
							temp4.GetComponent<Cementerio>()._sendInterfaceInfo = _sendInterfaceInfo;
							temp4.GetComponent<RawImage>().texture = mazoCartas[int.Parse((string)datas[1])];
							temp4.GetComponent<Cementerio>().id = int.Parse((string)datas[1]);
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
	}

	public void CartasALaMano (int cantidad)
	{
		for(int i = 0; i < cantidad ; i++ )
		{
			GameObject temp;
			temp = Instantiate(prefabCartaMano, _content);
			temp.GetComponent<RawImage>().texture = mazoCartas[cartasRecibidas[i]];
			temp.GetComponent<CartaMano>().id = cartasRecibidas[i];
			temp.GetComponent<CartaMano>()._mainGame = this;
			cartasEnLaMano.Add(temp);
			cartasRecibidas.Remove(cartasRecibidas[0]);
		}
	}

	public void OnClickStartRounds()
	{
		if(PhotonNetwork.IsMasterClient)
		{
		rondaInicial = true;
		}
	}

	public void RemoveAll()
	{
		for(int x = 0; x < listaPersonajesPrefab.Count; x++)
		{
			Destroy(listaPersonajesPrefab[x]);
		}

		listaPersonajesPrefab.Clear();
	}

	public void RestartRound()
	{
		for(int x = 0; x < _sendInterfaceInfo.playerNames.Count+1; x++)
		{
			pesonajesEnLaMesa[x].GetComponent<Image>().sprite = dorso;
		}

	}

	public void Asesinado()
	{
		if(asesinado == true && delay == 0f)
		{
			personajesDescartadosDestroy();
			if(!PhotonNetwork.IsMasterClient)
			{
				_lector.Cancelar();
				esMiTurno = false;
				habilidad = false;
				finalizarTurno = false;
				puntuaje();
				Destroy(personajeEscogidoHabilidad);
				personajeEscogidoHabilidad = null;
				oroOcartas = false;
				construir = false;
				cartasRecibidasEnTurno.Clear();
				GameObject.FindWithTag("habilidad").GetComponent<HabilidadPanel>().habilidadBoton.SetActive(false);
				GameObject.FindWithTag("habilidad").GetComponent<HabilidadPanelGuerrero>().habilidadBoton.SetActive(false);
				object[] datas10 = new object[] {1};
				RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient};
				PhotonNetwork.RaiseEvent(20, datas10, raiseEventOptions, SendOptions.SendUnreliable);

				for(int x = 0; x < _lector.cartasEnMesa.Count; x++)
				{
					if(_lector.cartasEnMesa[x].name == "Hospital")
					{
						_lector.cartasEnMesa[x].GetComponent<Hospital>().asesinado = false;
						object[] datas2 = new object[] {"Usó", "69",""};
						PhotonNetwork.RaiseEvent(76, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
						delay = 1f;
					}

				}
				asesinado = false;
				personajeAsesinado = 10;
			}
			else if (delay == 0f)
			{
				_lector.Cancelar();
				esMiTurno = false;
				habilidad = false;
				finalizarTurno = false;
				puntuaje();
				Destroy(personajeEscogidoHabilidad);
				personajeEscogidoHabilidad = null;
				_masterGame.turnoDe +=1;
				oroOcartas = false;
				construir = false;
				cartasRecibidasEnTurno.Clear();
				GameObject.FindWithTag("habilidad").GetComponent<HabilidadPanel>().habilidadBoton.SetActive(false);
				GameObject.FindWithTag("habilidad").GetComponent<HabilidadPanelGuerrero>().habilidadBoton.SetActive(false);
				for(int x = 0; x < _lector.cartasEnMesa.Count; x++)
				{
					if(_lector.cartasEnMesa[x].name == "Hospital")
					{
						_lector.cartasEnMesa[x].GetComponent<Hospital>().asesinado = false;
						object[] datas2 = new object[] {"Usó", "69",""};
						PhotonNetwork.RaiseEvent(76, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
						delay = 1f;
					}

				}
				asesinado = false;
				personajeAsesinado = 10;
			}

		}

	}

	public void Bypass()
	{
		if( bypass == true && elegirODescartar == true && oroOcartas == true && cartasRecibidasEnTurno.Count > 1 && delay == 0f)
		{
			for(int x = 0; x < cartasRecibidasEnTurno.Count; x++)
			{
				GameObject temp;
				temp = Instantiate(prefabCartaMano, _content);
				temp.GetComponent<RawImage>().texture = mazoCartas[cartasRecibidasEnTurno[x]];
				temp.GetComponent<CartaMano>().id = cartasRecibidasEnTurno[x];
				temp.GetComponent<CartaMano>()._mainGame = this;
				cartasEnLaMano.Add(temp);
				object[] datas2 = new object[] {PhotonNetwork.LocalPlayer.NickName, cartasEnLaMano.Count.ToString()};
				PhotonNetwork.RaiseEvent(82, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			}
			object[] datas3 = new object[] {"Usó", "57",""};
			PhotonNetwork.RaiseEvent(76, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			delay = 1f;
			elegirODescartar = false;
			habilidad = true;
			personajesPanel.SetActive(false);
		}

	}

	public void puntuaje()
	{
		int temp;
		temp = 0;
		for(int x = 0; x < _lector.cartasEnMesa.Count; x++)
		{
			temp += _lector.cartasEnMesa[x].GetComponent<CartaEnMesa>().puntos;
		}
		puntos.GetComponent<Text>().text = temp.ToString()+"P";
		string temp2;
		temp2 = temp.ToString()+"P";
		object[] datas = new object[] {PhotonNetwork.LocalPlayer.NickName, temp2};
		PhotonNetwork.RaiseEvent(32, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
	}

	public void ActivarBotonesDeCartas()
	{
		if(cartasActivadas == 0)
		{
			for(int x = 0; x < cartasEnLaMano.Count; x++)
			{
				cartasEnLaMano[x].GetComponent<Button>().enabled = true;
			}
			cartasActivadas = 1;
		}
	}


	public void personajesDescartadosDestroy()
	{
		for(int x = 0; x < personajesDescartadosLista.Count; x++)
		{
			Destroy(personajesDescartadosLista[x]);
		}
		personajesDescartadosLista.Clear();
		personajesDescartados.Clear();
		once = 0;
	}


	public void personajesDescartadosInstantiate()
	{

		if(once == 0)
		{
			if(personajesDescartados.Count == 4)
			{
				personajeDescartadoPanel.SetActive(true);
				for(int x = 0; x < personajesDescartados.Count-1; x++)
				{
					GameObject temp;
					temp = Instantiate(personajeDescartadoPrebab, persoanjeDescartadoContent);
					personajesDescartadosLista.Add(temp);
					temp.GetComponent<Image>().sprite = personajesDescartados[x];
				}
				GameObject temp2;
				temp2= Instantiate(personajeDescartadoPrebab, persoanjeDescartadoContent);
				personajesDescartadosLista.Add(temp2);
				temp2.GetComponent<Image>().sprite = dorso;
				once = 1;
			}

			if(personajesDescartados.Count == 3)
			{
				personajeDescartadoPanel.SetActive(true);
				for(int x = 0; x < personajesDescartados.Count-1; x++)
				{
					GameObject temp;
					temp = Instantiate(personajeDescartadoPrebab, persoanjeDescartadoContent);
					personajesDescartadosLista.Add(temp);
					temp.GetComponent<Image>().sprite = personajesDescartados[x];
				}
				GameObject temp2;
				temp2= Instantiate(personajeDescartadoPrebab, persoanjeDescartadoContent);
				personajesDescartadosLista.Add(temp2);
				temp2.GetComponent<Image>().sprite = dorso;
				once = 1;
			}

			if(personajesDescartados.Count == 2)
			{
				personajeDescartadoPanel.SetActive(true);
				for(int x = 0; x < personajesDescartados.Count-1; x++)
				{
					GameObject temp;
					temp = Instantiate(personajeDescartadoPrebab, persoanjeDescartadoContent);
					personajesDescartadosLista.Add(temp);
					temp.GetComponent<Image>().sprite = personajesDescartados[x];
				}
				GameObject temp2;
				temp2= Instantiate(personajeDescartadoPrebab, persoanjeDescartadoContent);
				personajesDescartadosLista.Add(temp2);
				temp2.GetComponent<Image>().sprite = dorso;
				once = 1;
			}
				
			if(personajesDescartados.Count == 1)
			{
				personajeDescartadoPanel.SetActive(true);

				GameObject temp2;
				temp2= Instantiate(personajeDescartadoPrebab, persoanjeDescartadoContent);
				personajesDescartadosLista.Add(temp2);
				temp2.GetComponent<Image>().sprite = dorso;
				once = 1;
			}
		}
	}
}
