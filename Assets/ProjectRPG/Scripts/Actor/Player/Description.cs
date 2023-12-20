using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Description : MonoBehaviour
{
    public Camera Camera;
    public DescriptionView DescriptionView;

    public GameObject DescriptionViewObj;
    public Inventory Inventory;
    public int index;
    public bool IsView;

    Vector3 point;

    private void Update()
    {
        point = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);

        if (gameObject.transform.position.x > point.x && gameObject.transform.position.x - 150 < point.x)
        {
            if (gameObject.transform.position.y + 150 > point.y && gameObject.transform.position.y < point.y)
            {
                if (Inventory.ItemList.Count > index)
                {
                    IsView = true;
                    return;
                }
            }
        }
        IsView = false;
    }
}
