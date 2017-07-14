using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndMove : MonoBehaviour {

	public GameObject enemies;
	public Vector3 spawnValues;
	public float spawnWait;
	public float spawnMaxWait;
	public float spawnMinWait;
	public int startWait;
	public bool stop;

	int randEnemy;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (waitSpawner());
	}
	
	// Update is called once per frame
	void Update () 
	{
		spawnWait = Random.Range (spawnMinWait, spawnMaxWait);	

	}

	[ExecuteInEditMode]
	void OnDrawGizmosSelected() 
	{
        Gizmos.color = new Color(0, 1, 0, 1F);
		Gizmos.DrawWireCube(transform.position, spawnValues);
    }

	IEnumerator waitSpawner()
	{
		yield return new WaitForSeconds (startWait);

		while (!stop) 
		{
			//randEnemy = Random.Range (minEnemies, maxEnemies);

			Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), 1, Random.Range (-spawnValues.z, spawnValues.z));

			Instantiate (enemies, spawnPosition + transform.TransformPoint (0, 0, 0), gameObject.transform.rotation);

			yield return new WaitForSeconds (spawnWait);



		}

	}




}
