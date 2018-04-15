using UnityEngine;
using System;

public class PlayerJump : MonoBehaviour {

    Rigidbody2D rb;

    public float jumpForce = 5;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.W) && Math.Abs(rb.velocity.y) < 0.01)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
        }

		if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics.gravity.y * 1.5f * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * 1 * Time.deltaTime;
        }
	}
}
