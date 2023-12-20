using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Inventory Inventory;

    public GameObject ShopUI;
    public GameObject InventoryUI;

    public List<ItemData> ShopList;

    private InteractableObject interactable;
    private CoinSystem coinSystem;


    private void Awake()
    {
        interactable = GetComponent<InteractableObject>();
        interactable.OnInteracted += (playerInteractable) => OpenShop(playerInteractable);
    }

    public void OpenShop(PlayerInteractor playerInteractor)
    {
        coinSystem = playerInteractor.GetComponent<CoinSystem>();
        ShopUI.SetActive(true);
        InventoryUI.SetActive(true);
        GetComponentInChildren<InventoryUI>().OnClickAction = Sell;
    }

    public void CloseShop()
    {
        coinSystem = null;
        ShopUI.SetActive(false);
        InventoryUI.SetActive(false);
    }

    public void Purchase(int index)
    {
        if (Inventory.ItemList[index].Price < coinSystem.Coin)
        {
            coinSystem.currentCoin -= Inventory.ItemList[index].Price;
            Inventory.AddItemData(new Item(ShopList[index], 1));
        }
    }

    public void Sell(int index)
    {
        coinSystem.currentCoin += Inventory.ItemList[index].Price;
        Inventory.ReduceItemData(new Item(Inventory.ItemList[index].GetItemData(),1));
    }
}
