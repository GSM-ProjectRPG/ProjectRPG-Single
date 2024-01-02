using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
    public event Action<GameObject, float> OnAttackHitted = (victim,damage)=> {  };
    public event Action<GameObject, float> OnSendedDamage;
    public event Action<GameObject> OnKill;

    private StatSystem _statSystem;
    private ActSystem _actSystem;

    private void Awake()
    {
        _statSystem = GetComponent<StatSystem>();
        _actSystem = GetComponent<ActSystem>();
    }

    /// <summary>
    /// 공격 로직을 매개변수로 받아 공격 핸들러를 반환합니다.
    /// </summary>
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
            attack.OnSendedDamage += OnSendedDamage;
            attack.OnKill += OnKill;
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
    /// 데미지 부여 함수입니다. OnHitted 이벤트를 작동시킵니다.
    /// </summary>
    public void SendDamage(DamageReciever damageReciever, float damage)
    {
        Health health = damageReciever.GetComponent<Health>();

        Action<float, GameObject> onHitted = (damageResult, _) => { OnAttackHitted?.Invoke(damageReciever.gameObject, damageResult); };
        Action<float, GameObject> onSendedDamage = (damageResult, _) => { OnSendedDamage?.Invoke(health.gameObject, damageResult); };
        Action<GameObject> onKill = (_) => { OnKill?.Invoke(health.gameObject); };

        damageReciever.OnTakeDamage += onHitted;
        if (health != null)
        {
            health.OnDamaged += onSendedDamage;
            health.OnDead += onKill;
        }

        damageReciever.TakeDamage(damage, gameObject);

        if (health != null)
        {
            health.OnDead -= onKill;
            health.OnDamaged -= onSendedDamage;
        }
        damageReciever.OnTakeDamage -= onHitted;
    }
}
