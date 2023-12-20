using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActSystem : MonoBehaviour
{
    private BuffSystem _buffSystem;

    private void Awake()
    {
        _buffSystem = GetComponent<BuffSystem>();
    }

    /// <summary>
    /// 행동 로직을 매개변수로 받아 행동 핸들러를 반환합니다.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public Action Act(Action action)
    {
        return () =>
        {
            if (!_buffSystem.ContainsBuff<Stun>())
            {
                action?.Invoke();
            }
        };
    }
}

/// <summary>
/// 람다식들을 저장하는 클래스입니다. 인덱서를 통하여 람다식을 참조할 수 있으며, 인덱스 범위를 벗어났을 경우 자동으로 확장됩니다.
/// </summary>
public class ActionList
{
    private List<Action> _actions = new();

    public int Count => _actions.Count;

    public Action this[int index]
    {
        get
        {
            while (_actions.Count <= index)
            {
                _actions.Add(() => { });
            }
            return _actions[index];
        }
        set
        {
            while (_actions.Count <= index)
            {
                _actions.Add(() => { });
            }
            _actions[index] = value;
        }
    }

    public int Add(Action action)
    {
        int index = Count;
        this[index] += action;
        return index;
    }
}