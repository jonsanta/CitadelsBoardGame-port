using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class LogInstance
{
	public List<GameObject> logList = new List<GameObject>();
}


public class Log : MonoBehaviour
{
	public GameObject test;

	public GameObject text;
	string texto;
	public MainGame _mainGame;
	private float contador;
	bool display = false;
	int once = 0;

	public GameObject log;
	public Transform OutsideContent;
	public Transform logContent;

	public GameObject textLog;
	public GameObject cartaLog;
	public GameObject personajeLog;

	public List<LogInstance> logList = new List<LogInstance>();
	public List<GameObject> logListTemp = new List<GameObject>();

	public void Update()
	{
		
		if(_mainGame.esMiTurno == true)
		{
			RestartAllUI();
		
			object[] datas = new object[] {PhotonNetwork.LocalPlayer.NickName};
			PhotonNetwork.RaiseEvent(12, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
			if(once == 0)
			{
				object[] datas2 = new object[] {"Turno De: ", PhotonNetwork.LocalPlayer.NickName, _mainGame.personajeLocal.ToString()};
				PhotonNetwork.RaiseEvent(74, datas2, RaiseEventOptions.Default, SendOptions.SendUnreliable);
				once = 1;
			}
		}
		else
		{
			object[] datas = new object[] {PhotonNetwork.LocalPlayer.NickName};
			PhotonNetwork.RaiseEvent(13, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);

		}

		if(_mainGame.esMiTurno == true || test.activeSelf == true)
		{
			log.SetActive(false);
		}

		if(_mainGame.esMiTurno == false && test.activeSelf == false)
		{
			log.SetActive(true);
			once = 0;
			if(display == true)
			{
				if(logList.Count > 0)
				{
					for(int x = 0; x < logList[0].logList.Count; x++)
					{
						if(logListTemp.Count != logList[0].logList.Count)
						{
							GameObject temp;
							temp = Instantiate(logList[0].logList[x], logContent);
							logListTemp.Add(temp);
						}
					}
					contador += Time.deltaTime;


					if(contador > 1.7f)
					{
						for(int x = 0; x < logListTemp.Count; x++)
						{
							Destroy(logList[0].logList[x]);
							Destroy(logListTemp[x]);
						}
						logListTemp.Clear();
						logList.Remove(logList[0]);
						contador = 0;
					}
				}
				else
				{
					display = false;
				}
			}

		}
	}

	public void RestartAllUI()
	{
		log.SetActive(false);
		for(int x = 0; x < logList.Count; x++)
		{
			for(int y = 0; y < logList[x].logList.Count; y++)
			{
				Destroy(logList[x].logList[y]);
			}
		}
		logList.Clear();
		for(int x = 0; x < logListTemp.Count; x++)
		{
			Destroy(logListTemp[x]);
		}

		logListTemp.Clear();
		display = false;
		contador = 0;
	}

	private void OnEnable()
	{
		PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
	}

	private void OnDisable()
	{
		PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
	}


	private void NetworkingClient_EventReceived(EventData obj)
	{
		if(obj.Code == 97)
		{
			object[] datas = (object[])obj.CustomData;
			int temp;
			temp = int.Parse((string)datas[0]);
			this.gameObject.GetComponent<MainGame>().personajeAsesinado = temp;
			texto = (string)datas[1]+GetComponent<MainGame>().mazoPersonajes[temp].name+"]";
		}
		if(obj.Code == 98)
		{
			object[] datas = (object[])obj.CustomData;
			GameObject temp;
			temp = Instantiate(textLog, OutsideContent);
			temp.GetComponent<Text>().text = (string)datas[0];
			LogInstance instance = new LogInstance();
			instance.logList.Add(temp);
			logList.Add(instance);
			display = true;
			instance = null;
		}

		if(obj.Code == 74)
		{
			object[] datas = (object[])obj.CustomData;
			GameObject temp;
			temp = Instantiate(textLog, OutsideContent);
			temp.GetComponent<Text>().text = (string)datas[0];
			LogInstance instance = new LogInstance();
			instance.logList.Add(temp);
			GameObject temp2;
			temp2 = Instantiate(personajeLog, OutsideContent);
			temp2.GetComponent<Text>().text = (string)datas[1];
			temp2.GetComponentInChildren<Image>().sprite = _mainGame.mazoPersonajes[int.Parse((string)datas[2])];
			instance.logList.Add(temp2);
			logList.Add(instance);
			display = true;
			instance = null;
			Debug.Log("Recibido Turno");
		}

		if(obj.Code == 75)
		{
			object[] datas = (object[])obj.CustomData;
			GameObject temp;
			temp = Instantiate(textLog, OutsideContent);
			temp.GetComponent<Text>().text = (string)datas[0];
			LogInstance instance = new LogInstance();
			instance.logList.Add(temp);
			GameObject temp2;
			temp2 = Instantiate(personajeLog, OutsideContent);
			temp2.GetComponentInChildren<Image>().sprite = _mainGame.mazoPersonajes[int.Parse((string)datas[1])];
			instance.logList.Add(temp2);
			logList.Add(instance);
			display = true;
			instance = null;
		}

		if(obj.Code == 76)
		{
			object[] datas = (object[])obj.CustomData;
			GameObject temp;
			temp = Instantiate(textLog, OutsideContent);
			temp.GetComponent<Text>().text = (string)datas[0];
			LogInstance instance = new LogInstance();
			instance.logList.Add(temp);
			GameObject temp2;
			temp2 = Instantiate(cartaLog, OutsideContent);
			if((string)datas[2] != "")
			{
				temp2.GetComponent<Text>().text = (string)datas[2];
			}
			temp2.GetComponentInChildren<RawImage>().texture = _mainGame.mazoCartas[int.Parse((string)datas[1])];
			instance.logList.Add(temp2);
			logList.Add(instance);
			display = true;
			instance = null;
		}
	}
}

