using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject_Battery : InteractableWorldObject
{
	public GameObject Item_Battery;
	public float animationTime;
	public DialogSystem batteryDialog;

	public override void Interaction()
	{
		GameObject batteryItem = Instantiate(Item_Battery) as GameObject;
		batteryItem.transform.SetParent(interactor.hud.InventoryContent.transform);
		interactor.InteractionAnimation(3, 3.5f);

		StartCoroutine(removeWorldItem());

	}
	IEnumerator removeWorldItem()
	{
		yield return new WaitForSeconds(1.5f);
		batteryDialog.startDialog(0);
		Destroy(gameObject);

	}
}
