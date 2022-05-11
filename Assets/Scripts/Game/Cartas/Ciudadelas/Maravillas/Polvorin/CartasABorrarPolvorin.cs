using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class CartasABorrarPolvorin : MonoBehaviour
{

	public string name;
	public PlayersInterface _playersInterface;
	public Polvorin _polvorin;
	public GameObject _polvorinObject;
	public string playerName;
    // Start is called before the first frame update
	public void OnClick()
	{
		if(name == "Cancelar")
		{
			for(int x = 0; x < _polvorinObject.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count; x++)
			{
				_polvorinObject.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano[x].GetComponent<Button>().enabled = true;
			}

			for(int x = 0; x < _polvorin.cartas.Count; x++)
			{
				Destroy(_polvorin.cartas[x]);
			}
				
			_polvorin.jugadores.Clear();
			_polvorin.cartas.Clear();
			_polvorin.once = 0;
			_polvorin.usado = false;
			_polvorin._habilidad.disabler.SetActive(false);
			_polvorin._habilidad.faro.SetActive(false);
			Destroy(this.gameObject);
		}

		if(playerName == PhotonNetwork.LocalPlayer.NickName)
		{
			
			GameObject temp;

			for(int x = 0; x < _polvorinObject.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count; x++)
			{
				_polvorinObject.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano[x].GetComponent<Button>().enabled = true;
			}

			for(int z = 0; z < _polvorin.GetComponent<CartaEnMesa>()._mainGame._lector.cartasEnMesa.Count; z++)
			{
				if(_polvorin.GetComponent<CartaEnMesa>()._mainGame._lector.cartasEnMesa[z].GetComponent<CartaEnMesa>().id.ToString() == name)
				{
					temp = _polvorin.GetComponent<CartaEnMesa>()._mainGame._lector.cartasEnMesa[z];
					_polvorin.GetComponent<CartaEnMesa>()._mainGame._lector.cartasEnMesa.Remove(temp);
					Destroy(temp);
				}
			}

			object[] datas3 = new object[] {playerName, name, "no"};
			PhotonNetwork.RaiseEvent(11, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			object[] datas2 = new object[] {playerName, name, "no"};
			PhotonNetwork.RaiseEvent(12, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			object[] datas5 = new object[] {"Destruye :",name ,PhotonNetwork.LocalPlayer.NickName};
			PhotonNetwork.RaiseEvent(76, datas5, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			_polvorinObject.GetComponent<CartaEnMesa>()._mainGame.delay = 1f;

			for(int x = 0; x < _polvorin.cartas.Count; x++)
			{
				Destroy(_polvorin.cartas[x]);
			}

			_polvorin.jugadores.Clear();
			_polvorin.cartas.Clear();
			_polvorin._habilidad.disabler.SetActive(false);
			_polvorin._habilidad.faro.SetActive(false);
			_polvorin.terminarHabilidad = true;
			Destroy(this.gameObject);

		}

		else if (playerName != "")
		{
			
			GameObject temp;

			for(int x = 0; x < _polvorinObject.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count; x++)
			{
				_polvorinObject.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano[x].GetComponent<Button>().enabled = true;
			}

			for(int z = 0; z < _playersInterface.cartasEnMesa.Count; z++)
			{
				if(_playersInterface.cartasEnMesa[z].name == name)
				{
					temp = _playersInterface.cartasEnMesa[z];
					_playersInterface.cartasEnMesa.Remove(temp);
					Destroy(temp);
				}
			}

			object[] datas3 = new object[] {playerName, name, "no"};
			PhotonNetwork.RaiseEvent(11, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			object[] datas2 = new object[] {playerName, name, "no"};
			PhotonNetwork.RaiseEvent(12, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			object[] datas5 = new object[] {"Destruye :",name ,_playersInterface.playerName};
			PhotonNetwork.RaiseEvent(76, datas5, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			_polvorinObject.GetComponent<CartaEnMesa>()._mainGame.delay = 1f;

			for(int x = 0; x < _polvorin.cartas.Count; x++)
			{
				Destroy(_polvorin.cartas[x]);
			}

			_polvorin.jugadores.Clear();
			_polvorin.cartas.Clear();
			_polvorin._habilidad.disabler.SetActive(false);
			_polvorin._habilidad.faro.SetActive(false);
			_polvorin.terminarHabilidad = true;
			Destroy(this.gameObject);

		}

	}
}
