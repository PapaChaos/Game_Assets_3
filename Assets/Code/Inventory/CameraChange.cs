using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
	public Camera Ref_camera;

	public bool drawDebugBox = true;

	public void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<PlayerMovement>())
		{
			other.GetComponent<PlayerMovement>().ChangeCameraView(Ref_camera);
		}
	}
	// Draws the Light bulb icon at position of the object.
	private void OnDrawGizmos()
	{
		if (drawDebugBox)
		{
			Gizmos.color = new Color(1, 0, 0, 0.2f);
			Gizmos.DrawCube((transform.position+ transform.GetComponent<BoxCollider>().center), transform.GetComponent<BoxCollider>().size);
		}
	}
}
