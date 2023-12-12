using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed => 3;
    public float runSpeed => moveSpeed * 1.5f;
    public float attackStat => 0;

    private Rigidbody rigid;
    private PlayerInputManager input;
    private Health health;
    private DamageReciever damageReciever;
    [SerializeField] private Animator animator;

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
    private bool isActing => _motionStopTime >= Time.time;
    private bool canAct => !isActing && !_isDead;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
        damageReciever = GetComponent<DamageReciever>();
        input = PlayerInputManager.Instance;
        //animator = GetComponent<Animator>();

        damageReciever.OnTakeDamage += (damage, attacker) => health.TakeDamage(damage, attacker);
        health.OnDead += (_) => { animator.SetTrigger("Die"); _isDead = true; };

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _cameraDistance = _cameraMaxDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (input.GetLookScrolling())
        {
            _cameraRotation += input.GetLookDelta().x;
        }
        _cameraDistance = Mathf.Clamp(_cameraDistance + input.GetCameraDistanceDelta(), _cameraMinDistance, _cameraMaxDistance);

        SetCameraPos();

        if (input.GetAttack() && canAct)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(SetMotionStun());
        }
        if (input.GetJump() && canAct)
        {
            _jumpInputBuffer = true;
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
        _motionStopTime = Time.time + animator.GetNextAnimatorClipInfo(0).Length;
    }

    private void FixedUpdate()
    {
        if (canAct)
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
            animator.SetTrigger("Jump");
            StartCoroutine(SetMotionStun());
            Vector3 jumpVector = rigid.velocity;
            jumpVector.y = _jumpPower;
            rigid.velocity = jumpVector;
            return;
        }

        Vector2 inputVector = input.GetMoveVector();

        Vector3 moveVelocity = Vector3.zero;
        if (inputVector != Vector2.zero)
        {
            if (input.GetRunning())
            {
                moveVelocity = Quaternion.Euler(0, _cameraRotation, 0) * new Vector3(inputVector.x, 0, inputVector.y) * runSpeed;
                animator.SetInteger("MoveMode", 2);
            }
            else
            {
                moveVelocity = Quaternion.Euler(0, _cameraRotation, 0) * new Vector3(inputVector.x, 0, inputVector.y) * moveSpeed;
                animator.SetInteger("MoveMode", 1);
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
            animator.SetInteger("MoveMode", 0);
        }

        rigid.velocity = new Vector3(moveVelocity.x, rigid.velocity.y, moveVelocity.z);
    }
}
