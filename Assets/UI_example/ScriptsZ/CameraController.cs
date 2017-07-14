using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : NetworkBehaviour
{
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        var rotateUpDown = Input.GetAxis("Vertical") * Time.deltaTime * 150.0f;
        transform.Rotate(rotateUpDown, 0, 0);

        var rotateLeftRight = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        transform.Rotate(0, rotateLeftRight, 0);
        
        if (Input.GetKey(KeyCode.Q))
        {
            var rotateLeft = Time.deltaTime * 150.0f;
            transform.Rotate(0, 0, rotateLeft);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            var rotateRight = Time.deltaTime * 150.0f * -1; 
            transform.Rotate(0, 0, rotateRight);
        }
    }
}
