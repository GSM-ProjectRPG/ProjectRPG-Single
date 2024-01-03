using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceBindingData ResourceBindingData { get; private set; }

    [SerializeField] private ResourceBindingData _resourceBindingData;

    private void Awake()
    {
        if (ResourceBindingData != null)
        {
            Debug.LogError("이미 리소스가 바인딩 되었습니다.");
            return;
        }
        ResourceBindingData = _resourceBindingData;
    }
}
