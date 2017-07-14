using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScale : MonoBehaviour {

	public float minSize = 1f;
	public float maxSize = 2f;
	float randsize;

	// Use this for initialization
	void Start () {

		randsize = Random.Range (minSize, maxSize);
		Vector3 randomSize = new Vector3 (randsize,randsize,randsize);
		gameObject.transform.localScale = randomSize;
	}
	

}
