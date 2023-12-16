using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActSystem : MonoBehaviour
{
    /// <summary>
    /// 공격을 정의하는 람다식을 저장합니다, TryAttack 메서드를 통해 공격할 수 있습니다.
    /// </summary>
    private ActionList ActActions = new();

    public int AddAct(Action action)
    {
        return ActActions.Add(action);
    }

    public void Act(int actIndex)
    {
        ActActions[actIndex]?.Invoke();
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