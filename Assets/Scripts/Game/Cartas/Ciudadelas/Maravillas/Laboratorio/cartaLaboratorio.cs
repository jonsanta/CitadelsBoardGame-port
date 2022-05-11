using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class cartaLaboratorio : MonoBehaviour
{
	public int id; 
	public GameObject _laboratorio;

    // Update is called once per frame
    public void OnClick()
    {

		if(id == 500)
		{
			_laboratorio.GetComponent<Laboratorio>()._habilidad.disabler.SetActive(false);
			_laboratorio.GetComponent<Laboratorio>()._habilidad.faro.SetActive(false);
			for(int x = 0; x < _laboratorio.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count; x++)
			{
				_laboratorio.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano[x].GetComponent<Button>().enabled = true;
			}

			for(int z = 0; z < _laboratorio.GetComponent<Laboratorio>().cartas.Count; z++)
			{
				Destroy(_laboratorio.GetComponent<Laboratorio>().cartas[z]);
			}

			Destroy(this.gameObject);
			_laboratorio.GetComponent<Laboratorio>().usado = false;

		}
		else
		{
			
			_laboratorio.GetComponent<Laboratorio>()._habilidad.disabler.SetActive(false);
			_laboratorio.GetComponent<Laboratorio>()._habilidad.faro.SetActive(false);
			for(int x = 0; x < _laboratorio.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count; x++)
			{
				_laboratorio.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano[x].GetComponent<Button>().enabled = true;
				if(_laboratorio.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano[x].GetComponent<CartaMano>().id == id)
				{
					GameObject temp;
					temp = _laboratorio.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano[x];
					_laboratorio.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Remove(temp);
					Destroy(temp);
					if(!PhotonNetwork.IsMasterClient)
					{
						object[] datas = new object[] {id, 0, 1};
						RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient};
						PhotonNetwork.RaiseEvent(70, datas, raiseEventOptions, SendOptions.SendUnreliable);
					}
					else
					{
						_laboratorio.GetComponent<CartaEnMesa>()._mainGame._masterGame.cardID.Add(id);
					}
					_laboratorio.GetComponent<CartaEnMesa>()._mainGame.oro +=1;
					object[] datas2 = new object[] {PhotonNetwork.LocalPlayer.NickName, _laboratorio.GetComponent<CartaEnMesa>()._mainGame.oro.ToString()};
					PhotonNetwork.RaiseEvent(81, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					object[] datas3 = new object[] {PhotonNetwork.LocalPlayer.NickName+" Usó: ", "70",""};
					PhotonNetwork.RaiseEvent(76, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					_laboratorio.GetComponent<CartaEnMesa>()._mainGame.delay = 1f;
				}
			}

			for(int z = 0; z < _laboratorio.GetComponent<Laboratorio>().cartas.Count; z++)
			{
				Destroy(_laboratorio.GetComponent<Laboratorio>().cartas[z]);
			}

			Destroy(this.gameObject);
		}
    }
}
