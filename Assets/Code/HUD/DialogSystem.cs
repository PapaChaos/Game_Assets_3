using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogSystem : MonoBehaviour
{
	public Text curDialog;
	public List<string> dialogText;
	public List<AudioClip> dialogAudio;
	public int dialogIndex;
	public int[] dialogOptionIndex;
	public List<Color> dialogTextColor;
	public AudioSource audioSource;
	public bool dialogHappening;
	public PlayerMovement player;
	public enum dialogEndingAction { BackToGame, ChangeScene, ChangeCamera}
	public dialogEndingAction endingAction = dialogEndingAction.BackToGame;
	public string SceneChangeName;
	public Camera curActiveCamera;
	public Camera changeCamera;

	public TToBM_HUD hud;

	void Start()
    {
		if (gameObject.GetComponents<AudioSource>().Length < 1)
			audioSource = gameObject.AddComponent<AudioSource>();
		else
			audioSource = gameObject.GetComponents<AudioSource>()[0]; //pick the first found source...
	}
	public void startDialog(int index)
	{
		PlayDialogIndex();
	}
	public int DialogOption()
	{
		//add text dialog options
		return 0;//to be added later
		//remember to delete text dialog options.
	}

	public void PlayDialogIndex()
	{
		if (dialogIndex > dialogText.Count)
		{
			if (endingAction == dialogEndingAction.BackToGame)
				player.hud.HudChange(0);

			else if (endingAction == dialogEndingAction.ChangeScene)
				SceneManager.LoadScene(SceneChangeName);

			else if (endingAction == dialogEndingAction.ChangeCamera)
			{
				curActiveCamera.enabled = false;
				curActiveCamera.transform.GetComponent<AudioListener>().enabled = false;
				curActiveCamera = changeCamera;
				curActiveCamera.enabled = true;
				curActiveCamera.transform.GetComponent<AudioListener>().enabled = true;
				if (curActiveCamera.GetComponent<DialogSystem>())
				{
					curActiveCamera.GetComponent<DialogSystem>().PlayDialogIndex();
				}
				else
				{
					player.hud.HudChange(0);
				}
				player.ActiveCamera = curActiveCamera;
				gameObject.SetActive(false);
			}
		}
		else
		{
			dialogHappening = true;
			curDialog.text = dialogText[dialogIndex];
			audioSource.PlayOneShot(dialogAudio[dialogIndex]);
		}
	}

	private void Update()
	{
		if (!audioSource.isPlaying && dialogHappening)
		{
			dialogIndex++;
			dialogHappening = false;
			PlayDialogIndex();
		}
	}
}
