using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogButtonScript : MonoBehaviour
{
	public int nextDialog;
	public DialogSystem ds;
    
	public void buttonpressed()
	{

		foreach (Button b in ds.dialogButtons)
		{
			Destroy(b.gameObject);
		}

		ds.dialogButtons.Clear();
		ds.dialogIndex = nextDialog;
		ds.PlayDialogIndex(nextDialog);
	}
}
