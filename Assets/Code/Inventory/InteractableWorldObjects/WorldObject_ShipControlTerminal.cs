using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldObject_ShipControlTerminal : InteractableWorldObject
{

	public Camera endingCam;


    public override void Interaction()
	{
		interactor.ChangeCameraView(endingCam);
		endingCam.GetComponent<DialogSystem>().startDialog();
	//	SceneManager.LoadScene("TheEnd");
	}
}
