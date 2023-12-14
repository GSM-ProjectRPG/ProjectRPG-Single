using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    private Dictionary<Type, Buff> _buffs = new();

    public bool ContainsBuff<T>()
    {
        return _buffs.ContainsKey(typeof(T));
    }

    public T GetBuff<T>() where T : Buff
    {
        if (_buffs.TryGetValue(typeof(T), out Buff buff))
        {
            return buff as T;
        }
        return null;
    }

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

    public void RemoveBuff<T>() where T : Buff
    {
        Type type = typeof(T);
        _buffs.TryGetValue(type, out Buff buff);
        _buffs.Remove(type);
        buff.OnDeleted(this);
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