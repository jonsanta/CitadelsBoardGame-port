﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Singletons/MasterManager")]
public class MasterManager : SingletonScriptableObject<MasterManager>
{
	[SerializeField]
	private GameSettings _gameSettings;
	public static GameSettings gameSettings {get { return instance._gameSettings;}}

}
