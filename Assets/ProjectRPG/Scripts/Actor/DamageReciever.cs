using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReciever : MonoBehaviour
{
    /// <summary>
    /// float : ���� ������, GameObject : ������
    /// </summary>
    public Action<float, GameObject> OnTakeDamage;

    public void TakeDamage(float damage, GameObject attacker)
    {
        OnTakeDamage?.Invoke(damage, attacker);
    }
}
