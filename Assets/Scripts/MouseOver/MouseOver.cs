using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private GameObject reference;
	private GameObject habilidadPanel;
	private bool mouse_over = false;
	void Update()
	{
		if (mouse_over)
		{
			reference.SetActive(true);
			reference.GetComponent<RawImage>().texture = GetComponent<RawImage>().texture;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		habilidadPanel = GameObject.FindGameObjectWithTag("habilidad");
		reference = habilidadPanel.GetComponent<HabilidadPanel>().cartaZoom;
		reference.GetComponent<MouseOverDestroyed>().carta = this.gameObject;
		mouse_over = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		mouse_over = false;
		reference.GetComponent<RawImage>().texture = null;
		reference.GetComponent<MouseOverDestroyed>().carta = null;
		reference.SetActive(false);
	}
}
