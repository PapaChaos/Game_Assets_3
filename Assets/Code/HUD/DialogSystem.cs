using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogSystem : MonoBehaviour
{
	public int dialogIndex;

	public Text curDialog;
	public int dialogIndexMax;
	public List<string> dialogText;
	public List<AudioClip> dialogAudio;
	public List<bool> dialogChoices;
	public List<DialogOptions> dialogoptions;
	public List<DialogLine> Dialog;

	public bool dialogHappening;
	public PlayerMovement player;
	public enum dialogEndingAction { BackToGame, ChangeScene, ChangeCamera }
	public dialogEndingAction endingAction = dialogEndingAction.BackToGame;
	public string SceneChangeName;
	public Camera curActiveCamera;
	public Camera changeCamera;
	public Button dialogButton;
	public List<Button> dialogButtons;
	public TToBM_HUD hud;
	public bool useNewSystem;
	private AudioSource audioSource;

	[System.Serializable]
	public class DialogOptions
	{
		public int dialogHappeningIndex;
		public List<string> Dialogs;
		public List<int> NextIndex;

		public DialogOptions()
		{
		}
	}

	[System.Serializable]
	public class DialogLine
	{
		public Text textObject;
		public bool dialogChoice;
		public Dialog dialog;
		public DialogChoice dialogChoices;

		public DialogLine()
		{
		}


		[System.Serializable]
		public class Dialog
		{
			public string dialogText;
			public AudioClip dialogAudio;
		}
		[System.Serializable]
		public class DialogChoice
		{
			public List<string> Dialogs;
			public List<int> NextIndex;

			public DialogChoice()
			{
			}
		}
	}
	

	void Start()
	{
		if (gameObject.GetComponents<AudioSource>().Length < 1)
			audioSource = gameObject.AddComponent<AudioSource>();
		else
			audioSource = gameObject.GetComponents<AudioSource>()[0]; //pick the first found source...
	}

	public void startDialog()
	{
		PlayDialogIndex(0);
		hud.HudChange(2);
	}



	//plays directly the index if valid. If not end dialog.
	public void PlayDialogIndex(int index)
	{
		bool ContinueDialog = true;

		if (!useNewSystem)
		{
			if (dialogIndex >= dialogText.Count)
			{
				endDialog();
				ContinueDialog = false;
			}
		}
		else
		{
			if (dialogIndex >= Dialog.Count)
			{
				endDialog();
				ContinueDialog = false;
			}
		}
		if (ContinueDialog)
		{
			bool dialogComingUp = false;
			if (!useNewSystem)
			{
				if (dialogoptions.Count > 0)
				{
					foreach (DialogOptions Diaopt in dialogoptions)
					{
						if (Diaopt.dialogHappeningIndex == dialogIndex)
						{
							dialogComingUp = true;

							spawnDialogButtons(Diaopt);
						}

					}
				}
			}
			else
			{
				if (Dialog[dialogIndex].dialogChoice)
				{
					dialogComingUp = true;
					spawnDialogButtons();
				}

			}
			if (dialogComingUp == false)
			{
				dialogHappening = true;
				if (useNewSystem)
				{
					curDialog.text = Dialog[dialogIndex].dialog.dialogText;
					audioSource.PlayOneShot(Dialog[dialogIndex].dialog.dialogAudio);
				}
				else
				{
					curDialog.text = dialogText[dialogIndex];
					audioSource.PlayOneShot(dialogAudio[dialogIndex]);
				}
			}
		}
	}

	private void Update()
	{
		if (!audioSource.isPlaying && dialogHappening)
		{
			dialogIndex++;
			dialogHappening = false;

			if (!useNewSystem)
			{
				if (dialogIndex >= dialogText.Count)
					endDialog();
				else
					PlayDialogIndex(dialogIndex);
			}
			else
			{
				if (dialogIndex >= Dialog.Count)
				{
					endDialog();
					print("Dialog Ending");
				}
				else
					PlayDialogIndex(dialogIndex);
			}
		}
	}

	//Ends the current dialog
	public void endDialog()
	{
		curDialog.text = "";

		//goes back to player.
		if (endingAction == dialogEndingAction.BackToGame)
			player.hud.HudChange(0);

		//changes the level.
		else if (endingAction == dialogEndingAction.ChangeScene)
			SceneManager.LoadScene(SceneChangeName);


		//changes the main camera and starts a new dialog if viable.
		else if (endingAction == dialogEndingAction.ChangeCamera)
		{
			curActiveCamera.enabled = false;
			curActiveCamera.transform.GetComponent<AudioListener>().enabled = false;
			curActiveCamera = changeCamera;
			curActiveCamera.enabled = true;
			curActiveCamera.transform.GetComponent<AudioListener>().enabled = true;
			if (curActiveCamera.GetComponent<DialogSystem>())
			{
				curActiveCamera.GetComponent<DialogSystem>().startDialog();
			}
			else
			{
				player.hud.HudChange(0);
			}
			player.ActiveCamera = curActiveCamera;
			gameObject.SetActive(false);
		}
	}


	//spawns dialog buttons
	private void spawnDialogButtons(DialogOptions diado)
	{
		if (!useNewSystem)
		{
			for (int v = 0; v < diado.Dialogs.Count; v++)
			{
				Button dialogbutton = Instantiate<Button>(dialogButton);

				dialogbutton.transform.GetComponentInChildren<Text>().text = diado.Dialogs[v];

				dialogbutton.GetComponent<DialogButtonScript>().nextDialog = diado.NextIndex[v];

				dialogbutton.GetComponent<DialogButtonScript>().ds = this;
				dialogButtons.Add(dialogbutton);
				dialogbutton.transform.SetParent(hud.dialogText.transform);
				Vector3 pos = dialogbutton.transform.position = hud.dialogText.transform.position;
				pos.y -= 40 * v;
				dialogbutton.transform.position = pos;
			}
		}
		else
		{
			for (int v = 0; v < Dialog[dialogIndex].dialogChoices.Dialogs.Count; v++)
			{
				Button dialogbutton = Instantiate<Button>(dialogButton);
				dialogbutton.transform.GetComponentInChildren<Text>().text = Dialog[dialogIndex].dialogChoices.Dialogs[v];

				dialogbutton.GetComponent<DialogButtonScript>().nextDialog = Dialog[dialogIndex].dialogChoices.NextIndex[v];

				dialogbutton.GetComponent<DialogButtonScript>().ds = this;
				dialogButtons.Add(dialogbutton);
				dialogbutton.transform.SetParent(hud.dialogText.transform);
				Vector3 pos = dialogbutton.transform.position = hud.dialogText.transform.position;
				pos.y -= 40 * v;
				dialogbutton.transform.position = pos;
			}
		}
	}
	private void spawnDialogButtons()
	{
		for (int v = 0; v < Dialog[dialogIndex].dialogChoices.Dialogs.Count; v++)
		{
			Button dialogbutton = Instantiate<Button>(dialogButton);
			dialogbutton.transform.GetComponentInChildren<Text>().text = Dialog[dialogIndex].dialogChoices.Dialogs[v];

			dialogbutton.GetComponent<DialogButtonScript>().nextDialog = Dialog[dialogIndex].dialogChoices.NextIndex[v];

			dialogbutton.GetComponent<DialogButtonScript>().ds = this;
			dialogButtons.Add(dialogbutton);
			dialogbutton.transform.SetParent(hud.dialogText.transform);
			Vector3 pos = dialogbutton.transform.position = hud.dialogText.transform.position;
			pos.y -= 40 * v;
			dialogbutton.transform.position = pos;
		}
	}
}
