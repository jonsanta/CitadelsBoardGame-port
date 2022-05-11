using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cartaDescartada : MonoBehaviour
{
	public MainGame _mainGame;
	public HabilidadPanel _habilidadPanel;
	public GameObject contenidoCarta;
	public int id;


	public void OnClick()
	{
		contenidoCarta.SetActive(true);
		_mainGame.cartasEnLaMano.Add(contenidoCarta);
		_habilidadPanel.cartasEnMano.Remove(contenidoCarta);
		_habilidadPanel.cartasEnBoton.Remove(this.gameObject);
		Destroy(this.gameObject);
	}
}
