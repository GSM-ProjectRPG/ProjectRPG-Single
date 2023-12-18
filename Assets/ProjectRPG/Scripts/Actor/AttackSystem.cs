using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

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

    /// <summary>
    /// 공격을 정의하는 람다식을 저장합니다, TryAttack 메서드를 통해 공격할 수 있습니다.
    /// </summary>
    public AttackActionList AttackActions = new();

    private StatSystem _statSystem;


    private void Start()
    {
        _statSystem = GetComponent<StatSystem>();
    }

    /// <summary>
    /// AttackActions에 정의된 공격을 시도합니다.
    /// </summary>
    public void TryAttack(int index)
    {
        AttackActions[index]?.Invoke();
    }

    /// <summary>
    /// 공격 프리펩을 생성하는 메서드입니다.
    /// </summary>
    public GameObject InstantiateAttack(GameObject attackPrefab, Vector3 position, Quaternion rotation)
    {
        GameObject instance = Instantiate(attackPrefab, position, rotation);
        Attack attack = instance.GetComponent<Attack>();

        if (attack != null)
        {
            attack.SetAttacker(this, _statSystem);
            attack.OnHitted += OnAttackHitted;
        }
        #region 경고 메시지 출력
#if UNITY_EDITOR
        else
        {
            StackFrame[] stackFrames = new StackTrace().GetFrames();
            string callingPos = string.Empty;

            if (stackFrames != null && stackFrames.Length >= 2)
            {
                MethodBase callingMethod = stackFrames[1].GetMethod();
                callingPos = callingMethod.DeclaringType.Name + "." + callingMethod.Name;
            }
            else
            {
                callingPos = "찾지못함";
            }

            Debug.LogWarning("Attack 컴포넌트를 포함하지 않는 프리펩을 복제할 때 " + typeof(AttackSystem).Name + "." + MethodBase.GetCurrentMethod().Name + " 메서드가 사용되었습니다.\n" +
                "이 작업의 결과는 UnityEngine.Object.Instantiate와 다르지 않지만, 더 많은 오버헤드를 발생시킵니다.\n" +
                "호출위치 : " + callingPos);
        }
#endif
        #endregion

        return instance;
    }

    /// <summary>
    /// 공격 람다식들을 저장하는 클래스입니다. 인덱서를 통하여 공격을 참조할 수 있으며, 인덱스 범위를 벗어났을 경우 자동으로 확장됩니다.
    /// </summary>
    public class AttackActionList
    {
        private List<Action> _actions = new();

        public Action this[int index]
        {
            get
            {
                while (_actions.Count <= index)
                {
                    _actions.Add(() => { });
                }
                return _actions[index];
            }
            set
            {
                while (_actions.Count <= index)
                {
                    _actions.Add(() => { });
                }
                _actions[index] = value;
            }
        }
    }
}
