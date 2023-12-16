using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이 클래스를 상속함으로서 투사체 등의 공격을 구현할 수 있습니다.
/// </summary>
public abstract class Attack : MonoBehaviour
{
    /// <summary>
    /// GameObject : 맞은오브젝트, float : 준 데미지
    /// </summary>
    public Action<GameObject, float> OnHitted;

    protected AttackSystem attacker;
    protected StatSystem attackerStatSystem;

    /// <summary>
    /// AttackSystem에서 호출하기 위한 초기설정 함수입니다.
    /// </summary>
    public virtual void SetAttacker(AttackSystem attacker,StatSystem statSystem)
    {
        this.attacker = attacker;
        attackerStatSystem = statSystem;
    }

    /// <summary>
    /// 자식 클래스에서 호출할 데미지 부여 함수입니다. OnHitted 이벤트를 작동시킵니다.
    /// </summary>
    protected void SendDamage(DamageReciever damageReciever, float damage)
    {
        Action<float, GameObject> action = (damageResult, _) => { OnHitted?.Invoke(damageReciever.gameObject, damageResult); };

        damageReciever.OnTakeDamage += action;
        GameObject attackerObject = null;
        if (attacker != null)
        {
            attackerObject = attacker.gameObject;
        }
        damageReciever.TakeDamage(damage, attackerObject);
        damageReciever.OnTakeDamage -= action;
    }
}
