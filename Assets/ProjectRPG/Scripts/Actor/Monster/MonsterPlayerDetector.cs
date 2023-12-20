using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPlayerDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;

    private MonsterStatSystem _statManager;

    [SerializeField] private bool _isPlayerDetectInChaseRange;
    [SerializeField] private bool _isPlayerDetectInAttackRange;

    private void Awake()
    {
        _statManager = GetComponent<MonsterStatSystem>();
    }

    public bool CanChasePlayer()
    {
        return _isPlayerDetectInChaseRange && !_isPlayerDetectInAttackRange;
    }

    public bool CanAttackPlayer()
    {
        return _isPlayerDetectInAttackRange;
    }

    public bool IsNotDetectedPlayerInChaseRange()
    {
        return !_isPlayerDetectInChaseRange;
    }

    public Vector3 GetDetectedPlayerPosition()
    {
        Vector3 playerPosition = Physics.OverlapSphere(transform.position, _statManager.ChaseRange, 1 << LayerMask.NameToLayer("Water"))[0].transform.position;
        return new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
    }

    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        Collider[] detectedPlayerInChaseRange = Physics.OverlapSphere(transform.position, _statManager.ChaseRange, 1 << LayerMask.NameToLayer("Water"));
        Collider[] detectedPlayerInAttackRange = Physics.OverlapSphere(transform.position, _statManager.AttackRange, 1 << LayerMask.NameToLayer("Water"));

        if (detectedPlayerInChaseRange.Length > 0)
        {
            _isPlayerDetectInChaseRange = true;

            if (detectedPlayerInAttackRange.Length > 0)
            {
                _isPlayerDetectInAttackRange = true;
            }
            else
            {
                _isPlayerDetectInAttackRange = false;
            }
        }
        else
        {
            _isPlayerDetectInChaseRange = false;
            _isPlayerDetectInAttackRange = false;
        }
    }
}
