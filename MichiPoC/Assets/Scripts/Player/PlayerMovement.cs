using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5;
    public float JumpForce = 5;
    public Animator Animator;

    private Rigidbody2D _rb;
    private bool _canMove = true;
    private bool _canBeStunned = true;
    private float _stunTime = 1.25f;
    private float _stunImmuneTime = 3.0f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(_canMove)
        {
            Movement();
            Jump();
        }
    }

    private void Movement()
    {
        var horizontal = Input.GetAxis("Horizontal");

        if(horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        Animator.SetFloat("MoveSpeed", Math.Abs(horizontal));

        float signedDirection = horizontal > 0 ? 1 : -1;

        Vector2 position = new Vector2(transform.position.x + signedDirection * GetComponent<BoxCollider2D>().size.x / 2.0f, transform.position.y);
        Vector2 direction = new Vector2(horizontal, 0).normalized;

        //Debug.DrawRay(position, direction * 1f, Color.red, 1/120f);
        RaycastHit2D[] hits = new RaycastHit2D[100];
        ContactFilter2D filter = new ContactFilter2D();
        Physics2D.Raycast(position, direction, filter, hits, 0.05f);

        bool hitSomething = false;

        foreach(var hit in hits)
        {
            if(hit.collider != null && hit.collider.gameObject.tag != "Player")
            {
                //Debug.Log("Hit " + hit.collider.gameObject.name);
                hitSomething = true;
            }
        }

        if (!hitSomething)
        {
            transform.position = new Vector3(horizontal, 0.0f, 0.0f) * Time.deltaTime * Speed + transform.position;
        }
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

    public void StunAndKnockBack(Vector3 puncherPosition)
    {
        if(_canBeStunned)
        {
            _canMove = false;
            _canBeStunned = false;
            StartCoroutine(StunDuration());
            
            float direction = transform.position.x > puncherPosition.x ? 1 : -1;

            _rb.AddForce(new Vector2(direction * 100, 50));
        }
    }

    private IEnumerator StunDuration()
    {
        yield return new WaitForSeconds(_stunTime);
        
        _canMove = true;

        yield return new WaitForSeconds(_stunImmuneTime);

        _canBeStunned = true;
    }
}
