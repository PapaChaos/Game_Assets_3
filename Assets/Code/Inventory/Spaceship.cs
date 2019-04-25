using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Spaceship : InteractableWorldObject
{
    void Start()
    {
		interactionType = interactionTypes.Use;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public override void Interact()
	{
		SceneManager.LoadScene("TheEnd");
	}

}
