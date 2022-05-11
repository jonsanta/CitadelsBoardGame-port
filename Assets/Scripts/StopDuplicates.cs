using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopDuplicates : MonoBehaviour
{
	private static StopDuplicates playerInstance;
	void Awake(){

		if (playerInstance == null) {
			playerInstance = this;
		} else {
			DestroyObject(gameObject);
		}
	}
}
