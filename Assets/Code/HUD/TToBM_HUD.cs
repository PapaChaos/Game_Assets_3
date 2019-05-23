using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TToBM_HUD : MonoBehaviour
{
	public Text CameraName, interactionText, DateandTimeText, dialogText;
	public int[] DaysPerMonth;
	public int Day =1, Month = 1, Year = 2459, Hour = 12, Minute = 2, Second = 1;
	public GameObject InventoryButton, InventoryWindow, InventoryContent;
	public bool Open = false;
	public DialogSystem curActiveDialog;
	public PlayerMovement player;
	public bool startingDialog;


	public void Start()

	{
		if (!startingDialog)
		{
			dialogText.enabled = false;
			InvokeRepeating("updateGameTime", 0, 1.0f);
		}
		else
		{
			dialogText.enabled = true;
			curActiveDialog.startDialog(0);

		}

	}

	public void ChangeCameraName(string name)
	{
		CameraName.text = name;
	}
	public void ChangeInteractionText(string text)
	{
		interactionText.text = text;
	}


	public void HudChange(int hudtype)
	{
		if (hudtype == 0)//Standard Camera
		{
			player.canMove = true;
			dialogText.enabled = false;
		}
		else if (hudtype == 1)//Inventory
		{

		}
		else if (hudtype == 2)//Dialog
		{
			player.canMove = false;
			Inventory(false);
			player.animator.SetInteger("Interaction", 0);
			dialogText.enabled = true; 
		}
	}


	public void Inventory(bool Opening)
	{
		Open = Opening;
		InventoryWindow.SetActive(Opening);
		InventoryButton.SetActive(!Opening);
	}



	void updateGameTime()
	{
		if(Second < 59)
		Second++;

		else
		{
			Second = 0;

			if(Minute < 59)
			Minute++;
			else
			{
				Minute = 0;
				if(Hour<23)
				Hour++;
				else
				{
					Day++;
					Hour = 0;
				}
			}

		}
		string datetimestring;
		string ddSec;
		if (Second < 10)
			ddSec = string.Format("0{0}", Second);
		else
			ddSec = Second.ToString();

		string ddMin;
		if (Minute < 10)
			ddMin = string.Format("0{0}", Minute);
		else
			ddMin = Minute.ToString();

		string ddHour;
		if (Hour < 10)
			ddHour = string.Format("0{0}", Hour);
		else
			ddHour = Hour.ToString();

		string ddDay;
		if (Day < 10)
			ddDay = string.Format("0{0}", Day);
		else
			ddDay = Day.ToString();

		string ddMonth;
		if (Month < 10)
			ddMonth = string.Format("0{0}", Month);
		else
			ddMonth = Month.ToString();
		datetimestring = string.Format("{0}/{1}/{2}  {3}:{4}:{5}", ddDay, ddMonth, Year, ddHour, ddMin, ddSec);

		DateandTimeText.text = datetimestring;
	}
}
