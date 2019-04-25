using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWorldObject : MonoBehaviour
{
	public enum interactionTypes{ None, Open, Close, LookAt, PickUp, Use};
	public interactionTypes interactionType = interactionTypes.None;
	public string ObjectName;
	public virtual void Interact()
	{

	}

	public string interactionText()
	{
		switch (interactionType)
		{

			case interactionTypes.Open:
				return "Open "+ObjectName;
			case interactionTypes.Close:
				return "Close " + ObjectName;
			case interactionTypes.PickUp:
				return "Pick up " + ObjectName;
			case interactionTypes.LookAt:
				return "Look at " + ObjectName;
			case interactionTypes.Use:
				return "Use ";

			default:
				return "" + ObjectName;
		}
	}
}
