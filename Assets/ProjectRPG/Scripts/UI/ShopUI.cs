using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject ShopView;

    public Image[] ShopImg;
    public Text[] ShopTxt;

    public Shop Shop;
    public InventoryUI InventoryUI;

    public InteractableObject Interactable;

    public Action<int> OnClickShopAction;

    public bool isOpenShop;

    private void Awake()
    {
        Shop = FindObjectOfType<Shop>();
        Interactable = Shop.gameObject.GetComponent<InteractableObject>();
        Interactable.OnInteracted += (playerInteractable) => OpenShop(playerInteractable);
        OnClickShopAction = Shop.Purchase;
    }

    public void OpenShop(PlayerInteractor playerInteractor)
    {
        Shop.CoinSystem = playerInteractor.GetComponent<CoinSystem>();
        ShopView.SetActive(true);
        InventoryUI.OpenInventory();
        SetShop();
        PlayerInputManager.Instance.MouseLock = false;
        InventoryUI.OnClickInventoryAction = Shop.Sell;
        isOpenShop = true;
    }

    public void CloseShop()
    {
        Shop.CoinSystem = null;
        ShopView.SetActive(false);
        InventoryUI.CloseInventory();
        PlayerInputManager.Instance.MouseLock = true;
        isOpenShop = false;
    }
    
    public void SetShop()
    {
        for (int i = 0; i < Shop.ShopList.Count; i++)
        {
            ShopImg[i].sprite = Shop.ShopList[i].itemImg;
            ShopTxt[i].text = Shop.ShopList[i].itemName + "\n" + Shop.ShopList[i].price + "골드";
        }
    }

    public void OnClickShop(int index)
    {
        OnClickShopAction?.Invoke(index);
    }
}
