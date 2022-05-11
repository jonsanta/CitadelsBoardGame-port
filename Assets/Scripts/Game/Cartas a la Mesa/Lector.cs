using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Lector : MonoBehaviour
{   //construir
	public int numeroConstruido;
	public int construccionesPermitidas;
	public GameObject construir;
	public GameObject cancelar;
	public GameObject carta;
	public GameObject cartaPrefab;
	public GameObject cartaPrefabContent;

	//Lista de Cartas en la mesa
	public List<GameObject> cartasEnMesa = new List<GameObject>();


	//lector de TXT
	public List<string> costes;
	public List<string> colores;
	public List<string> costesMaravilla;
	public string FileName;
	public string FileName2;
	public string FileName3;

	private TextAsset textAsset;
	private TextAsset textAsset2;
	private TextAsset textAsset3;
    // Start is called before the first frame update

    void Start()
    {
		textAsset = Resources.Load("textFiles/" + FileName) as TextAsset;
		textAsset2 = Resources.Load("textFiles/" + FileName2) as TextAsset;
		textAsset3 = Resources.Load("textFiles/" + FileName3) as TextAsset;
		readTextFile();
		construccionesPermitidas = 1;
    }

	void Update()
	{
		if(numeroConstruido == construccionesPermitidas)
		{
			Cancelar();
			carta = null;
			GetComponent<MainGame>().construir = false;
		}
		if(GetComponent<MainGame>().esMiTurno == false)
		{
			numeroConstruido = 0;
			construccionesPermitidas = 1;
			carta = null;
			GetComponent<MainGame>().construir = false;
			Cancelar();
		}

		if(GetComponent<MainGame>().construir == false)
		{
			carta = null;
			Cancelar();
		}

	}

	public void readTextFile()
	{
		costes = textAsset.text.Split ('\n').ToList();
		costesMaravilla = textAsset2.text.Split ('\n').ToList();
		colores = textAsset3.text.Split ('\n').ToList();
	}

	public void ActivarInterfaz()
	{
		construir.SetActive(true);
		cancelar.SetActive(true);
	}

	public void Construir()
	{
		if(carta.GetComponent<CartaMano>().id < 57)
		{
			object[] datas5 = new object[] {"Construye :",carta.GetComponent<CartaMano>().id.ToString() ,""};
			PhotonNetwork.RaiseEvent(76, datas5, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			GetComponent<MainGame>().oro -= carta.GetComponent<CartaMano>().coste;
			GameObject temp;
			temp = Instantiate(cartaPrefab, cartaPrefabContent.transform);
			cartasEnMesa.Add(temp);
			temp.GetComponent<RawImage>().texture = GetComponent<MainGame>().mazoCartas[carta.GetComponent<CartaMano>().id];
			temp.GetComponent<CartaEnMesa>().id = carta.GetComponent<CartaMano>().id;
			temp.GetComponent<CartaEnMesa>()._mainGame = GetComponent<MainGame>();
			temp.GetComponent<CartaEnMesa>().puntos = carta.GetComponent<CartaMano>().puntos;
			temp.GetComponent<CartaEnMesa>().name = carta.GetComponent<CartaMano>().name;
			temp.name = temp.GetComponent<CartaEnMesa>().name;
			temp.GetComponent<CartaEnMesa>().color = carta.GetComponent<CartaMano>().color;
			GetComponent<MainGame>().cartasEnLaMano.Remove(carta);
			object[] datas3 = new object[] {PhotonNetwork.LocalPlayer.NickName, carta.GetComponent<CartaMano>().id.ToString()};
			PhotonNetwork.RaiseEvent(84, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			Destroy(carta);
			object[] datas2 = new object[] {PhotonNetwork.LocalPlayer.NickName, GetComponent<MainGame>().cartasEnLaMano.Count.ToString()};
			PhotonNetwork.RaiseEvent(82, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			int temp4;
			temp4 = -carta.GetComponent<CartaMano>().coste;
			object[] datas4 = new object[] {PhotonNetwork.LocalPlayer.NickName, GetComponent<MainGame>().oro.ToString()};
			PhotonNetwork.RaiseEvent(81, datas4, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			construir.SetActive(false);
			cancelar.SetActive(false);
			numeroConstruido +=1;
			GetComponent<MainGame>().delay = 1f;
		}

		if(carta.GetComponent<CartaMano>().id >= 57)
		{
			object[] datas5 = new object[] {"Construye :",carta.GetComponent<CartaMano>().id.ToString() ,""};
			PhotonNetwork.RaiseEvent(76, datas5, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			GetComponent<MainGame>().oro -= carta.GetComponent<CartaMano>().coste-carta.GetComponent<CartaMano>().precioReducido;
			GameObject temp;
			temp = Instantiate(cartaPrefab, cartaPrefabContent.transform);
			cartasEnMesa.Add(temp);
			temp.GetComponent<RawImage>().texture = GetComponent<MainGame>().mazoCartas[carta.GetComponent<CartaMano>().id];
			temp.GetComponent<CartaEnMesa>().id = carta.GetComponent<CartaMano>().id;
			temp.GetComponent<CartaEnMesa>()._mainGame = GetComponent<MainGame>();
			temp.GetComponent<CartaEnMesa>().puntos = carta.GetComponent<CartaMano>().puntos;
			temp.GetComponent<CartaEnMesa>().violeta = 1;
			temp.GetComponent<CartaEnMesa>().name = carta.GetComponent<CartaMano>().name;
			temp.name = temp.GetComponent<CartaEnMesa>().name;
			temp.GetComponent<CartaEnMesa>().color = carta.GetComponent<CartaMano>().color;
			GetComponent<MainGame>().cartasEnLaMano.Remove(carta);
			object[] datas3 = new object[] {PhotonNetwork.LocalPlayer.NickName, carta.GetComponent<CartaMano>().id.ToString()};
			PhotonNetwork.RaiseEvent(84, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			Destroy(carta);
			object[] datas2 = new object[] {PhotonNetwork.LocalPlayer.NickName, GetComponent<MainGame>().cartasEnLaMano.Count.ToString()};
			PhotonNetwork.RaiseEvent(82, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			int temp4;
			temp4 = -carta.GetComponent<CartaMano>().coste;
			object[] datas4 = new object[] {PhotonNetwork.LocalPlayer.NickName, GetComponent<MainGame>().oro.ToString()};
			PhotonNetwork.RaiseEvent(81, datas4, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			construir.SetActive(false);
			cancelar.SetActive(false);
			numeroConstruido +=1;
			GetComponent<MainGame>().delay = 1f;
		}
	}

	public void Cancelar()
	{
		construir.SetActive(false);
		cancelar.SetActive(false);
		carta = null;
	}
}
