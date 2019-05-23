using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject_Q1Sink : InteractableWorldObject
{
	public QuestList ql;
	public int questObjectiveIndex;

	public override void Interaction()
	{
		ql.questsDone[questObjectiveIndex] = true;
	}
}
