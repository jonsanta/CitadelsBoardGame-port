using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverDestroyed : MonoBehaviour
{
	public GameObject carta;
    // Update is called once per frame
    void Update()
    {
		if(carta == null)
		{
			this.gameObject.SetActive(false);
		}


    }
}
