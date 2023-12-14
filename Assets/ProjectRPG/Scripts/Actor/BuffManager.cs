using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    private Dictionary<Type, Buff> _buffs = new();

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
    /// 버프 추가
    /// </summary>
    public void AddBuff(Buff addedBuff)
    {
        if (_buffs.ContainsKey(addedBuff.GetType()))
        {
            _buffs[addedBuff.GetType()].MergeBuff(addedBuff);
        }
        else
        {
            _buffs.Add(addedBuff.GetType(), addedBuff);
        }
        addedBuff.OnAdded(this);
    }

    /// <summary>
    /// 특정 타입의 버프 제거
    /// </summary>
    public void RemoveBuff<T>() where T : Buff
    {
        Type type = typeof(T);
        if (_buffs.TryGetValue(type, out Buff buff))
        {
            _buffs.Remove(type);
            buff.OnDeleted(this);
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
    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract Sprite Sprite { get; }

    public abstract void MergeBuff(Buff other);
    public abstract void OnAdded(BuffManager manager);
    public abstract void OnUpdate(BuffManager manager);
    public abstract void OnDeleted(BuffManager manager);
}