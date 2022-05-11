using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class cartaMuseo : MonoBehaviour
{
	public GameObject _museo;
	public int id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Onclick()
    {
		if(id == 500)
		{
			_museo.GetComponent<Museo>()._habilidad.disabler.SetActive(false);
			_museo.GetComponent<Museo>()._habilidad.faro.SetActive(false);
			for(int x = 0; x < _museo.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count; x++)
			{
				_museo.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano[x].GetComponent<Button>().enabled = true;
			}

			for(int z = 0; z < _museo.GetComponent<Museo>().cartas.Count; z++)
			{
				Destroy(_museo.GetComponent<Museo>().cartas[z]);
			}

			Destroy(this.gameObject);
			_museo.GetComponent<Museo>().usado = false;

		}
		else
		{
			_museo.GetComponent<Museo>()._habilidad.disabler.SetActive(false);
			_museo.GetComponent<Museo>()._habilidad.faro.SetActive(false);
			for(int x = 0; x < _museo.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count; x++)
			{
				_museo.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano[x].GetComponent<Button>().enabled = true;
				if(_museo.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano[x].GetComponent<CartaMano>().id == id)
				{
					GameObject temp;
					temp = _museo.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano[x];
					_museo.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Remove(temp);
					Destroy(temp);
					_museo.GetComponent<CartaEnMesa>().puntos +=1;
					object[] datas2 = new object[] {PhotonNetwork.LocalPlayer.NickName, _museo.GetComponent<CartaEnMesa>()._mainGame.cartasEnLaMano.Count.ToString()};
					PhotonNetwork.RaiseEvent(82, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					if(_museo.GetComponentInChildren<Text>().text != "")
					{
						int temp2;
						temp2 = int.Parse(_museo.GetComponentInChildren<Text>().text);
						int temp3;
						temp3 = temp2 +=1;
						_museo.GetComponentInChildren<Text>().text = temp3.ToString();
						object[] datas3 = new object[] {"Usó", "71",""};
						PhotonNetwork.RaiseEvent(76, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
						_museo.GetComponent<CartaEnMesa>()._mainGame.delay = 1f;
					}
					else
					{
						_museo.GetComponentInChildren<Text>().text = "1";
						object[] datas3 = new object[] {"Usó", "71",""};
						PhotonNetwork.RaiseEvent(76, datas3, RaiseEventOptions.Default, SendOptions.SendUnreliable);
						_museo.GetComponent<CartaEnMesa>()._mainGame.delay = 1f;
					}
				}
			}

			for(int z = 0; z < _museo.GetComponent<Museo>().cartas.Count; z++)
			{
				Destroy(_museo.GetComponent<Museo>().cartas[z]);
			}

			Destroy(this.gameObject);
		}
		
        
    }
}
