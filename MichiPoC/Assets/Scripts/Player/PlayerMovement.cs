using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5;
    public float JumpForce = 5;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
        Jump();
    }

    private void Movement()
    {
        var horizontal = Input.GetAxis("Horizontal");

        transform.position = new Vector3(horizontal, 0.0f, 0.0f) * Time.deltaTime * Speed + transform.position;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && Math.Abs(_rb.velocity.y) < 0.01)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * JumpForce;
        }

        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector2.up * Physics.gravity.y * 1.5f * Time.deltaTime;
        }
        else if (_rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * 1 * Time.deltaTime;
        }
    }

}
