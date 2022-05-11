using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class HabilidadPanelGuerrero : MonoBehaviour
{
	public bool terminarHabilidad = false;

	public GameObject habilidadBoton;
	public GameObject prefab;
	public GameObject habilidadPanel;
	public Transform content;
	public MainGame _mainGame;
	public int once;

	public bool click = false;

	public List<GameObject> jugadores = new List<GameObject>();
	public List<GameObject> cardsToRemove = new List<GameObject>();

	void Start()
	{
		once = 0;
		click = false;
		terminarHabilidad = false;
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
			for(int x  = 0; x < _mainGame._sendInterfaceInfo.playerNames.Count; x++)
			{
				if(_mainGame._sendInterfaceInfo.mesas[x].GetComponent<PlayersInterface>().personaje.sprite.name != "Obispo")
				{
					GameObject temp;
					temp = Instantiate(prefab, content);
					jugadores.Add(temp);
					temp.GetComponentInChildren<Text>().text = _mainGame._sendInterfaceInfo.playerNames[x];
					temp.GetComponent<BotonGuerrero>().name = _mainGame._sendInterfaceInfo.playerNames[x];
					temp.GetComponent<BotonGuerrero>()._playersInterface = _mainGame._sendInterfaceInfo.mesas[x].GetComponent<PlayersInterface>();
					temp.GetComponent<BotonGuerrero>()._habilidadPanelGuerrero = this;
				}

			}

			GameObject temp3;
			temp3 = Instantiate(prefab, content);
			jugadores.Add(temp3);
			temp3.GetComponentInChildren<Text>().text = PhotonNetwork.LocalPlayer.NickName;
			temp3.GetComponent<BotonGuerrero>().name = PhotonNetwork.LocalPlayer.NickName;
			temp3.GetComponent<BotonGuerrero>()._habilidadPanelGuerrero = this;

			GameObject temp2;
			temp2 = Instantiate(prefab, content);
			jugadores.Add(temp2);
			temp2.GetComponentInChildren<Text>().text = "Cancelar";
			temp2.GetComponent<BotonGuerrero>().name = "Cancelar";
			temp2.GetComponent<BotonGuerrero>()._habilidadPanelGuerrero = this;
		}
		once = 1;
	}
		
	public void OnClick()
	{
		click = true;
	}
}
