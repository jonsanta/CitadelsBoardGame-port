using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Polvorin : MonoBehaviour
{
private GameObject habilidadUI;

	public HabilidadPanel _habilidad;
	public List<GameObject> cartas = new List<GameObject>();
	public List<GameObject> jugadores = new List<GameObject>();
	public int once = 0;
	public bool usado = false;
	public bool terminarHabilidad = false;

	public void Update()
	{
		GetComponent<Button>().onClick.AddListener(OnClick);

		if(terminarHabilidad == true)
		{
			object[] datas3 = new object[] {PhotonNetwork.LocalPlayer.NickName, "74", "no"};
			PhotonNetwork.RaiseEvent(11, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			GameObject temp;
			temp = this.gameObject;
			GetComponent<CartaEnMesa>()._mainGame._lector.cartasEnMesa.Remove(temp);
			Destroy(this.gameObject);
		}
	}

	public void OnClick()
	{
		for(int x = 0; x < GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count; x++)
		{
			GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano[x].GetComponent<Button>().enabled = false;
		}

		if(GetComponent<CartaEnMesa>()._mainGame.esMiTurno == true && usado == false)
		{
			_habilidad = GameObject.FindWithTag("habilidad").GetComponent<HabilidadPanel>();
			_habilidad.disabler.SetActive(true);
			_habilidad.faro.SetActive(true);

			if(once == 0)
			{
				for(int x  = 0; x < GetComponent<CartaEnMesa>()._mainGame._sendInterfaceInfo.playerNames.Count; x++)
				{
					GameObject temp;
					temp = Instantiate(_habilidad.polvorinPrefab, _habilidad.faroContent);
					jugadores.Add(temp);
					temp.GetComponentInChildren<Text>().text =  GetComponent<CartaEnMesa>()._mainGame._sendInterfaceInfo.playerNames[x];
					temp.GetComponent<BotonPolvorin>().name =  GetComponent<CartaEnMesa>()._mainGame._sendInterfaceInfo.playerNames[x];
					temp.GetComponent<BotonPolvorin>()._playersInterface =  GetComponent<CartaEnMesa>()._mainGame._sendInterfaceInfo.mesas[x].GetComponent<PlayersInterface>();
					temp.GetComponent<BotonPolvorin>()._polvorin = this.gameObject;
				}

				GameObject temp3;
				temp3 = Instantiate(_habilidad.polvorinPrefab, _habilidad.faroContent);
				jugadores.Add(temp3);
				temp3.GetComponentInChildren<Text>().text = PhotonNetwork.LocalPlayer.NickName;
				temp3.GetComponent<BotonPolvorin>().name = PhotonNetwork.LocalPlayer.NickName;
				temp3.GetComponent<BotonPolvorin>()._polvorin = this.gameObject;

				GameObject temp2;
				temp2 = Instantiate(_habilidad.polvorinPrefab, _habilidad.faroContent);
				jugadores.Add(temp2);
				temp2.GetComponentInChildren<Text>().text = "Cancelar";
				temp2.GetComponent<BotonPolvorin>().name = "Cancelar";
				temp2.GetComponent<BotonPolvorin>()._polvorin = this.gameObject;
			}
			once = 1;
			usado = true;
		}
	}
}

