using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    public Dictionary<Type, Buff> _buffs { get; protected set; } = new();

    public event Action<Buff> OnAddBuff;
    public event Action<Buff> OnRemoveBuff;

    /// <summary>
    /// 특정 타입의 버프가 존재하는지 확인
    /// </summary>
    public bool ContainsBuff<T>()
    {
        return _buffs.ContainsKey(typeof(T));
    }

    /// <summary>
    /// 특정 타입의 버프가 존재하면 가져오기
    /// </summary>
    public T GetBuff<T>() where T : Buff
    {
        if (_buffs.TryGetValue(typeof(T), out Buff buff))
        {
            return buff as T;
        }
        return null;
    }

    /// <summary>
    /// 현재 버프 개수 반환
    /// </summary>
    public int GetBuffCount()
    {
        return _buffs.Count;
    }

    /// <summary>
    /// 버프 추가
    /// </summary>
    public void AddBuff<T>(T addedBuff) where T : Buff<T>
    {
        if (addedBuff == null) return;

        if (_buffs.ContainsKey(addedBuff.GetType()))
        {
            T originBuff = _buffs[addedBuff.GetType()] as T;
            originBuff.MergeBuff(addedBuff);
        }
        else
        {
            _buffs.Add(addedBuff.GetType(), addedBuff);
            addedBuff.OnAdded(this);
            OnAddBuff?.Invoke(addedBuff);
        }
        addedBuff.OnAdded(this);
    }

    /// <summary>
    /// 특정 타입의 버프 제거
    /// </summary>
    public void RemoveBuff<T>() where T : Buff<T>
    {
        Type type = typeof(T);
        if (_buffs.TryGetValue(type, out Buff buff))
        {
            _buffs.Remove(type);
            buff.OnDeleted(this);
            OnRemoveBuff?.Invoke(buff);
        }
    }

    /// <summary>
    /// 모든 버프 제거
    /// </summary>
    public void ClearBuff()
    {
        Action action = () => { };
        foreach (var item in _buffs.Values)
        {
            action += () =>
            {
                item.OnDeleted(this);
                OnRemoveBuff?.Invoke(item);
            };
        }
        _buffs.Clear();
        action?.Invoke();
    }

    public void BuffForeach(Action<Buff> action)
    {
        foreach (var item in _buffs.Values)
        {
            action?.Invoke(item);
        }
    }

    private void Update()
    {
        Action action = null;
        foreach (var item in _buffs)
        {
            action += () =>
            {
                item.Value.OnUpdate(this);
            };
        }
        action?.Invoke();
    }

    private void OnDestroy()
    {
        Action action = null;
        foreach (var item in _buffs)
        {
            action += () =>
            {
                item.Value.OnDeleted(this);
            };
        }
        action?.Invoke();
    }
}

public abstract class Buff
{
    public virtual string Name => _buffData.BuffName;
    public virtual string Description => _buffData.BuffDescription;
    public virtual Sprite Sprite => _buffData.BuffImage;

    protected abstract BuffData _buffData { get; }

    public abstract void OnAdded(BuffSystem manager);
    public abstract void OnUpdate(BuffSystem manager);
    public abstract void OnDeleted(BuffSystem manager);
}

public abstract class Buff<T> : Buff where T : Buff<T>
{
    public abstract void MergeBuff(T other);
}