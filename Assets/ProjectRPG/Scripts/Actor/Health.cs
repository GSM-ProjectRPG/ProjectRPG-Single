using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatSystem))]
public class Health : MonoBehaviour
{
    public Action OnHealthChanged;
    /// <summary>
    /// float : 받은데미지, GameObject : 공격자
    /// </summary>
    public Action<float, GameObject> OnDamaged;
    /// <summary>
    /// float : 받은힐량, GameObject : 치유자
    /// </summary>
    public Action<float, GameObject> OnHealed;
    /// <summary>
    /// GameObject : 처치자
    /// </summary>
    public Action<GameObject> OnDead;

    public float MaxHealth { get; protected set; }
    public float CurruntHealth { get; protected set; }

    private bool _isDead = false;

    public void Start()
    {
        StatSystem statManager = GetComponent<StatSystem>();
        if (statManager != null)
        {
            MaxHealth = statManager.GetCurruntStat().Health;
        }

        CurruntHealth = MaxHealth;
    }

    public void TakeDamage(float damage, GameObject attacker)
    {
        if (damage > 0)
        {
            float origin = CurruntHealth;
            damage = Mathf.Max(damage, 0);

            CurruntHealth = Mathf.Max(CurruntHealth - damage, 0);

            OnDamaged?.Invoke(origin - CurruntHealth, attacker);
            OnHealthChanged?.Invoke();

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

            OnHealed?.Invoke(CurruntHealth - origin, healer);
            OnHealthChanged?.Invoke();
        }
    }

    public void Kill(GameObject killer)
    {
        float origin = CurruntHealth;
        CurruntHealth = 0;

        OnDamaged?.Invoke(origin, killer);

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
            OnHealed?.Invoke(delta, caller);
        }
        else
        {
            OnDamaged?.Invoke(delta, caller);
        }
        OnHealthChanged?.Invoke();

        if (CurruntHealth == 0)
        {
            HandleDie(caller);
        }
    }

    public void SetMaxHealth(float maxHealth)
    {
        MaxHealth = maxHealth;
        CurruntHealth = Mathf.Min(CurruntHealth, MaxHealth);
        OnHealthChanged?.Invoke();
    }

    private void HandleDie(GameObject killer)
    {
        if (_isDead) return;
        _isDead = true;
        OnDead?.Invoke(killer);
    }
}
