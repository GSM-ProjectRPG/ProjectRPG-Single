using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Inventory Inventory;
    public InventoryUI InventoryUI;

    public GameObject ShopUI;

    public List<ItemData> ShopList;

    private InteractableObject _interactable;
    private CoinSystem _coinSystem;

    private void Awake()
    {
        _interactable = GetComponent<InteractableObject>();
        _interactable.OnInteracted += (playerInteractable) => OpenShop(playerInteractable);
    }

    public void OpenShop(PlayerInteractor playerInteractor)
    {
        _coinSystem = playerInteractor.GetComponent<CoinSystem>();
        ShopUI.SetActive(true);
        InventoryUI.gameObject.SetActive(true);
        PlayerInputManager.Instance.MouseLock = false;
        InventoryUI.OnClickAction = Sell;
    }

    public void CloseShop()
    {
        _coinSystem = null;
        ShopUI.SetActive(false);
        InventoryUI.gameObject.SetActive(false);
        PlayerInputManager.Instance.MouseLock = true;
    }

    public void Purchase(int index)
    {
        if (ShopList[index].price <= _coinSystem.Coin)
        {
            _coinSystem.Coin -= ShopList[index].price;
            Inventory.AddItemData(new Item(ShopList[index], 1));
        }
    }

    public void Sell(int index)
    {
        if (Inventory.ItemList.Count <= index) return;
        _coinSystem.Coin += Inventory.ItemList[index].Price;
        Inventory.ReduceItemData(new Item(Inventory.ItemList[index].GetItemData(),1));
    }
}
