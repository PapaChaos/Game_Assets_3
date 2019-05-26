using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject_Q1Sink : InteractableWorldObject
{
	public QuestList ql;
	public int questObjectiveIndex;

	public override void Interaction()
	{
		//ql.objectiveText.text = "Leave and get some coffee.";
		ql.questTexts[0].text = "Leave and get some coffee.";

		ql.questsDone[questObjectiveIndex] = true;
	}
}
