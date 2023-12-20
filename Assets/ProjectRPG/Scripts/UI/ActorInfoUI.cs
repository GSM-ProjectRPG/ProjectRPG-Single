using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorInfoUI : MonoBehaviour
{
    public Text ActorNameText;
    public Slider HpSlider;
    public Text HpText;

    public virtual void BindActor(GameObject actor)
    {
        Health _health = actor.GetComponent<Health>();

        StatSystem statSystem = actor.GetComponent<StatSystem>();
        if (statSystem != null)
        {
            ActorNameText.text = statSystem.Name;
        }

        _health.OnHealthChanged += () =>
        {
            HpSlider.value = _health.CurruntHealth / _health.MaxHealth;
            HpText.text = _health.CurruntHealth + "/" + _health.MaxHealth;
        };
        _health.OnDead += (_) =>
        {
            Destroy(gameObject);
        };
    }

    private void Update()
    {
        if(Camera.main != null)
        {
            transform.LookAt(Camera.main.transform.position);
        }
    }
}