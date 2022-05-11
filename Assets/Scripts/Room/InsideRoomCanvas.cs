using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideRoomCanvas : MonoBehaviour
{

	[SerializeField]
	private PlayerListingMenu _playerListingsMenu;
	[SerializeField]
	private LeaveRoomMenu _leaveRoomMenu;
	private AllCanvas _allCanvas;

	public void FirstInitialize(AllCanvas canvases)
	{
		_allCanvas = canvases;
		_playerListingsMenu.FirstInitialize(canvases);
		_leaveRoomMenu.FirstInitialize(canvases);
	}
		
	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
