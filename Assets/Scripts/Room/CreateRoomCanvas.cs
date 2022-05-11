using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoomCanvas : MonoBehaviour
{
	[SerializeField]
	private RoomListingMenu _roomListingMenu;
	[SerializeField]
	private CreateRoom _createRoom;

	private AllCanvas _allCanvas;

	public void FirstInitialize(AllCanvas canvases)
	{
		_allCanvas = canvases;
		_createRoom.FirstInitialize(canvases);
		_roomListingMenu.FirstInitialize(canvases);
	}
}
