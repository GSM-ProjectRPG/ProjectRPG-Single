using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed => 3;
    public float RunSpeed => MoveSpeed * 1.5f;
    public float AttackStat => 0;

    private Rigidbody _rigid;
    private PlayerInputManager _input;
    private Health _health;
    private DamageReciever _damageReciever;
    private PlayerStatSystem _statManager;
    private PlayerInteractor _interactor;
    private AttackSystem _attackSystem;
    private ActSystem _actSystem;
    private SkillSystem _skillSystem;

    [SerializeField] private Animator _animator;

    [Header("캐릭터 설정")]
    [SerializeField] private float _jumpPower = 3f;
    [SerializeField] private SkillData _punchSkillData;
    [Header("카메라 설정")]
    [SerializeField] private Camera _camera;
    [SerializeField] private float _cameraMaxDistance = 3f;
    [SerializeField] private float _cameraMinDistance = 2f;
    [SerializeField] private float _cameraHeight = 1f;
    [SerializeField] private float _cameraInclination = 0.5f;
    [SerializeField] private float _cameraLookHegit = 1f;

    private float _cameraDistance;
    private float _cameraRotation = 0;
    private float _motionStopTime;
    private bool _isDead = false;
    private bool _isActing => _motionStopTime >= Time.time;
    private bool _canAct => !_isActing && !_isDead;

    private Action _interactionHandler;
    private Action _jumpHandler;
    private Action _moveHandler;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _health = GetComponent<Health>();
        _damageReciever = GetComponent<DamageReciever>();
        _statManager = GetComponent<PlayerStatSystem>();
        _interactor = GetComponent<PlayerInteractor>();
        _attackSystem = GetComponent<AttackSystem>();
        _actSystem = GetComponent<ActSystem>();
        _skillSystem = GetComponent<SkillSystem>();
        _input = PlayerInputManager.Instance;
        //animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ActorManager.Instance.RegistPlayer(gameObject);
        _damageReciever.OnTakeDamage += (damage, attacker) => _health.TakeDamage(damage, attacker);
        _health.OnDead += (_) => { _animator.SetTrigger("Die"); _isDead = true; ActorManager.Instance.DeleteActor(gameObject); };
        _statManager.OnLevelUp += () =>
        {
            float maxHealth = _statManager.GetCurruntStat().Health;
            _health.SetMaxHealth(maxHealth);
            _health.SetHealth(maxHealth, gameObject);
        };

        Skill punchSkill = new Skill(_punchSkillData, _attackSystem.Attack(Punch));
        _skillSystem.RegistSkill(punchSkill);
        _skillSystem.SelectSkill(punchSkill);
        _interactionHandler = _actSystem.Act(() => _interactor.TryInteract());
        _jumpHandler = _actSystem.Act(Jump);
        _moveHandler = _actSystem.Act(() => Move(_input.GetMoveVector()));

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _cameraDistance = _cameraMaxDistance;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_input.GetLookScrolling())
        {
            _cameraRotation += _input.GetLookDelta().x;
        }
        _cameraDistance = Mathf.Clamp(_cameraDistance + _input.GetCameraDistanceDelta(), _cameraMinDistance, _cameraMaxDistance);

        SetCameraPos();

        if (_input.GetAttack() && _canAct)
        {
            _skillSystem.UseSkill();
        }
        if (_input.GetJump() && _canAct)
        {
            _jumpInputBuffer = true;
        }
        if (_input.GetInteraction())
        {
            _interactionHandler?.Invoke();
        }
    }

    private void Punch()
    {
        _animator.SetTrigger("Attack");
        StartCoroutine(SetMotionStun());
    }

    private void SetCameraPos()
    {
        Vector3 rayDirection = Quaternion.Euler(0, _cameraRotation + 180, 0) * new Vector3(0, _cameraInclination, 1);
        if (Physics.Raycast(transform.position + Vector3.up * _cameraHeight, rayDirection, out RaycastHit hit, _cameraDistance))
        {
            _camera.transform.position = hit.point;
        }
        else
        {
            _camera.transform.position = transform.position + Vector3.up * _cameraHeight + rayDirection * _cameraDistance;
        }
        _camera.transform.LookAt(transform.position + Vector3.up * _cameraLookHegit);
    }


    [ContextMenu("카메라 위치 확인")]
    private void TestCameraPos()
    {
        _cameraDistance = _cameraMaxDistance;
        SetCameraPos();
    }

    private IEnumerator SetMotionStun()
    {
        _motionStopTime = Time.time + 1;
        yield return null;
        _motionStopTime = Time.time + _animator.GetNextAnimatorClipInfo(0).Length;
    }

    private void FixedUpdate()
    {
        if (_canAct)
        {
            _moveHandler?.Invoke();

            if (_jumpInputBuffer)
            {
                _jumpHandler?.Invoke();
            }
            _jumpInputBuffer = false;
        }
    }

    private bool _jumpInputBuffer;

    private void Jump()
    {
        _animator.SetTrigger("Jump");
        StartCoroutine(SetMotionStun());
        Vector3 jumpVector = _rigid.velocity;
        jumpVector.y = _jumpPower;
        _rigid.velocity = jumpVector;
        return;
    }

    private void Move(Vector2 inputVector)
    {
        Vector3 moveVelocity = Vector3.zero;
        if (inputVector != Vector2.zero)
        {
            if (_input.GetRunning())
            {
                moveVelocity = Quaternion.Euler(0, _cameraRotation, 0) * new Vector3(inputVector.x, 0, inputVector.y) * RunSpeed;
                _animator.SetInteger("MoveMode", 2);
            }
            else
            {
                moveVelocity = Quaternion.Euler(0, _cameraRotation, 0) * new Vector3(inputVector.x, 0, inputVector.y) * MoveSpeed;
                _animator.SetInteger("MoveMode", 1);
            }
        }

        if (moveVelocity != Vector3.zero)
        {
            //rigid.MovePosition(rigid.position + moveDelta);

            float moveDegree = -Mathf.Atan2(moveVelocity.z, moveVelocity.x) * Mathf.Rad2Deg + 90;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, moveDegree, 0), Time.fixedDeltaTime * 10f);
        }
        else
        {
            _animator.SetInteger("MoveMode", 0);
        }

        _rigid.velocity = new Vector3(moveVelocity.x, _rigid.velocity.y, moveVelocity.z);
    }
}
