using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
public class PlayerMovement : MonoBehaviour
{
	private Vector3 MouseLocation;
	private bool AbleToWalkToMouseLocation;
	public TToBM_HUD hud;
	NavMeshAgent agent;
	public GameObject interacting;
	public Camera ActiveCamera;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		ChangeCameraView(ActiveCamera);
	}

	void Update()
	{
		if (!hud.Open)
		{
			Time.timeScale = 1;

			if (!EventSystem.current.IsPointerOverGameObject())
			{
				RaycastHit hit;
				if (Physics.Raycast(ActiveCamera.ScreenPointToRay(Input.mousePosition), out hit, 2000))
				{
					if (hit.transform.gameObject.GetComponent<InteractableWorldObject>())
						hud.ChangeInteractionText(hit.transform.gameObject.GetComponent<InteractableWorldObject>().interactionText());
					else
					{
						hud.ChangeInteractionText("Walk to ");
					}
				}
				if (Input.GetMouseButtonDown(0))
				{
					interacting = null;
					//RaycastHit hit;
					print("Raycasting");
					if (Physics.Raycast(ActiveCamera.ScreenPointToRay(Input.mousePosition), out hit, 2000))
					{
						agent.destination = hit.point;
						print(hit.point);
						if (hit.transform.gameObject.GetComponent<InteractableWorldObject>())
						{
							interacting = hit.transform.gameObject;
							//print(Vector3.Distance(interacting.transform.position, transform.position));

						}
					}

				}
			}
			if (interacting && Vector3.Distance(interacting.transform.position, transform.position) < 3f)
			{
				interacting.GetComponent<InteractableWorldObject>().Interact();
				interacting = null;
				hud.ChangeInteractionText("");
			}
		}
		else
			Time.timeScale = 0;
    }

	public void ChangeCameraView(Camera cam)
	{
		ActiveCamera.enabled = false;
		ActiveCamera.transform.GetComponent<AudioListener>().enabled = false;
		ActiveCamera = cam;
		ActiveCamera.enabled = true;
		ActiveCamera.transform.GetComponent<AudioListener>().enabled = true;
		if (cam.transform.GetComponent<CameraInfo>())
			hud.ChangeCameraName(cam.transform.GetComponent<CameraInfo>().CameraName);
		else
			hud.ChangeCameraName("ERROR: UNKNOWN CAMERA!!!");
	}
}
