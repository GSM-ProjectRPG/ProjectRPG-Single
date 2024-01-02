using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] float _moveSpeed;

    private float _damage;
    private GameObject _attacker;
    private DamageReciever _target;
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
        if(_time > 10)
        {
            HandleDestroy();
        }
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            transform.LookAt(_target.transform.position);
            _rigid.MovePosition(Vector3.MoveTowards(_rigid.position, _target.transform.position, _moveSpeed * Time.fixedDeltaTime));
        }
        else
        {
            _rigid.MovePosition(_rigid.position + _rigid.rotation * Vector3.forward * _moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void SetTarget(float damage, GameObject attacker, DamageReciever target)
    {
        _damage = damage;
        _attacker = attacker;
        _target = target;
    }

    private void HandleDestroy()
    {
        if(_isDestroyed) return;
        _isDestroyed = true;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _target.gameObject)
        {
            _target.TakeDamage(_damage, _attacker);
        }
        HandleDestroy();
    }
}
