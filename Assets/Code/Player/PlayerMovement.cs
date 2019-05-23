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
	public GameObject MainSkeleton;
	public Animator animator;
	public bool canMove = true;
	public bool engCode, hangarCode;
	void Start()
	{
		hud.player = this;
		agent = GetComponent<NavMeshAgent>();
		if(!hud.startingDialog)
		ChangeCameraView(ActiveCamera);
	}

	void Update()
	{
		if (canMove)
		{
			float velocity = agent.velocity.magnitude / agent.speed;

			if (velocity > 0)
			{
				animator.SetInteger("Interaction", 1);
				animator.SetFloat("movementSpeed", velocity);
			}
			if (velocity == 0)
			{
				animator.SetInteger("Interaction", 0);
			}
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
						//print("Raycasting");
						if (Physics.Raycast(ActiveCamera.ScreenPointToRay(Input.mousePosition), out hit, 2000))
						{
							agent.destination = hit.point;

							print("Raycasting" + hit.point);
							if (hit.transform.gameObject.GetComponent<InteractableWorldObject>())
							{
								interacting = hit.transform.gameObject;
							}
						}

					}
				}
				if (interacting)
				{
					if (interacting.GetComponent<InteractableWorldObject>().InteractableRange == 0)
					{
						if (Vector3.Distance(interacting.transform.position, transform.position) < 1f)
						{


							interacting.GetComponent<InteractableWorldObject>().interactor = this;
							interacting.GetComponent<InteractableWorldObject>().Interact();
							interacting = null;
							hud.ChangeInteractionText("");
							agent.destination = agent.transform.position;
						}
					}
					else if (interacting.GetComponent<InteractableWorldObject>().InteractableRange > 0)
					{
						if (Vector3.Distance(interacting.transform.position, transform.position) < interacting.GetComponent<InteractableWorldObject>().InteractableRange)
						{


							interacting.GetComponent<InteractableWorldObject>().interactor = this;
							interacting.GetComponent<InteractableWorldObject>().Interact();
							interacting = null;
							hud.ChangeInteractionText("");
							agent.destination = agent.transform.position;
						}
					}
				}

			}
			else
				Time.timeScale = 0;
		}

	}
	public void InteractionAnimation(int action, float reactiv)
	{
		canMove = false;
		animator.SetInteger("Interaction", action);
		StartCoroutine(reactivateMovement(reactiv));

	}
	IEnumerator reactivateMovement(float timer)
	{
		yield return new WaitForSeconds(timer);
		canMove = true;
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
