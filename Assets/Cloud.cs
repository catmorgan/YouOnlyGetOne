using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

    public float speed = 15f;
    Vector3 temp;

    // Update is called once per frame
    void Update()
    {
        temp = transform.localScale;
        temp.x += Time.deltaTime;
        temp.y += Time.deltaTime;
        temp.z += Time.deltaTime;

        transform.Translate(0,0,speed * Time.deltaTime);
        transform.localScale=temp;        
 

    }
}  
