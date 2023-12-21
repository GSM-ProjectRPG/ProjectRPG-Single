using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffIconUI : MonoBehaviour
{
    public Image BuffImage;
    public Image Cover;

    public virtual void BindBuff(Buff buff)
    {
        BuffImage.sprite = buff.Sprite;
        Cover.sprite = buff.Sprite;
        Cover.fillAmount = 0;
    }
}