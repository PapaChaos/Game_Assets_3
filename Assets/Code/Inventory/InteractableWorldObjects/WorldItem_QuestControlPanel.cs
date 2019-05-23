using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem_QuestControlPanel : InteractableWorldObject
{
	public GameObject lever;
	public Vector3 leverStartPos, leverEndPos;
	public DialogSystem ds;
	private void Start()
	{
		
		leverStartPos = lever.transform.position;
		leverEndPos = leverStartPos;
		leverEndPos.x +=-0.2f;
		leverEndPos.y += 0.04f;
	}

	public override void Interaction()
	{
		//anim 2 or 4?
		interactor.InteractionAnimation(2, 2);
		lever.transform.position = leverEndPos;
		interactor.ChangeCameraView(ds.GetComponent<Camera>());
		ds.startDialog(0);
		interactor.hud.HudChange(2);
	}
}
