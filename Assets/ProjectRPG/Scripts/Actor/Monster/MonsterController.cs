using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Rigidbody _rigid;
    private Health _health;
    private DamageReciever _damageReciever;
    private MonsterStatSystem _statManager;
    private MonsterPlayerDetector _playerDetector;
    private AttackSystem _attackSystem;
    private ActSystem _actSystem;

    private float _motionStopTime;
    private bool _canAct => _motionStopTime < Time.time;
    private bool _isReturning;

    private Action _chaseHandler;
    private Action _attackHandler;
    private Action _returnHandler;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _health = GetComponent<Health>();
        _damageReciever = GetComponent<DamageReciever>();
        _statManager = GetComponent<MonsterStatSystem>();
        _playerDetector = GetComponent<MonsterPlayerDetector>();
        _attackSystem = GetComponent<AttackSystem>();
        _actSystem = GetComponent<ActSystem>();

        _damageReciever.OnTakeDamage += (damage, attacker) => _health.TakeDamage(damage, attacker);
        _health.OnDead += (killer) => Die(killer);

        _attackHandler = _attackSystem.Attack(Attack);
        _chaseHandler = _actSystem.Act(Chase);
        _returnHandler = _actSystem.Act(StartReturn);
    }

    private void Update()
    {
        if (_canAct && CanReturn() && !_isReturning)
        {
            _returnHandler?.Invoke();
        }
        else if (_canAct && _playerDetector.CanAttackPlayer() && !_isReturning)
        {
            _attackHandler?.Invoke();
        }
        else if (_canAct && _playerDetector.CanChasePlayer() && !_isReturning)
        {
            _chaseHandler?.Invoke();
        }
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack");
        StartCoroutine(SetMotionStun());
    }

    private IEnumerator SetMotionStun()
    {
        _motionStopTime = Time.time + 1;
        yield return null;
        _motionStopTime = Time.time + _animator.GetNextAnimatorClipInfo(0).Length;
    }

    private void Chase()
    {
        Vector3 moveVelocity = _playerDetector.GetDetectedPlayerPosition() - transform.position;
        _rigid.velocity = new Vector3(moveVelocity.x, _rigid.velocity.y, moveVelocity.z) * _statManager.MoveSpeed;
        transform.rotation = Quaternion.LookRotation(_playerDetector.GetDetectedPlayerPosition() - transform.position);
        _animator.SetInteger("MoveMode", 1);
    }

    private void StartReturn()
    {
        _isReturning = true;
        StartCoroutine(ReturnCoroutine());
    }

    private IEnumerator ReturnCoroutine()
    {
        while (!IsReturnPosition())
        {
            Vector3 returnPos = new Vector3(_statManager.ReturnPosition.x, transform.position.y, _statManager.ReturnPosition.z);
            Vector3 moveVelocity = returnPos - transform.position;
            _rigid.velocity = new Vector3(moveVelocity.x, _rigid.velocity.y, moveVelocity.z) * _statManager.MoveSpeed;
            transform.rotation = Quaternion.LookRotation(_statManager.ReturnPosition - transform.position);
            _animator.SetInteger("MoveMode", 1);
            yield return null;
        }

        _isReturning = false;
        _animator.SetInteger("MoveMode", 0);

        yield break;
    }

    private bool IsReturnPosition()
    {
        return Vector3.Distance(transform.position, new Vector3(_statManager.ReturnPosition.x, transform.position.y, _statManager.ReturnPosition.z)) < 0.5f;
    }

    private bool CanReturn()
    {
        bool isNotDetectPlayer = _playerDetector.IsNotDetectedPlayerInChaseRange();
        bool isReturnDistance = Vector3.Distance(_statManager.ReturnPosition, transform.position) >= _statManager.ReturnDistance;
        bool isReturnPosition = IsReturnPosition();
        return (isNotDetectPlayer || isReturnDistance) && !isReturnPosition;
    }

    private void Die(GameObject killer)
    {

    }
}
