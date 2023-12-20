using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorInfoSummoner : MonoBehaviour
{
    public GameObject infoUI;
    public float uiShowTime;

    private Health _health;
    private ActorInfoUI _ui;

    private float lastDamagedTime;


    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.OnDamaged += (_, _) =>
        {
            if (_ui == null)
            {
                _ui = Instantiate(infoUI).GetComponent<ActorInfoUI>();
                _ui.BindActor(gameObject);
            }

            lastDamagedTime = Time.time;
        };
    }

    private void Update()
    {
        if (_ui != null)
        {
            _ui.gameObject.SetActive(lastDamagedTime + uiShowTime > Time.time);
        }
    }
}
