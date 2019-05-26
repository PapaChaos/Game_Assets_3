using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Anna : InteractableWorldObject
{
	public Camera dialogCamera;
	public Camera MainCamera;

	public override void Interaction()
	{
		base.Interaction();
		interactor.ChangeCameraView(dialogCamera);
		
		dialogCamera.GetComponent<DialogSystem>().startDialog();
		dialogCamera.GetComponent<DialogSystem>().player = interactor;
		dialogCamera.GetComponent<DialogSystem>().curActiveCamera = dialogCamera;
		dialogCamera.GetComponent<DialogSystem>().InitiatedObject = transform.parent.gameObject;
	}
}
