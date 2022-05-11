using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/GameSettings")]
public class GameSettings : ScriptableObject
    // Start is called before the first frame update
{
	[SerializeField]
	private string _gameVersion = "0.0.0";
	public string gameVersion {get { return _gameVersion;}}
	[SerializeField]
	public string nickName;


}
