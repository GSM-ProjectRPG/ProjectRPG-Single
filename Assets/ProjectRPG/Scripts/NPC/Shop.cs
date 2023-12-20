using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Inventory Inventory;
    public CoinSystem CoinSystem;

    public GameObject ShopUI;
    public GameObject InventoryUI;

    public List<ItemData> ShopList;

    InteractableObject interactable;

    private void Awake()
    {
        interactable = GetComponent<InteractableObject>();
        interactable.OnInteracted += (playerInteractable) => OpenShop();
    }

    public void OpenShop()
    {
        ShopUI.SetActive(true);
        InventoryUI.SetActive(true);
        GetComponentInChildren<InventoryUI>().OnClickAction = Sell;
    }

    public void CloseShop()
    {
        ShopUI.SetActive(false);
        InventoryUI.SetActive(false);
    }

    public void Purchase(int index)
    {
        if (Inventory.ItemList[index].itemData.price < CoinSystem.Coin)
        {
            CoinSystem.Coin -= Inventory.ItemList[index].itemData.price;
            Inventory.AddItemData(new Item(ShopList[index], 1));
        }
    }

    public void Sell(int index)
    {
        foreach(Item item in Inventory.ItemList)
        if (Inventory.ItemList[index].itemData)
        {
            CoinSystem.Coin += Inventory.ItemList[index].itemData.price;
            Inventory.RemoveItemData(new Item(Inventory.ItemList[index].itemData, 1));
        }
    }
}
