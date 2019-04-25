using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
	public Camera Ref_camera;

	public void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<PlayerMovement>())
		{
			other.GetComponent<PlayerMovement>().ChangeCameraView(Ref_camera);
		}
	}
}
