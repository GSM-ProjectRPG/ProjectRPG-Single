using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Inventory Inventory;
    public InventoryUI InventoryUI;

    public GameObject ShopUI;

    public List<ItemData> ShopList;

    private InteractableObject interactable;
    private CoinSystem coinSystem;

    private void Awake()
    {
        interactable = GetComponent<InteractableObject>();
        interactable.OnInteracted += (playerInteractable) => OpenShop(playerInteractable);
        InventoryUI.OnClickAction = Sell;
    }

    public void OpenShop(PlayerInteractor playerInteractor)
    {
        coinSystem = playerInteractor.GetComponent<CoinSystem>();
        ShopUI.SetActive(true);
        InventoryUI.gameObject.SetActive(true);
        PlayerInputManager.Instance.MouseLock = false;
        InventoryUI.OnClickAction = Sell;
    }

    public void CloseShop()
    {
        coinSystem = null;
        ShopUI.SetActive(false);
        InventoryUI.gameObject.SetActive(false);
        PlayerInputManager.Instance.MouseLock = true;
    }

    public void Purchase(int index)
    {
        if (ShopList[index].price <= coinSystem.Coin)
        {
            coinSystem.Coin -= ShopList[index].price;
            Inventory.AddItemData(new Item(ShopList[index], 1));
        }
    }

    public void Sell(int index)
    {
        if (Inventory.ItemList.Count <= index) return;
        coinSystem.Coin += Inventory.ItemList[index].Price;
        Inventory.ReduceItemData(new Item(Inventory.ItemList[index].GetItemData(),1));
    }
}
