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
    [Header("Jump Attack")]
    [SerializeField] private float _jumpAttackCool;

    [Space()]
    [Header("Dash Attack")]
    [SerializeField] private float _dashAttackCool;
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashDurationTime;
    [SerializeField] private bool _isDashing;

    [Space()]
    [Header("Range Attack")]
    [SerializeField] private float _rangeAttackCool;
    [SerializeField] private GameObject _rangeAttackObject;

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
        int rand = Random.Range(0, 4);

        switch (rand)
        {
            case 0:
                SetAttackCoolTime(_punchAttackCool);
                StartCoroutine(PunchAttackCoroutine());
                break;
            case 1:
                SetAttackCoolTime(_jumpAttackCool);
                StartCoroutine(JumpAttackCoroutine());
                break;
            case 2:
                SetAttackCoolTime(_dashAttackCool);
                StartCoroutine(DashAttackCoroutine());
                break;
            case 3:
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

        yield break;
    }

    private IEnumerator JumpAttackCoroutine()
    {


        yield break;
    }

    private IEnumerator DashAttackCoroutine()
    {
        _animator.SetTrigger("DashAttack");
        yield return null;
        yield return new WaitForSeconds(_animator.GetNextAnimatorClipInfo(0).Length);

        Vector3 dashDir = _playerDetector.GetDetectedPlayerPosition() - transform.position;
        _rigid.AddForce(dashDir.normalized * _dashForce, ForceMode.Impulse);
        yield return new WaitForSeconds(_dashDurationTime);

        _rigid.velocity = Vector3.zero;

        yield break;
    }

    private IEnumerator RangeAttackCoroutine()
    {


        yield break;
    }

    private void SetAttackCoolTime(float cool)
    {
        _attackCool = Time.time + cool;
    }
}
