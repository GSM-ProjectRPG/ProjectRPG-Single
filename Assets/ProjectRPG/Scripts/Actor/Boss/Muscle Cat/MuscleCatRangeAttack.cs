using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuscleCatRangeAttack : MonoBehaviour
{
    private AttackSystem _attacker;
    private Rigidbody _rigid;

    private float _moveSpeed;
    private float _damage;
    private float _time;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time > 10)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        _rigid.MovePosition(Vector3.forward * _moveSpeed * Time.fixedDeltaTime);
    }

    public void SetAttackData(float damage, float moveSpeed, AttackSystem attacker, Vector3 targetPos)
    {
        _damage = damage;
        _moveSpeed = moveSpeed;
        _attacker = attacker;

        transform.LookAt(targetPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ActorManager.Instance.Player)
        {
            _attacker.SendDamage(other.GetComponent<DamageReciever>(), _damage);
        }
        Destroy(gameObject);
    }
}
