using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private AttackType _attackType;
    private Vector2 _startPos;
    private float _direction = -1;
    private float _moveDistance;

    private float _slowMoveSpeed = 0.15f;
    private float _fastMoveSpeed = 0.75f;
    private float _dashSpeed = 15.0f;
    private float _dashCooldown = 2.0f;

    private bool _shouldDash = true;
    private Vector2 _dashEndPos;
    private Vector2 _rightDashEndPos;
    private Vector2 _leftDashEndPos;

    private void Start()
    {
        _attackType = GetComponent<EnemyManager>().AttackType;
        _startPos = transform.position;
        _moveDistance = transform.lossyScale.x / 4.0f;
        _rightDashEndPos = _startPos + new Vector2(_moveDistance, 0);
        _leftDashEndPos = _startPos - new Vector2(_moveDistance, 0);
        _dashEndPos = _leftDashEndPos;
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
            _direction = transform.position.x < _startPos.x - _moveDistance ? _direction * -1 : _direction;
        }
        else
        {
            _direction = transform.position.x > _startPos.x + _moveDistance ? _direction * -1 : _direction;
        }

        transform.position = new Vector3(transform.position.x + (moveSpeed * Time.deltaTime * _direction), transform.position.y);
    }

    private void Dash()
    {
        if (_shouldDash)
        {
            _shouldDash = false;

            StartCoroutine(DashCooldown());

            if (Vector2.Distance(transform.position, _leftDashEndPos) < Vector2.Distance(transform.position, _rightDashEndPos))
            {
                _dashEndPos = _rightDashEndPos;
            }
            else
            {
                _dashEndPos = _leftDashEndPos;
            }
        }

        transform.position = Vector2.Lerp(transform.position, _dashEndPos, Time.deltaTime * _dashSpeed);
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(_dashCooldown);

        _shouldDash = true;
    }


}
