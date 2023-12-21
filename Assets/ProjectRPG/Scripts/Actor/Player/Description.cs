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
        point = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);

        if (Inventory is null) return;

        if (gameObject.transform.position.x > point.x && gameObject.transform.position.x - GetComponent<RectTransform>().rect.height < point.x)
        {
            if (gameObject.transform.position.y + GetComponent<RectTransform>().rect.width > point.y && gameObject.transform.position.y < point.y)
            {
                if (Inventory.ItemList.Count > Index)
                {
                    IsView = true;
                    return;
                }
            }
        }
        IsView = false;
    }
}
