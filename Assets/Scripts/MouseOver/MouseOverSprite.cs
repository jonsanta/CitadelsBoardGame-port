using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOverSprite : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private GameObject reference;
	private GameObject habilidadPanel;
	private bool mouse_over = false;
	public Sprite dorso;
	void Update()
	{
		if (mouse_over)
		{
			if(GetComponent<Image>().sprite != dorso)
			{
				reference.SetActive(true);
				reference.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		habilidadPanel = GameObject.FindGameObjectWithTag("habilidad");
		reference = habilidadPanel.GetComponent<HabilidadPanel>().cartaZoomSprite;
		reference.GetComponent<MouseOverDestroyed>().carta = this.gameObject;
		mouse_over = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		mouse_over = false;
		reference.GetComponent<Image>().sprite = null;
		reference.GetComponent<MouseOverDestroyed>().carta = null;
		reference.SetActive(false);
	}
}
