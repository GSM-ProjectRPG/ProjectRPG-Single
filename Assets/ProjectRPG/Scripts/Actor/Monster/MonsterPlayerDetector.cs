using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPlayerDetector : MonoBehaviour
{
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

    public Vector3 GetDetectedPlayerPosition()
    {
        if (ActorManager.Instance.Player == null) return new Vector3(0, 0, 0);
        Vector3 playerPosition = ActorManager.Instance.Player.transform.position;
        return new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
    }

    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        if (ActorManager.Instance.Player == null) return;
        float distanceToPlayer = Vector3.Distance(ActorManager.Instance.Player.transform.position, transform.position);

        if (distanceToPlayer <= _statManager.ChaseRange)
        {
            _isPlayerDetectInChaseRange = true;

            if (distanceToPlayer <= _statManager.AttackRange)
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
