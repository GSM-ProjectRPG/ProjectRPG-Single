using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DescriptionView : MonoBehaviour
{
    public GameObject DescriptionViewObj;
    public Description[] Descriptions;
    public Camera camera;

    Vector3 point;

    private void Awake()
    {
        camera = Camera.main;
    }
    private void Update()
    {
        point = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
        DescriptionViewObj.transform.position = new Vector3(point.x, point.y + 100, point.z);
        if (IsView())
        {
            DescriptionViewObj.SetActive(true);
            foreach (Description description in Descriptions)
            {
                if (description.IsView == true)
                {
                    OnDescription(description.index);
                }
            }
        }
        else
        {
            DescriptionViewObj.SetActive(false);
        }
    }

    bool IsView()
    {
        bool ON = false;
        foreach (Description description in Descriptions)
        {
            if (description.IsView == true)
            {
                ON = true;
            }
        }
        return ON;
    }

    public void OnDescription(int index)
    {
        GetComponentInChildren<Text>().text = Descriptions[index].Inventory.ItemList[index].itemData.itemDescription;
    }
}
