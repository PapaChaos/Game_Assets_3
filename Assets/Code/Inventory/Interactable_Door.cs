using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Door : InteractableWorldObject
{
	public bool closed = true;
	public bool Locked = true;

	public float openingHeight;
	public Vector3 ClosedLocation;
	public float doorSpeed = 3.5f;

	//public AudioSource sfx_Locked;
	//public AudioSource sfx_Moving;
	public AudioSource audioSource;
	public AudioClip sfx_Locked;
	public AudioClip sfx_Moving;
	public enum needCode{ none, EngineerCode, HangarCode};
	public needCode code = needCode.none;
	void Start()
	{
		sfx_Locked = Resources.Load<AudioClip>("Audio/SFX/error");
		sfx_Moving = Resources.Load<AudioClip>("Audio/SFX/space_door_close_and_open");
		audioSource = gameObject.AddComponent<AudioSource>();

		if (closed)
			ClosedLocation = transform.position;
		else
		{
			ClosedLocation = transform.position;
			ClosedLocation.y -= openingHeight;
		}

		if (closed)
			interactionType = interactionTypes.Open;
		else
			interactionType = interactionTypes.Close;
	}

	public override void Interact()
	{
		if (!Locked || code == needCode.HangarCode && interactor.hangarCode || code == needCode.EngineerCode && interactor.engCode)
		{
			//add sfx here
			/*
			if (closed)
				transform.position = new Vector3(ClosedLocation.x, ClosedLocation.y + openingHeight, ClosedLocation.z);

			else
				transform.position = new Vector3(ClosedLocation.x, ClosedLocation.y, ClosedLocation.z);*/

			closed = !closed;

			if (closed)
				interactionType = interactionTypes.Open;
			else
				interactionType = interactionTypes.Close;

			audioSource.PlayOneShot(sfx_Moving);
		}
		else
		{
			audioSource.PlayOneShot(sfx_Locked);
		}
	}
	void moveDoor(bool closed)
	{

	}
	void Update()
	{
		Vector3 pointA = ClosedLocation;
		Vector3 pointB = new Vector3(ClosedLocation.x, ClosedLocation.y + openingHeight, ClosedLocation.z);
		float speed = doorSpeed * Time.deltaTime;
		/*
		if (closed && transform.position == pointB || !closed && transform.position == pointA)
			closed = !closed;
		*/

		if (closed && transform.position != pointA)
			transform.position = Vector3.MoveTowards(transform.position, pointA, speed);



		else if (!closed && transform.position != pointB)
			transform.position = Vector3.MoveTowards(transform.position, pointB, speed);

	}
}
