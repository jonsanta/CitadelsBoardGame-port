using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personajeHabilidadButton : MonoBehaviour
{
	public HabilidadPanel _habilidadPanel;
	public int id;

	public void Update()
	{
		if(_habilidadPanel._mainGame.personajeAsesinado == id)
		{
			_habilidadPanel.personajes.Remove(this.gameObject);
			Destroy(this.gameObject);
		}
	}

    // Start is called before the first frame update
	public void OnClick()
	{
		_habilidadPanel.tempID = id;
		_habilidadPanel.sent = true;
		for(int x = 0; x < _habilidadPanel.personajes.Count; x++)
		{
			Destroy(_habilidadPanel.personajes[x]);
		}
		_habilidadPanel.personajes.Clear();
	}


}
