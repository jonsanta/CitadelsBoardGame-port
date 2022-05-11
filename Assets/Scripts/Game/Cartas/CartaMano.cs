using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaMano : MonoBehaviour
{
	public GameObject seleccionada;

	public bool bypass = false;
	public MainGame _mainGame;
	public int id;
	public string name;
	public int coste;
	public int puntos;
	public int color;
	public int precioReducido = 0;

	private bool cartaRepetida = false;

	public void Start()
	{
		cartaRepetida = false;
		bypass = false;
		precioReducido = 0;
		seleccionada.SetActive(false);
	}

	public void Update()
	{
		if(_mainGame._lector.carta == this.gameObject)
		{
			seleccionada.SetActive(true);

		}
		else
		{
			seleccionada.SetActive(false);
		}

		if(id < 57)
		{
			coste = int.Parse(_mainGame._lector.costes[id]);
			puntos = int.Parse(_mainGame._lector.costes[id]);
			color = int.Parse(_mainGame._lector.colores[id]);
			name = _mainGame.mazoCartas[id].name;
		}
		if(id >= 57)
		{
			int temp;
			temp = id - 57;
			name = _mainGame.mazoCartas[id].name;
			if(temp == 0)
			{
				coste = int.Parse(_mainGame._lector.costesMaravilla[temp]);
				puntos = int.Parse(_mainGame._lector.costesMaravilla[temp+1]);
				color = 0;
			}else
			{
				coste = int.Parse(_mainGame._lector.costesMaravilla[temp*2]);
				puntos = int.Parse(_mainGame._lector.costesMaravilla[(temp*2)+1]);
				color = 0;
			}

		}
	}

	public void OnClick()
	{
		if(	_mainGame.delay == 0f)
		{
			_mainGame._lector.Cancelar();
			precioReducido = 0;
			for(int y = 0; y < _mainGame._lector.cartasEnMesa.Count; y++)
			{
				if(_mainGame._lector.cartasEnMesa[y].name == "Fabrica")
				{
					precioReducido = 1;
				}

			}
	
			int encontrado;
			encontrado = 0;

			for(int z = 0; z < _mainGame._lector.cartasEnMesa.Count; z++)
			{
				if(_mainGame._lector.cartasEnMesa[z].name == "Cantera")
				{
					encontrado = 1;
				}

			}

			if(encontrado == 1)
			{
				bypass = true;
			}
			else
			{
				bypass = false;

			}

			if(_mainGame._lector.cartasEnMesa.Count != 0)
			{
				for(int x = 0; x < _mainGame._lector.cartasEnMesa.Count; x++)
				{
					if( _mainGame._lector.cartasEnMesa[x].name == name)
					{
						if(bypass == false)
						{
						cartaRepetida = true;
						}
					}

				}

				if(id < 57 && coste <= _mainGame.oro && cartaRepetida == false)
				{
					_mainGame._lector.ActivarInterfaz();
					_mainGame._lector.carta = this.gameObject;
				}

				if(id >= 57 && (coste)-precioReducido <= _mainGame.oro && cartaRepetida == false)
				{
					_mainGame._lector.ActivarInterfaz();
					_mainGame._lector.carta = this.gameObject;
				}
				cartaRepetida = false;
			}
			else
			{
				if(coste <= _mainGame.oro)
				{
					_mainGame._lector.ActivarInterfaz();
					_mainGame._lector.carta = this.gameObject;
				}

			}
		}
	}


}
