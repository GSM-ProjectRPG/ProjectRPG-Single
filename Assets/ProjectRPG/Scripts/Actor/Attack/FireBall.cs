using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] float _moveSpeed;

    private float _damage;
    private AttackSystem _attacker;
    private DamageReciever _target;
    private Collider _targetCollider;
    private Rigidbody _rigid;
    private float _time;
    private bool _isDestroyed = false;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time > 10)
        {
            HandleDestroy();
        }
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            Vector3 targetPos = _targetCollider.bounds.center;

            transform.LookAt(targetPos);
            _rigid.MovePosition(Vector3.MoveTowards(_rigid.position, targetPos, _moveSpeed * Time.fixedDeltaTime));
        }
        else
        {
            _rigid.MovePosition(_rigid.position + _rigid.rotation * Vector3.forward * _moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void SetTarget(float damage, AttackSystem attacker, DamageReciever target)
    {
        _damage = damage;
        _attacker = attacker;
        _target = target;
        if (_target != null)
        {
            _targetCollider = _target.GetComponentInChildren<Collider>();
        }
    }

    private void HandleDestroy()
    {
        if (_isDestroyed) return;
        _isDestroyed = true;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _attacker.gameObject) return;

        if (_target != null && other.gameObject == _target.gameObject)
        {
            _attacker.SendDamage(_target, _damage);
        }
        else if (other.GetComponent<DamageReciever>() != null)
        {
            _attacker.SendDamage(other.GetComponent<DamageReciever>(), _damage);
        }
        HandleDestroy();
    }
}
