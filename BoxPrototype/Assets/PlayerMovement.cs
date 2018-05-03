using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var horizontal = Input.GetAxis("Horizontal");

        this.transform.position = new Vector3(horizontal, 0.0f, 0.0f) * Time.deltaTime * 5 + transform.position;
    }
}
