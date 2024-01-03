using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuscleCatController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Rigidbody _rigid;
    private Health _health;
    private DamageReciever _damageReciever;
    private MonsterStatSystem _statManager;
    private MonsterPlayerDetector _playerDetector;
    private AttackSystem _attackSystem;
    private ActSystem _actSystem;
    private MuscleCatAttacker _attacker;

    private Action _chaseHandler;
    private Action _attackHandler;

    private bool _isDead = false;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _health = GetComponent<Health>();
        _damageReciever = GetComponent<DamageReciever>();
        _statManager = GetComponent<MonsterStatSystem>();
        _playerDetector = GetComponent<MonsterPlayerDetector>();
        _attackSystem = GetComponent<AttackSystem>();
        _actSystem = GetComponent<ActSystem>();
        _attacker = GetComponent<MuscleCatAttacker>();

        _damageReciever.OnTakeDamage += (damage, attacker) => _health.TakeDamage(damage, attacker);
        _health.OnDead += (killer) => Die(killer);

        _attackHandler = _attackSystem.Attack(_attacker.Attack);
        _chaseHandler = _actSystem.Act(Chase);
    }

    private void Update()
    {
        if (_playerDetector.CanAttackPlayer() && _attacker.CanAttack && !_isDead)
        {
            _attackHandler?.Invoke();
        }
        else if (!_isDead && !_attacker.IsAttacking)
        {
            _chaseHandler?.Invoke();
        }
    }

    private void Chase()
    {
        Vector3 moveVelocity = (_playerDetector.GetDetectedPlayerPosition() - transform.position).normalized;
        _rigid.velocity = new Vector3(moveVelocity.x, _rigid.velocity.y, moveVelocity.z) * _statManager.MoveSpeed;
        Quaternion rot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_playerDetector.GetDetectedPlayerPosition() - transform.position), Time.deltaTime * 5);
        transform.rotation = rot;
    }

    private void Die(GameObject killer)
    {
        _isDead = true;
        _animator.SetTrigger("Die");
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        yield return null;
        yield return new WaitForSeconds(_animator.GetNextAnimatorClipInfo(0).Length);

        Destroy(gameObject);
    }
}
