using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopContent", menuName = "Item/ShopContent")]
public class ShopContent : ScriptableObject
{
    [SerializeField] private List<ShopItem> _updatesShopItems;
    [SerializeField] private List<ShopItem> _itemShopItems;

    public IEnumerable<ShopItem> UpdatesShopItems => _updatesShopItems;
    public IEnumerable<ShopItem> ItemShopItems => _itemShopItems;
}
