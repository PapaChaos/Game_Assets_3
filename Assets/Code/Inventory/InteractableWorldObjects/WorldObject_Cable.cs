using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WorldObject_Cable : InteractableWorldObject
{
	public GameObject Item_Cable;
	public float animationTime;
	public DialogSystem ds;
	public WorldObject_intercom intercom;

	public override void Interaction()
	{
		GameObject wireItem = Instantiate(Item_Cable) as GameObject;
		wireItem.transform.SetParent(interactor.hud.InventoryContent.transform);
		interactor.InteractionAnimation(3,3.5f);

		StartCoroutine(removeWorldItem());
		
	}
	IEnumerator removeWorldItem()
	{
		yield return new WaitForSeconds(1.5f);
		interactor.ChangeCameraView(ds.GetComponent<Camera>());
		interactor.hud.HudChange(2);
		ds.startDialog();
		interactor.hud.GetComponent<QuestList>().questTexts[0].text = "Find an intercom in one of the crew quarters and overload it.";
		intercom.playerGotCabled = true;
		Destroy(gameObject);

	}
}
