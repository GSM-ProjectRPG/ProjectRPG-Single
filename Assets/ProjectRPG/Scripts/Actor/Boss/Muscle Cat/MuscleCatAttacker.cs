using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuscleCatAttacker : MonoBehaviour
{
    public bool CanAttack => _attackCool < Time.time;
    public bool IsAttacking { get; private set; }

    [SerializeField] private Animator _animator;

    [Space()]
    [Header("Punch Attack")]
    [SerializeField] private float _punchAttackCool;
    [SerializeField] private float _punchAttackDamage;
    [SerializeField] private Vector3 _punchAttackArea;

    [Space()]
    [Header("Dash Attack")]
    [SerializeField] private float _dashAttackCool;
    [SerializeField] private float _dashAttackDamage;
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashDurationTime;
    [SerializeField] private bool _canAttackWithDash;

    [Space()]
    [Header("Range Attack")]
    [SerializeField] private float _rangeAttackCool;
    [SerializeField] private float _rangeAttackDamage;
    [SerializeField] private float _rangeAttackSpeed;
    [SerializeField] private GameObject _rangeAttackObject;
    [SerializeField] private Transform _rangeAttackSpawnPos;

    private Rigidbody _rigid;
    private AttackSystem _attackSystem;
    private MonsterPlayerDetector _playerDetector;

    private float _attackCool;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _attackSystem = GetComponent<AttackSystem>();
        _playerDetector = GetComponent<MonsterPlayerDetector>();
    }

    public void Attack()
    {
        IsAttacking = true;
        int rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                SetAttackCoolTime(_punchAttackCool);
                StartCoroutine(PunchAttackCoroutine());
                break;
            case 1:
                SetAttackCoolTime(_dashAttackCool);
                StartCoroutine(DashAttackCoroutine());
                break;
            case 2:
                SetAttackCoolTime(_rangeAttackCool);
                StartCoroutine(RangeAttackCoroutine());
                break;
        }
    }

    private IEnumerator PunchAttackCoroutine()
    {
        _animator.SetTrigger("PunchAttack");

        for (int i=0; i<3; i++)
        {
            yield return null;
            Collider[] checkedPlayer = Physics.OverlapBox(transform.position + transform.forward, _punchAttackArea, Quaternion.identity, 1 << LayerMask.NameToLayer("Player"));

            if (checkedPlayer.Length > 0)
            {
                _attackSystem.SendDamage(checkedPlayer[0].GetComponent<DamageReciever>(), _punchAttackDamage);
            }
            yield return new WaitForSeconds(_animator.GetNextAnimatorClipInfo(0).Length);
        }

        IsAttacking = false;
        yield break;
    }

    private IEnumerator DashAttackCoroutine()
    {
        _canAttackWithDash = true;

        _animator.SetTrigger("DashAttack");
        yield return null;
        yield return new WaitForSeconds(_animator.GetNextAnimatorClipInfo(0).Length);

        Vector3 dashDir = _playerDetector.GetDetectedPlayerPosition() - transform.position;
        _rigid.AddForce(dashDir.normalized * _dashForce, ForceMode.Impulse);
        yield return new WaitForSeconds(_dashDurationTime);

        _rigid.velocity = Vector3.zero;
        _canAttackWithDash = false;

        IsAttacking = false;
        yield break;
    }

    private IEnumerator RangeAttackCoroutine()
    {
        _animator.SetTrigger("RangeAttack");

        GameObject rangeAttackObject = Instantiate(_rangeAttackObject, _rangeAttackSpawnPos.position, Quaternion.identity);
        rangeAttackObject.GetComponent<MuscleCatRangeAttack>().SetAttackData(_rangeAttackDamage, _rangeAttackSpeed, _attackSystem, _playerDetector.GetDetectedPlayerPosition());
        
        IsAttacking = false;
        yield break;
    }

    private void SetAttackCoolTime(float cool)
    {
        _attackCool = Time.time + cool;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == ActorManager.Instance.Player && _canAttackWithDash)
        {
            _attackSystem.SendDamage(collision.gameObject.GetComponent<DamageReciever>(), _dashAttackDamage);

            _canAttackWithDash = false;
        }
    }
}
