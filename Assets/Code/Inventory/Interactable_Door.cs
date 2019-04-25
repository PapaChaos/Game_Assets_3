using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Door : InteractableWorldObject
{
	public bool closed = true;
	public bool Locked = true;

	public float openingHeight;
	Vector3 ClosedLocation;

	private void Start()
	{
		if (closed)
			ClosedLocation = transform.position;
		else
		{
			ClosedLocation = transform.position;
			ClosedLocation.y -= openingHeight;
		}
	}

	public override void Interact()
	{
		if (!Locked)
		{
			if (closed)
				transform.position = new Vector3(ClosedLocation.x, ClosedLocation.y + openingHeight, ClosedLocation.z);

			else
				transform.position = new Vector3(ClosedLocation.x, ClosedLocation.y, ClosedLocation.z);

			closed = !closed;
		}
		else
		{

		}
	}

	private void Update()
	{
		if (closed)
			interactionType = interactionTypes.Open;
		else
			interactionType = interactionTypes.Close;
	}
}
