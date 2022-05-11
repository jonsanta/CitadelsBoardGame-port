using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class RecaudarImpuestos : MonoBehaviour
{
	public GameObject boton;
	public int color;
	public int cartasRegistradas;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
		if(GetComponent<MainGame>().esMiTurno == false)
		{
			boton.SetActive(false);
		}
			
    }

	public void OnClick()
	{
		int recaudado;
		recaudado = 0;
		for(int x = 0; x < GetComponent<Lector>().cartasEnMesa.Count; x++)
		{
			if(color == GetComponent<Lector>().cartasEnMesa[x].GetComponent<CartaEnMesa>().color)
			{
				Debug.Log("Recaudado");
				GetComponent<MainGame>().oro +=1;
				recaudado += 1;
				object[] datas = new object[] {PhotonNetwork.LocalPlayer.NickName, GetComponent<MainGame>().oro.ToString()};
				PhotonNetwork.RaiseEvent(81, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
				cartasRegistradas += 1;
			}
			if(x == GetComponent<Lector>().cartasEnMesa.Count-1 )
			{
				if(recaudado != 0)
				{
					object[] datas2 = new object[] {PhotonNetwork.LocalPlayer.NickName+" Recaudado: "+recaudado.ToString()+" Oros"};
					PhotonNetwork.RaiseEvent(98, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
					GetComponent<MainGame>().delay = 1f;
				}
			}

		}
		color = 0;
		boton.SetActive(false);
		cartasRegistradas = 0;
	}
}
