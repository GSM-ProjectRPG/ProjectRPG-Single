using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DescriptionUI : MonoBehaviour
{
    public GameObject DescriptionView;
    public GameObject[] Descriptions;
    public Inventory Inventory;

    Vector2 point;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        ActorManager.Instance.OnRegistedPlayer += GetInventory;
    }

    private void Update()
    {
        point = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        DescriptionView.transform.position = new Vector2(point.x, point.y + 100);
        IsView();
        
    }

    private void GetInventory()
    {
        Inventory = ActorManager.Instance.Player.GetComponent<Inventory>();
    }

    private void IsView()
    {
        bool isView = false;
        point = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);

        for (int index = 0; index < Inventory.ItemList.Count; index++)
        {
            if (Inventory is null) continue;
            if (Descriptions[index].transform.position.x < point.x || Descriptions[index].transform.position.x - Descriptions[index].GetComponent<RectTransform>().rect.height > point.x) continue;
            if (Descriptions[index].transform.position.y + Descriptions[index].GetComponent<RectTransform>().rect.width < point.y || Descriptions[index].transform.position.y > point.y) continue;
            DescriptionView.SetActive(true);
            SetDescription(index);
            return;
        }
        DescriptionView.SetActive(false);
    }

    public void SetDescription(int descriptionIndex)
    {
        if (Inventory.ItemList.Count <= descriptionIndex) return;
        GetComponentInChildren<Text>().text = Inventory.ItemList[descriptionIndex].ItemDescription;
    }
}
