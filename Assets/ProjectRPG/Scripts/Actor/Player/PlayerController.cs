using System.Collections;
using System.Collections.Generic;
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
    private PlayerStatManager _statManager;
    private PlayerInteractor _interactor;
    [SerializeField] private Animator _animator;

    [Header("캐릭터 설정")]
    [SerializeField] private float _jumpPower = 3f;
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

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _health = GetComponent<Health>();
        _damageReciever = GetComponent<DamageReciever>();
        _statManager = GetComponent<PlayerStatManager>();
        _interactor = GetComponent<PlayerInteractor>();
        _input = PlayerInputManager.Instance;
        //animator = GetComponent<Animator>();

        _damageReciever.OnTakeDamage += (damage, attacker) => _health.TakeDamage(damage, attacker);
        _health.OnDead += (_) => { _animator.SetTrigger("Die"); _isDead = true; };
        _statManager.OnLevelUp += () =>
        {
            float maxHealth = _statManager.GetCurruntStat().Health;
            _health.SetMaxHealth(maxHealth);
            _health.SetHealth(maxHealth, gameObject);
        };

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
            _animator.SetTrigger("Attack");
            StartCoroutine(SetMotionStun());
        }
        if (_input.GetJump() && _canAct)
        {
            _jumpInputBuffer = true;
        }
        if (_input.GetInteraction())
        {
            _interactor.TryInteract();
        }
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
            MoveHandler();
        }
    }

    private bool _jumpInputBuffer;

    private void MoveHandler()
    {
        if (_jumpInputBuffer)
        {
            _jumpInputBuffer = false;
            _animator.SetTrigger("Jump");
            StartCoroutine(SetMotionStun());
            Vector3 jumpVector = _rigid.velocity;
            jumpVector.y = _jumpPower;
            _rigid.velocity = jumpVector;
            return;
        }

        Vector2 inputVector = _input.GetMoveVector();

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
