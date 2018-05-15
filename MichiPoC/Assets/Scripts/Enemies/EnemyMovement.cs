using System;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private AttackType _attackType;
    private Vector2 _startPos;
    private float _direction = -1;
    private float _moveDistance;

    private float _slowMoveSpeed = 1.5f;
    private float _fastMoveSpeed = 2.5f;
    private float _dashSpeed = 10.0f;
    private float _dashCooldown = 3.0f;

    private bool _shouldDash = true;
    private float _dashEndXPos;
    private Vector2 _rightDashEndPos;
    private Vector2 _leftDashEndPos;
    private Vector2 _lastDashPos;

    private void Start()
    {
        _attackType = GetComponent<EnemyManager>().AttackType;
        _startPos = transform.position;
        _moveDistance = transform.lossyScale.x * 1.2f;
    }

    private void Update()
    {
        if (!GetComponent<EnemyManager>().Dead)
        {
            switch (_attackType)
            {
                case AttackType.DestroyGuitar:
                    MoveSlowly();
                    break;

                case AttackType.StunPlayer:
                    Dash();
                    break;

                case AttackType.PunchGuitarOutOfHands:
                    MoveFast();
                    break;
            }
        }
    }

    private void MoveSlowly()
    {
        MoveWithSpeed(_slowMoveSpeed);
    }

    private void MoveFast()
    {
        MoveWithSpeed(_fastMoveSpeed);
    }

    private void MoveWithSpeed(float moveSpeed)
    {
        if (_direction == -1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        transform.position = new Vector3(transform.position.x + (moveSpeed * Time.deltaTime * _direction), transform.position.y);
    }

    private void Dash()
    {
        if (_shouldDash)
        {
            _shouldDash = false;

            _rightDashEndPos = transform.Position2D() + new Vector2(_moveDistance, 0);
            _leftDashEndPos = transform.Position2D() - new Vector2(_moveDistance, 0);

            StartCoroutine(DashCooldown());

            if (_direction == 1)
            {
                _dashEndXPos = _rightDashEndPos.x;
            }
            else
            {
                _dashEndXPos = _leftDashEndPos.x;
            }
        }

        if(Math.Abs(transform.position.x - _dashEndXPos) < 0.025)
        {
            if(_direction == 1)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        _lastDashPos = transform.Position2D();
        transform.position = new Vector2(Mathf.Lerp(transform.position.x, _dashEndXPos, Time.deltaTime * _dashSpeed), transform.position.y);
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(_dashCooldown);

        _shouldDash = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        _direction *= -1;

        if(_attackType == AttackType.StunPlayer)
        {
            _dashEndXPos = _lastDashPos.x;
            transform.SetPosition2D(_lastDashPos);
        }
    }

}
