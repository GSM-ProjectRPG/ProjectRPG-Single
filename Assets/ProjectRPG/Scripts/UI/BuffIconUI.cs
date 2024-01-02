using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffIconUI : MonoBehaviour, IBuffIconUI<Buff>
{
    public Image BuffImage;
    public Image Cover;

    private TimerBuff _timerBuff;

    public virtual void BindBuff(Buff buff)
    {
        BuffImage.sprite = buff.Sprite;
        Cover.sprite = buff.Sprite;
        Cover.fillAmount = 0;

        _timerBuff = buff as TimerBuff;
    }

    private void Update()
    {
        Cover.fillAmount = _timerBuff.RemainingTimeRate;
    }
}