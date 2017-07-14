using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTimer : MonoBehaviour {

	public float killTimeMin;
	public float killTimeMax;
	float randKillTime;

	// Use this for initialization
	void Start () {
		randKillTime = Random.Range (killTimeMin, killTimeMax);
		Destroy (gameObject, randKillTime);	
	}
	

}
