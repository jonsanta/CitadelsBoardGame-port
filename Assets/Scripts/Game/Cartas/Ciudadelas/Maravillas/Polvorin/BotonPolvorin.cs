using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class BotonPolvorin : MonoBehaviour
{
	public GameObject cartaPrefab;
	public GameObject _polvorin;
	public string name;
	public PlayersInterface _playersInterface;
	public int tempResultado;


	public void OnClick()
	{
		if(name == "Cancelar")
		{
			for(int x = 0; x < _polvorin.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count; x++)
			{
				_polvorin.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano[x].GetComponent<Button>().enabled = true;
			}

			for(int x = 0; x < _polvorin.GetComponent<Polvorin>().jugadores.Count; x++)
			{
				Destroy(_polvorin.GetComponent<Polvorin>().jugadores[x]);
			}
			_polvorin.GetComponent<Polvorin>().jugadores.Clear();
			_polvorin.GetComponent<Polvorin>().once = 0;
			_polvorin.GetComponent<Polvorin>().usado = false;
			_polvorin.GetComponent<Polvorin>()._habilidad.disabler.SetActive(false);
			_polvorin.GetComponent<Polvorin>()._habilidad.faro.SetActive(false);
			Destroy(this.gameObject);
		}

		else if(name == PhotonNetwork.LocalPlayer.NickName)
		{
			for(int x = 0; x < _polvorin.GetComponent<CartaEnMesa>()._mainGame._lector.cartasEnMesa.Count; x++)
			{
				if(_polvorin.GetComponent<CartaEnMesa>()._mainGame._lector.cartasEnMesa[x].name != "Polvorin")
				{
					GameObject temp;
					temp = Instantiate(cartaPrefab, _polvorin.GetComponent<Polvorin>()._habilidad.faroContent);
					temp.GetComponentInChildren<Text>().text = "";
					temp.GetComponent<RawImage>().texture = _polvorin.GetComponent<CartaEnMesa>()._mainGame.mazoCartas[_polvorin.GetComponent<CartaEnMesa>()._mainGame._lector.cartasEnMesa[x].GetComponent<CartaEnMesa>().id];
					temp.GetComponent<CartasABorrarPolvorin>().name = _polvorin.GetComponent<CartaEnMesa>()._mainGame._lector.cartasEnMesa[x].GetComponent<CartaEnMesa>().id.ToString();
					temp.GetComponent<CartasABorrarPolvorin>()._playersInterface = _playersInterface;
					temp.GetComponent<CartasABorrarPolvorin>()._polvorinObject = _polvorin;
					temp.GetComponent<CartasABorrarPolvorin>()._polvorin = _polvorin.GetComponent<Polvorin>();
					temp.GetComponent<CartasABorrarPolvorin>().playerName = GetComponentInChildren<Text>().text;
					_polvorin.GetComponent<Polvorin>().cartas.Add(temp);
				}
			}

			GameObject temp2;
			temp2 = Instantiate(cartaPrefab, _polvorin.GetComponent<Polvorin>()._habilidad.faroContent);
			temp2.GetComponentInChildren<Text>().text = "Cancelar";
			temp2.GetComponent<CartasABorrarPolvorin>().name = "Cancelar";
			temp2.GetComponent<CartasABorrarPolvorin>()._playersInterface = _playersInterface;
			temp2.GetComponent<CartasABorrarPolvorin>()._polvorinObject = _polvorin;
			temp2.GetComponent<CartasABorrarPolvorin>()._polvorin = _polvorin.GetComponent<Polvorin>();
			Destroy(temp2.GetComponent<MouseOver>());
			_polvorin.GetComponent<Polvorin>().cartas.Add(temp2);


			for(int x = 0; x < _polvorin.GetComponent<Polvorin>().jugadores.Count; x++)
			{
				Destroy(_polvorin.GetComponent<Polvorin>().jugadores[x]);
			}
			Destroy(this.gameObject);

		}
		else
		{
			for(int x = 0; x < _playersInterface.cartasEnMesa.Count; x++)
			{

				GameObject temp;
				temp = Instantiate(cartaPrefab, _polvorin.GetComponent<Polvorin>()._habilidad.faroContent);
				temp.GetComponentInChildren<Text>().text = "";
				temp.GetComponent<RawImage>().texture = _polvorin.GetComponent<CartaEnMesa>()._mainGame.mazoCartas[int.Parse(_playersInterface.cartasEnMesa[x].name)];
				temp.GetComponent<CartasABorrarPolvorin>().name = _playersInterface.cartasEnMesa[x].name;
				temp.GetComponent<CartasABorrarPolvorin>()._playersInterface = _playersInterface;
				temp.GetComponent<CartasABorrarPolvorin>()._polvorinObject = _polvorin;
				temp.GetComponent<CartasABorrarPolvorin>()._polvorin = _polvorin.GetComponent<Polvorin>();
				temp.GetComponent<CartasABorrarPolvorin>().playerName = GetComponentInChildren<Text>().text;
				_polvorin.GetComponent<Polvorin>().cartas.Add(temp);

			}

			GameObject temp2;
			temp2 = Instantiate(cartaPrefab, _polvorin.GetComponent<Polvorin>()._habilidad.faroContent);
			temp2.GetComponentInChildren<Text>().text = "Cancelar";
			temp2.GetComponent<CartasABorrarPolvorin>().name = "Cancelar";
			temp2.GetComponent<CartasABorrarPolvorin>()._playersInterface = _playersInterface;
			temp2.GetComponent<CartasABorrarPolvorin>()._polvorinObject = _polvorin;
			temp2.GetComponent<CartasABorrarPolvorin>()._polvorin = _polvorin.GetComponent<Polvorin>();
			Destroy(temp2.GetComponent<MouseOver>());
			_polvorin.GetComponent<Polvorin>().cartas.Add(temp2);

			for(int x = 0; x < _polvorin.GetComponent<Polvorin>().jugadores.Count; x++)
			{
				Destroy(_polvorin.GetComponent<Polvorin>().jugadores[x]);
			}
			Destroy(this.gameObject);
		}

	}
}

