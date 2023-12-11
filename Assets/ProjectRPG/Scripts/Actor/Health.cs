using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    /// <summary>
    /// float : ����������, GameObject : ������
    /// </summary>
    public Action<float, GameObject> OnDamaged;
    /// <summary>
    /// float : ��������, GameObject : ġ����
    /// </summary>
    public Action<float, GameObject> OnHealed;
    /// <summary>
    /// GameObject : óġ��
    /// </summary>
    public Action<GameObject> OnDead;

    public float MaxHealth;
    public float CurruntHealth { get; protected set; }

    private bool _isDead = false;

    public void Start()
    {
        CurruntHealth = MaxHealth;
    }

    public void TakeDamage(float damage, GameObject attacker)
    {
        if (damage > 0)
        {
            float origin = CurruntHealth;
            damage = Mathf.Max(damage, 0);

            CurruntHealth = Mathf.Max(CurruntHealth - damage, 0);

            OnDamaged(origin - CurruntHealth, attacker);

            if (CurruntHealth == 0)
            {
                HandleDie(attacker);
            }
        }
    }

    public void TakeHeal(float heal, GameObject healer)
    {
        if (heal > 0)
        {
            float origin = CurruntHealth;
            heal = Mathf.Max(heal, 0);

            CurruntHealth = Mathf.Min(CurruntHealth + heal, MaxHealth);

            OnHealed(CurruntHealth - origin, healer);
        }
    }

    public void Kill(GameObject killer)
    {
        float origin = CurruntHealth;
        CurruntHealth = 0;

        OnDamaged(origin, killer);

        HandleDie(killer);
    }

    public void SetHealth(float health, GameObject caller)
    {
        health = Mathf.Clamp(health, 0, MaxHealth);
        float origin = CurruntHealth;

        CurruntHealth = health;

        float delta = CurruntHealth - origin;
        if (delta > 0)
        {
            OnHealed(delta, caller);
        }
        else
        {
            OnDamaged(delta, caller);
        }

        if(CurruntHealth == 0)
        {
            HandleDie(caller);
        }
    }

    private void HandleDie(GameObject killer)
    {
        if (_isDead) return;
        _isDead = true;
        OnDead(killer);
    }
}
