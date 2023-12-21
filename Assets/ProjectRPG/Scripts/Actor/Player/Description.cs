using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Description : MonoBehaviour
{
    public DescriptionUI DescriptionView;
    public GameObject DescriptionViewObj;
    public Inventory Inventory;
    public int Index;
    public bool IsView;

    private Camera _camera;

    Vector3 point;

    private void Awake()
    {
        _camera = Camera.main;
        ActorManager.Instance.OnRegistedPlayer += GetInventory;
        
    }


    private void GetInventory()
    {
        Inventory = ActorManager.Instance.Player.GetComponent<Inventory>();
    }

    private void Update()
    {
        
    }
}
