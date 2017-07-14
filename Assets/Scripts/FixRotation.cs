using System.Collections;
using UnityEngine;

public class FixRotation : MonoBehaviour {

	public Transform camTran;
	public Transform iconTran;

	//Quaternion rotation;

	void Awake()
	{
		iconTran.rotation = transform.rotation;
	}



	void LateUpdate()
	{
		//transform.rotation = rotation;

		iconTran.transform.eulerAngles = new Vector3(90, camTran.transform.eulerAngles.y,0);
	}
}
