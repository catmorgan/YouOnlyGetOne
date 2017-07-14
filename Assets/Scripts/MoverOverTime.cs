using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverOverTime : MonoBehaviour {

	public Vector3 directionValues;
	public float minSpeed = 1f;
	public float maxSpeed = 5f;
	float randSpeed;




void Update() {

		randSpeed = Random.Range (minSpeed, maxSpeed);
		transform.Translate(directionValues * Time.deltaTime * randSpeed);
        //transform.Translate(Vector3.up * Time.deltaTime, Space.World);
    }
	
}
