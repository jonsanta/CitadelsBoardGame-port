using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerNameInput : MonoBehaviour
{
	[SerializeField] private InputField nameInputField = null;
	[SerializeField] private Button continueButton = null;

	public GameObject avanzarASala;

	private string namePublic;

	private const string PlayerPrefsNameKey = "Nombre";

	private void start(){
		SetUpInputField();

	}


	private void SetUpInputField(){

		if(!PlayerPrefs.HasKey(PlayerPrefsNameKey)){return;}

		string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);
		nameInputField.text = defaultName;

		SetPlayerName(defaultName);
	}

	public void SetPlayerName (string name)
	{
		continueButton.interactable = !string.IsNullOrEmpty(name);
	}

	public void SavePlayerName(){

		string playerName = nameInputField.text;

		MasterManager.gameSettings.nickName = playerName;
		PlayerPrefs.SetString(PlayerPrefsNameKey, playerName);

	}

	public void OnButtonPressed()
	{

		SavePlayerName();
		GameObject acceso = Instantiate(avanzarASala);
		Destroy(this.gameObject);
	}
}
