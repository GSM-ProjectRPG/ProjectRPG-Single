using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffIconUILayer : MonoBehaviour
{
    public GameObject BuffIconPrefab;

    private Dictionary<Buff, BuffIconUI> _icons = new();

    private void Awake()
    {
        ActorManager.Instance.OnRegistedPlayer += () =>
        {
            BuffSystem buffSystem = ActorManager.Instance.Player.GetComponent<BuffSystem>();

            buffSystem.OnAddedBuff += (buff) =>
            {
                BuffIconUI icon = Instantiate(BuffIconPrefab, transform).GetComponent<BuffIconUI>();
                icon.BindBuff(buff);
                _icons.Add(buff, icon);
            };
            buffSystem.OnRemovedBuff += (buff) =>
            {
                Destroy(_icons[buff].gameObject);
            };
        };
    }
}
