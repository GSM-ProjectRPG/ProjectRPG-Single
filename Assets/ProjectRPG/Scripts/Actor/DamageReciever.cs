using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReciever : MonoBehaviour
{
    /// <summary>
    /// float : 받은 데미지, GameObject : 공격자
    /// </summary>
    public Action<float, GameObject> OnTakeDamage;

    public void TakeDamage(float damage, GameObject attacker)
    {
        OnTakeDamage?.Invoke(damage, attacker);
    }
}
