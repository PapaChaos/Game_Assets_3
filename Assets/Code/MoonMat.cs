using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonMat : MonoBehaviour
{
	public Vector3 startPos;
	public Vector3 endingPos;
	public float delta;
	void Update()
	{
		delta += Time.deltaTime;
		Vector3 position = Vector3.Lerp(startPos, endingPos, Mathf.Abs(Mathf.Sin(delta)));
		gameObject.transform.position = position;
	}
}