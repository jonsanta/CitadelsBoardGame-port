using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class FinalizarTurno : MonoBehaviour
{
	public bool esMiTurno = false;
	public MainGame _mainGame;
    // Start is called before the first frame update

	public void Update()
	{
		if(_mainGame.esMiTurno == true && _mainGame.personajesPanel.activeSelf == false && _mainGame.habilidadPanel.GetComponent<HabilidadPanel>().habilidadPanel.activeSelf == false && _mainGame.delay == 0 && _mainGame.finalizarTurno == true)
		{
			_mainGame.FinalizarBoton.SetActive(true);
		}
		else
		{
			_mainGame.FinalizarBoton.SetActive(false);
		}
	}


	public void OnClick_FinalizarTurno()
	{
		if(!PhotonNetwork.IsMasterClient)
		{
			if(_mainGame.personajeEscogidoHabilidad != null)
			{
				Destroy(_mainGame.personajeEscogidoHabilidad);
				_mainGame.personajeEscogidoHabilidad = null;
			}
			for(int x = 0; x < _mainGame._lector.cartasEnMesa.Count; x++)
			{
				if(_mainGame._lector.cartasEnMesa[x].name == "Hospicio")
				{
					if(_mainGame.oro == 0)
					{
						_mainGame.oro +=1;
						object[] datas3 = new object[] {PhotonNetwork.LocalPlayer.NickName, _mainGame.oro.ToString()};
						PhotonNetwork.RaiseEvent(81, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
						object[] datas5 = new object[] {"+1 ORO", "68",""};
						PhotonNetwork.RaiseEvent(76, datas5, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					}
				}

				if(_mainGame._lector.cartasEnMesa[x].name == "Hospital")
				{
					_mainGame._lector.cartasEnMesa[x].GetComponent<Hospital>().asesinado = false;
				}

				if(_mainGame._lector.cartasEnMesa[x].name == "Parque")
				{
					_mainGame._lector.cartasEnMesa[x].GetComponent<Parque>().request = true;
				}

			}
			_mainGame.personajesDescartadosDestroy();
			_mainGame._lector.Cancelar();
			_mainGame.puntuaje();
			_mainGame.esMiTurno = false;
			_mainGame.habilidad = false;
			_mainGame.oroOcartas = false;
			_mainGame.finalizarTurno = false;
			_mainGame.construir = false;
			_mainGame.asesinado = false;
			_mainGame.personajeAsesinado = 10;
			_mainGame.cartasRecibidasEnTurno.Clear();
			object[] datas = new object[] {1};
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient};
			PhotonNetwork.RaiseEvent(20, datas, raiseEventOptions, SendOptions.SendUnreliable);

		}
		else
		{
			if(_mainGame.personajeEscogidoHabilidad != null)
			{
				Destroy(_mainGame.personajeEscogidoHabilidad);
				_mainGame.personajeEscogidoHabilidad = null;
			}

			for(int x = 0; x < _mainGame._lector.cartasEnMesa.Count; x++)
			{
				if(_mainGame._lector.cartasEnMesa[x].name == "Hospicio")
				{
					if(_mainGame.oro == 0)
					{
						_mainGame.oro +=1;
						object[] datas4 = new object[] {PhotonNetwork.LocalPlayer.NickName, _mainGame.oro.ToString()};
						PhotonNetwork.RaiseEvent(81, datas4, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					}
				}

				if(_mainGame._lector.cartasEnMesa[x].name == "Hospital")
				{
					_mainGame._lector.cartasEnMesa[x].GetComponent<Hospital>().asesinado = false;
				}

				if(_mainGame._lector.cartasEnMesa[x].name == "Parque")
				{
					_mainGame._lector.cartasEnMesa[x].GetComponent<Parque>().request = true;
				}

			}

			_mainGame.personajesDescartadosDestroy();
			_mainGame._lector.Cancelar();
			_mainGame.puntuaje();
			_mainGame.esMiTurno = false;
			_mainGame.habilidad = false;
			_mainGame._masterGame.turnoDe +=1;
			_mainGame.oroOcartas = false;
			_mainGame.finalizarTurno = false;
			_mainGame.construir = false;
			_mainGame.asesinado = false;
			_mainGame.personajeAsesinado = 10;
			_mainGame.cartasRecibidasEnTurno.Clear();

		}
	}

}
