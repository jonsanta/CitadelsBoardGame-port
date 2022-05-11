using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaFaro : MonoBehaviour
{
	public int id;
	public Faro _faro;
	// Start is called before the first frame update


	public void OnClick()
	{
		_faro.id = id;
		_faro.selected = true;
		for(int x = 0; x <_faro.cartasPrefab.Count; x++)
		{
			Destroy(_faro.cartasPrefab[x]);
		}
		_faro._habilidad.faro.SetActive(false);
		Destroy(this.gameObject);
	}
}
