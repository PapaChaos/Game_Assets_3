using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Linq;


[CustomEditor(typeof(DialogSystem))]
public class DialogEditor : Editor
{
	//private variables 
	bool showDialog = true;
	bool showDialogOptions = false;
	bool showWholeDialogTree = false;



	public override void OnInspectorGUI()
	{
		DialogSystem ds = (DialogSystem)target;
		GUILayout.Label("Current Dialog Index: " + ds.dialogIndex);
		ds.dialogButton = (Button)EditorGUILayout.ObjectField("Dialog Button:", ds.dialogButton, typeof(Button), true);
		ds.useNewSystem = EditorGUILayout.Toggle("Use new Dialog System?", ds.useNewSystem);
		ds.endingAction = (DialogSystem.dialogEndingAction)EditorGUILayout.EnumPopup("Dialog Ending Action: ", ds.endingAction);

		if (ds.endingAction == DialogSystem.dialogEndingAction.ChangeScene)
		{
			ds.SceneChangeName = EditorGUILayout.TextField("Next Scene:", ds.SceneChangeName);
		}
		else if(ds.endingAction == DialogSystem.dialogEndingAction.ChangeCamera)
		{
			ds.changeCamera = (Camera)EditorGUILayout.ObjectField("Next Camera:", ds.changeCamera, typeof(Camera), true);
		}

		if (!Selection.activeTransform)
		{
			showDialogOptions = false;
		}
		EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

		//Adding a fold out at the end for all the waypoints. This way EditorGUI doesn't look so massive.
		GUILayout.BeginHorizontal();
		showWholeDialogTree = EditorGUILayout.Foldout(showWholeDialogTree, "Dialog: " + ds.Dialog.Count);

		GUILayout.EndHorizontal();
		if (showWholeDialogTree)
			if (Selection.activeTransform)
			{
				if (ds.Dialog.Count.Equals(0))
				{
					if (GUILayout.Button("Add"))
					{
						ds.Dialog.Add(new DialogSystem.DialogLine());
						/*ds.Dialog.Sort();
						ds.Dialog[0].dialog.dialogText = "";
						ds.Dialog[0].dialog.dialogAudio = null;
						ds.Dialog[0].dialogChoice = false;
						ds.Dialog[0].dialogChoices.Dialogs = new List<string>();
						ds.Dialog[0].dialogChoices.NextIndex = new List<int>();*/

					}
				}
				if (!ds.Dialog.Count.Equals(0))
				{
					for (int listcount = 0; listcount < ds.Dialog.Count; listcount++)
					{

						EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
						GUILayout.BeginHorizontal();

						if (GUILayout.Button("Add Before"))
						{
							ds.Dialog.Insert(listcount, new DialogSystem.DialogLine());
							ds.Dialog[0].dialog.dialogText = "";
							ds.Dialog[0].dialog.dialogAudio = null;
							ds.Dialog[0].dialogChoice = false;
							ds.Dialog[0].dialogChoices.Dialogs = new List<string>();
							ds.Dialog[0].dialogChoices.NextIndex = new List<int>();
							/*ds.dialogText.Insert(listcount,"");
							ds.dialogAudio.Insert(listcount, null);
							ds.dialogChoices.Insert(listcount,false);*/
						}
						if (GUILayout.Button("Remove"))
						{
							ds.Dialog.RemoveAt(listcount);
							/*ds.dialogText.RemoveAt(listcount);
							ds.dialogAudio.RemoveAt(listcount);
							ds.dialogChoices.RemoveAt(listcount);*/
						}
						if (GUILayout.Button("Add after"))
						{
							ds.Dialog.Insert(listcount + 1, new DialogSystem.DialogLine());
							ds.Dialog[listcount + 1].dialog.dialogText = "asdfasdf";
							ds.Dialog[listcount + 1].dialog.dialogAudio = null;
							ds.Dialog[listcount + 1].dialogChoice = false;
							ds.Dialog[listcount + 1].dialogChoices.Dialogs = new List<string>();
							ds.Dialog[listcount + 1].dialogChoices.NextIndex = new List<int>();
							/*ds.dialogText.Insert(listcount+1, "");
							ds.dialogAudio.Insert(listcount+1, null);
							ds.dialogChoices.Insert(listcount+1, false);*/
						}
						GUILayout.EndHorizontal();
						GUILayout.Label("Index: " + listcount);
						ds.Dialog[listcount].dialogChoice = EditorGUILayout.Toggle("Dialog Options?", ds.Dialog[listcount].dialogChoice);
						if (!ds.Dialog[listcount].dialogChoice)
						{
							ds.Dialog[listcount].dialog.dialogText = EditorGUILayout.TextField("Dialog Text:", ds.Dialog[listcount].dialog.dialogText);
							ds.Dialog[listcount].dialog.dialogAudio = (AudioClip)EditorGUILayout.ObjectField("Audio Clip:", ds.Dialog[listcount].dialog.dialogAudio, typeof(AudioClip), true);
						}
						else if (ds.Dialog[listcount].dialogChoice)
						{
							if (!ds.Dialog[listcount].dialogChoices.Dialogs.Any())
							{
								ds.Dialog[listcount].dialogChoices.Dialogs.Add("");
								ds.Dialog[listcount].dialogChoices.NextIndex.Add(9999);
							}

							for (int d = 0; d < ds.Dialog[listcount].dialogChoices.Dialogs.Count(); d++)
							{
								EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
								GUILayout.Label("Dialog Option: " + d);

								GUILayout.BeginHorizontal();
								if (GUILayout.Button("Add Before"))
								{
									ds.Dialog[listcount].dialogChoices.Dialogs.Insert(d, "");
									ds.Dialog[listcount].dialogChoices.NextIndex.Insert(d, 9999);
								}

								if (d != 0)
								{
									if (GUILayout.Button("Remove"))
									{
										ds.Dialog[listcount].dialogChoices.Dialogs.RemoveAt(d);
										ds.Dialog[listcount].dialogChoices.NextIndex.RemoveAt(d);
									}
								}

								if (GUILayout.Button("Add after"))
								{
									ds.Dialog[listcount].dialogChoices.Dialogs.Insert(d + 1, "");
									ds.Dialog[listcount].dialogChoices.NextIndex.Insert(d + 1, 9999);

								}
								GUILayout.EndHorizontal();
								ds.Dialog[listcount].dialogChoices.Dialogs[d] = EditorGUILayout.TextField("Dialog option text:", ds.Dialog[listcount].dialogChoices.Dialogs[d]);
								ds.Dialog[listcount].dialogChoices.NextIndex[d] = EditorGUILayout.IntField("Index after dialog option:", ds.Dialog[listcount].dialogChoices.NextIndex[d]);

							}
						}
						/*
						if (ds.dialogText.Count() < listcount)
							ds.dialogText[listcount] = EditorGUILayout.TextField("Subtitle:", ds.dialogText[listcount]);

						if (ds.dialogAudio.Count() < listcount)
							ds.dialogAudio[listcount] = (AudioClip)EditorGUILayout.ObjectField(ds.dialogAudio[listcount], typeof(AudioClip), true);
						*/
						GUILayout.Space(5);
					}
				}
				EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);


			}
		if (!Selection.activeTransform)
		{
			showWholeDialogTree = false;
		}
	}
}
