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

    private Action _chaseHandler;
    private Action _attackHandler;

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
    }

    private void Update()
    {
        if (_canAct && _playerDetector.CanAttackPlayer())
        {
            _attackHandler?.Invoke();
        }
        else if (_canAct && _playerDetector.CanChasePlayer())
        {
            _chaseHandler?.Invoke();
        }
        else if (_canAct)
        {
            _animator.SetInteger("MoveMode", 0);
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
        Vector3 moveVelocity = (_playerDetector.GetDetectedPlayerPosition() - transform.position).normalized;
        _rigid.velocity = new Vector3(moveVelocity.x, _rigid.velocity.y, moveVelocity.z) * _statManager.MoveSpeed;
        transform.rotation = Quaternion.LookRotation(_playerDetector.GetDetectedPlayerPosition() - transform.position);
        _animator.SetInteger("MoveMode", 1);
    }

    private void Die(GameObject killer)
    {
        _animator.SetTrigger("Die");
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        _motionStopTime = Time.time + 1;
        yield return null;
        _motionStopTime = Time.time + _animator.GetNextAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(_animator.GetNextAnimatorClipInfo(0).Length);

        Destroy(gameObject);
    }
}
