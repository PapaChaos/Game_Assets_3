using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogButtonScript : MonoBehaviour
{
	public int nextDialog;
	public DialogSystem ds;
	public bool badend;
    
	public void buttonpressed()
	{
		if(badend)
		{
			SceneManager.LoadScene("badend");
		}
		foreach (Button b in ds.dialogButtons)
		{
			Destroy(b.gameObject);
		}

		ds.dialogButtons.Clear();
		ds.dialogIndex = nextDialog;
		ds.PlayDialogIndex(nextDialog);
	}
}
