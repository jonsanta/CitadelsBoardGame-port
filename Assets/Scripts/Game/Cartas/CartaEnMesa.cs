using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartaEnMesa : MonoBehaviour
{
	public int id;
	public int puntos;
	public string name;
	public int color;
	public MainGame _mainGame;
	public int violeta = 0;


	public void Start()
	{
		if(this.gameObject.name == "Escuela de Magia")
		{
			gameObject.AddComponent<EscueladeMagia>();

		}

		if(this.gameObject.name == "Fuente de los Deseos")
		{
			gameObject.AddComponent<FuenteDeLosDeseos>();

		}

		if(this.gameObject.name == "Faro")
		{
			gameObject.AddComponent<Faro>();

		}

		if(this.gameObject.name == "Herreria")
		{
			gameObject.AddComponent<Hererria>();
			gameObject.AddComponent<Button>();

		}

		if(this.gameObject.name == "Hospital")
		{
			gameObject.AddComponent<Hospital>();

		}

		if(this.gameObject.name == "Parque")
		{
			gameObject.AddComponent<Parque>();

		}

		if(this.gameObject.name == "Laboratorio")
		{
			gameObject.AddComponent<Laboratorio>();
			gameObject.AddComponent<Button>();

		}

		if(this.gameObject.name == "Museo")
		{
			gameObject.AddComponent<Museo>();
			gameObject.AddComponent<Button>();

		}

		if(this.gameObject.name == "Polvorin")
		{
			gameObject.AddComponent<Polvorin>();
			gameObject.AddComponent<Button>();

		}

		if(this.gameObject.name == "Sala de Mapas")
		{
			gameObject.AddComponent<SalaDeMapas>();
		}

		if(this.gameObject.name == "Sala del Trono")
		{
			gameObject.AddComponent<SalaDelTono>();
		}

		if(this.gameObject.name == "Tesoro Imperial")
		{
			gameObject.AddComponent<TesoroImperial>();
		}

	}

}
