using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 상호작용 가능한 게임 오브젝트는 이 컴포넌트를 장착하고, OnInteracted 이벤트를 사용하여 상호작용을 구현합니다.
/// </summary>
public class InteractableObject : MonoBehaviour
{
    public Action<PlayerInteractor> OnInteracted;

    public void CallInteract(PlayerInteractor interactor)
    {
        OnInteracted?.Invoke(interactor);
    }
}