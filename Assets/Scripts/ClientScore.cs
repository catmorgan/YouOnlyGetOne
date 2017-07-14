using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClientScore : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    [ClientRpc]
    // this will be invoked on all clients
    public void RpcDoOnClient(string scores)
    {
        Debug.Log("OnClient " + scores.ToString());
    }
}
