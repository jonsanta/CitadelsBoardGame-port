using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botones : MonoBehaviour
{
	public int color;
	public EndGame _endGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnClick()
    {
		_endGame.ciudadEmbrujada.GetComponent<CartaEnMesa>().color = color;
		_endGame.displayBotones.SetActive(false);
		_endGame.ciudadEmbrujadaEncontrada = false;
    }
}
