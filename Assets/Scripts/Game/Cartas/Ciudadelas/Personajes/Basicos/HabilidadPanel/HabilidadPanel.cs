using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class HabilidadPanel : MonoBehaviour
{
	public GameObject disabler;
	public GameObject faro;
	public GameObject faroPrefab;
	public GameObject laboratorioPrefab;
	public GameObject museoPrefab;
	public GameObject polvorinPrefab;
	public Transform faroContent;

	public Text text;
	public Text text2;
	public GameObject botonDescarte;
	public GameObject botonDescarteCancelar;
	public List<GameObject> cartasEnBoton = new List<GameObject>();

	public List<GameObject> cartasEnMano = new List<GameObject>();
	public List<int> cartasEnManoID = new List<int>();

	public bool saveCards = false;
	public int number;
	public GameObject cartaZoom;
	public GameObject cartaZoomSprite;

	public GameObject habilidadPanel;
	public GameObject habilidadPanel2;

	public GameObject reference;

	public GameObject habilidadBoton;
	public MainGame _mainGame;
	public List<GameObject> personajes = new List<GameObject>();
	public List<GameObject> jugadores = new List<GameObject>();
	public GameObject prefab;
	public GameObject prefab2;
	public Transform content;
	public Transform content2;
	public int tempID;
	public string name;
	public bool sent = false;
	public int once = 0;
	public bool click = false;


	public void Start()
	{
		sent = false;
		once = 0;
		click = false;
		saveCards = false;

	}

	public void Update()
	{

		if(_mainGame.habilidad == false)
		{
			habilidadBoton.SetActive(false);
		}

	}

	public void usoDeHabilidad ()
	{
		habilidadPanel.SetActive(true);

		if(once == 0)
		{
			for(int x  = number; x < 8; x++)
			{
				GameObject temp;
				temp = Instantiate(prefab, content);
				personajes.Add(temp);
				temp.GetComponent<personajeHabilidadButton>()._habilidadPanel = this;
				temp.GetComponent<personajeHabilidadButton>().id = x;
				temp.GetComponent<Image>().sprite = _mainGame.mazoPersonajes[x];

			}
		}
		once = 1;
	}

	public void usoDeHabilidad2 ()
	{
		habilidadPanel2.SetActive(true);

		if(once == 0)
		{
			for(int x  = 0; x < _mainGame._sendInterfaceInfo.playerNames.Count; x++)
			{
				GameObject temp;
				temp = Instantiate(prefab2, content2);
				jugadores.Add(temp);
				temp.GetComponentInChildren<Text>().text = _mainGame._sendInterfaceInfo.playerNames[x];
				temp.GetComponent<JugadorHabilidadPrefab>()._habilidadPanel = this;
				temp.GetComponent<JugadorHabilidadPrefab>().name = _mainGame._sendInterfaceInfo.playerNames[x];

			}
			GameObject temp2;
			temp2 = Instantiate(prefab2, content2);
			jugadores.Add(temp2);
			temp2.GetComponentInChildren<Text>().text = "Mazo";
			temp2.GetComponent<JugadorHabilidadPrefab>().name = "Mazo";
			temp2.GetComponent<JugadorHabilidadPrefab>()._habilidadPanel = this;
			GameObject temp3;
			temp3 = Instantiate(prefab2, content2);
			jugadores.Add(temp3);
			temp3.GetComponentInChildren<Text>().text = "Cancelar";
			temp3.GetComponent<JugadorHabilidadPrefab>().name = "Cancelar";
			temp3.GetComponent<JugadorHabilidadPrefab>()._habilidadPanel = this;
		}
		once = 1;
	}

	public void OnClick()
	{
		click = true;

	}

	public void Confirmar()
	{
		for(int x = 0; x < cartasEnBoton.Count; x++)
		{
			int temp;
			temp = int.Parse(cartasEnBoton[x].name);
			cartasEnManoID.Add(temp);
			Destroy(cartasEnBoton[x]);
			Destroy(cartasEnMano[x]);
		}
		cartasEnMano.Clear();
		cartasEnBoton.Clear();
		sent = true;

	}

	public void Cancelar()
	{
		for(int x = 0; x < cartasEnMano.Count; x++)
		{
			_mainGame.cartasEnLaMano.Add(cartasEnMano[x]);
			Destroy(cartasEnBoton[x]);
			cartasEnMano[x].SetActive(true);
		}
		cartasEnMano.Clear();
		cartasEnManoID.Clear();
		cartasEnBoton.Clear();
		botonDescarteCancelar.SetActive(false);
		botonDescarte.SetActive(false);
		text2.text = "";
		click = false;
		name = "";
		once = 0;
		tempID = 0;

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
		if(obj.Code == 95)
		{

			object[] datas = (object[])obj.CustomData;

			if((string)datas[0] == PhotonNetwork.LocalPlayer.NickName)
			{
				int temp;
				temp = int.Parse((string)datas[1]);
				_mainGame.oro += temp;
				object[] datas3 = new object[] {PhotonNetwork.LocalPlayer.NickName, _mainGame.oro.ToString()};
				PhotonNetwork.RaiseEvent(81, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);

			}
		}
	}
}
