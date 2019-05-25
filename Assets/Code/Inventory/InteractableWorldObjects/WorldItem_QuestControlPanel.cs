using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem_QuestControlPanel : InteractableWorldObject
{
	public GameObject lever;
	public Vector3 leverStartPos, leverEndPos;
	public DialogSystem ds;
	public bool used;
	private void Start()
	{
		
		leverStartPos = lever.transform.position;
		leverEndPos = leverStartPos;
		leverEndPos.x +=-0.2f;
		leverEndPos.y += 0.04f;
	}

	public override void Interaction()
	{
		if (!used)
		{
			used = true;
			interactor.InteractionAnimation(2, 2);
			lever.transform.position = leverEndPos;
			interactor.ChangeCameraView(ds.GetComponent<Camera>());
			ds.startDialog();
			interactor.hud.HudChange(2);
		}
	}
}
