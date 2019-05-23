﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject_Q1Door : InteractableWorldObject
{
	public QuestList ql;
	public int questObjectiveIndex;
	public int sinkDoneIndex;
	public DialogSystem dialogEnding;

	public override void Interaction()
	{
		if (ql.questsDone[sinkDoneIndex])
		{
			dialogEnding.player.ActiveCamera.enabled = false;
			dialogEnding.player.ActiveCamera.transform.GetComponent<AudioListener>().enabled = false;
			dialogEnding.player.ActiveCamera = dialogEnding.curActiveCamera;
			dialogEnding.player.ActiveCamera.enabled = true;
			dialogEnding.curActiveCamera.GetComponent<AudioListener>().enabled = true;
			dialogEnding.enabled = true;
			dialogEnding.startDialog(0);
			ql.questsDone[questObjectiveIndex] = true;
		}

	}
}
