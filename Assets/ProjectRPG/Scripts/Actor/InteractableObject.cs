using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Action<PlayerInteractor> OnInteracted;

    public void CallInteract(PlayerInteractor interactor)
    {
        OnInteracted?.Invoke(interactor);
    }
}