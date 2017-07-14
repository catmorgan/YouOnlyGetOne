
using UnityEngine;
using System.Collections;

public class RotateOverTime : MonoBehaviour {


	public float RotateSpeedX = 0;
	public float RotateSpeedY = 0;
	public float RotateSpeedZ = 0;
	public bool RandomStartX = false;
	public bool RandomStartY = false;
	public bool RandomStartZ = false;


	void Start () {
		// set random rotation
		if (RandomStartX)
			transform.Rotate(Vector3.left, Random.Range(0,360));
		if (RandomStartY)
			transform.Rotate(Vector3.up, Random.Range(0,360));
		if (RandomStartZ)
			transform.Rotate(Vector3.forward, Random.Range(0,360));
	}

	void Update () {
		// update rotation
		transform.Rotate(Vector3.left, Time.deltaTime * RotateSpeedX, Space.Self);
		transform.Rotate(Vector3.up, Time.deltaTime * RotateSpeedY, Space.Self);
		transform.Rotate(Vector3.forward, Time.deltaTime * RotateSpeedZ, Space.Self);
	}
}
