using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldObject_ShipControlTerminal : InteractableWorldObject
{
    public override void Interaction()
	{
		SceneManager.LoadScene("TheEnd");
	}
}
