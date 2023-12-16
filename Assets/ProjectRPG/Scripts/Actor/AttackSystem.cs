using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 공격을 등록하고, 실행 요청받는 공격관리자입니다.
/// </summary>
[RequireComponent(typeof(StatSystem))]
public class AttackSystem : MonoBehaviour
{
    /// <summary>
    /// GameObject : 공격을 맞은오브젝트, float : 가해진 최종 데미지
    /// </summary>
    public Action<GameObject, float> OnAttackHitted;

    private StatSystem statSystem;

    public AttackActionList AttackActions = new();


    private void Start()
    {
        statSystem = GetComponent<StatSystem>();
    }

    /// <summary>
    /// AddAttackData 메서드를 통해 정의된 공격을 시도합니다.
    /// </summary>
    public void TryAttack(int index)
    {
        AttackActions[index]?.Invoke();
    }

    /// <summary>
    /// 공격을 생성하는 메서드입니다.
    /// </summary>
    public GameObject InstantiateAttack(GameObject attackPrefab, Vector3 position, Quaternion rotation)
    {
        GameObject instance = Instantiate(attackPrefab, position, rotation);
        Attack attack = instance.GetComponent<Attack>();

        if (attack != null)
        {
            attack.SetAttacker(this, statSystem);
            attack.OnHitted += OnAttackHitted;
        }

        return instance;
    }

    public class AttackActionList
    {
        private List<Action> actions = new();

        public Action this[int index]
        {
            get
            {
                while (actions.Count <= index)
                {
                    actions.Add(() => { });
                }
                return actions[index];
            }
            set
            {
                while (actions.Count <= index)
                {
                    actions.Add(() => { });
                }
                actions[index] = value;
            }
        }
    }
}
