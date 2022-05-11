using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class BotonGuerrero : MonoBehaviour
{
	public int precioEstablecido;
	public GameObject cartaPrefab;
	public string name;
	public HabilidadPanelGuerrero _habilidadPanelGuerrero;
	public PlayersInterface _playersInterface;
	public int tempResultado;

	public int precio = 1;

    // Start is called before the first frame update
    void Start()
    {
		precio = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnClick()
	{
		if(name == "Cancelar")
		{
			for(int x = 0; x < _habilidadPanelGuerrero.jugadores.Count; x++)
			{
				Destroy(_habilidadPanelGuerrero.jugadores[x]);
			}
			_habilidadPanelGuerrero.jugadores.Clear();
			_habilidadPanelGuerrero.once = 0;
			_habilidadPanelGuerrero.click = false;
			_habilidadPanelGuerrero.habilidadPanel.SetActive(false);
			Destroy(this.gameObject);
		}

		else if(name == PhotonNetwork.LocalPlayer.NickName)
		{
			precioEstablecido = 0;
			for(int x = 0; x < _habilidadPanelGuerrero._mainGame._lector.cartasEnMesa.Count; x++)
			{
				for(int z = 0; z < _habilidadPanelGuerrero._mainGame._lector.cartasEnMesa.Count; z++)
				{
					if(_habilidadPanelGuerrero._mainGame._lector.cartasEnMesa[z].name == "Gran Muralla")
					{
						precioEstablecido = 1;
					}

				}

				if(precioEstablecido == 1)
				{
					precio = 0;
				}else
				{
					precio = 1;
				}

				int tempCoste;
				tempCoste = int.Parse(_habilidadPanelGuerrero._mainGame._lector.costes[_habilidadPanelGuerrero._mainGame._lector.cartasEnMesa[x].GetComponent<CartaEnMesa>().id]);
				tempResultado = tempCoste - precio;
				if(tempResultado <= _habilidadPanelGuerrero._mainGame.oro)
				{
					if(_habilidadPanelGuerrero._mainGame._lector.cartasEnMesa[x].name != "Torreon")
					{
						GameObject temp;
						temp = Instantiate(cartaPrefab, _habilidadPanelGuerrero.content);
						temp.GetComponentInChildren<Text>().text = "";
						temp.GetComponent<RawImage>().texture = _habilidadPanelGuerrero._mainGame.mazoCartas[_habilidadPanelGuerrero._mainGame._lector.cartasEnMesa[x].GetComponent<CartaEnMesa>().id];
						temp.GetComponent<cartasAborrar>().name = _habilidadPanelGuerrero._mainGame._lector.cartasEnMesa[x].GetComponent<CartaEnMesa>().id.ToString();
						temp.GetComponent<cartasAborrar>().coste = int.Parse(_habilidadPanelGuerrero._mainGame._lector.costes[_habilidadPanelGuerrero._mainGame._lector.cartasEnMesa[x].GetComponent<CartaEnMesa>().id]);
						temp.GetComponent<cartasAborrar>()._playersInterface = _playersInterface;
						temp.GetComponent<cartasAborrar>().precio = precio;
						temp.GetComponent<cartasAborrar>()._habilidadPanelGuerrero = _habilidadPanelGuerrero;
						temp.GetComponent<cartasAborrar>().playerName = GetComponentInChildren<Text>().text;
						_habilidadPanelGuerrero.cardsToRemove.Add(temp);
					}
				}

			}

			GameObject temp2;
			temp2 = Instantiate(cartaPrefab, _habilidadPanelGuerrero.content);
			temp2.GetComponentInChildren<Text>().text = "Cancelar";
			temp2.GetComponent<cartasAborrar>().name = "Cancelar";
			temp2.GetComponent<cartasAborrar>()._playersInterface = _playersInterface;
			temp2.GetComponent<cartasAborrar>()._habilidadPanelGuerrero = _habilidadPanelGuerrero;
			Destroy(temp2.GetComponent<MouseOver>());
			_habilidadPanelGuerrero.cardsToRemove.Add(temp2);


			for(int x = 0; x < _habilidadPanelGuerrero.jugadores.Count; x++)
			{
				Destroy(_habilidadPanelGuerrero.jugadores[x]);
			}
			Destroy(this.gameObject);

		}
		else
		{
			for(int x = 0; x < _playersInterface.cartasEnMesa.Count; x++)
			{

				for(int z = 0; z < _playersInterface.cartasEnMesa.Count; z++)
				{
					if(_playersInterface.cartasEnMesa[z].name == "66")
					{
						precioEstablecido = 1;
					}

				}

				if(precioEstablecido == 1)
				{
					precio = 0;
				}else
				{
					precio = 1;
				}

				int tempCoste;
				tempCoste = int.Parse(_habilidadPanelGuerrero._mainGame._lector.costes[int.Parse(_playersInterface.cartasEnMesa[x].name)]);
				tempResultado = tempCoste - precio;
				if( tempResultado <= _habilidadPanelGuerrero._mainGame.oro)
				{
	
					if(_playersInterface.cartasEnMesa[x].name != "80")
					{
						GameObject temp;
						temp = Instantiate(cartaPrefab, _habilidadPanelGuerrero.content);
						temp.GetComponentInChildren<Text>().text = "";
						temp.GetComponent<RawImage>().texture = _habilidadPanelGuerrero._mainGame.mazoCartas[int.Parse(_playersInterface.cartasEnMesa[x].name)];
						temp.GetComponent<cartasAborrar>().name = _playersInterface.cartasEnMesa[x].name;
						temp.GetComponent<cartasAborrar>().coste = int.Parse(_habilidadPanelGuerrero._mainGame._lector.costes[int.Parse(_playersInterface.cartasEnMesa[x].name)]);
						temp.GetComponent<cartasAborrar>()._playersInterface = _playersInterface;
						temp.GetComponent<cartasAborrar>().precio = precio;
						temp.GetComponent<cartasAborrar>()._habilidadPanelGuerrero = _habilidadPanelGuerrero;
						temp.GetComponent<cartasAborrar>().playerName = GetComponentInChildren<Text>().text;
						_habilidadPanelGuerrero.cardsToRemove.Add(temp);
					}
				}

			}

			GameObject temp2;
			temp2 = Instantiate(cartaPrefab, _habilidadPanelGuerrero.content);
			temp2.GetComponentInChildren<Text>().text = "Cancelar";
			temp2.GetComponent<cartasAborrar>().name = "Cancelar";
			temp2.GetComponent<cartasAborrar>()._playersInterface = _playersInterface;
			temp2.GetComponent<cartasAborrar>()._habilidadPanelGuerrero = _habilidadPanelGuerrero;
			Destroy(temp2.GetComponent<MouseOver>());
			_habilidadPanelGuerrero.cardsToRemove.Add(temp2);

			for(int x = 0; x < _habilidadPanelGuerrero.jugadores.Count; x++)
			{
				Destroy(_habilidadPanelGuerrero.jugadores[x]);
			}
			Destroy(this.gameObject);
		}

	}
}
