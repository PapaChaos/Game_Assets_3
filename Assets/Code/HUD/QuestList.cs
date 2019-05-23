using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestList : MonoBehaviour
{
	public Text ObjectivesText;
	public Text objectiveText;
	public List<string> quests;
	public List<bool> questsDone;
	public List<Text> questTexts;
	// Start is called before the first frame update
	void Start()
	{
		float i = 0;
		int count = 0;
		foreach (string quest in quests)
		{
			questsDone.Add(false);
			Text questObjective = Instantiate(objectiveText);
			questObjective.text = quest;
			Vector3 textPos = ObjectivesText.transform.position;
			i -= 25;
			textPos.y += i;
			questTexts.Add(questObjective);
			questObjective.transform.position = textPos;
			questObjective.transform.SetParent(ObjectivesText.transform);

		}
	}

	public void questdone(int questNumber)
	{
		questsDone[questNumber] = true;
		if (CheckQuestsDone())
			print("quests are done");
		else
			print("quests are not done");

	}
	private bool CheckQuestsDone()
	{
		bool allQuestsDone = true;
		foreach(bool queststatus in questsDone)
		{
			if (queststatus == false)
				allQuestsDone = false;
		}
		return allQuestsDone;
	}
}
