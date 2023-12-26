using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Inventory Inventory;

    public List<ItemData> ShopList;

    public CoinSystem CoinSystem;

    private void Awake()
    {
        ActorManager.Instance.OnRegistedPlayer += GetInventory;
    }

    private void GetInventory()
    {
        Inventory = ActorManager.Instance.Player.GetComponent<Inventory>();
    }

    public void Purchase(int index)
    {
        if (ShopList[index].price <= CoinSystem.Coin)
        {
            CoinSystem.Coin -= ShopList[index].price;
            Inventory.AddItem(new Item(ShopList[index], 1));
        }
    }

    public void Sell(int index)
    {
        if (Inventory.ItemList.Count <= index) return;
        CoinSystem.Coin += Inventory.ItemList[index].Price;
        Inventory.ReduceItem(new Item(Inventory.ItemList[index].GetItemData(),1));
    }
}
