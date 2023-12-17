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
[RequireComponent(typeof(ActSystem))]
public class AttackSystem : MonoBehaviour
{
    /// <summary>
    /// GameObject : 공격을 맞은오브젝트, float : 가해진 최종 데미지
    /// </summary>
    public Action<GameObject, float> OnAttackHitted;

    /// <summary>
    /// 공격을 정의하는 람다식을 저장합니다, TryAttack 메서드를 통해 공격할 수 있습니다.
    /// </summary>
    private ActionList AttackActions = new();

    private StatSystem _statSystem;
    private ActSystem _actSystem;


    private void Start()
    {
        _statSystem = GetComponent<StatSystem>();
        _actSystem = GetComponent<ActSystem>();
    }

    public Action Attack(Action action)
    {
        return _actSystem.Act(action);
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
}
