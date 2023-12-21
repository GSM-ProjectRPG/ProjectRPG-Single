using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorInfoSummoner : MonoBehaviour
{
    public GameObject InfoUIPrefab;
    public float UiShowTime;
    public float UiHeight;

    private Health _health;
    private ActorInfoUI _ui;

    private float _lastDamagedTime;


    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.OnDamaged += (_, _) =>
        {
            if (_ui == null)
            {
                _ui = Instantiate(InfoUIPrefab).GetComponent<ActorInfoUI>();
                _ui.BindActor(gameObject);
            }

            _lastDamagedTime = Time.time;
        };
    }

    private void Update()
    {
        if (_ui != null)
        {
            _ui.gameObject.SetActive(_lastDamagedTime + UiShowTime > Time.time);
            _ui.transform.position = GetUIPos();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (InfoUIPrefab != null)
        {
            RectTransform rectTransfrom = InfoUIPrefab?.GetComponentInChildren<Canvas>()?.GetComponent<RectTransform>();
            if (rectTransfrom != null)
            {
                Gizmos.DrawWireCube(GetUIPos(), new Vector3(rectTransfrom.rect.width * rectTransfrom.lossyScale.x, rectTransfrom.rect.height * rectTransfrom.lossyScale.y));
            }
        }
    }

    private Vector3 GetUIPos()
    {
        return transform.position + new Vector3(0, UiHeight, 0);
    }
}
