using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Laboratorio : MonoBehaviour
{

	public bool usado;
	public HabilidadPanel _habilidad;
	public List<GameObject> cartas = new List<GameObject>();

	public void Update()
	{
		GetComponent<Button>().onClick.AddListener(OnClick);


		if(GetComponent<CartaEnMesa>()._mainGame.esMiTurno == false)
		{
			usado = false;
		}

	}

	public void OnClick()
	{
		if(GetComponent<CartaEnMesa>()._mainGame.esMiTurno == true && usado == false && GetComponent<CartaEnMesa>()._mainGame.delay == 0f)
		{
			_habilidad = GameObject.FindWithTag("habilidad").GetComponent<HabilidadPanel>();
			_habilidad.disabler.SetActive(true);
			_habilidad.faro.SetActive(true);

			for(int x = 0; x < GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count; x++)
			{
				GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano[x].GetComponent<Button>().enabled = false;
				GameObject temp;
				temp = Instantiate(_habilidad.laboratorioPrefab, _habilidad.faroContent);
				cartas.Add(temp);
				temp.GetComponent<cartaLaboratorio>().id = GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano[x].GetComponent<CartaMano>().id;
				temp.GetComponent<cartaLaboratorio>()._laboratorio = this.gameObject;
				temp.GetComponent<RawImage>().texture = GetComponent<CartaEnMesa>()._mainGame.mazoCartas[GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano[x].GetComponent<CartaMano>().id];
			}
			GameObject temp2;
			temp2 = Instantiate(_habilidad.laboratorioPrefab, _habilidad.faroContent);
			cartas.Add(temp2);
			temp2.GetComponent<cartaLaboratorio>().id = 500;
			temp2.GetComponentInChildren<Text>().text = "Cancelar";
			temp2.GetComponent<cartaLaboratorio>()._laboratorio = this.gameObject;
			usado = true;
		}
	}
}
