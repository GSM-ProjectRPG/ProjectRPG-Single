using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffIconUILayer : MonoBehaviour
{
    public GameObject BasicBuffIconPrefab;

    private Dictionary<Buff, BuffIconUI> _icons = new();

    private void Awake()
    {
        ActorManager.Instance.OnRegistedPlayer += () =>
        {
            BuffSystem buffSystem = ActorManager.Instance.Player.GetComponent<BuffSystem>();

            buffSystem.OnAddedBuff += (buff) =>
            {
                BuffIconUI icon = Instantiate(BasicBuffIconPrefab, transform).GetComponent<BuffIconUI>();
                icon.BindBuff(buff);
                _icons.Add(buff, icon);
            };
            buffSystem.OnRemovedBuff += (buff) =>
            {
                GameObject g = _icons[buff].gameObject;
                _icons.Remove(buff);
                Destroy(g);
            };
        };
    }
}

public interface IBuffIconUI<T> where T : Buff
{
    public void BindBuff(T buff);
}