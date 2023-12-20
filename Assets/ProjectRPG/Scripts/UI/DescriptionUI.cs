using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DescriptionUI : MonoBehaviour
{
    public GameObject DescriptionViewObj;
    public Description[] Descriptions;
    public Camera camera;

    Vector2 point;

    private void Update()
    {
        point = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        DescriptionViewObj.transform.position = new Vector2(point.x, point.y + 100);
        if (IsView())
        {
            DescriptionViewObj.SetActive(true);
            foreach (Description description in Descriptions)
            {
                if (description.IsView == true)
                {
                    SetDescription(description.Index);
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

    public void SetDescription(int descriptionIndex)
    {
        GetComponentInChildren<Text>().text = Descriptions[descriptionIndex].Inventory.ItemList[descriptionIndex].ItemDescription;
    }
}