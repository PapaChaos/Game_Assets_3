using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWorldObject : MonoBehaviour
{
	public enum interactionTypes{ None, Open, Close, LookAt, PickUp, Use, Talk};
	public interactionTypes interactionType = interactionTypes.None;
	public string ObjectName;
	public PlayerMovement interactor;
	public float InteractableRange;


	public virtual void Interact()
	{
		Interaction();
	}
	public virtual void Interaction()
	{

	}
	public string interactionText()
	{
		switch (interactionType)
		{

			case interactionTypes.Open:
				return "Open " + ObjectName;
			case interactionTypes.Close:
				return "Close " + ObjectName;
			case interactionTypes.PickUp:
				return "Pick up " + ObjectName;
			case interactionTypes.LookAt:
				return "Look at " + ObjectName;
			case interactionTypes.Use:
				return "Use " + ObjectName;
			case interactionTypes.Talk:
				return "Talk to " + ObjectName;

			default:
				return "" + ObjectName;
		}
	}
}
