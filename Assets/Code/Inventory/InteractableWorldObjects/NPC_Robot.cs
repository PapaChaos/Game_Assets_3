using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Robot : InteractableWorldObject
{
	public GameObject player;
	public float totalRotation;
	public bool broken, batteryQuest, batteryQuestDone, cableQuest, cableQuestDone;
	public GameObject battery;
	public DialogSystem q2Ending;


	public override void Interaction()
	{
		base.Interaction();
		if (!batteryQuest && !broken)
		{
		interactor.hud.curActiveDialog = gameObject.GetComponent<DialogSystem>();
		gameObject.GetComponent<DialogSystem>().player = interactor;
		gameObject.GetComponent<DialogSystem>().startDialog();
		interactor.hud.HudChange(2);
		interactor.engCode = true;
		interactor.hangarCode = true;
		batteryQuest = true;
		}
	}

	void Update()
	{
		if (!broken && batteryQuest)
			if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 5)
			{
				Vector3 lastPoint = transform.TransformDirection(Vector3.forward);
				transform.LookAt(player.transform);
				totalRotation += Vector3.Angle(lastPoint, transform.TransformDirection(Vector3.forward));

				if (totalRotation > 1800)
				{
					broken = true;
					Quaternion someRotation = Quaternion.Euler(-0.7544f, 0.0027f, 25.401f);
					Instantiate(battery, new Vector3(-0.7543f, 0.002740f, 25.401f), someRotation);
					battery.GetComponent<WorldObject_Battery>().batteryDialog = q2Ending;
					print("battery got this far...");
				}
			}
			else
			{
				if (!battery.GetComponent<WorldObject_Battery>().batteryDialog || battery.GetComponent<WorldObject_Battery>().batteryDialog == null)
				{
					print("battery getting set?");
					battery.GetComponent<WorldObject_Battery>().batteryDialog = q2Ending;
				}
			}
	}
}
