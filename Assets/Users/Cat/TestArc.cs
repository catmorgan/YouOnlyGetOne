using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestArc : NetworkBehaviour {

    public Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!isLocalPlayer)
        {
            return;
        }

        var vertices = 10.0f / Time.fixedDeltaTime;
        var line = GetComponent<LineRenderer>();
        line.numPositions = (int) vertices;
        var initialForce = new Vector3(0, 2 * 5f, 10f);
        var velocity = rb.velocity + initialForce * rb.mass;
        var position = transform.position;
        var i = 0;
        for (var t = 0f; t < 10.0f; t+= Time.fixedDeltaTime)
        {
            line.SetPosition(i, position);
            velocity += Physics.gravity * Time.fixedDeltaTime;
            position += velocity * Time.fixedDeltaTime;
            i++;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(0, 2 * 5f, 10f, ForceMode.Impulse);
        }
    }
}
