using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCanvas : MonoBehaviour
{
	[SerializeField]
	private CreateRoomCanvas _createRoomCanvas;
	public CreateRoomCanvas createRoomCanvas { get { return _createRoomCanvas;}}

	[SerializeField]
	private InsideRoomCanvas _insideRoomCanvas;
	public InsideRoomCanvas insideRoomCanvas { get { return _insideRoomCanvas;}}

	private void Awake()
	{
		FirstInitialize();
	}

	private void FirstInitialize()
	{
		createRoomCanvas.FirstInitialize(this);
		insideRoomCanvas.FirstInitialize(this);

	}
}
