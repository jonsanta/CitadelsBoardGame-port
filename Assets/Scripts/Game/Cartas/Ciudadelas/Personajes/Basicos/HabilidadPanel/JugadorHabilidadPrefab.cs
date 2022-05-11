using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JugadorHabilidadPrefab : MonoBehaviour
{
	public HabilidadPanel _habilidadPanel;
	public string name;
	public bool destruirTodo = false;
	public GameObject cartaDescartadaPrefab;

	public void Update()
	{
		if(destruirTodo == true)
		{
			for(int x = 0; x < _habilidadPanel.jugadores.Count; x++)
			{
				Destroy(_habilidadPanel.jugadores[x]);
			}
			_habilidadPanel.jugadores.Clear();
			_habilidadPanel.habilidadPanel2.SetActive(false);
			_habilidadPanel.once = 0;
			_habilidadPanel.click = false;
		}

		if(_habilidadPanel.saveCards == true)
		{
			for(int z = 0; z < _habilidadPanel.cartasEnMano.Count; z++)
			{
				_habilidadPanel.text2.text = "ELIGE CARTAS QUE QUIERES GUARDAR";
				GameObject temp;
				temp = Instantiate(cartaDescartadaPrefab,_habilidadPanel.content2);
				temp.GetComponent<cartaDescartada>().contenidoCarta = _habilidadPanel.cartasEnMano[z];
				temp.GetComponent<cartaDescartada>().id = _habilidadPanel.cartasEnMano[z].GetComponent<CartaMano>().id;
				temp.GetComponent<cartaDescartada>()._mainGame = _habilidadPanel._mainGame;
				temp.GetComponent<cartaDescartada>()._habilidadPanel = _habilidadPanel;
				temp.GetComponent<RawImage>().texture = _habilidadPanel._mainGame.mazoCartas[temp.GetComponent<cartaDescartada>().id];
				temp.name = temp.GetComponent<cartaDescartada>().id.ToString();
				_habilidadPanel.cartasEnBoton.Add(temp);
				_habilidadPanel.cartasEnMano[z].SetActive(false);
				_habilidadPanel.botonDescarte.SetActive(true);
				_habilidadPanel.botonDescarteCancelar.SetActive(true);
			}
			_habilidadPanel.saveCards = false;
			for(int x = 0; x < _habilidadPanel.jugadores.Count; x++)
			{
				Destroy(_habilidadPanel.jugadores[x]);
			}
			_habilidadPanel.jugadores.Clear();
		}
	}

	public void OnClick()
	{
		if(name == "Cancelar")
		{
			_habilidadPanel.saveCards = false;
			destruirTodo = true;
		}

		else if(name == "Mazo")
		{
			_habilidadPanel.name = name;
			_habilidadPanel.saveCards = true;

		}
		else
		{
			_habilidadPanel.name = name;
			_habilidadPanel.sent = true;

			for(int x = 0; x < _habilidadPanel.jugadores.Count; x++)
			{
				Destroy(_habilidadPanel.jugadores[x]);
			}
			_habilidadPanel.jugadores.Clear();
		}
	}


}
