using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject_intercom : InteractableWorldObject
{
	public DialogSystem ds;
	public bool playerGotCabled;

	public override void Interaction()
	{
		if (playerGotCabled)
		{
			interactor.ChangeCameraView(ds.GetComponent<Camera>());
			interactor.hud.HudChange(2);
			ds.startDialog();
		}
	}
}
