using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class cartasAborrar : MonoBehaviour
{
	public int precio;
	public string name;
	public int coste;
	public string playerName;
	public HabilidadPanelGuerrero _habilidadPanelGuerrero;
	public PlayersInterface _playersInterface;
    // Start is called before the first frame update


	public void OnClick()
	{
		if(name == "Cancelar")
		{
			for(int x = 0; x < _habilidadPanelGuerrero.cardsToRemove.Count; x++)
			{
				Destroy(_habilidadPanelGuerrero.cardsToRemove[x]);
			}
			_habilidadPanelGuerrero.jugadores.Clear();
			_habilidadPanelGuerrero.cardsToRemove.Clear();
			_habilidadPanelGuerrero.once = 0;
			_habilidadPanelGuerrero.click = false;
			_habilidadPanelGuerrero.habilidadPanel.SetActive(false);
			Destroy(this.gameObject);
		}

		if(playerName == PhotonNetwork.LocalPlayer.NickName)
		{
			_habilidadPanelGuerrero._mainGame.oro -= (coste-precio);
			object[] datas = new object[] {PhotonNetwork.LocalPlayer.NickName, _habilidadPanelGuerrero._mainGame.oro.ToString()};
			PhotonNetwork.RaiseEvent(81, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			GameObject temp;

			for(int z = 0; z < _habilidadPanelGuerrero._mainGame._lector.cartasEnMesa.Count; z++)
			{
				if(_habilidadPanelGuerrero._mainGame._lector.cartasEnMesa[z].GetComponent<CartaEnMesa>().id.ToString() == name)
				{
					temp = _habilidadPanelGuerrero._mainGame._lector.cartasEnMesa[z];
					_habilidadPanelGuerrero._mainGame._lector.cartasEnMesa.Remove(temp);
					Destroy(temp);
				}
			}

			object[] datas3 = new object[] {playerName, name, "yes"};
			PhotonNetwork.RaiseEvent(11, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			object[] datas2 = new object[] {playerName, name, "yes"};
			PhotonNetwork.RaiseEvent(12, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);

			for(int x = 0; x < _habilidadPanelGuerrero.cardsToRemove.Count; x++)
			{
				Destroy(_habilidadPanelGuerrero.cardsToRemove[x]);
			}
			_habilidadPanelGuerrero.jugadores.Clear();
			_habilidadPanelGuerrero.cardsToRemove.Clear();
			_habilidadPanelGuerrero.once = 0;
			_habilidadPanelGuerrero.click = false;
			_habilidadPanelGuerrero.terminarHabilidad = true;
			_habilidadPanelGuerrero.habilidadPanel.SetActive(false);
			_habilidadPanelGuerrero._mainGame.delay = 1f;
			Destroy(this.gameObject);

		}

		else if (playerName != "")
		{
			_habilidadPanelGuerrero._mainGame.oro -= (coste-precio);
			object[] datas = new object[] {PhotonNetwork.LocalPlayer.NickName, _habilidadPanelGuerrero._mainGame.oro.ToString()};
			PhotonNetwork.RaiseEvent(81, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			GameObject temp;

			for(int z = 0; z < _playersInterface.cartasEnMesa.Count; z++)
			{
				if(_playersInterface.cartasEnMesa[z].name == name)
				{
					temp = _playersInterface.cartasEnMesa[z];
					_playersInterface.cartasEnMesa.Remove(temp);
					Destroy(temp);
				}
			}

			object[] datas3 = new object[] {playerName, name, "yes"};
			PhotonNetwork.RaiseEvent(11, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			object[] datas2 = new object[] {playerName, name, "yes"};
			PhotonNetwork.RaiseEvent(12, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			object[] datas5 = new object[] {"Destruye :",name ,_playersInterface.playerName};
			PhotonNetwork.RaiseEvent(76, datas5, RaiseEventOptions.Default, SendOptions.SendUnreliable);

			for(int x = 0; x < _habilidadPanelGuerrero.cardsToRemove.Count; x++)
			{
				Destroy(_habilidadPanelGuerrero.cardsToRemove[x]);
			}
			_habilidadPanelGuerrero.jugadores.Clear();
			_habilidadPanelGuerrero.cardsToRemove.Clear();
			_habilidadPanelGuerrero.once = 0;
			_habilidadPanelGuerrero.click = false;
			_habilidadPanelGuerrero.terminarHabilidad = true;
			_habilidadPanelGuerrero.habilidadPanel.SetActive(false);
			_habilidadPanelGuerrero._mainGame.delay = 1f;
			Destroy(this.gameObject);
		}

	}
}
